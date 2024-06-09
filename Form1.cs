using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab4
{
    public partial class MainForm : Form
    {
        private EncryptionSettings settings;
        private static readonly HttpClient client = new HttpClient();

        public MainForm()
        {
            InitializeComponent();
            LoadSettings();

            fileEncryptButton.Click += FileEncrypt_Button;
            fileDecryptButton.Click += FileDecrypt_Button;
            folderEncryptButton.Click += FolderEncrypt_Button;
            folderDecryptButton.Click += FolderDecrypt_Button;
            sendToServerButton.Click += SendToServer_Button;
        }

        private void LoadSettings()
        {
            try
            {
                using (FileStream fs = new FileStream("settings.bin", FileMode.Open))
                {
                    IFormatter formatter = new BinaryFormatter();
                    settings = (EncryptionSettings)formatter.Deserialize(fs);
                }
            }
            catch
            {
                settings = new EncryptionSettings { Algorithm = "Aes", Key = null, IV = null };
            }
        }

        private void SaveSettings()
        {
            using (FileStream fs = new FileStream("settings.bin", FileMode.Create))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, settings);
            }
        }

        private void SendToServer_Button(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string[] paths = dialog.FileNames;
                    Task.Run(() =>
                    {
                        foreach (string path in paths)
                        {
                            UploadFileToServer(path);
                        }
                        UpdateStatus("Pliki zostały wysłane na serwer.");
                    });
                }
            }
        }

        private async void UploadFileToServer(string path)
        {
            try
            {
                using (var content = new MultipartFormDataContent())
                {
                    var fileContent = new ByteArrayContent(File.ReadAllBytes(path));
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    content.Add(fileContent, "file", Path.GetFileName(path));

                    var response = await client.PostAsync("https://serverfile.com/upload", content);
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                UpdateStatus("Error uploading file: " + ex.Message);
            }
        }

        private void FileEncrypt_Button(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string sourceFilePath = dialog.FileName;
                    string destinationFilePath = Path.Combine(Path.GetDirectoryName(sourceFilePath), Path.GetFileNameWithoutExtension(sourceFilePath) + "_encrypted" + Path.GetExtension(sourceFilePath));

                    Task.Run(() =>
                    {
                        EncryptFile(sourceFilePath, destinationFilePath, settings.Algorithm);
                        UpdateStatus("Plik zaszyfrowany pomyślnie.");
                    });
                }
            }
        }

        private void FileDecrypt_Button(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string sourceFilePath = dialog.FileName;
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourceFilePath);
                    string fileExtension = Path.GetExtension(sourceFilePath);

                    if (fileNameWithoutExtension.EndsWith("_encrypted"))
                    {
                        fileNameWithoutExtension = fileNameWithoutExtension.Substring(0, fileNameWithoutExtension.Length - "_encrypted".Length);
                    }

                    string destinationFilePath = Path.Combine(Path.GetDirectoryName(sourceFilePath), fileNameWithoutExtension + "_decrypted" + fileExtension);

                    Task.Run(() =>
                    {
                        DecryptFile(sourceFilePath, destinationFilePath, settings.Algorithm);
                        UpdateStatus("Plik odszyfrowany pomyślnie.");
                    });
                }
            }
        }

        private void FolderEncrypt_Button(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = dialog.SelectedPath;
                    string destinationFolderPath = folderPath + "_encrypted";

                    Task.Run(() =>
                    {
                        EncryptFolder(folderPath, destinationFolderPath, settings.Algorithm);
                        UpdateStatus("Folder zaszyfrowany pomyślnie.");
                    });
                }
            }
        }

        private void FolderDecrypt_Button(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = dialog.SelectedPath;
                    string destinationFolderPath = folderPath;
                    if (folderPath.EndsWith("_encrypted"))
                    {
                        destinationFolderPath = folderPath.Substring(0, folderPath.Length - "_encrypted".Length) + "_decrypted";
                    }
                    else
                    {
                        destinationFolderPath += "_decrypted";
                    }

                    Task.Run(() =>
                    {
                        DecryptFolder(folderPath, destinationFolderPath, settings.Algorithm);
                        UpdateStatus("Folder odzaszyfrowany pomyślnie.");
                    });
                }
            }
        }

        private SymmetricAlgorithm GetAlgorithmByName(string algorithmName)
        {
            switch (algorithmName)
            {
                case "Aes":
                    return Aes.Create();
                case "DES":
                    return DES.Create();
                case "RC2":
                    return RC2.Create();
                case "Rijndael":
                    return Rijndael.Create();
                case "TripleDES":
                    return TripleDES.Create();
                default:
                    throw new ArgumentException("Błąd, algorytm nierozpoznany.");
            }
        }

        private void EncryptFile(string inputFilePath, string outputFilePath, string algorithmName)
        {
            using (SymmetricAlgorithm algorithm = GetAlgorithmByName(algorithmName))
            {
                algorithm.GenerateKey();
                algorithm.GenerateIV();

                settings.Key = algorithm.Key;
                settings.IV = algorithm.IV;
                SaveSettings();

                using (FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    inputFileStream.CopyTo(cryptoStream);
                }
            }
        }

        private void DecryptFile(string inputFilePath, string outputFilePath, string algorithmName)
        {
            using (SymmetricAlgorithm algorithm = GetAlgorithmByName(algorithmName))
            {
                algorithm.Key = settings.Key;
                algorithm.IV = settings.IV;

                using (FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(inputFileStream, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(outputFileStream);
                }
            }
        }

        private void EncryptFolder(string sourceFolderPath, string destinationFolderPath, string algorithmName)
        {
            Directory.CreateDirectory(destinationFolderPath);

            foreach (string filePath in Directory.GetFiles(sourceFolderPath))
            {
                string fileName = Path.GetFileName(filePath);
                string destFilePath = Path.Combine(destinationFolderPath, fileName + ".encrypted");

                EncryptFile(filePath, destFilePath, algorithmName);
            }
        }

        private void DecryptFolder(string sourceFolderPath, string destinationFolderPath, string algorithmName)
        {
            Directory.CreateDirectory(destinationFolderPath);

            foreach (string filePath in Directory.GetFiles(sourceFolderPath, "*.encrypted"))
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string destFilePath = Path.Combine(destinationFolderPath, fileName);

                DecryptFile(filePath, destFilePath, algorithmName);
            }
        }

        private void UpdateStatus(string message)
        {
            if (labelInfo.InvokeRequired)
            {
                labelInfo.Invoke(new Action(() => labelInfo.Text = message));
            }
            else
            {
                labelInfo.Text = message;
            }
        }

        private void FileEncrypt_Button_1(object sender, EventArgs e)
        {

        }

        private void FolderEncrypt_Button_1(object sender, EventArgs e)
        {

        }
    }

    [Serializable]
    public class EncryptionSettings
    {
        public string Algorithm { get; set; }
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}
