namespace Map_v1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtLat = new System.Windows.Forms.TextBox();
            this.txtLong = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadIntoMap = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.myTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.btnClearMap = new System.Windows.Forms.Button();
            this.btnConnectMarker = new System.Windows.Forms.Button();
            this.btnRemoveMarker = new System.Windows.Forms.Button();
            this.btnStartPoint = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label43 = new System.Windows.Forms.Label();
            this.txtDistanceKm = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txtDistanceM = new System.Windows.Forms.TextBox();
            this.btnMeasureDistance = new System.Windows.Forms.Button();
            this.btnSaveTableTo = new System.Windows.Forms.Button();
            this.btnSaveTable = new System.Windows.Forms.Button();
            this.lblCheckConneton = new System.Windows.Forms.Label();
            this.txtYstep = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtXstep = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnLoadTable = new System.Windows.Forms.Button();
            this.dataGridViewPlan = new System.Windows.Forms.DataGridView();
            this.snrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lngDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waypointsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.farmerDataSet = new Map_v1.FarmerDataSet();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbAreaUnit = new System.Windows.Forms.ComboBox();
            this.chkStart = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.cmbMapTypes = new System.Windows.Forms.ComboBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnRoute = new System.Windows.Forms.Button();
            this.btnPoligon = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.txtRoverLat = new System.Windows.Forms.TextBox();
            this.txtHeading = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.txtRoverLng = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.cmbRoutes = new System.Windows.Forms.ComboBox();
            this.btnLoadRoute = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtWheelTurnNo = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.txtRoverRemHead = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtRoverRemLng = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtRoverRemLat = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSendMessage = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.lbRoverMsg = new System.Windows.Forms.ListBox();
            this.comPorts = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnBackward = new System.Windows.Forms.Button();
            this.bthLeft = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDiag = new System.Windows.Forms.Button();
            this.map = new GMap.NET.WindowsForms.GMapControl();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.waypointsTableAdapter = new Map_v1.FarmerDataSetTableAdapters.waypointsTableAdapter();
            this.timerRover = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnClearListbox = new System.Windows.Forms.Button();
            this.btnPutRoverMap = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waypointsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.farmerDataSet)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLat
            // 
            this.txtLat.Location = new System.Drawing.Point(6, 70);
            this.txtLat.Name = "txtLat";
            this.txtLat.Size = new System.Drawing.Size(100, 20);
            this.txtLat.TabIndex = 2;
            this.txtLat.Text = "28.459806";
            // 
            // txtLong
            // 
            this.txtLong.Location = new System.Drawing.Point(6, 109);
            this.txtLong.Name = "txtLong";
            this.txtLong.Size = new System.Drawing.Size(100, 20);
            this.txtLong.TabIndex = 3;
            this.txtLong.Text = "77.2874577";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Latitude:";
            // 
            // btnLoadIntoMap
            // 
            this.btnLoadIntoMap.Location = new System.Drawing.Point(6, 135);
            this.btnLoadIntoMap.Name = "btnLoadIntoMap";
            this.btnLoadIntoMap.Size = new System.Drawing.Size(100, 34);
            this.btnLoadIntoMap.TabIndex = 5;
            this.btnLoadIntoMap.Text = "Load into map";
            this.btnLoadIntoMap.UseVisualStyleBackColor = true;
            this.btnLoadIntoMap.Click += new System.EventHandler(this.btnLoadIntoMap_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Longitude:";
            // 
            // myTooltip
            // 
            this.myTooltip.ToolTipTitle = "btnRouting";
            // 
            // btnClearMap
            // 
            this.btnClearMap.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearMap.BackgroundImage = global::Map_v1.Properties.Resources.map_delete;
            this.btnClearMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearMap.Location = new System.Drawing.Point(222, 7);
            this.btnClearMap.Name = "btnClearMap";
            this.btnClearMap.Size = new System.Drawing.Size(49, 44);
            this.btnClearMap.TabIndex = 24;
            this.btnClearMap.Tag = "Clear";
            this.myTooltip.SetToolTip(this.btnClearMap, "Clear the map");
            this.btnClearMap.UseVisualStyleBackColor = true;
            this.btnClearMap.Click += new System.EventHandler(this.btnClarMap_Click);
            // 
            // btnConnectMarker
            // 
            this.btnConnectMarker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnConnectMarker.BackgroundImage = global::Map_v1.Properties.Resources.map_edit;
            this.btnConnectMarker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConnectMarker.Location = new System.Drawing.Point(112, 6);
            this.btnConnectMarker.Name = "btnConnectMarker";
            this.btnConnectMarker.Size = new System.Drawing.Size(50, 44);
            this.btnConnectMarker.TabIndex = 17;
            this.myTooltip.SetToolTip(this.btnConnectMarker, "Connect corners");
            this.btnConnectMarker.UseVisualStyleBackColor = false;
            this.btnConnectMarker.Click += new System.EventHandler(this.btnConnectMarker_Click);
            // 
            // btnRemoveMarker
            // 
            this.btnRemoveMarker.BackgroundImage = global::Map_v1.Properties.Resources.delete_marker;
            this.btnRemoveMarker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoveMarker.Location = new System.Drawing.Point(168, 7);
            this.btnRemoveMarker.Name = "btnRemoveMarker";
            this.btnRemoveMarker.Size = new System.Drawing.Size(47, 44);
            this.btnRemoveMarker.TabIndex = 16;
            this.myTooltip.SetToolTip(this.btnRemoveMarker, "Remove last marker");
            this.btnRemoveMarker.UseVisualStyleBackColor = true;
            this.btnRemoveMarker.Click += new System.EventHandler(this.btnRemoveMarker_Click);
            // 
            // btnStartPoint
            // 
            this.btnStartPoint.BackgroundImage = global::Map_v1.Properties.Resources.start_mark;
            this.btnStartPoint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStartPoint.Location = new System.Drawing.Point(60, 7);
            this.btnStartPoint.Name = "btnStartPoint";
            this.btnStartPoint.Size = new System.Drawing.Size(46, 43);
            this.btnStartPoint.TabIndex = 7;
            this.btnStartPoint.Tag = "Routing";
            this.myTooltip.SetToolTip(this.btnStartPoint, "Set the Starting point");
            this.btnStartPoint.UseVisualStyleBackColor = true;
            this.btnStartPoint.Click += new System.EventHandler(this.btnStartPoint_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(573, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(338, 600);
            this.tabControl1.TabIndex = 12;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Silver;
            this.tabPage1.Controls.Add(this.label43);
            this.tabPage1.Controls.Add(this.txtDistanceKm);
            this.tabPage1.Controls.Add(this.label42);
            this.tabPage1.Controls.Add(this.txtDistanceM);
            this.tabPage1.Controls.Add(this.btnMeasureDistance);
            this.tabPage1.Controls.Add(this.btnSaveTableTo);
            this.tabPage1.Controls.Add(this.btnSaveTable);
            this.tabPage1.Controls.Add(this.lblCheckConneton);
            this.tabPage1.Controls.Add(this.txtYstep);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.txtXstep);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.btnLoadTable);
            this.tabPage1.Controls.Add(this.dataGridViewPlan);
            this.tabPage1.Controls.Add(this.btnClearMap);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.cmbAreaUnit);
            this.tabPage1.Controls.Add(this.chkStart);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.txtArea);
            this.tabPage1.Controls.Add(this.btnConnectMarker);
            this.tabPage1.Controls.Add(this.btnRemoveMarker);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.txtY);
            this.tabPage1.Controls.Add(this.txtX);
            this.tabPage1.Controls.Add(this.cmbMapTypes);
            this.tabPage1.Controls.Add(this.txtOutput);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtLat);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtLong);
            this.tabPage1.Controls.Add(this.btnRoute);
            this.tabPage1.Controls.Add(this.btnLoadIntoMap);
            this.tabPage1.Controls.Add(this.btnPoligon);
            this.tabPage1.Controls.Add(this.btnStartPoint);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(330, 574);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Planner";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(180, 218);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(75, 13);
            this.label43.TabIndex = 42;
            this.label43.Text = "Distance [km]:";
            // 
            // txtDistanceKm
            // 
            this.txtDistanceKm.Location = new System.Drawing.Point(256, 215);
            this.txtDistanceKm.Name = "txtDistanceKm";
            this.txtDistanceKm.Size = new System.Drawing.Size(68, 20);
            this.txtDistanceKm.TabIndex = 41;
            this.txtDistanceKm.Text = "0";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(180, 192);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(69, 13);
            this.label42.TabIndex = 40;
            this.label42.Text = "Distance [m]:";
            // 
            // txtDistanceM
            // 
            this.txtDistanceM.Location = new System.Drawing.Point(256, 189);
            this.txtDistanceM.Name = "txtDistanceM";
            this.txtDistanceM.Size = new System.Drawing.Size(68, 20);
            this.txtDistanceM.TabIndex = 39;
            this.txtDistanceM.Text = "0";
            // 
            // btnMeasureDistance
            // 
            this.btnMeasureDistance.BackgroundImage = global::Map_v1.Properties.Resources.measuring_tape;
            this.btnMeasureDistance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMeasureDistance.Location = new System.Drawing.Point(277, 6);
            this.btnMeasureDistance.Name = "btnMeasureDistance";
            this.btnMeasureDistance.Size = new System.Drawing.Size(47, 44);
            this.btnMeasureDistance.TabIndex = 38;
            this.btnMeasureDistance.UseVisualStyleBackColor = true;
            this.btnMeasureDistance.Click += new System.EventHandler(this.btnMeasureDistance_Click);
            // 
            // btnSaveTableTo
            // 
            this.btnSaveTableTo.Location = new System.Drawing.Point(177, 264);
            this.btnSaveTableTo.Name = "btnSaveTableTo";
            this.btnSaveTableTo.Size = new System.Drawing.Size(72, 41);
            this.btnSaveTableTo.TabIndex = 33;
            this.btnSaveTableTo.Text = "Save table TO:";
            this.btnSaveTableTo.UseVisualStyleBackColor = true;
            this.btnSaveTableTo.Click += new System.EventHandler(this.btnSaveTableTo_Click);
            // 
            // btnSaveTable
            // 
            this.btnSaveTable.Location = new System.Drawing.Point(252, 258);
            this.btnSaveTable.Name = "btnSaveTable";
            this.btnSaveTable.Size = new System.Drawing.Size(72, 24);
            this.btnSaveTable.TabIndex = 32;
            this.btnSaveTable.Text = "Save table";
            this.btnSaveTable.UseVisualStyleBackColor = true;
            this.btnSaveTable.Click += new System.EventHandler(this.btnSaveTable_Click);
            // 
            // lblCheckConneton
            // 
            this.lblCheckConneton.AutoSize = true;
            this.lblCheckConneton.Location = new System.Drawing.Point(10, 296);
            this.lblCheckConneton.Name = "lblCheckConneton";
            this.lblCheckConneton.Size = new System.Drawing.Size(41, 13);
            this.lblCheckConneton.TabIndex = 31;
            this.lblCheckConneton.Text = "label13";
            // 
            // txtYstep
            // 
            this.txtYstep.Location = new System.Drawing.Point(112, 272);
            this.txtYstep.Name = "txtYstep";
            this.txtYstep.Size = new System.Drawing.Size(52, 20);
            this.txtYstep.TabIndex = 30;
            this.txtYstep.Text = "0.0002";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(112, 258);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "Y step:";
            // 
            // txtXstep
            // 
            this.txtXstep.Location = new System.Drawing.Point(54, 272);
            this.txtXstep.Name = "txtXstep";
            this.txtXstep.Size = new System.Drawing.Size(52, 20);
            this.txtXstep.TabIndex = 28;
            this.txtXstep.Text = "0.0005";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(54, 258);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "X step:";
            // 
            // btnLoadTable
            // 
            this.btnLoadTable.Location = new System.Drawing.Point(252, 281);
            this.btnLoadTable.Name = "btnLoadTable";
            this.btnLoadTable.Size = new System.Drawing.Size(72, 24);
            this.btnLoadTable.TabIndex = 26;
            this.btnLoadTable.Text = "Load table";
            this.btnLoadTable.UseVisualStyleBackColor = true;
            this.btnLoadTable.Click += new System.EventHandler(this.btnLoadTable_Click);
            // 
            // dataGridViewPlan
            // 
            this.dataGridViewPlan.AutoGenerateColumns = false;
            this.dataGridViewPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.snrDataGridViewTextBoxColumn,
            this.latDataGridViewTextBoxColumn,
            this.lngDataGridViewTextBoxColumn});
            this.dataGridViewPlan.DataSource = this.waypointsBindingSource;
            this.dataGridViewPlan.Location = new System.Drawing.Point(6, 312);
            this.dataGridViewPlan.Name = "dataGridViewPlan";
            this.dataGridViewPlan.Size = new System.Drawing.Size(321, 182);
            this.dataGridViewPlan.TabIndex = 25;
            this.dataGridViewPlan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPlan_CellClick);
            // 
            // snrDataGridViewTextBoxColumn
            // 
            this.snrDataGridViewTextBoxColumn.DataPropertyName = "Snr";
            this.snrDataGridViewTextBoxColumn.HeaderText = "Snr";
            this.snrDataGridViewTextBoxColumn.Name = "snrDataGridViewTextBoxColumn";
            // 
            // latDataGridViewTextBoxColumn
            // 
            this.latDataGridViewTextBoxColumn.DataPropertyName = "Lat";
            this.latDataGridViewTextBoxColumn.HeaderText = "Lat";
            this.latDataGridViewTextBoxColumn.Name = "latDataGridViewTextBoxColumn";
            // 
            // lngDataGridViewTextBoxColumn
            // 
            this.lngDataGridViewTextBoxColumn.DataPropertyName = "Lng";
            this.lngDataGridViewTextBoxColumn.HeaderText = "Lng";
            this.lngDataGridViewTextBoxColumn.Name = "lngDataGridViewTextBoxColumn";
            // 
            // waypointsBindingSource
            // 
            this.waypointsBindingSource.DataMember = "waypoints";
            this.waypointsBindingSource.DataSource = this.farmerDataSet;
            // 
            // farmerDataSet
            // 
            this.farmerDataSet.DataSetName = "FarmerDataSet";
            this.farmerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(257, 147);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Unit:";
            // 
            // cmbAreaUnit
            // 
            this.cmbAreaUnit.FormattingEnabled = true;
            this.cmbAreaUnit.Location = new System.Drawing.Point(257, 162);
            this.cmbAreaUnit.Name = "cmbAreaUnit";
            this.cmbAreaUnit.Size = new System.Drawing.Size(67, 21);
            this.cmbAreaUnit.TabIndex = 22;
            this.cmbAreaUnit.SelectedIndexChanged += new System.EventHandler(this.cmbAreaUnit_SelectedIndexChanged);
            // 
            // chkStart
            // 
            this.chkStart.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkStart.AutoSize = true;
            this.chkStart.Location = new System.Drawing.Point(57, 49);
            this.chkStart.Name = "chkStart";
            this.chkStart.Size = new System.Drawing.Size(39, 23);
            this.chkStart.TabIndex = 21;
            this.chkStart.Text = "Start";
            this.chkStart.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(183, 147);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Area:";
            // 
            // txtArea
            // 
            this.txtArea.Location = new System.Drawing.Point(183, 163);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(68, 20);
            this.txtArea.TabIndex = 19;
            this.txtArea.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 211);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Marker\'s LONG:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Marker\'s LAT:";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(6, 227);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(100, 20);
            this.txtY.TabIndex = 13;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(6, 188);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(100, 20);
            this.txtX.TabIndex = 12;
            // 
            // cmbMapTypes
            // 
            this.cmbMapTypes.FormattingEnabled = true;
            this.cmbMapTypes.Location = new System.Drawing.Point(122, 109);
            this.cmbMapTypes.Name = "cmbMapTypes";
            this.cmbMapTypes.Size = new System.Drawing.Size(202, 21);
            this.cmbMapTypes.TabIndex = 11;
            this.cmbMapTypes.SelectedIndexChanged += new System.EventHandler(this.cbMapTypes_SelectedIndexChanged);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(122, 70);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(202, 20);
            this.txtOutput.TabIndex = 10;
            // 
            // btnRoute
            // 
            this.btnRoute.BackgroundImage = global::Map_v1.Properties.Resources.flight_area;
            this.btnRoute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRoute.Location = new System.Drawing.Point(6, 7);
            this.btnRoute.Name = "btnRoute";
            this.btnRoute.Size = new System.Drawing.Size(46, 44);
            this.btnRoute.TabIndex = 9;
            this.btnRoute.Tag = "Routing";
            this.btnRoute.UseVisualStyleBackColor = true;
            // 
            // btnPoligon
            // 
            this.btnPoligon.BackgroundImage = global::Map_v1.Properties.Resources.area3;
            this.btnPoligon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPoligon.Location = new System.Drawing.Point(119, 140);
            this.btnPoligon.Name = "btnPoligon";
            this.btnPoligon.Size = new System.Drawing.Size(45, 43);
            this.btnPoligon.TabIndex = 8;
            this.btnPoligon.Tag = "Routing";
            this.btnPoligon.UseVisualStyleBackColor = true;
            this.btnPoligon.Click += new System.EventHandler(this.btnPoligon_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tabPage5.Controls.Add(this.groupBox1);
            this.tabPage5.Controls.Add(this.button1);
            this.tabPage5.Controls.Add(this.dataGridView2);
            this.tabPage5.Controls.Add(this.cmbRoutes);
            this.tabPage5.Controls.Add(this.btnLoadRoute);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(330, 574);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Lunch";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.groupBox1.Controls.Add(this.label44);
            this.groupBox1.Controls.Add(this.label40);
            this.groupBox1.Controls.Add(this.txtRoverLat);
            this.groupBox1.Controls.Add(this.txtHeading);
            this.groupBox1.Controls.Add(this.label41);
            this.groupBox1.Controls.Add(this.txtRoverLng);
            this.groupBox1.Location = new System.Drawing.Point(89, 302);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 115);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rover:";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(155, 16);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(77, 13);
            this.label44.TabIndex = 43;
            this.label44.Text = "Heading [deg]:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(6, 16);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(48, 13);
            this.label40.TabIndex = 40;
            this.label40.Text = "Latitude:";
            // 
            // txtRoverLat
            // 
            this.txtRoverLat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoverLat.Location = new System.Drawing.Point(9, 30);
            this.txtRoverLat.Name = "txtRoverLat";
            this.txtRoverLat.Size = new System.Drawing.Size(126, 26);
            this.txtRoverLat.TabIndex = 38;
            this.txtRoverLat.Text = "---";
            // 
            // txtHeading
            // 
            this.txtHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeading.Location = new System.Drawing.Point(158, 30);
            this.txtHeading.Name = "txtHeading";
            this.txtHeading.Size = new System.Drawing.Size(42, 26);
            this.txtHeading.TabIndex = 42;
            this.txtHeading.Text = "--";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(6, 59);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(57, 13);
            this.label41.TabIndex = 41;
            this.label41.Text = "Longitude:";
            // 
            // txtRoverLng
            // 
            this.txtRoverLng.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoverLng.Location = new System.Drawing.Point(9, 75);
            this.txtRoverLng.Name = "txtRoverLng";
            this.txtRoverLng.Size = new System.Drawing.Size(126, 26);
            this.txtRoverLng.TabIndex = 39;
            this.txtRoverLng.Text = "---";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 302);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 41);
            this.button1.TabIndex = 37;
            this.button1.Text = "Start rover to follow";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(4, 39);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(323, 257);
            this.dataGridView2.TabIndex = 36;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // cmbRoutes
            // 
            this.cmbRoutes.FormattingEnabled = true;
            this.cmbRoutes.Location = new System.Drawing.Point(4, 4);
            this.cmbRoutes.Name = "cmbRoutes";
            this.cmbRoutes.Size = new System.Drawing.Size(245, 21);
            this.cmbRoutes.TabIndex = 35;
            // 
            // btnLoadRoute
            // 
            this.btnLoadRoute.Location = new System.Drawing.Point(255, 3);
            this.btnLoadRoute.Name = "btnLoadRoute";
            this.btnLoadRoute.Size = new System.Drawing.Size(72, 30);
            this.btnLoadRoute.TabIndex = 34;
            this.btnLoadRoute.Text = "Load route";
            this.btnLoadRoute.UseVisualStyleBackColor = true;
            this.btnLoadRoute.Click += new System.EventHandler(this.btnLoadRoute_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage2.Controls.Add(this.btnPutRoverMap);
            this.tabPage2.Controls.Add(this.btnClearListbox);
            this.tabPage2.Controls.Add(this.txtWheelTurnNo);
            this.tabPage2.Controls.Add(this.label46);
            this.tabPage2.Controls.Add(this.txtRoverRemHead);
            this.tabPage2.Controls.Add(this.label45);
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Controls.Add(this.txtRoverRemLng);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.txtRoverRemLat);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.txtSendMessage);
            this.tabPage2.Controls.Add(this.btnSendMessage);
            this.tabPage2.Controls.Add(this.lbRoverMsg);
            this.tabPage2.Controls.Add(this.comPorts);
            this.tabPage2.Controls.Add(this.btnRefresh);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.btnConnect);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(330, 574);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Remote";
            // 
            // txtWheelTurnNo
            // 
            this.txtWheelTurnNo.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtWheelTurnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWheelTurnNo.Location = new System.Drawing.Point(196, 20);
            this.txtWheelTurnNo.Name = "txtWheelTurnNo";
            this.txtWheelTurnNo.Size = new System.Drawing.Size(40, 22);
            this.txtWheelTurnNo.TabIndex = 52;
            this.txtWheelTurnNo.Text = "1";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(193, 4);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(79, 13);
            this.label46.TabIndex = 53;
            this.label46.Text = "Wheel turn No:";
            // 
            // txtRoverRemHead
            // 
            this.txtRoverRemHead.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtRoverRemHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoverRemHead.Location = new System.Drawing.Point(8, 150);
            this.txtRoverRemHead.Name = "txtRoverRemHead";
            this.txtRoverRemHead.Size = new System.Drawing.Size(53, 22);
            this.txtRoverRemHead.TabIndex = 50;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(7, 132);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(50, 13);
            this.label45.TabIndex = 51;
            this.label45.Text = "Heading:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(10, 506);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(97, 13);
            this.label19.TabIndex = 47;
            this.label19.Text = "Message to Rover:";
            // 
            // txtRoverRemLng
            // 
            this.txtRoverRemLng.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtRoverRemLng.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoverRemLng.Location = new System.Drawing.Point(7, 109);
            this.txtRoverRemLng.Name = "txtRoverRemLng";
            this.txtRoverRemLng.Size = new System.Drawing.Size(144, 22);
            this.txtRoverRemLng.TabIndex = 44;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(4, 93);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 13);
            this.label18.TabIndex = 45;
            this.label18.Text = "Longitude:";
            // 
            // txtRoverRemLat
            // 
            this.txtRoverRemLat.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtRoverRemLat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoverRemLat.Location = new System.Drawing.Point(7, 71);
            this.txtRoverRemLat.Name = "txtRoverRemLat";
            this.txtRoverRemLat.Size = new System.Drawing.Size(144, 22);
            this.txtRoverRemLat.TabIndex = 42;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(4, 55);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 13);
            this.label17.TabIndex = 43;
            this.label17.Text = "Latitude:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 220);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(108, 13);
            this.label16.TabIndex = 41;
            this.label16.Text = "Message from Rover:";
            // 
            // txtSendMessage
            // 
            this.txtSendMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSendMessage.Location = new System.Drawing.Point(3, 522);
            this.txtSendMessage.Name = "txtSendMessage";
            this.txtSendMessage.Size = new System.Drawing.Size(256, 26);
            this.txtSendMessage.TabIndex = 29;
            this.txtSendMessage.Text = "forward,50,2 status";
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(268, 503);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(56, 45);
            this.btnSendMessage.TabIndex = 28;
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // lbRoverMsg
            // 
            this.lbRoverMsg.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRoverMsg.FormattingEnabled = true;
            this.lbRoverMsg.ItemHeight = 15;
            this.lbRoverMsg.Location = new System.Drawing.Point(8, 236);
            this.lbRoverMsg.Name = "lbRoverMsg";
            this.lbRoverMsg.Size = new System.Drawing.Size(316, 259);
            this.lbRoverMsg.TabIndex = 27;
            // 
            // comPorts
            // 
            this.comPorts.FormattingEnabled = true;
            this.comPorts.Location = new System.Drawing.Point(8, 21);
            this.comPorts.Name = "comPorts";
            this.comPorts.Size = new System.Drawing.Size(63, 21);
            this.comPorts.TabIndex = 20;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(82, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(56, 42);
            this.btnRefresh.TabIndex = 19;
            this.btnRefresh.Text = "Refresh COM list";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "COM ports:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnRight);
            this.panel1.Controls.Add(this.btnBackward);
            this.panel1.Controls.Add(this.bthLeft);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnForward);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Location = new System.Drawing.Point(157, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(165, 165);
            this.panel1.TabIndex = 49;
            // 
            // btnRight
            // 
            this.btnRight.BackgroundImage = global::Map_v1.Properties.Resources.Right_pic;
            this.btnRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRight.Location = new System.Drawing.Point(110, 60);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(48, 43);
            this.btnRight.TabIndex = 14;
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnBackward
            // 
            this.btnBackward.BackgroundImage = global::Map_v1.Properties.Resources.Down_pic;
            this.btnBackward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBackward.Location = new System.Drawing.Point(57, 109);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(48, 46);
            this.btnBackward.TabIndex = 12;
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // bthLeft
            // 
            this.bthLeft.BackgroundImage = global::Map_v1.Properties.Resources.Left_pic;
            this.bthLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bthLeft.Location = new System.Drawing.Point(3, 60);
            this.bthLeft.Name = "bthLeft";
            this.bthLeft.Size = new System.Drawing.Size(48, 43);
            this.bthLeft.TabIndex = 13;
            this.bthLeft.UseVisualStyleBackColor = true;
            this.bthLeft.Click += new System.EventHandler(this.bthLeft_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImage = global::Map_v1.Properties.Resources.Stop_pic;
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStop.Location = new System.Drawing.Point(57, 60);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(48, 43);
            this.btnStop.TabIndex = 15;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnForward
            // 
            this.btnForward.BackgroundImage = global::Map_v1.Properties.Resources.Up_pic;
            this.btnForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnForward.Location = new System.Drawing.Point(57, 7);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(48, 47);
            this.btnForward.TabIndex = 11;
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnStart.BackgroundImage = global::Map_v1.Properties.Resources.Start_pic;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStart.Location = new System.Drawing.Point(3, 7);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(48, 43);
            this.btnStart.TabIndex = 16;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.BackgroundImage = global::Map_v1.Properties.Resources.dis_connect;
            this.btnConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConnect.Location = new System.Drawing.Point(144, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(46, 48);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Tag = "Routing";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.tabPage3.Controls.Add(this.label39);
            this.tabPage3.Controls.Add(this.label38);
            this.tabPage3.Controls.Add(this.label37);
            this.tabPage3.Controls.Add(this.label36);
            this.tabPage3.Controls.Add(this.label35);
            this.tabPage3.Controls.Add(this.label32);
            this.tabPage3.Controls.Add(this.label33);
            this.tabPage3.Controls.Add(this.label34);
            this.tabPage3.Controls.Add(this.label31);
            this.tabPage3.Controls.Add(this.label30);
            this.tabPage3.Controls.Add(this.label29);
            this.tabPage3.Controls.Add(this.label28);
            this.tabPage3.Controls.Add(this.label27);
            this.tabPage3.Controls.Add(this.label26);
            this.tabPage3.Controls.Add(this.label25);
            this.tabPage3.Controls.Add(this.label24);
            this.tabPage3.Controls.Add(this.label23);
            this.tabPage3.Controls.Add(this.label22);
            this.tabPage3.Controls.Add(this.label21);
            this.tabPage3.Controls.Add(this.label20);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(330, 574);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "GPS";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(143, 27);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(103, 13);
            this.label39.TabIndex = 19;
            this.label39.Text = "Position fix indicator:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(13, 66);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(45, 13);
            this.label38.TabIndex = 18;
            this.label38.Text = "Altitude:";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(13, 53);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(57, 13);
            this.label37.TabIndex = 17;
            this.label37.Text = "Longitude:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(13, 40);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(48, 13);
            this.label36.TabIndex = 16;
            this.label36.Text = "Latitude:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(13, 27);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(54, 13);
            this.label35.TabIndex = 15;
            this.label35.Text = "UTC time:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(58, 146);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(16, 13);
            this.label32.TabIndex = 14;
            this.label32.Text = "---";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(58, 133);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(16, 13);
            this.label33.TabIndex = 13;
            this.label33.Text = "---";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(58, 120);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(16, 13);
            this.label34.TabIndex = 12;
            this.label34.Text = "---";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(11, 146);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(40, 13);
            this.label31.TabIndex = 11;
            this.label31.Text = "VDOP:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(11, 133);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(41, 13);
            this.label30.TabIndex = 10;
            this.label30.Text = "HDOP:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(11, 120);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(40, 13);
            this.label29.TabIndex = 9;
            this.label29.Text = "PDOP:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(289, 146);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(16, 13);
            this.label28.TabIndex = 8;
            this.label28.Text = "---";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(289, 133);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(16, 13);
            this.label27.TabIndex = 7;
            this.label27.Text = "---";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(289, 120);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(16, 13);
            this.label26.TabIndex = 6;
            this.label26.Text = "---";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(180, 146);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(101, 13);
            this.label25.TabIndex = 5;
            this.label25.Text = "Satelites not USED:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(180, 133);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(83, 13);
            this.label24.TabIndex = 4;
            this.label24.Text = "Satelites USED:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(180, 120);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(92, 13);
            this.label23.TabIndex = 3;
            this.label23.Text = "Satelites in VIEW:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 3);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 13);
            this.label22.TabIndex = 2;
            this.label22.Text = "GGA (fixed data):";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(172, 97);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(115, 13);
            this.label21.TabIndex = 1;
            this.label21.Text = "GSV (satelites in view):";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(3, 96);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(160, 13);
            this.label20.TabIndex = 0;
            this.label20.Text = "GSA (DOP and active satellites):";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.textBox5);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.textBox4);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.textBox3);
            this.tabPage4.Controls.Add(this.textBox1);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.textBox2);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.btnDiag);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(330, 574);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Diagnostic";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(236, 99);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 13);
            this.label14.TabIndex = 50;
            this.label14.Text = "Heading [⁰]:";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(238, 115);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(44, 20);
            this.textBox5.TabIndex = 49;
            this.textBox5.Text = "---";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(236, 58);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 13);
            this.label13.TabIndex = 48;
            this.label13.Text = "Speed [km/h]:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(238, 76);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(44, 20);
            this.textBox4.TabIndex = 47;
            this.textBox4.Text = "---";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "Raining:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(8, 155);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(56, 20);
            this.textBox3.TabIndex = 45;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 76);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(56, 20);
            this.textBox1.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "Temp [⁰C]:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(8, 115);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(56, 20);
            this.textBox2.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "Battery [V]:";
            // 
            // btnDiag
            // 
            this.btnDiag.BackgroundImage = global::Map_v1.Properties.Resources.flight_data;
            this.btnDiag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDiag.Location = new System.Drawing.Point(6, 6);
            this.btnDiag.Name = "btnDiag";
            this.btnDiag.Size = new System.Drawing.Size(46, 48);
            this.btnDiag.TabIndex = 40;
            this.btnDiag.Tag = "Routing";
            this.btnDiag.UseVisualStyleBackColor = true;
            // 
            // map
            // 
            this.map.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.map.Bearing = 0F;
            this.map.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.map.CanDragMap = true;
            this.map.EmptyTileColor = System.Drawing.Color.Navy;
            this.map.GrayScaleMode = false;
            this.map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.map.LevelsKeepInMemmory = 5;
            this.map.Location = new System.Drawing.Point(0, 0);
            this.map.MarkersEnabled = true;
            this.map.MaxZoom = 2;
            this.map.MinZoom = 2;
            this.map.MouseWheelZoomEnabled = true;
            this.map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.map.Name = "map";
            this.map.NegativeMode = false;
            this.map.PolygonsEnabled = true;
            this.map.RetryLoadTile = 0;
            this.map.RoutesEnabled = true;
            this.map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.map.ShowTileGridLines = false;
            this.map.Size = new System.Drawing.Size(567, 600);
            this.map.TabIndex = 13;
            this.map.Zoom = 0D;
            this.map.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.map_OnMarkerEnter);
            this.map.OnMarkerLeave += new GMap.NET.WindowsForms.MarkerLeave(this.map_OnMarkerLeave);
            this.map.MouseClick += new System.Windows.Forms.MouseEventHandler(this.map_MouseClick);
            this.map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.map_MouseDown);
            this.map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.map_MouseMove);
            this.map.MouseUp += new System.Windows.Forms.MouseEventHandler(this.map_MouseUp);
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM15";
            // 
            // waypointsTableAdapter
            // 
            this.waypointsTableAdapter.ClearBeforeFill = true;
            // 
            // timerRover
            // 
            this.timerRover.Interval = 10;
            this.timerRover.Tick += new System.EventHandler(this.timerRover_Tick);
            // 
            // btnClearListbox
            // 
            this.btnClearListbox.Location = new System.Drawing.Point(184, 496);
            this.btnClearListbox.Name = "btnClearListbox";
            this.btnClearListbox.Size = new System.Drawing.Size(75, 23);
            this.btnClearListbox.TabIndex = 54;
            this.btnClearListbox.Text = "Clear";
            this.btnClearListbox.UseVisualStyleBackColor = true;
            this.btnClearListbox.Click += new System.EventHandler(this.btnClearListbox_Click);
            // 
            // btnPutRoverMap
            // 
            this.btnPutRoverMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPutRoverMap.Location = new System.Drawing.Point(8, 179);
            this.btnPutRoverMap.Name = "btnPutRoverMap";
            this.btnPutRoverMap.Size = new System.Drawing.Size(143, 38);
            this.btnPutRoverMap.TabIndex = 55;
            this.btnPutRoverMap.Text = "Put the rover on the Map";
            this.btnPutRoverMap.UseVisualStyleBackColor = false;
            this.btnPutRoverMap.Click += new System.EventHandler(this.btnPutRoverMap_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 610);
            this.Controls.Add(this.map);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Mission Planer";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waypointsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.farmerDataSet)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtLat;
        private System.Windows.Forms.TextBox txtLong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoadIntoMap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStartPoint;
        private System.Windows.Forms.ToolTip myTooltip;
        private System.Windows.Forms.Button btnPoligon;
        private System.Windows.Forms.Button btnRoute;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private GMap.NET.WindowsForms.GMapControl map;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button bthLeft;
        private System.Windows.Forms.Button btnBackward;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comPorts;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.ComboBox cmbMapTypes;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRemoveMarker;
        private System.Windows.Forms.Button btnConnectMarker;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.CheckBox chkStart;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbAreaUnit;
        private System.Windows.Forms.Button btnClearMap;
        private System.Windows.Forms.DataGridView dataGridViewPlan;
        private FarmerDataSet farmerDataSet;
        private System.Windows.Forms.BindingSource waypointsBindingSource;
        private FarmerDataSetTableAdapters.waypointsTableAdapter waypointsTableAdapter;
        private System.Windows.Forms.Button btnLoadTable;
        private System.Windows.Forms.TextBox txtXstep;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtYstep;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblCheckConneton;
        private System.Windows.Forms.Button btnSaveTable;
        private System.Windows.Forms.Button btnSaveTableTo;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ComboBox cmbRoutes;
        private System.Windows.Forms.Button btnLoadRoute;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSendMessage;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.ListBox lbRoverMsg;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtRoverRemLng;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtRoverRemLat;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Timer timerRover;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDiag;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn snrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn latDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lngDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnMeasureDistance;
        private System.Windows.Forms.TextBox txtRoverLat;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox txtRoverLng;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txtDistanceM;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox txtDistanceKm;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txtHeading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRoverRemHead;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txtWheelTurnNo;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Button btnClearListbox;
        private System.Windows.Forms.Button btnPutRoverMap;
    }
}

