using System.IO;
using System.Text;
using System.Windows.Forms;
using ZastitaInformacija_19322.Models;
using ZastitaInformacija_19322.Services;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace ZastitaInformacija_19322
{
    public partial class Form1 : Form
    {
        private LoggerService loggerService = new LoggerService();
        private PlayfairCipherService playfair = new PlayfairCipherService();
        private RC6Service rc6 = new RC6Service(); 
        private SHA1Service sha1 = new SHA1Service();
        private bool selectedPath = false;
        private string outputPath = "C:\\Users\\Anastasija\\Desktop\\X";
        public Form1()
        {
            InitializeComponent();
            fileSystemWatcher1.EnableRaisingEvents = false;
            checkBox1.Checked = false;

            //inicijalna podesavanja
            encrypt_rbtn.Checked = true;
            rc6_rbtn.Checked = true;
            pcbc_cbox.Checked = true;
            sha1_cbox.Checked = true;
            start_btn.Visible = false;
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            string message = string.Format("File_Changed {0} {1}", e.FullPath, e.Name);
            //MessageBox.Show(message);
            loggerService.LogToFile(message);
            AppendLog(message);

            LoadFilesToListBox();
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            string message = $"File_Created {e.FullPath} {e.Name}";
            MessageBox.Show(message);
            loggerService.LogToFile(message);
            AppendLog(message);
            LoadFilesToListBox();

            // Automatically process the new file
            if (encrypt_rbtn.Checked)
            {
                Pack(e.FullPath);
            }
            else if (decrypt_rbtn.Checked)
            {
                Unpack(e.FullPath);
            }
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            string message = string.Format("File_Deleted {0} {1}", e.FullPath, e.Name);
            //MessageBox.Show(message);
            loggerService.LogToFile(message);
            AppendLog(message);

            LoadFilesToListBox();
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            string message = string.Format("File_Renamed {0} {1}", e.FullPath, e.Name);
            MessageBox.Show(message);
            loggerService.LogToFile(message);
            AppendLog(message);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            fileSystemWatcher1.EnableRaisingEvents = !fileSystemWatcher1.EnableRaisingEvents;
            start_btn.Visible = false;

            string message;
            if (fileSystemWatcher1.EnableRaisingEvents)
                message = string.Format("File_System_Watcher turned on");
            else
                message = string.Format("File_System_Watcher turned off");
            loggerService.LogToFile(message);
            AppendLog(message);

        }

        private void browse_btn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select folder to monitor";
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    fileSystemWatcher1.Path = dialog.SelectedPath;
                    path_txt.Text = dialog.SelectedPath;

                    string message = string.Format("File_System_Watcher Selected_Path {0}", dialog.SelectedPath);
                    loggerService.LogToFile(message);
                    AppendLog(message);

                    selectedPath = true;
                    LoadFilesToListBox();
                }
            }
        }

        private void LoadFilesToListBox()
        {
            if (!selectedPath)
                return;

            string folderPath = fileSystemWatcher1.Path;
            bool onlyTxt = playfair_rbtn.Checked;

            filesListBox.Items.Clear();
            try
            {
                string[] files;

                if (onlyTxt)
                {
                    files = Directory.GetFiles(folderPath)
                         .Where(f => Path.GetExtension(f).ToLower() == ".txt")
                         .ToArray();
                }
                else
                {
                    files = Directory.GetFiles(folderPath);
                }
                filesListBox.Items.AddRange(files.Select(f => Path.GetFileName(f)).ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading files: {ex.Message}");
                string message = $"Error loading files from {folderPath}: {ex.Message}";
                loggerService.LogToFile(message);
                AppendLog(message);
            }
        }
        private void AppendLog(string message)
        {
            logTextBox.Text = $"{DateTime.Now}: {message}{Environment.NewLine}" + logTextBox.Text;

            logTextBox.SelectionStart = 0;
            logTextBox.ScrollToCaret();
        }

        private void rc6_rbtn_CheckedChanged(object sender, EventArgs e)
        {
            pcbc_cbox.Enabled = true;

            LoadFilesToListBox();
        }

        private void playfair_rbtn_CheckedChanged(object sender, EventArgs e)
        {
            pcbc_cbox.Checked = false;
            pcbc_cbox.Enabled = false;

            LoadFilesToListBox();
        }

        private void filesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileSystemWatcher1.EnableRaisingEvents)
                return;

            if (filesListBox.SelectedItem != null)
            {
                start_btn.Visible = true;
            }
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            if (filesListBox.SelectedItem == null)
                return;

            string fileName = filesListBox.SelectedItem.ToString();
            string fullPath = Path.Combine(fileSystemWatcher1.Path, fileName);

            AppendLog($"Selected file: {fileName}");

            if (encrypt_rbtn.Checked)
            {
                Pack(fullPath);
            }
            else if (decrypt_rbtn.Checked)
            {
                Unpack(fullPath);
            }
        }

        private void encrypt_rbtn_CheckedChanged(object sender, EventArgs e)
        {
            start_btn.Text = "Encrypt";
        }

        private void decrypt_rbtn_CheckedChanged(object sender, EventArgs e)
        {
            start_btn.Text = "Decrypt";
        }

        private void Pack(string filePath)
        {
            string algorithm = "";
            string hash = "";

            if (rc6_rbtn.Checked)
                algorithm = pcbc_cbox.Checked ? "PCBC" : "RC6";
            else if (playfair_rbtn.Checked)
                algorithm = "Playfair Cipher";

            if (sha1_cbox.Checked)
                hash = "SHA-1";

            byte[] fileData = File.ReadAllBytes(filePath);

            byte[] encryptedData = null;
            if (rc6_rbtn.Checked)
                encryptedData = rc6.Encrypt(fileData, pcbc_cbox.Checked);
            else if (playfair_rbtn.Checked)
                encryptedData = playfair.Encrypt(fileData);

            if (encryptedData == null)
            {
                MessageBox.Show("No encryption method selected!");
                return;
            }

            string computedHash = "";
            if (sha1_cbox.Checked)
            {
                SHA1Service sha1 = new SHA1Service();
                sha1.Update(encryptedData);
                computedHash = sha1.ComputeHashString();
            }

            FileHeader header = new FileHeader()
            {
                FileName = Path.GetFileName(filePath),
                FileSize = fileData.Length,
                CreatedAt = File.GetCreationTime(filePath),
                EncryptionAlgorithm = algorithm,
                HashAlgorithm = hash,
                HashValue = computedHash
            };

            string headerJson = System.Text.Json.JsonSerializer.Serialize(header);
            byte[] headerBytes = Encoding.UTF8.GetBytes(headerJson);
            byte[] headerLengthBytes = BitConverter.GetBytes(headerBytes.Length);

            string outputPath = Path.Combine(this.outputPath, Path.GetFileName(filePath));

            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(headerLengthBytes, 0, headerLengthBytes.Length);
                fs.Write(headerBytes, 0, headerBytes.Length);
                fs.Write(encryptedData, 0, encryptedData.Length);
            }

            AppendLog($"Packed and encrypted file: {filePath} → {outputPath}");
        }

        private void Unpack(string packedFilePath)
        {
            // Read the entire packed file
            byte[] packedBytes = File.ReadAllBytes(packedFilePath);

            // First 4 bytes = header length
            int headerLength = BitConverter.ToInt32(packedBytes, 0);

            // Next headerLength bytes = header JSON
            string headerJson = Encoding.UTF8.GetString(packedBytes, 4, headerLength);
            FileHeader header = System.Text.Json.JsonSerializer.Deserialize<FileHeader>(headerJson);

            if (header == null)
            {
                MessageBox.Show("Failed to read header!");
                return;
            }

            // Remaining bytes = encrypted data
            int encryptedDataOffset = 4 + headerLength;
            byte[] encryptedData = new byte[packedBytes.Length - encryptedDataOffset];
            Array.Copy(packedBytes, encryptedDataOffset, encryptedData, 0, encryptedData.Length);

            // Verify hash if present
            if (!string.IsNullOrEmpty(header.HashAlgorithm) && header.HashAlgorithm == "SHA-1")
            {
                SHA1Service sha1 = new SHA1Service();
                sha1.Update(encryptedData);
                string computedHash = sha1.ComputeHashString();

                if (computedHash != header.HashValue)
                {
                    MessageBox.Show("Warning: File hash mismatch! File may be corrupted or tampered with.");
                    return;
                }
            }

            // Decrypt
            byte[] decryptedData = null;

            if (header.EncryptionAlgorithm == "RC6")
            {
                decryptedData = rc6.Decrypt(encryptedData, false);
            }
            else if (header.EncryptionAlgorithm == "PCBC")
            {
                decryptedData = rc6.Decrypt(encryptedData, true);
            }
            else if (header.EncryptionAlgorithm == "Playfair Cipher")
            {
                decryptedData = playfair.Decrypt(encryptedData);
            }
            else
            {
                MessageBox.Show("Unknown encryption method in header!");
                return;
            }

            // Save decrypted file to output folder
            string outputFilePath = Path.Combine(this.outputPath, header.FileName);
            File.WriteAllBytes(outputFilePath, decryptedData);

            AppendLog($"Unpacked and decrypted file: {packedFilePath} → {outputFilePath}");
        }

    }
}
