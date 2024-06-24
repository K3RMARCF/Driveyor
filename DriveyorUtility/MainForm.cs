using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;

using DriveyorUtility;

namespace DriveyorUtility
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            cbBoxAddrID.SelectedIndexChanged += cbBoxAddrID_SelectedIndexChanged;
            InitializeComboBoxItems();
        }
        private enum CommandType
        {
            None,
            LA,
            LM
        }
        private void InitializeComboBoxItems()
        {
            // Initialize Direction ComboBox
            CmbBoxDir.Items.AddRange(new object[]
            {
        new ComboBoxItem { Text = "Left to Right", Value = 0 },
        new ComboBoxItem { Text = "Right to Left", Value = 1 }
            });
            CmbBoxDir.DisplayMember = "Text";
            CmbBoxDir.ValueMember = "Value";

            // Initialize Double-sided ComboBox
            CmbBoxDbSide.Items.AddRange(new object[]
            {
        new ComboBoxItem { Text = "No", Value = 0 },
        new ComboBoxItem { Text = "Yes", Value = 1 }
            });
            CmbBoxDbSide.DisplayMember = "Text";
            CmbBoxDbSide.ValueMember = "Value";

            // Initialize Travel Correction ComboBox
            CmbBoxTravCorr.Items.AddRange(new object[]
            {
        new ComboBoxItem { Text = "No", Value = 0 },
        new ComboBoxItem { Text = "Yes", Value = 1 }
            });
            CmbBoxTravCorr.DisplayMember = "Text";
            CmbBoxTravCorr.ValueMember = "Value";
        }

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }

        string[] COM_Port_Detected = SerialPort.GetPortNames();
        string cv_ComPort = "";
        SerialPort sp = null;
        private StringBuilder dataBuffer = new StringBuilder();
        private List<string> receivedDataList = new List<string>();
        private Dictionary<string, (ConveyorParameters conveyorParams, MotorParameters motorParams)> cardParameters = new Dictionary<string, (ConveyorParameters, MotorParameters)>();
        private CommandType currentCommand = CommandType.None;
        private void MainForm_Load(object sender, EventArgs e)
        {
            comB_COM_Port.Items.AddRange(COM_Port_Detected);
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if the selected tab is the "Parameters" tab
            if (tabControl1.SelectedTab == tabPage2)
            {
                PopulateComboBoxWithAddressIDs();
                var result = MessageBox.Show("Do you want to change all card parameters?", "Parameter Settings", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Change all card parameters
                    CfmParamChange.Click -= FunctionY;
                    CfmParamChange.Click += FunctionX;//function x called when changing all params

                }
                else
                {
                    // Change specific card parameters
                    CfmParamChange.Click -= FunctionX;
                    CfmParamChange.Click += FunctionY;//function y called when changing all params

                }
            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            cv_ComPort = comB_COM_Port.Text;
            if (sp == null)
            {
                try
                {
                    sp = new SerialPort(cv_ComPort, 115200, Parity.None, 8, StopBits.One);
                    sp.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    sp.Open();
                    sp.DtrEnable = false;
                    sp.RtsEnable = false;
                    lblMsg2.Text = "Connection Success";
                    btnConnect.Text = "Disconnect";
                }
                catch
                {
                    lblMsg2.Text = "Connection Error";
                }
            }
            else
            {
                btnConnect.Text = "Connect";
                sp.Close();
            }
        }
        //-------------------------------------------------------------------------------
        //General functions
        private void SendCommands(byte[] command)
        {
            if (sp != null && sp.IsOpen)
            {
                sp.Write(command, 0, command.Length);
                System.Diagnostics.Debug.WriteLine($"Command sent: {BitConverter.ToString(command)}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Serial port is not open.");
            }
        }
        private void Refresh_Click(object sender, EventArgs e)
        {
            {
                // Clear all current data
                ClearAllData();

                SendLACommand();

            }

        }
        private void Rf()
        {
            ClearAllData();

            SendLACommand();
        }
        private void ClearAllData()
        {
            receivedDataList.Clear();
            dataBuffer.Clear();
            cardParameters.Clear();
            panel12.Invalidate(); // Trigger repaint to clear the display
            System.Diagnostics.Debug.WriteLine("All data cleared.");
        }

        private void SendLACommand()
        {

            currentCommand = CommandType.LA; // Set the current command to LA

            // Send the LA command
            byte[] bytetosendla = { 0x30, 0x30, 0x30, 0x30, 0x24, 0x6C, 0x61, 0x0D, 0x0A, 0x06 };
            sp.Write(bytetosendla, 0, bytetosendla.Length);
            System.Diagnostics.Debug.WriteLine("LA command sent.");
        }
        private void SendCommand(string hexAddress, string command)
        {
            string fullCommand = $"{hexAddress}${command}\r\n"; // Adjust the format as needed
            byte[] bytetosend = Encoding.ASCII.GetBytes(fullCommand);
            sp.Write(bytetosend, 0, bytetosend.Length);
            System.Diagnostics.Debug.WriteLine($"Command sent: {BitConverter.ToString(bytetosend)}");
        }


        private string ConvertToHex(string input)
        {
            // Assuming input is already in the correct format for an address (e.g., "0006")
            return input;
        }
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            System.Diagnostics.Debug.WriteLine("Data Received:");
            System.Diagnostics.Debug.WriteLine(indata);

            // Append incoming data to the buffer
            dataBuffer.Append(indata);

            // Process complete messages terminated by form feed character '\f'
            while (dataBuffer.ToString().Contains("\f"))
            {
                // Find the position of the form feed character
                int endIndex = dataBuffer.ToString().IndexOf('\f');

                // Extract the complete message
                string completeMessage = dataBuffer.ToString().Substring(0, endIndex).Trim();
                System.Diagnostics.Debug.WriteLine("Complete Message:");
                System.Diagnostics.Debug.WriteLine(completeMessage);

                // Add the complete message to the list
                receivedDataList.Add(completeMessage);

                // Remove the processed message from the buffer
                dataBuffer.Remove(0, endIndex + 1);

                // Extract ID and parameters from the complete message
                string id = ExtractID(completeMessage);
                var conveyorParams = ExtractConveyorParameters(completeMessage);
                var motorParams = ExtractMotorParameters(completeMessage);

                // Log extracted parameters
                System.Diagnostics.Debug.WriteLine($"Extracted ID: {id}");
                System.Diagnostics.Debug.WriteLine($"Extracted Conveyor Parameters: {conveyorParams}");
                System.Diagnostics.Debug.WriteLine($"Extracted Motor Parameters: {motorParams}");

                // Store parameters in the dictionary only if valid
                if (!string.IsNullOrEmpty(id))
                {
                    if (!cardParameters.ContainsKey(id))
                    {
                        cardParameters[id] = (conveyorParams, motorParams);
                    }
                    else
                    {
                        // Preserve the previous parameters if the new ones are null
                        var currentConveyorParams = cardParameters[id].conveyorParams;
                        var currentMotorParams = cardParameters[id].motorParams;

                        if (conveyorParams != null)
                        {
                            currentConveyorParams = UpdateConveyorParameters(currentConveyorParams, conveyorParams);
                        }

                        if (motorParams != null)
                        {
                            currentMotorParams = UpdateMotorParameters(currentMotorParams, motorParams);
                        }

                        cardParameters[id] = (currentConveyorParams, currentMotorParams);
                    }

                    // Log the updated card parameters
                    System.Diagnostics.Debug.WriteLine($"Updated card parameters for ID {id}:");
                    System.Diagnostics.Debug.WriteLine($"Conveyor Parameters: {cardParameters[id].conveyorParams}");
                    System.Diagnostics.Debug.WriteLine($"Motor Parameters: {cardParameters[id].motorParams}");
                }

                // Invalidate the panel to trigger the Paint event only if the parameters are valid
                if (conveyorParams != null || motorParams != null)
                {
                    this.Invoke(new MethodInvoker(delegate {
                        panel12.Invalidate();
                    }));
                }
                this.Invoke(new MethodInvoker(delegate {
                    PopulateComboBoxWithAddressIDs();
                }));
                if (currentCommand == CommandType.LA)
                {
                    Thread.Sleep(5000); // delay to let LA command finish reading all cards
                    currentCommand = CommandType.LM; // Move to the next command

                    SendLMCommand(); // Send the LM command
                }
                else if (currentCommand == CommandType.LM)
                {
                    currentCommand = CommandType.None; // Reset the command type after processing LM
                }
            }
        }


        private ConveyorParameters UpdateConveyorParameters(ConveyorParameters current, ConveyorParameters update)
        {
            if (update.PalletLength != 0) current.PalletLength = update.PalletLength;
            if (update.StopPosition != 0) current.StopPosition = update.StopPosition;
            if (update.GapSize != 0) current.GapSize = update.GapSize;
            if (update.TravelSize != 0) current.TravelSize = update.TravelSize;
            if (update.TimeoutSteps != 0) current.TimeoutSteps = update.TimeoutSteps;
            if (update.Direction != 0) current.Direction = update.Direction;
            if (update.DoubleSided != 0) current.DoubleSided = update.DoubleSided;
            if (update.TravelCorrection != 0) current.TravelCorrection = update.TravelCorrection;
            if (update.RevExternal != 0) current.RevExternal = update.RevExternal;
            if (update.RevInternal != 0) current.RevInternal = update.RevInternal;
            if (update.InhExternal != 0) current.InhExternal = update.InhExternal;
            if (update.InhInternal != 0) current.InhInternal = update.InhInternal;
            return current;
        }

        private MotorParameters UpdateMotorParameters(MotorParameters current, MotorParameters update)
        {
            if (update.MC != 0) current.MC = update.MC;
            if (update.MD != 0) current.MD = update.MD;
            if (update.MI != 0) current.MI = update.MI;
            if (update.MR != 0) current.MR = update.MR;
            if (update.MJ != 0) current.MJ = update.MJ;
            if (update.MA != 0) current.MA = update.MA;
            if (update.MB != 0) current.MB = update.MB;
            if (update.MF != 0) current.MF = update.MF;
            if (!string.IsNullOrEmpty(update.MotOK)) current.MotOK = update.MotOK;
            if (update.Temperature != 0) current.Temperature = update.Temperature;
            return current;
        }
        private ConveyorParameters ExtractConveyorParameters(string message)
        {
            try
            {
                ConveyorParameters parameters = new ConveyorParameters();
                string[] lines = message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                System.Diagnostics.Debug.WriteLine("Extracting Conveyor Parameters:");

                foreach (string line in lines)
                {
                    System.Diagnostics.Debug.WriteLine($"Processing line: {line}");
                    if (line.Contains(","))
                    {
                        string[] keyValues = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string keyValue in keyValues)
                        {
                            string[] parts = keyValue.Split(new[] { '=' }, 2);
                            if (parts.Length != 2) continue;

                            string key = parts[0].Trim();
                            string value = parts[1].Trim();

                            System.Diagnostics.Debug.WriteLine($"Extracting key: {key}, value: {value}");

                            switch (key)
                            {
                                case "dir":
                                    parameters.Direction = int.Parse(value);
                                    break;
                                case "db sided":
                                    parameters.DoubleSided = int.Parse(value);
                                    break;
                                case "REV_ext":
                                    parameters.RevExternal = int.Parse(value);
                                    break;
                                case "REV_int":
                                    parameters.RevInternal = int.Parse(value);
                                    break;
                                case "INH_ext":
                                    parameters.InhExternal = int.Parse(value);
                                    break;
                                case "INH_int":
                                    parameters.InhInternal = int.Parse(value);
                                    break;
                                default:
                                    System.Diagnostics.Debug.WriteLine($"Unknown key: {key}");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        string[] parts = line.Split(new[] { '=' }, 2);
                        if (parts.Length != 2) continue;

                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        System.Diagnostics.Debug.WriteLine($"Extracting key: {key}, value: {value}");

                        switch (key)
                        {
                            case "pallet len":
                                parameters.PalletLength = int.Parse(value);
                                break;
                            case "stop pos":
                                parameters.StopPosition = int.Parse(value);
                                break;
                            case "gap size":
                                parameters.GapSize = int.Parse(value);
                                break;
                            case "travel size":
                                parameters.TravelSize = int.Parse(value);
                                break;
                            case "timeout steps":
                                parameters.TimeoutSteps = int.Parse(value);
                                break;
                            case "travel corr":
                                parameters.TravelCorrection = int.Parse(value);
                                break;
                            default:
                                System.Diagnostics.Debug.WriteLine($"Unknown key: {key}");
                                break;
                        }
                    }
                }

                System.Diagnostics.Debug.WriteLine($"Extracted Conveyor Parameters: {parameters}");
                return parameters;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error extracting conveyor parameters: {ex.Message}");
                return null;
            }
        }
        private MotorParameters ExtractMotorParameters(string message)
        {
            try
            {
                // Split the message into lines
                string[] lines = message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // Initialize a MotorParameters object
                MotorParameters motorParams = new MotorParameters();

                // Iterate through each line to find and set the motor parameters
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    foreach (string part in parts)
                    {
                        string[] keyValue = part.Split('=');
                        if (keyValue.Length != 2) continue; // Ensure the line contains a key-value pair

                        string key = keyValue[0].Trim();
                        string value = keyValue[1].Trim();

                        switch (key)
                        {
                            case "mc":
                                motorParams.MC = int.Parse(value);
                                break;
                            case "md":
                                motorParams.MD = int.Parse(value);
                                break;
                            case "mi":
                                motorParams.MI = int.Parse(value);
                                break;
                            case "mr":
                                motorParams.MR = int.Parse(value);
                                break;
                            case "mj":
                                motorParams.MJ = int.Parse(value);
                                break;
                            case "ma":
                                motorParams.MA = int.Parse(value);
                                break;
                            case "mb":
                                motorParams.MB = int.Parse(value);
                                break;
                            case "mf":
                                motorParams.MF = int.Parse(value);
                                break;
                            case "mot OK":
                                motorParams.MotOK = value;
                                break;
                            default:
                                break;
                        }
                    }
                    if (line.StartsWith("T="))
                    {
                        string temperatureString = line.Substring(2).TrimEnd('C');
                        motorParams.Temperature = double.Parse(temperatureString);
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Extracted Motor Parameters: {motorParams}");
                return motorParams;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error extracting motor parameters: {ex.Message}");
                return null;
            }
        }

        private string ExtractID(string message)
        {
            // Extract the ID from the message (assuming ID is always at the start)
            string[] lines = message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length > 0 && lines[0].Length >= 4)
            {
                return lines[0].Substring(0, 4);
            }
            return null;
        }
        private void AddressSettingDataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            System.Diagnostics.Debug.WriteLine("Address Setting Data Received:");
            System.Diagnostics.Debug.WriteLine(indata);

            // Check for specific responses indicating success or failure
            if (indata.Contains("#ok"))
            {
                System.Diagnostics.Debug.WriteLine("Address successfully changed response received.");
                this.Invoke(new MethodInvoker(delegate
                {
                    MessageBox.Show("Address successfully changed!");
                }));

                // Unsubscribe from the event to avoid multiple triggers
                sp.DataReceived -= AddressSettingDataReceivedHandler;
                sp.DataReceived += DataReceivedHandler;
            }
            else if (indata.Contains("no enum"))
            {
                System.Diagnostics.Debug.WriteLine("Address setting failed: No enumeration response received.");
                this.Invoke(new MethodInvoker(delegate
                {
                    MessageBox.Show("Address setting failed: No enumeration.");
                }));

                // Unsubscribe from the event to avoid multiple triggers
                sp.DataReceived -= AddressSettingDataReceivedHandler;
                sp.DataReceived += DataReceivedHandler;
            }
        }

        //-------------------------------------------------------------------------------
        //Identify tab functions
        private void btnAll_Blink_Click(object sender, EventArgs e)
        {
            // cmd = "0000$si";
            byte[] bytetosend = { 0x30, 0x30, 0x30, 0x30, 0x24, 0x73, 0x69, 0x0D, 0x0A, 0x06 };
            sp.Write(bytetosend, 0, bytetosend.Length);

        }
        private void btnAll_StopBlink_Click(object sender, EventArgs e)
        {
            // cmd = "0000$sj";
            byte[] bytetosend = { 0x30, 0x30, 0x30, 0x30, 0x24, 0x73, 0x6A, 0x0D, 0x0A, 0x06 };
            sp.Write(bytetosend, 0, bytetosend.Length);
        }
        private void IdtIDLed_Click(object sender, EventArgs e)
        {
            ClearData();
            string userInput = Microsoft.VisualBasic.Interaction.InputBox("Enter the address ID:", "Input Address ID", "0000");
            //message box for user to put in addr ID
            if (!string.IsNullOrEmpty(userInput))
            {
                string hexAddress = ConvertToHex(userInput);
                Console.WriteLine(hexAddress);
                SendCommand(hexAddress, "si"); // Example command
            }
            else
            {
                Microsoft.VisualBasic.Interaction.InputBox("Invalid Input");
            }
            //convert numbers to HEX and write
        }
        private void ConvParam_Click(object sender, EventArgs e)
        {
            if (sp == null)
                return;

            ClearData();
            currentCommand = CommandType.LA; // Set the current command to LA

            // Send the LA command
            byte[] bytetosendla = { 0x30, 0x30, 0x30, 0x30, 0x24, 0x6C, 0x61, 0x0D, 0x0A, 0x06 };
            sp.Write(bytetosendla, 0, bytetosendla.Length);
        }
        private void SendLMCommand()
        {
            byte[] bytetosendlm = { 0x30, 0x30, 0x30, 0x30, 0x24, 0x6C, 0x6D, 0x0D, 0x0A, 0x06 };
            sp.Write(bytetosendlm, 0, bytetosendlm.Length);
            System.Diagnostics.Debug.WriteLine("LM command sent.");
        }


        //Output Of Data Handler Received
        private void panel12_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font font = new Font("Arial", 12);
            Brush brush = Brushes.Black;

            float y = panel12.AutoScrollPosition.Y; // Initial vertical position with auto-scroll adjustment
            float lineHeight = font.GetHeight(g); // Line height based on the font

            // Draw parameters for each card in the dictionary
            foreach (var kvp in cardParameters.OrderBy(k => k.Key))
            {
                string id = kvp.Key;
                ConveyorParameters conveyorParams = kvp.Value.conveyorParams;
                MotorParameters motorParams = kvp.Value.motorParams;

                // Draw ID
                g.DrawString($"ID: {id}", font, brush, new PointF(10, y));
                y += lineHeight;

                // Draw Conveyor Parameters
                if (conveyorParams != null)
                {
                    g.DrawString($"Pallet Length: {conveyorParams.PalletLength}", font, brush, new PointF(10, y));
                    g.DrawString($"Stop Position: {conveyorParams.StopPosition}", font, brush, new PointF(10, y + lineHeight));
                    g.DrawString($"Gap Size: {conveyorParams.GapSize}", font, brush, new PointF(10, y + lineHeight * 2));
                    g.DrawString($"Travel Size: {conveyorParams.TravelSize}", font, brush, new PointF(10, y + lineHeight * 3));
                    g.DrawString($"Timeout Steps: {conveyorParams.TimeoutSteps}", font, brush, new PointF(10, y + lineHeight * 4));
                    g.DrawString($"Direction: {conveyorParams.Direction}", font, brush, new PointF(10, y + lineHeight * 5));
                    g.DrawString($"Double Sided: {conveyorParams.DoubleSided}", font, brush, new PointF(10, y + lineHeight * 6));
                    g.DrawString($"Travel Correction: {conveyorParams.TravelCorrection}", font, brush, new PointF(10, y + lineHeight * 7));
                    g.DrawString($"REV_INT: {conveyorParams.RevInternal}", font, brush, new PointF(10, y + lineHeight * 8));
                    g.DrawString($"REV_ext: {conveyorParams.RevExternal}", font, brush, new PointF(10, y + lineHeight * 9));
                    g.DrawString($"INH_ext: {conveyorParams.InhExternal}", font, brush, new PointF(10, y + lineHeight * 10));
                    g.DrawString($"INH_INT: {conveyorParams.InhInternal}", font, brush, new PointF(10, y + lineHeight * 11));
                    y += lineHeight * 12; // Adjust based on the number of parameters
                }

                // Draw Motor Parameters
                if (motorParams != null)
                {
                    g.DrawString($"Motor Current: {motorParams.MC}", font, brush, new PointF(10, y));
                    g.DrawString($"Motor Hold Current: {motorParams.MD}", font, brush, new PointF(10, y + lineHeight));
                    g.DrawString($"Motor Microstepping Size: {motorParams.MI}", font, brush, new PointF(10, y + lineHeight * 2));
                    g.DrawString($"Motor Run Speed: {motorParams.MR}", font, brush, new PointF(10, y + lineHeight * 3));
                    g.DrawString($"Over/under travel speed: {motorParams.MJ}", font, brush, new PointF(10, y + lineHeight * 4));
                    g.DrawString($"Motor Acceleration: {motorParams.MA}", font, brush, new PointF(10, y + lineHeight * 5));
                    g.DrawString($"Motor Direction: {motorParams.MB}", font, brush, new PointF(10, y + lineHeight * 6));
                    g.DrawString($"Motor Speed Profile: {motorParams.MF}", font, brush, new PointF(10, y + lineHeight * 7));
                    g.DrawString($"Mot OK: {motorParams.MotOK}", font, brush, new PointF(10, y + lineHeight * 8));
                    g.DrawString($"Temperature: {motorParams.Temperature}C", font, brush, new PointF(10, y + lineHeight * 9));
                    y += lineHeight * 10; // Adjust based on the number of parameters
                }

                y += lineHeight * 2; // Add extra space between cards
            }

            // Adjust the AutoScrollMinSize based on the content height
            panel12.AutoScrollMinSize = new Size(panel12.Width, (int)y - panel12.AutoScrollPosition.Y);
        }

        private void ClearData()
        {
            receivedDataList.Clear();
            dataBuffer.Clear();
            panel12.Invalidate(); // Trigger repaint to clear the display
        }

        //--------------------------------------------------------------------------------------------------
        //Set Address Functions
        private void SpecAddr_Click(object sender, EventArgs e)
        {
            // Get user input in messagebox, convert to hex then do the rest of the operation...
            string userInput = Microsoft.VisualBasic.Interaction.InputBox("Enter address ID:", "Input New Address ID", "0000");
            if (!string.IsNullOrEmpty(userInput))
            {
                // Clear existing data
                ClearData();

                // Use the new event handler for address setting
                sp.DataReceived -= DataReceivedHandler; // Unsubscribe from the general handler
                sp.DataReceived += AddressSettingDataReceivedHandler; // Subscribe to the new handler

                // 1. Send 0000$sz
                byte[] bytetosend_sz = { 0x30, 0x30, 0x30, 0x30, 0x24, 0x73, 0x7A, 0x0D, 0x0A, 0x06 };
                SendCommands(bytetosend_sz);
                Thread.Sleep(1000);

                // 2. Send 0000$sl with the new address
                string formattedAddress = userInput.PadLeft(4, '0'); // Ensure the address is 4 digits
                string fullCommand_sl = $"0000$sl{formattedAddress}\r\n";
                byte[] bytetosend_sl = Encoding.ASCII.GetBytes(fullCommand_sl).Concat(new byte[] { 0x06 }).ToArray();
                SendCommands(bytetosend_sl);
                Thread.Sleep(1000);

                // 3. Send 0000$sf
                byte[] bytetosend_sf = { 0x30, 0x30, 0x30, 0x30, 0x24, 0x73, 0x66, 0x0D, 0x0A, 0x06 };
                SendCommands(bytetosend_sf);
                Thread.Sleep(1000);

                // 4. Send 0000$se
                byte[] bytetosend_se = { 0x30, 0x30, 0x30, 0x30, 0x24, 0x73, 0x65, 0x0D, 0x0A, 0x06 };
                SendCommands(bytetosend_se);
                Thread.Sleep(1000);

                // Print the commands to the console
                Console.WriteLine("Commands sent:");
                Console.WriteLine("0000$sz");
                Console.WriteLine(fullCommand_sl);
                Console.WriteLine("0000$sf");
                Console.WriteLine("0000$se");

                // Clear data after sending all commands to ensure display is updated
                ClearData();
                Rf();
            }
            else
            {
                MessageBox.Show("Invalid Input");
            }
        }
        private void EditSpecAddrID_Click(object sender, EventArgs e)
        {
            // Prompt the user for the current address ID
            string currentAddress = Microsoft.VisualBasic.Interaction.InputBox("Enter the current address ID:", "Input Current Address ID", "0000");
            if (string.IsNullOrEmpty(currentAddress))
            {
                MessageBox.Show("Invalid Current Address Input");
                return;
            }

            // Prompt the user for the new address ID
            string newAddress = Microsoft.VisualBasic.Interaction.InputBox("Enter the new address ID:", "Input New Address ID", "0000");
            if (string.IsNullOrEmpty(newAddress))
            {
                MessageBox.Show("Invalid New Address Input");
                return;
            }

            // Ensure both addresses are 4 digits
            string formattedCurrentAddress = currentAddress.PadLeft(4, '0');
            string formattedNewAddress = newAddress.PadLeft(4, '0');

            // Clear data for the old address
            if (cardParameters.ContainsKey(formattedCurrentAddress))
            {
                cardParameters.Remove(formattedCurrentAddress);
            }

            // Clear existing data
            ClearData();

            // Send commands in sequence with proper formatting
            // 1. Send currentAddress$sl{newAddress}\r\n
            string fullCommand_sl = $"{formattedCurrentAddress}$sl{formattedNewAddress}\r\n";
            byte[] bytetosend_sl = Encoding.ASCII.GetBytes(fullCommand_sl).Concat(new byte[] { 0x06 }).ToArray();
            SendCommands(bytetosend_sl);
            System.Threading.Thread.Sleep(1000);

            // 2. Send currentAddress$sf\r\n
            string command_sf = $"{formattedCurrentAddress}$sf\r\n";
            byte[] bytetosend_sf = Encoding.ASCII.GetBytes(command_sf).Concat(new byte[] { 0x06 }).ToArray();
            SendCommands(bytetosend_sf);
            System.Threading.Thread.Sleep(1000);

            // 3. Send currentAddress$se\r\n
            string command_se = $"{formattedCurrentAddress}$se\r\n";
            byte[] bytetosend_se = Encoding.ASCII.GetBytes(command_se).Concat(new byte[] { 0x06 }).ToArray();
            SendCommands(bytetosend_se);
            System.Threading.Thread.Sleep(1000);

            // Print the commands to the console
            Console.WriteLine("Commands sent:");
            Console.WriteLine(fullCommand_sl);
            Console.WriteLine(command_sf);
            Console.WriteLine(command_se);
            ClearData();
            // Refresh data after changing the address
            Rf();
        }

        //--------------------------------------------------------------------------------------------------
        //Set Params
        private void PopulateComboBoxWithAddressIDs()
        {
            cbBoxAddrID.Items.Clear();
            foreach (var id in cardParameters.Keys)
            {
                cbBoxAddrID.Items.Add(id);
            }
        }
        private void FunctionX(object sender, EventArgs e)
        {
            // Implement the logic for Function X
            //take all textbox n combobox reading
            //0000{cmd}{val from txt/cmbbox}
            //
            MessageBox.Show("Change All Param Settings");
        }

        private void FunctionY(object sender, EventArgs e)
        {
            // Implement the logic for Function Y
            MessageBox.Show("Change individual card setting.");
        }

        private void cbBoxAddrID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedID = cbBoxAddrID.SelectedItem.ToString();

            // Check if the dictionary contains the selected ID
            if (cardParameters.ContainsKey(selectedID))
            {
                // Retrieve the parameters from the dictionary
                var parameters = cardParameters[selectedID];
                var conveyorParams = parameters.conveyorParams;
                var motorParams = parameters.motorParams;

                // Update the Conveyor Card Settings text fields
                txtPalletLen.Text = conveyorParams.PalletLength.ToString();
                txtStopPos.Text = conveyorParams.StopPosition.ToString();
                txtGapSize.Text = conveyorParams.GapSize.ToString();
                txtTravelSteps.Text = conveyorParams.TravelSize.ToString();

                // Update the ComboBox selections
                SetComboBoxSelectedValue(CmbBoxDir, conveyorParams.Direction);
                SetComboBoxSelectedValue(CmbBoxDbSide, conveyorParams.DoubleSided);
                SetComboBoxSelectedValue(CmbBoxTravCorr, conveyorParams.TravelCorrection);

                // Update the Motor Card Settings text fields
                txtMotorCurrent.Text = motorParams.MC.ToString();
                txtMotorSpeed.Text = motorParams.MR.ToString();
                txtTravelSpeed.Text = motorParams.MJ.ToString();
            }
            else
            {
                // Clear the text fields if the selected ID is not found
                txtPalletLen.Clear();
                txtStopPos.Clear();
                txtGapSize.Clear();
                txtTravelSteps.Clear();
                CmbBoxDir.SelectedIndex = -1;
                CmbBoxDbSide.SelectedIndex = -1;
                CmbBoxTravCorr.SelectedIndex = -1;
                txtMotorCurrent.Clear();
                txtMotorSpeed.Clear();
                txtTravelSpeed.Clear();
            }
        }

        private void SetComboBoxSelectedValue(ComboBox comboBox, int value)
        {
            foreach (ComboBoxItem item in comboBox.Items)
            {
                if ((int)item.Value == value)
                {
                    comboBox.SelectedItem = item;
                    return;
                }
            }
            comboBox.SelectedIndex = -1; // If the value is not found, clear the selection
        }
        //method to calculate Timeout derived from {pallet len x4} automatically
        private void AutoCalcTimeout()
        {

        }
        //method to calculate Motor Acceleration derrived from Motor Speed
        private void AutoCalcAccel()
        {

        }

        
    }
}

