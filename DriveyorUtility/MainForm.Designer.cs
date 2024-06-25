namespace DriveyorUtility
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.Refresh = new System.Windows.Forms.Button();
            this.lblMsg2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.comB_COM_Port = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel12 = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMotorCurrent = new System.Windows.Forms.TextBox();
            this.txtMotorSpeed = new System.Windows.Forms.TextBox();
            this.txtTravelSpeed = new System.Windows.Forms.TextBox();
            this.CfmParamChange = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CmbBoxTravCorr = new System.Windows.Forms.ComboBox();
            this.CmbBoxDbSide = new System.Windows.Forms.ComboBox();
            this.CmbBoxDir = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPalletLen = new System.Windows.Forms.TextBox();
            this.txtTravelSteps = new System.Windows.Forms.TextBox();
            this.txtGapSize = new System.Windows.Forms.TextBox();
            this.txtStopPos = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cbBoxAddrID = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.EditSpecAddrID = new System.Windows.Forms.Button();
            this.ConvParam = new System.Windows.Forms.Button();
            this.SpecAddr = new System.Windows.Forms.Button();
            this.btnAll_Blink = new System.Windows.Forms.Button();
            this.btnAll_StopBlink = new System.Windows.Forms.Button();
            this.IdtIDLed = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.confirmButtonTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel10);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1348, 132);
            this.panel1.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Transparent;
            this.panel10.Controls.Add(this.Refresh);
            this.panel10.Controls.Add(this.lblMsg2);
            this.panel10.Controls.Add(this.label1);
            this.panel10.Controls.Add(this.btnConnect);
            this.panel10.Controls.Add(this.comB_COM_Port);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Margin = new System.Windows.Forms.Padding(4);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1344, 128);
            this.panel10.TabIndex = 1;
            // 
            // Refresh
            // 
            this.Refresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Refresh.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Refresh.Location = new System.Drawing.Point(803, 32);
            this.Refresh.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(144, 52);
            this.Refresh.TabIndex = 23;
            this.Refresh.Text = "Refresh";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // lblMsg2
            // 
            this.lblMsg2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMsg2.AutoSize = true;
            this.lblMsg2.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg2.Location = new System.Drawing.Point(553, 55);
            this.lblMsg2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMsg2.Name = "lblMsg2";
            this.lblMsg2.Size = new System.Drawing.Size(213, 30);
            this.lblMsg2.TabIndex = 22;
            this.lblMsg2.Text = "Connection Status";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 30);
            this.label1.TabIndex = 21;
            this.label1.Text = "COM Port: ";
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnConnect.AutoSize = true;
            this.btnConnect.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(380, 32);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(152, 52);
            this.btnConnect.TabIndex = 19;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // comB_COM_Port
            // 
            this.comB_COM_Port.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comB_COM_Port.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comB_COM_Port.FormattingEnabled = true;
            this.comB_COM_Port.Location = new System.Drawing.Point(189, 46);
            this.comB_COM_Port.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.comB_COM_Port.Name = "comB_COM_Port";
            this.comB_COM_Port.Size = new System.Drawing.Size(160, 37);
            this.comB_COM_Port.TabIndex = 20;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 132);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1348, 670);
            this.panel3.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1052, 670);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Linen;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.panel12);
            this.tabPage1.Location = new System.Drawing.Point(4, 41);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1044, 625);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Read Data";
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.Linen;
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(4, 4);
            this.panel12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(1032, 613);
            this.panel12.TabIndex = 14;
            this.panel12.Paint += new System.Windows.Forms.PaintEventHandler(this.panel12_Paint_1);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Linen;
            this.tabPage2.Controls.Add(this.panel6);
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Location = new System.Drawing.Point(4, 41);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1044, 625);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parameter Settings";
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.groupBox2);
            this.panel6.Controls.Add(this.groupBox1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(4, 104);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1036, 517);
            this.panel6.TabIndex = 22;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtMotorCurrent);
            this.groupBox2.Controls.Add(this.txtMotorSpeed);
            this.groupBox2.Controls.Add(this.txtTravelSpeed);
            this.groupBox2.Controls.Add(this.CfmParamChange);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(515, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(515, 513);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Motor Card Settings";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(27, 127);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(162, 30);
            this.label11.TabIndex = 43;
            this.label11.Text = "Motor Speed:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 168);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 60);
            this.label4.TabIndex = 42;
            this.label4.Text = "Over/Under\r\nTravel Speed:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(27, 65);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(178, 30);
            this.label10.TabIndex = 41;
            this.label10.Text = "Motor Current:";
            // 
            // txtMotorCurrent
            // 
            this.txtMotorCurrent.Location = new System.Drawing.Point(247, 65);
            this.txtMotorCurrent.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtMotorCurrent.Name = "txtMotorCurrent";
            this.txtMotorCurrent.Size = new System.Drawing.Size(207, 36);
            this.txtMotorCurrent.TabIndex = 38;
            // 
            // txtMotorSpeed
            // 
            this.txtMotorSpeed.Location = new System.Drawing.Point(247, 127);
            this.txtMotorSpeed.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtMotorSpeed.Name = "txtMotorSpeed";
            this.txtMotorSpeed.Size = new System.Drawing.Size(207, 36);
            this.txtMotorSpeed.TabIndex = 37;
            // 
            // txtTravelSpeed
            // 
            this.txtTravelSpeed.Location = new System.Drawing.Point(247, 188);
            this.txtTravelSpeed.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtTravelSpeed.Name = "txtTravelSpeed";
            this.txtTravelSpeed.Size = new System.Drawing.Size(207, 36);
            this.txtTravelSpeed.TabIndex = 36;
            // 
            // CfmParamChange
            // 
            this.CfmParamChange.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CfmParamChange.Location = new System.Drawing.Point(70, 417);
            this.CfmParamChange.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.CfmParamChange.Name = "CfmParamChange";
            this.CfmParamChange.Size = new System.Drawing.Size(384, 47);
            this.CfmParamChange.TabIndex = 21;
            this.CfmParamChange.Text = "Confirm Changes";
            this.CfmParamChange.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CmbBoxTravCorr);
            this.groupBox1.Controls.Add(this.CmbBoxDbSide);
            this.groupBox1.Controls.Add(this.CmbBoxDir);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPalletLen);
            this.groupBox1.Controls.Add(this.txtTravelSteps);
            this.groupBox1.Controls.Add(this.txtGapSize);
            this.groupBox1.Controls.Add(this.txtStopPos);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(515, 513);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conveyor Card Settings";
            // 
            // CmbBoxTravCorr
            // 
            this.CmbBoxTravCorr.FormattingEnabled = true;
            this.CmbBoxTravCorr.Location = new System.Drawing.Point(281, 434);
            this.CmbBoxTravCorr.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.CmbBoxTravCorr.Name = "CmbBoxTravCorr";
            this.CmbBoxTravCorr.Size = new System.Drawing.Size(207, 37);
            this.CmbBoxTravCorr.TabIndex = 40;
            // 
            // CmbBoxDbSide
            // 
            this.CmbBoxDbSide.FormattingEnabled = true;
            this.CmbBoxDbSide.Location = new System.Drawing.Point(281, 373);
            this.CmbBoxDbSide.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.CmbBoxDbSide.Name = "CmbBoxDbSide";
            this.CmbBoxDbSide.Size = new System.Drawing.Size(207, 37);
            this.CmbBoxDbSide.TabIndex = 39;
            // 
            // CmbBoxDir
            // 
            this.CmbBoxDir.FormattingEnabled = true;
            this.CmbBoxDir.Location = new System.Drawing.Point(281, 311);
            this.CmbBoxDir.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.CmbBoxDir.Name = "CmbBoxDir";
            this.CmbBoxDir.Size = new System.Drawing.Size(207, 37);
            this.CmbBoxDir.TabIndex = 38;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(15, 373);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(165, 30);
            this.label13.TabIndex = 37;
            this.label13.Text = "Double-sided:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 434);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(209, 30);
            this.label6.TabIndex = 36;
            this.label6.Text = "Travel Correction:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 311);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 30);
            this.label5.TabIndex = 35;
            this.label5.Text = "Direction:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(15, 127);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 30);
            this.label9.TabIndex = 33;
            this.label9.Text = "Stop Pos:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(15, 188);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 30);
            this.label8.TabIndex = 32;
            this.label8.Text = "Gap Size:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 231);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 60);
            this.label7.TabIndex = 31;
            this.label7.Text = "Over/Under\r\nTravel Steps:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 30);
            this.label2.TabIndex = 30;
            this.label2.Text = "Pallet Len:";
            // 
            // txtPalletLen
            // 
            this.txtPalletLen.Location = new System.Drawing.Point(281, 65);
            this.txtPalletLen.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtPalletLen.Name = "txtPalletLen";
            this.txtPalletLen.Size = new System.Drawing.Size(207, 36);
            this.txtPalletLen.TabIndex = 26;
            // 
            // txtTravelSteps
            // 
            this.txtTravelSteps.Location = new System.Drawing.Point(281, 250);
            this.txtTravelSteps.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtTravelSteps.Name = "txtTravelSteps";
            this.txtTravelSteps.Size = new System.Drawing.Size(207, 36);
            this.txtTravelSteps.TabIndex = 27;
            // 
            // txtGapSize
            // 
            this.txtGapSize.Location = new System.Drawing.Point(281, 188);
            this.txtGapSize.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtGapSize.Name = "txtGapSize";
            this.txtGapSize.Size = new System.Drawing.Size(207, 36);
            this.txtGapSize.TabIndex = 28;
            // 
            // txtStopPos
            // 
            this.txtStopPos.Location = new System.Drawing.Point(281, 127);
            this.txtStopPos.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtStopPos.Name = "txtStopPos";
            this.txtStopPos.Size = new System.Drawing.Size(207, 36);
            this.txtStopPos.TabIndex = 29;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(4, 4);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1036, 100);
            this.panel5.TabIndex = 21;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.cbBoxAddrID);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1033, 100);
            this.panel7.TabIndex = 0;
            // 
            // cbBoxAddrID
            // 
            this.cbBoxAddrID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbBoxAddrID.FormattingEnabled = true;
            this.cbBoxAddrID.Location = new System.Drawing.Point(585, 31);
            this.cbBoxAddrID.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.cbBoxAddrID.Name = "cbBoxAddrID";
            this.cbBoxAddrID.Size = new System.Drawing.Size(160, 37);
            this.cbBoxAddrID.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(75, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(481, 30);
            this.label3.TabIndex = 20;
            this.label3.Text = "Import/Modify From Current Card Setting:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Linen;
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(1052, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(296, 670);
            this.panel4.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel8.Controls.Add(this.EditSpecAddrID);
            this.panel8.Controls.Add(this.ConvParam);
            this.panel8.Controls.Add(this.SpecAddr);
            this.panel8.Controls.Add(this.btnAll_Blink);
            this.panel8.Controls.Add(this.btnAll_StopBlink);
            this.panel8.Controls.Add(this.IdtIDLed);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(296, 667);
            this.panel8.TabIndex = 2;
            // 
            // EditSpecAddrID
            // 
            this.EditSpecAddrID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EditSpecAddrID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditSpecAddrID.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditSpecAddrID.Location = new System.Drawing.Point(18, 569);
            this.EditSpecAddrID.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.EditSpecAddrID.Name = "EditSpecAddrID";
            this.EditSpecAddrID.Size = new System.Drawing.Size(256, 84);
            this.EditSpecAddrID.TabIndex = 37;
            this.EditSpecAddrID.Text = "Edit One Card Address";
            this.EditSpecAddrID.UseVisualStyleBackColor = true;
            this.EditSpecAddrID.Click += new System.EventHandler(this.EditSpecAddrID_Click);
            // 
            // ConvParam
            // 
            this.ConvParam.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ConvParam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConvParam.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConvParam.Location = new System.Drawing.Point(18, 367);
            this.ConvParam.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.ConvParam.Name = "ConvParam";
            this.ConvParam.Size = new System.Drawing.Size(256, 84);
            this.ConvParam.TabIndex = 36;
            this.ConvParam.Text = "List All Parameters";
            this.ConvParam.UseVisualStyleBackColor = true;
            this.ConvParam.Click += new System.EventHandler(this.ConvParam_Click);
            // 
            // SpecAddr
            // 
            this.SpecAddr.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SpecAddr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SpecAddr.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpecAddr.Location = new System.Drawing.Point(20, 468);
            this.SpecAddr.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.SpecAddr.Name = "SpecAddr";
            this.SpecAddr.Size = new System.Drawing.Size(256, 84);
            this.SpecAddr.TabIndex = 35;
            this.SpecAddr.Text = "Edit All Card Address";
            this.SpecAddr.UseVisualStyleBackColor = true;
            this.SpecAddr.Click += new System.EventHandler(this.SpecAddr_Click);
            // 
            // btnAll_Blink
            // 
            this.btnAll_Blink.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAll_Blink.AutoSize = true;
            this.btnAll_Blink.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAll_Blink.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAll_Blink.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAll_Blink.Location = new System.Drawing.Point(20, 64);
            this.btnAll_Blink.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnAll_Blink.Name = "btnAll_Blink";
            this.btnAll_Blink.Size = new System.Drawing.Size(256, 84);
            this.btnAll_Blink.TabIndex = 32;
            this.btnAll_Blink.Text = "Identify All Card";
            this.btnAll_Blink.UseVisualStyleBackColor = true;
            this.btnAll_Blink.Click += new System.EventHandler(this.btnAll_Blink_Click);
            // 
            // btnAll_StopBlink
            // 
            this.btnAll_StopBlink.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAll_StopBlink.AutoSize = true;
            this.btnAll_StopBlink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAll_StopBlink.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAll_StopBlink.Location = new System.Drawing.Point(20, 163);
            this.btnAll_StopBlink.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnAll_StopBlink.Name = "btnAll_StopBlink";
            this.btnAll_StopBlink.Size = new System.Drawing.Size(260, 90);
            this.btnAll_StopBlink.TabIndex = 33;
            this.btnAll_StopBlink.Text = "Stop Identifying\r\nAll Card";
            this.btnAll_StopBlink.UseVisualStyleBackColor = true;
            this.btnAll_StopBlink.Click += new System.EventHandler(this.btnAll_StopBlink_Click);
            // 
            // IdtIDLed
            // 
            this.IdtIDLed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IdtIDLed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdtIDLed.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdtIDLed.Location = new System.Drawing.Point(20, 266);
            this.IdtIDLed.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.IdtIDLed.Name = "IdtIDLed";
            this.IdtIDLed.Size = new System.Drawing.Size(256, 84);
            this.IdtIDLed.TabIndex = 34;
            this.IdtIDLed.Text = "Identify Specific Card";
            this.IdtIDLed.UseVisualStyleBackColor = true;
            this.IdtIDLed.Click += new System.EventHandler(this.IdtIDLed_Click);
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Margin = new System.Windows.Forms.Padding(4);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(292, 46);
            this.panel9.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SeaShell;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.panel11);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 802);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1348, 95);
            this.panel2.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1344, 36);
            this.textBox1.TabIndex = 3;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.button1);
            this.panel11.Controls.Add(this.label12);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel11.Location = new System.Drawing.Point(0, 36);
            this.panel11.Margin = new System.Windows.Forms.Padding(4);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1344, 55);
            this.panel11.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1124, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 58);
            this.button1.TabIndex = 44;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Berlin Sans FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(13, 16);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(170, 23);
            this.label12.TabIndex = 0;
            this.label12.Text = "Information Display";
            // 
            // confirmButtonTimer
            // 
            this.confirmButtonTimer.Interval = 3000;
            this.confirmButtonTimer.Tick += new System.EventHandler(this.confirmButtonTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1348, 897);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Driveyor UI";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ComboBox cbBoxAddrID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMotorCurrent;
        private System.Windows.Forms.TextBox txtMotorSpeed;
        private System.Windows.Forms.TextBox txtTravelSpeed;
        private System.Windows.Forms.Button CfmParamChange;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CmbBoxTravCorr;
        private System.Windows.Forms.ComboBox CmbBoxDbSide;
        private System.Windows.Forms.ComboBox CmbBoxDir;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPalletLen;
        private System.Windows.Forms.TextBox txtTravelSteps;
        private System.Windows.Forms.TextBox txtGapSize;
        private System.Windows.Forms.TextBox txtStopPos;
        private System.Windows.Forms.Button EditSpecAddrID;
        private System.Windows.Forms.Button ConvParam;
        private System.Windows.Forms.Button SpecAddr;
        private System.Windows.Forms.Button btnAll_Blink;
        private System.Windows.Forms.Button btnAll_StopBlink;
        private System.Windows.Forms.Button IdtIDLed;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Button Refresh;
        private System.Windows.Forms.Label lblMsg2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox comB_COM_Port;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Timer confirmButtonTimer;
    }
}

