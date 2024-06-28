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
using System.ComponentModel.DataAnnotations;

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
        private bool isButtonClickProcessed = false;
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

                // Always unsubscribe before subscribing to avoid multiple subscriptions
                CfmParamChange.Click -= EditAllParam;
                CfmParamChange.Click -= EditOneCardParam;

                if (result == DialogResult.Yes)
                {
                    // Change all card parameters
                    CfmParamChange.Click += EditAllParam;
                }
                else
                {
                    // Change specific card parameters
                    CfmParamChange.Click += EditOneCardParam;
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            cv_ComPort = comB_COM_Port.Text;

            // If the serial port is not initialized or is closed, then connect
            if (sp == null || !sp.IsOpen)
            {
                try
                {
                    // Initialize the serial port
                    sp = new SerialPort(cv_ComPort, 115200, Parity.None, 8, StopBits.One);
                    sp.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    sp.Open();
                    sp.DtrEnable = false;
                    sp.RtsEnable = false;
                    lblMsg2.Text = "Connection Success";
                    btnConnect.Text = "Disconnect";
                }
                catch (Exception ex)
                {
                    lblMsg2.Text = "Connection Error: " + ex.Message;
                }
            }
            else
            {
                // If the serial port is open, then disconnect
                try
                {
                    sp.Close();
                    sp.Dispose();
                    sp = null;
                    lblMsg2.Text = "Disconnected";
                    btnConnect.Text = "Connect";
                }
                catch (Exception ex)
                {
                    lblMsg2.Text = "Error during disconnection: " + ex.Message;
                }
            }
        }

        //-------------------------------------------------------------------------------
        //General functions
        private Dictionary<string, string> ReadDisplayParameters()
        {
            var displayParameters = new Dictionary<string, string>();

            // Get the application's base directory
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Display.txt");

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("#")) // Skip comments
                    {
                        continue;
                    }

                    if (line.Contains(":"))
                    {
                        var parts = line.Split(new[] { ':' }, 2);
                        if (parts.Length == 2)
                        {
                            var key = parts[0].Trim();
                            var value = parts[1].Trim();
                            displayParameters[key] = value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading display parameters: {ex.Message}");
            }

            return displayParameters;
        }
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
                if (!CheckSerialPortConnection())
                    return;
                
                // Clear all current data
                ClearAllData();
                ClearComboBox();
                ClearTextFields();
                SendLACommand();
            }
        }
        private void ClearComboBox()
        {
            cbBoxAddrID.Items.Clear();
            cbBoxAddrID.Text = string.Empty; // Optionally clear the selected text
        }
        private bool CheckSerialPortConnection()
        {
            if (sp == null)
            {
                MessageBox.Show("Please connect to a port");
                return false;
            }
            return true;
        }

        private void Rf()
        {
            CheckSerialPortConnection();
            ClearAllData();
            ClearComboBox();
            ClearTextFields();
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
        private void ClearTextFields()
        {
            // Clear all the text fields and combo boxes
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

                        // Special case for "mot OK"
                        if (keyValue.Length == 1 && keyValue[0].Trim() == "mot OK")
                        {
                            motorParams.MotOK = "OK";
                            continue;
                        }
                        
                        if (keyValue.Length != 2) continue; // Ensure the line contains a key-value pair

                        string key = keyValue[0].Trim();
                        string value = keyValue[1].Trim();

                        Console.WriteLine($"Extracting key: {key}, value: {value}");

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
                            default:
                                Console.WriteLine($"Unknown key: {key}");
                                break;
                        }
                    }
                }

                // Handle temperature separately if it exists
                foreach (string line in lines)
                {
                    if (line.StartsWith("T="))
                    {
                        string temperatureString = line.Substring(2).TrimEnd('C');
                        motorParams.Temperature = double.Parse(temperatureString);
                    }
                }

                Console.WriteLine($"Extracted Motor Parameters: {motorParams}");
                return motorParams;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting motor parameters: {ex.Message}");
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
            if (!CheckSerialPortConnection())
                return;
            // cmd = "0000$si";
            byte[] bytetosend = { 0x30, 0x30, 0x30, 0x30, 0x24, 0x73, 0x69, 0x0D, 0x0A, 0x06 };
            sp.Write(bytetosend, 0, bytetosend.Length);

        }
        private void btnAll_StopBlink_Click(object sender, EventArgs e)
        {
            if (!CheckSerialPortConnection())
                return;
            // cmd = "0000$sj";
            byte[] bytetosend = { 0x30, 0x30, 0x30, 0x30, 0x24, 0x73, 0x6A, 0x0D, 0x0A, 0x06 };
            sp.Write(bytetosend, 0, bytetosend.Length);
        }
        private void IdtIDLed_Click(object sender, EventArgs e)
        {
            if (!CheckSerialPortConnection())
                return;
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
            if (!CheckSerialPortConnection())
                return;

            ClearData();
            currentCommand = CommandType.LA; // Set the current command to LA
            // Send the LA command
            byte[] bytetosendla = { 0x30, 0x30, 0x30, 0x30, 0x24, 0x6C, 0x61, 0x0D, 0x0A, 0x06 };
            sp.Write(bytetosendla, 0, bytetosendla.Length);
            System.Diagnostics.Debug.WriteLine("LA command sent.");
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
            var displayParameters = ReadDisplayParameters();

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
                    DrawParameter(g, font, brush, "Pallet Length", conveyorParams.PalletLength, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Stop Position", conveyorParams.StopPosition, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Gap Size", conveyorParams.GapSize, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Over/Under Travel Size", conveyorParams.TravelSize, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Timeout Steps", conveyorParams.TimeoutSteps, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Direction", conveyorParams.Direction, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Double Sided", conveyorParams.DoubleSided, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Travel Correction", conveyorParams.TravelCorrection, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "REV_INT", conveyorParams.RevInternal, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "REV_ext", conveyorParams.RevExternal, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "INH_ext", conveyorParams.InhExternal, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "INH_INT", conveyorParams.InhInternal, displayParameters, ref y, lineHeight);
                }

                // Draw Motor Parameters
                if (motorParams != null)
                {
                    DrawParameter(g, font, brush, "Motor Current", motorParams.MC, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Motor Hold Current", motorParams.MD, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Motor Microstepping Size", motorParams.MI, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Motor Run Speed", motorParams.MR, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Over/Under Travel Speed", motorParams.MJ, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Motor Acceleration", motorParams.MA, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Motor Direction", motorParams.MB, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Motor Speed Profile", motorParams.MF, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Mot OK", motorParams.MotOK, displayParameters, ref y, lineHeight);
                    DrawParameter(g, font, brush, "Temperature", motorParams.Temperature, displayParameters, ref y, lineHeight);
                }

                y += lineHeight * 2; // Add extra space between cards
            }

            // Adjust the AutoScrollMinSize based on the content height
            panel12.AutoScrollMinSize = new Size(panel12.Width, (int)y - panel12.AutoScrollPosition.Y);
        }

        private void DrawParameter(Graphics g, Font font, Brush brush, string parameterName, object parameterValue, Dictionary<string, string> displayParameters, ref float y, float lineHeight)
        {
            string displayName = displayParameters.ContainsKey(parameterName) ? displayParameters[parameterName] : parameterName;
            g.DrawString($"{displayName}: {parameterValue}", font, brush, new PointF(10, y));
            y += lineHeight;
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
            if (!CheckSerialPortConnection())
                return;
            // Get user input in messagebox, convert to hex then do the rest of the operation...
            string userInput = Microsoft.VisualBasic.Interaction.InputBox("Enter address ID:", "Input New Address ID", "0000");
            if (userInput == "0000")
            {
                MessageBox.Show("Address ID '0000' is not allowed. Please enter a different address ID.", "Invalid Address ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            if (!CheckSerialPortConnection())
                return;
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
        private void EditAllParam(object sender, EventArgs e)
        {
            if (!isButtonClickProcessed)
            {
                isButtonClickProcessed = true;
                CfmParamChange.Enabled = false;
                confirmButtonTimer.Start();
                if (cbBoxAddrID.SelectedItem == null)
                {
                    MessageBox.Show("Please select a valid card address ID from the dropdown.", "No Address Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isButtonClickProcessed = false;
                    CfmParamChange.Enabled = true;
                    confirmButtonTimer.Stop();
                    return;
                }
                if (!ValidateMotorParameters())
                {
                    isButtonClickProcessed = false;
                    CfmParamChange.Enabled = true;
                    confirmButtonTimer.Stop();
                    return;
                }
                string[] commands = new string[]
                {
                    $"0000$cl{txtPalletLen.Text}\r\n",
                    $"0000$cp{txtStopPos.Text}\r\n",
                    $"0000$cg{txtGapSize.Text}\r\n",
                    $"0000$cv{txtTravelSteps.Text}\r\n",
                    $"0000$ct{AutoCalcTimeout()}\r\n",
                    $"0000$cd{CmbBoxDir.SelectedIndex}\r\n",
                    $"0000$cs{CmbBoxDbSide.SelectedIndex}\r\n",
                    $"0000$cx{CmbBoxTravCorr.SelectedIndex}\r\n",
                    $"0000$mc{txtMotorCurrent.Text}\r\n",
                    $"0000$mr{txtMotorSpeed.Text}\r\n",
                    $"0000$mj{txtTravelSpeed.Text}\r\n",
                    $"0000$ma{AutoCalcAccel()}\r\n"
                };

                foreach (string command in commands)
                {
                    byte[] bytetosend = Encoding.ASCII.GetBytes(command).Concat(new byte[] { 0x06 }).ToArray();
                    Debug.WriteLine($"Sending command: {command}");
                    SendCommands(bytetosend);
                    Thread.Sleep(1000); // Add a 500 milliseconds delay between commands
                }

                MessageBox.Show("All card parameters changed.");
                Rf();
            }
        }

        private void EditOneCardParam(object sender, EventArgs e)
        {
            if (!isButtonClickProcessed)
            {
                isButtonClickProcessed = true;
                CfmParamChange.Enabled = false;
                confirmButtonTimer.Start();
                if (cbBoxAddrID.SelectedItem == null)
                {
                    MessageBox.Show("Please select a valid card address ID from the dropdown.", "No Address Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isButtonClickProcessed = false;
                    CfmParamChange.Enabled = true;
                    confirmButtonTimer.Stop();
                    return;
                }
                if (!ValidateMotorParameters())
                {
                    isButtonClickProcessed = false;
                    CfmParamChange.Enabled = true;
                    confirmButtonTimer.Stop();
                    return;
                }
                string selectedID = cbBoxAddrID.SelectedItem.ToString();

                string[] commands = new string[]
                {
                    $"{selectedID}$cl{txtPalletLen.Text}\r\n",
                    $"{selectedID}$cp{txtStopPos.Text}\r\n",
                    $"{selectedID}$cg{txtGapSize.Text}\r\n",
                    $"{selectedID}$cv{txtTravelSteps.Text}\r\n",
                    $"{selectedID}$ct{AutoCalcTimeout()}\r\n",
                    $"{selectedID}$cd{CmbBoxDir.SelectedIndex}\r\n",
                    $"{selectedID}$cs{CmbBoxDbSide.SelectedIndex}\r\n",
                    $"{selectedID}$cx{CmbBoxTravCorr.SelectedIndex}\r\n",
                    $"{selectedID}$mc{txtMotorCurrent.Text}\r\n",
                    $"{selectedID}$mr{txtMotorSpeed.Text}\r\n",
                    $"{selectedID}$mj{txtTravelSpeed.Text}\r\n",
                    $"{selectedID}$ma{AutoCalcAccel()}\r\n"
                };

                foreach (string command in commands)
                {
                    byte[] bytetosend = Encoding.ASCII.GetBytes(command).Concat(new byte[] { 0x06 }).ToArray();
                    Debug.WriteLine($"Sending command: {command}");
                    SendCommands(bytetosend);
                    Thread.Sleep(1000); // Add a 2000 milliseconds delay between commands
                }

                MessageBox.Show($"Parameters for card {selectedID} changed.");
                Rf();
            }
        }
        private void cbBoxAddrID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CfmParamChange.Enabled = false;
                confirmButtonTimer.Start();

                // Check if an item is selected in the ComboBox
                if (cbBoxAddrID.SelectedItem == null)
                {
                    MessageBox.Show("Please select a valid card address ID from the dropdown.", "No Address Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine($"Exception: {ex}");
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
        private int AutoCalcTimeout()
        {
            // Get the pallet length from the text box
            if (int.TryParse(txtPalletLen.Text, out int palletLength))
            {
                // Calculate the timeout value
                int timeout = palletLength * 4;

                // Return the calculated timeout value
                return timeout;
            }
            else
            {
                // Handle the case where the pallet length is not a valid number
                MessageBox.Show("Invalid Pallet Length", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0; // Return a default value or handle this case appropriately
            }
        }
        //method to calculate Motor Acceleration derrived from Motor Speed
        private int AutoCalcAccel()
        {
            if (double.TryParse(txtMotorSpeed.Text, out double p) && double.TryParse(txtStopPos.Text, out double s))
            {
                // Calculate the minimum acceleration using the given formula
                double minAccel = (3 * Math.PI / 4) * (Math.Pow(p, 2) / s);

                // Add a small value to ensure rounding up
                minAccel += 0.5;

                // Round up to the next whole number
                int roundedMinAccel = (int)Math.Ceiling(minAccel);

                // Display or use the rounded value as needed
                Console.WriteLine($"Minimum Acceleration: {roundedMinAccel}");

                // Return the rounded value
                return roundedMinAccel;
            }
            else
            {
                MessageBox.Show("Invalid Motor Speed or Stop Position input.");
                return -1; // Return a default or error value
            }
        }
        private void confirmButtonTimer_Tick(object sender, EventArgs e)
        {
            isButtonClickProcessed = false;
            CfmParamChange.Enabled = true;
            confirmButtonTimer.Stop();
        }
        private bool ValidateMotorParameters()
        {
            var motorParams = new MotorParameters
            {
                MC = int.Parse(txtMotorCurrent.Text),
                MR = int.Parse(txtMotorSpeed.Text),
                MJ = int.Parse(txtTravelSpeed.Text)
            };

            var context = new ValidationContext(motorParams, null, null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(motorParams, context, results, true);

            if (!isValid)
            {
                StringBuilder errorMessages = new StringBuilder();
                foreach (var validationResult in results)
                {
                    errorMessages.AppendLine(validationResult.ErrorMessage);
                }
                MessageBox.Show(errorMessages.ToString(), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isValid;
        }

    }
}

