using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriveyorUtility
{
    public partial class ParameterDisplayForm : Form
    {
        private TextBox textBox;
        private Button exportButton;

        public ParameterDisplayForm(string parameters)
        {
            InitializeComponents(parameters);
        }
        private void InitializeComponents(string parameters)
        {
            this.textBox = new TextBox();
            this.exportButton = new Button();

            // TextBox
            this.textBox.Multiline = true;
            this.textBox.ReadOnly = true;
            this.textBox.Text = parameters;
            this.textBox.Dock = DockStyle.Fill;
            this.textBox.Height = this.ClientSize.Height - 40;
            this.textBox.Font = new Font(this.textBox.Font.FontFamily, 10);

            // Button
            this.exportButton.Text = "Export to Text File";
            this.exportButton.Dock = DockStyle.Bottom;
            this.exportButton.Click += ExportButton_Click;

            // Form
            this.Text = "Parameters";
            this.ClientSize = new Size(300, 400);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.exportButton);
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            // Get the application's base directory
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string paramSettingDirectory = Path.Combine(baseDirectory, "ParamSetting");

            // Create the directory if it doesn't exist
            if (!Directory.Exists(paramSettingDirectory))
            {
                Directory.CreateDirectory(paramSettingDirectory);
            }

            string addrID = ExtractAddrIDFromText(this.textBox.Text); // Method to extract AddrID from the text

            if (string.IsNullOrEmpty(addrID))
            {
                MessageBox.Show("Invalid address ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fileName = $"ParamSetting__{addrID}.txt";
            string filePath = Path.Combine(paramSettingDirectory, fileName);

            if (File.Exists(filePath))
            {
                DialogResult result = MessageBox.Show("File already exists. Do you want to overwrite it?", "File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                File.WriteAllText(filePath, this.textBox.Text);
                MessageBox.Show("File saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close the form
                this.FindForm().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string ExtractAddrIDFromText(string text)
        {
            // Assuming the address ID is in the format "ID: XXXX" at the start of the text
            string[] lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length > 0 && lines[0].StartsWith("ID: "))
            {
                return lines[0].Substring(4).Trim();
            }
            return null;
        }
    }
}

