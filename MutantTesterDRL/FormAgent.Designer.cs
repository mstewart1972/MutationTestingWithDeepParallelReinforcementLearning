namespace DeepQLearning
{
    partial class FormAgent
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.startLearning = new System.Windows.Forms.Button();
            this.displayBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtMinFactor = new System.Windows.Forms.TextBox();
            this.txtMaxFactor = new System.Windows.Forms.TextBox();
            this.txtRestartLoss = new System.Windows.Forms.TextBox();
            this.txtRestartInterval = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtSolution = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtActions = new System.Windows.Forms.TextBox();
            this.txtOperators = new System.Windows.Forms.TextBox();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.RestartLearning = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLearnBurn = new System.Windows.Forms.TextBox();
            this.txtLearnTotal = new System.Windows.Forms.TextBox();
            this.chkCharts = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLearningL2Decay = new System.Windows.Forms.TextBox();
            this.txtLearningL1Decay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLearningMomentum = new System.Windows.Forms.TextBox();
            this.txtLearningRate = new System.Windows.Forms.TextBox();
            this.cboLearningMethod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLearningBatch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPunish = new System.Windows.Forms.TextBox();
            this.txtReward = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumberItems = new System.Windows.Forms.TextBox();
            this.chkObstructItems = new System.Windows.Forms.CheckBox();
            this.chkRandomItems = new System.Windows.Forms.CheckBox();
            this.chkSharedExperience = new System.Windows.Forms.CheckBox();
            this.loadNet = new System.Windows.Forms.Button();
            this.saveNet = new System.Windows.Forms.Button();
            this.PauseBtn = new System.Windows.Forms.Button();
            this.goSlow = new System.Windows.Forms.Button();
            this.goNormal = new System.Windows.Forms.Button();
            this.goFast = new System.Windows.Forms.Button();
            this.goVeryFast = new System.Windows.Forms.Button();
            this.StopLearning = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.canvas = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.canvas.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // startLearning
            // 
            this.startLearning.Location = new System.Drawing.Point(7, 17);
            this.startLearning.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.startLearning.Name = "startLearning";
            this.startLearning.Size = new System.Drawing.Size(38, 22);
            this.startLearning.TabIndex = 0;
            this.startLearning.Text = "Start";
            this.startLearning.UseVisualStyleBackColor = true;
            this.startLearning.Click += new System.EventHandler(this.startLearning_Click);
            // 
            // displayBox
            // 
            this.displayBox.Location = new System.Drawing.Point(0, 0);
            this.displayBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.displayBox.Multiline = true;
            this.displayBox.Name = "displayBox";
            this.displayBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.displayBox.Size = new System.Drawing.Size(682, 540);
            this.displayBox.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.txtMinFactor);
            this.groupBox1.Controls.Add(this.txtMaxFactor);
            this.groupBox1.Controls.Add(this.txtRestartLoss);
            this.groupBox1.Controls.Add(this.txtRestartInterval);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txtSource);
            this.groupBox1.Controls.Add(this.txtSolution);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtActions);
            this.groupBox1.Controls.Add(this.txtOperators);
            this.groupBox1.Controls.Add(this.txtCategory);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.RestartLearning);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtLearnBurn);
            this.groupBox1.Controls.Add(this.txtLearnTotal);
            this.groupBox1.Controls.Add(this.chkCharts);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtLearningL2Decay);
            this.groupBox1.Controls.Add(this.txtLearningL1Decay);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtLearningMomentum);
            this.groupBox1.Controls.Add(this.txtLearningRate);
            this.groupBox1.Controls.Add(this.cboLearningMethod);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtLearningBatch);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPunish);
            this.groupBox1.Controls.Add(this.txtReward);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNumberItems);
            this.groupBox1.Controls.Add(this.chkObstructItems);
            this.groupBox1.Controls.Add(this.chkRandomItems);
            this.groupBox1.Controls.Add(this.chkSharedExperience);
            this.groupBox1.Controls.Add(this.loadNet);
            this.groupBox1.Controls.Add(this.saveNet);
            this.groupBox1.Controls.Add(this.PauseBtn);
            this.groupBox1.Controls.Add(this.goSlow);
            this.groupBox1.Controls.Add(this.goNormal);
            this.groupBox1.Controls.Add(this.goFast);
            this.groupBox1.Controls.Add(this.goVeryFast);
            this.groupBox1.Controls.Add(this.StopLearning);
            this.groupBox1.Controls.Add(this.startLearning);
            this.groupBox1.Location = new System.Drawing.Point(9, 571);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(1156, 73);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(485, 17);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(60, 13);
            this.label21.TabIndex = 50;
            this.label21.Text = "Max Factor";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(487, 35);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(57, 13);
            this.label22.TabIndex = 49;
            this.label22.Text = "Min Factor";
            // 
            // txtMinFactor
            // 
            this.txtMinFactor.Location = new System.Drawing.Point(547, 33);
            this.txtMinFactor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMinFactor.Name = "txtMinFactor";
            this.txtMinFactor.Size = new System.Drawing.Size(35, 20);
            this.txtMinFactor.TabIndex = 48;
            // 
            // txtMaxFactor
            // 
            this.txtMaxFactor.Location = new System.Drawing.Point(548, 11);
            this.txtMaxFactor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMaxFactor.Name = "txtMaxFactor";
            this.txtMaxFactor.Size = new System.Drawing.Size(34, 20);
            this.txtMaxFactor.TabIndex = 47;
            // 
            // txtRestartLoss
            // 
            this.txtRestartLoss.Location = new System.Drawing.Point(448, 32);
            this.txtRestartLoss.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRestartLoss.Name = "txtRestartLoss";
            this.txtRestartLoss.Size = new System.Drawing.Size(36, 20);
            this.txtRestartLoss.TabIndex = 46;
            // 
            // txtRestartInterval
            // 
            this.txtRestartInterval.Location = new System.Drawing.Point(345, 32);
            this.txtRestartInterval.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRestartInterval.Name = "txtRestartInterval";
            this.txtRestartInterval.Size = new System.Drawing.Size(32, 20);
            this.txtRestartInterval.TabIndex = 45;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(379, 35);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(66, 13);
            this.label19.TabIndex = 44;
            this.label19.Text = "Restart Loss";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(264, 33);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(79, 13);
            this.label20.TabIndex = 43;
            this.label20.Text = "Restart Interval";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(1037, 53);
            this.txtSource.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(111, 20);
            this.txtSource.TabIndex = 42;
            // 
            // txtSolution
            // 
            this.txtSolution.Location = new System.Drawing.Point(698, 54);
            this.txtSolution.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSolution.Name = "txtSolution";
            this.txtSolution.Size = new System.Drawing.Size(246, 20);
            this.txtSolution.TabIndex = 41;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(951, 58);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(85, 13);
            this.label18.TabIndex = 40;
            this.label18.Text = "Mutation Source";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(608, 58);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(89, 13);
            this.label17.TabIndex = 39;
            this.label17.Text = "Mutation Solution";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(446, 56);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(42, 13);
            this.label16.TabIndex = 38;
            this.label16.Text = "Actions";
            // 
            // txtActions
            // 
            this.txtActions.Location = new System.Drawing.Point(488, 51);
            this.txtActions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtActions.Name = "txtActions";
            this.txtActions.Size = new System.Drawing.Size(116, 20);
            this.txtActions.TabIndex = 37;
            // 
            // txtOperators
            // 
            this.txtOperators.Location = new System.Drawing.Point(413, 52);
            this.txtOperators.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtOperators.Name = "txtOperators";
            this.txtOperators.Size = new System.Drawing.Size(32, 20);
            this.txtOperators.TabIndex = 36;
            // 
            // txtCategory
            // 
            this.txtCategory.Location = new System.Drawing.Point(322, 53);
            this.txtCategory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(32, 20);
            this.txtCategory.TabIndex = 35;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(356, 55);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 13);
            this.label15.TabIndex = 34;
            this.label15.Text = "Operators";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(226, 54);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 13);
            this.label14.TabIndex = 33;
            this.label14.Text = "Mutation Category";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(268, 11);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 13);
            this.label13.TabIndex = 32;
            this.label13.Text = "Learning Method";
            // 
            // RestartLearning
            // 
            this.RestartLearning.Location = new System.Drawing.Point(218, 17);
            this.RestartLearning.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RestartLearning.Name = "RestartLearning";
            this.RestartLearning.Size = new System.Drawing.Size(43, 22);
            this.RestartLearning.TabIndex = 9;
            this.RestartLearning.Text = "Reset";
            this.RestartLearning.UseVisualStyleBackColor = true;
            this.RestartLearning.Click += new System.EventHandler(this.RestartLearning_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(745, 13);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Total";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(746, 35);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Burn";
            // 
            // txtLearnBurn
            // 
            this.txtLearnBurn.Location = new System.Drawing.Point(778, 33);
            this.txtLearnBurn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtLearnBurn.Name = "txtLearnBurn";
            this.txtLearnBurn.Size = new System.Drawing.Size(32, 20);
            this.txtLearnBurn.TabIndex = 29;
            // 
            // txtLearnTotal
            // 
            this.txtLearnTotal.Location = new System.Drawing.Point(778, 11);
            this.txtLearnTotal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtLearnTotal.Name = "txtLearnTotal";
            this.txtLearnTotal.Size = new System.Drawing.Size(62, 20);
            this.txtLearnTotal.TabIndex = 28;
            // 
            // chkCharts
            // 
            this.chkCharts.AutoSize = true;
            this.chkCharts.Checked = true;
            this.chkCharts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCharts.Location = new System.Drawing.Point(1010, 14);
            this.chkCharts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkCharts.Name = "chkCharts";
            this.chkCharts.Size = new System.Drawing.Size(56, 17);
            this.chkCharts.TabIndex = 27;
            this.chkCharts.Text = "Charts";
            this.chkCharts.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(914, 14);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "L1 decay";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(913, 36);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "L2 decay";
            // 
            // txtLearningL2Decay
            // 
            this.txtLearningL2Decay.Location = new System.Drawing.Point(965, 34);
            this.txtLearningL2Decay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtLearningL2Decay.Name = "txtLearningL2Decay";
            this.txtLearningL2Decay.Size = new System.Drawing.Size(32, 20);
            this.txtLearningL2Decay.TabIndex = 24;
            // 
            // txtLearningL1Decay
            // 
            this.txtLearningL1Decay.Location = new System.Drawing.Point(966, 11);
            this.txtLearningL1Decay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtLearningL1Decay.Name = "txtLearningL1Decay";
            this.txtLearningL1Decay.Size = new System.Drawing.Size(32, 20);
            this.txtLearningL1Decay.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(844, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Rate";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(814, 37);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Momentum";
            // 
            // txtLearningMomentum
            // 
            this.txtLearningMomentum.Location = new System.Drawing.Point(877, 35);
            this.txtLearningMomentum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtLearningMomentum.Name = "txtLearningMomentum";
            this.txtLearningMomentum.Size = new System.Drawing.Size(32, 20);
            this.txtLearningMomentum.TabIndex = 20;
            // 
            // txtLearningRate
            // 
            this.txtLearningRate.Location = new System.Drawing.Point(877, 12);
            this.txtLearningRate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtLearningRate.Name = "txtLearningRate";
            this.txtLearningRate.Size = new System.Drawing.Size(32, 20);
            this.txtLearningRate.TabIndex = 19;
            // 
            // cboLearningMethod
            // 
            this.cboLearningMethod.FormattingEnabled = true;
            this.cboLearningMethod.Items.AddRange(new object[] {
            "adam",
            "adagrad",
            "windowgrad",
            "adadelta",
            "nesterov",
            "SGD"});
            this.cboLearningMethod.Location = new System.Drawing.Point(358, 11);
            this.cboLearningMethod.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboLearningMethod.Name = "cboLearningMethod";
            this.cboLearningMethod.Size = new System.Drawing.Size(115, 21);
            this.cboLearningMethod.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(660, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Batch";
            // 
            // txtLearningBatch
            // 
            this.txtLearningBatch.Location = new System.Drawing.Point(695, 11);
            this.txtLearningBatch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtLearningBatch.Name = "txtLearningBatch";
            this.txtLearningBatch.Size = new System.Drawing.Size(48, 20);
            this.txtLearningBatch.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(580, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Reward";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(581, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Punish";
            // 
            // txtPunish
            // 
            this.txtPunish.Location = new System.Drawing.Point(624, 34);
            this.txtPunish.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPunish.Name = "txtPunish";
            this.txtPunish.Size = new System.Drawing.Size(32, 20);
            this.txtPunish.TabIndex = 13;
            // 
            // txtReward
            // 
            this.txtReward.Location = new System.Drawing.Point(624, 11);
            this.txtReward.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtReward.Name = "txtReward";
            this.txtReward.Size = new System.Drawing.Size(32, 20);
            this.txtReward.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(662, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Items";
            // 
            // txtNumberItems
            // 
            this.txtNumberItems.Location = new System.Drawing.Point(695, 34);
            this.txtNumberItems.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNumberItems.Name = "txtNumberItems";
            this.txtNumberItems.Size = new System.Drawing.Size(48, 20);
            this.txtNumberItems.TabIndex = 11;
            // 
            // chkObstructItems
            // 
            this.chkObstructItems.AutoSize = true;
            this.chkObstructItems.Location = new System.Drawing.Point(1073, 12);
            this.chkObstructItems.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkObstructItems.Name = "chkObstructItems";
            this.chkObstructItems.Size = new System.Drawing.Size(66, 17);
            this.chkObstructItems.TabIndex = 10;
            this.chkObstructItems.Text = "Obstruct";
            this.chkObstructItems.UseVisualStyleBackColor = true;
            // 
            // chkRandomItems
            // 
            this.chkRandomItems.AutoSize = true;
            this.chkRandomItems.Location = new System.Drawing.Point(1073, 34);
            this.chkRandomItems.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkRandomItems.Name = "chkRandomItems";
            this.chkRandomItems.Size = new System.Drawing.Size(66, 17);
            this.chkRandomItems.TabIndex = 0;
            this.chkRandomItems.Text = "Random";
            this.chkRandomItems.UseVisualStyleBackColor = true;
            // 
            // chkSharedExperience
            // 
            this.chkSharedExperience.AutoSize = true;
            this.chkSharedExperience.Checked = true;
            this.chkSharedExperience.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSharedExperience.Location = new System.Drawing.Point(1010, 34);
            this.chkSharedExperience.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkSharedExperience.Name = "chkSharedExperience";
            this.chkSharedExperience.Size = new System.Drawing.Size(60, 17);
            this.chkSharedExperience.TabIndex = 9;
            this.chkSharedExperience.Text = "Shared";
            this.chkSharedExperience.UseVisualStyleBackColor = true;
            // 
            // loadNet
            // 
            this.loadNet.Location = new System.Drawing.Point(177, 17);
            this.loadNet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.loadNet.Name = "loadNet";
            this.loadNet.Size = new System.Drawing.Size(40, 22);
            this.loadNet.TabIndex = 8;
            this.loadNet.Text = "Load";
            this.loadNet.UseVisualStyleBackColor = true;
            this.loadNet.Click += new System.EventHandler(this.loadNet_Click);
            // 
            // saveNet
            // 
            this.saveNet.Location = new System.Drawing.Point(132, 17);
            this.saveNet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.saveNet.Name = "saveNet";
            this.saveNet.Size = new System.Drawing.Size(45, 22);
            this.saveNet.TabIndex = 7;
            this.saveNet.Text = "Save";
            this.saveNet.UseVisualStyleBackColor = true;
            this.saveNet.Click += new System.EventHandler(this.saveNet_Click);
            // 
            // PauseBtn
            // 
            this.PauseBtn.Location = new System.Drawing.Point(44, 17);
            this.PauseBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PauseBtn.Name = "PauseBtn";
            this.PauseBtn.Size = new System.Drawing.Size(49, 22);
            this.PauseBtn.TabIndex = 6;
            this.PauseBtn.Text = "Pause";
            this.PauseBtn.UseVisualStyleBackColor = true;
            this.PauseBtn.Click += new System.EventHandler(this.PauseBtn_Click);
            // 
            // goSlow
            // 
            this.goSlow.Location = new System.Drawing.Point(170, 42);
            this.goSlow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.goSlow.Name = "goSlow";
            this.goSlow.Size = new System.Drawing.Size(48, 22);
            this.goSlow.TabIndex = 5;
            this.goSlow.Text = "Slow";
            this.goSlow.UseVisualStyleBackColor = true;
            this.goSlow.Click += new System.EventHandler(this.goSlow_Click);
            // 
            // goNormal
            // 
            this.goNormal.Location = new System.Drawing.Point(118, 42);
            this.goNormal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.goNormal.Name = "goNormal";
            this.goNormal.Size = new System.Drawing.Size(48, 22);
            this.goNormal.TabIndex = 4;
            this.goNormal.Text = "Normal";
            this.goNormal.UseVisualStyleBackColor = true;
            this.goNormal.Click += new System.EventHandler(this.goNormal_Click);
            // 
            // goFast
            // 
            this.goFast.Location = new System.Drawing.Point(65, 42);
            this.goFast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.goFast.Name = "goFast";
            this.goFast.Size = new System.Drawing.Size(48, 22);
            this.goFast.TabIndex = 3;
            this.goFast.Text = "Fast";
            this.goFast.UseVisualStyleBackColor = true;
            this.goFast.Click += new System.EventHandler(this.goFast_Click);
            // 
            // goVeryFast
            // 
            this.goVeryFast.Location = new System.Drawing.Point(7, 42);
            this.goVeryFast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.goVeryFast.Name = "goVeryFast";
            this.goVeryFast.Size = new System.Drawing.Size(54, 22);
            this.goVeryFast.TabIndex = 2;
            this.goVeryFast.Text = "Fastest";
            this.goVeryFast.UseVisualStyleBackColor = true;
            this.goVeryFast.Click += new System.EventHandler(this.goVeryFast_Click);
            // 
            // StopLearning
            // 
            this.StopLearning.Location = new System.Drawing.Point(92, 17);
            this.StopLearning.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StopLearning.Name = "StopLearning";
            this.StopLearning.Size = new System.Drawing.Size(40, 22);
            this.StopLearning.TabIndex = 1;
            this.StopLearning.Text = "Stop";
            this.StopLearning.UseVisualStyleBackColor = true;
            this.StopLearning.Click += new System.EventHandler(this.StopLearning_Click);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(6, 17);
            this.chart1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(427, 278);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "chart1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.canvas);
            this.groupBox2.Location = new System.Drawing.Point(9, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(704, 557);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Visualization";
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.Info;
            this.canvas.Controls.Add(this.displayBox);
            this.canvas.Location = new System.Drawing.Point(22, 17);
            this.canvas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(692, 540);
            this.canvas.TabIndex = 0;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chart2);
            this.groupBox3.Controls.Add(this.chart1);
            this.groupBox3.Location = new System.Drawing.Point(717, 10);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(457, 557);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output";
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(4, 300);
            this.chart2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chart2.Name = "chart2";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(427, 257);
            this.chart2.TabIndex = 5;
            this.chart2.Text = "chart2";
            // 
            // FormAgent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 663);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "FormAgent";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mutation Testing Deep Q Learning Agent";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAgent_FormClosed);
            this.Load += new System.EventHandler(this.FormAgent_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.canvas.ResumeLayout(false);
            this.canvas.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox displayBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.Button startLearning;
        internal System.Windows.Forms.Button StopLearning;
        internal System.Windows.Forms.Button PauseBtn;
        internal System.Windows.Forms.Button goSlow;
        internal System.Windows.Forms.Button goNormal;
        internal System.Windows.Forms.Button goFast;
        internal System.Windows.Forms.Button goVeryFast;
        internal System.Windows.Forms.Button loadNet;
        internal System.Windows.Forms.Button saveNet;
        internal System.Windows.Forms.CheckBox chkSharedExperience;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        internal System.Windows.Forms.CheckBox chkRandomItems;
        internal System.Windows.Forms.Panel canvas;
        internal System.Windows.Forms.CheckBox chkObstructItems;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtNumberItems;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtPunish;
        internal System.Windows.Forms.TextBox txtReward;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtLearningBatch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txtLearningMomentum;
        internal System.Windows.Forms.TextBox txtLearningRate;
        internal System.Windows.Forms.ComboBox cboLearningMethod;
        internal System.Windows.Forms.CheckBox chkCharts;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.TextBox txtLearningL2Decay;
        internal System.Windows.Forms.TextBox txtLearningL1Decay;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.TextBox txtLearnBurn;
        internal System.Windows.Forms.TextBox txtLearnTotal;
        internal System.Windows.Forms.Button RestartLearning;
        internal System.Windows.Forms.TextBox txtOperators;
        internal System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        internal System.Windows.Forms.TextBox txtActions;
        internal System.Windows.Forms.TextBox txtSource;
        internal System.Windows.Forms.TextBox txtSolution;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        internal System.Windows.Forms.TextBox txtRestartLoss;
        internal System.Windows.Forms.TextBox txtRestartInterval;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        internal System.Windows.Forms.TextBox txtMinFactor;
        internal System.Windows.Forms.TextBox txtMaxFactor;
    }
}

