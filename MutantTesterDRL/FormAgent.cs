using ConvnetSharp;
using DeepQLearning.DRLAgent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DeepQLearning
{
    public partial class FormAgent : Form
    {
        Pen blackPen = new Pen(Color.Black);
        Pen greenPen = new Pen(Color.LightGreen, 5);

        // worker thread
        private Thread workerThread = null;

        Boolean needToStop = false, paused = false, needToStart = false, infiniteItems = false, staticExperience = false;
        internal QAgent qAgent;

        int interval = 30;

        string instanceNumber = "";

        int experiencesize = 0;
        int learnthreshold = 0;
        int duration = 0;

        // file
        string netFile = "";

        // charts
        Series series1a, series1b, series2, series2a, series2b, series2c;


        public FormAgent(string[] args)
        {
            InitializeComponent();

            // initialize form
            initializeForm(args);

            // initialize charts
            if (chkCharts.Checked) initializeCharts();

            // Fix Panel double buffering issue
            typeof(Panel).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, canvas, new object[] { true });
        }

        private void initializeForm(string[] args)
        {
            // set defaults
            this.Text += " (Instance " + args[0] + ")";
            SetInstance(args[0]);
            SetExperience(Convert.ToInt32(args[1]));
            SetLearnThreshold(Convert.ToInt32(args[2]));
            cboLearningMethod.SelectedItem = args[3];
            txtLearningRate.Text = args[4];
            txtLearningMomentum.Text = args[5];
            txtLearningBatch.Text = args[6];
            txtLearnTotal.Text = args[7];
            txtLearnBurn.Text = args[8];
            txtLearningL1Decay.Text = args[9];            // 1000-subToMul=kill, 0100-subToAdd=kill, 0010-subToDiv=kill, 0001-subToRem=live
            txtLearningL2Decay.Text = args[10];
            txtNumberItems.Text = args[11];
            txtOperators.Text = args[11];
            txtActions.Text = ((long)Math.Pow(2, Int64.Parse(txtNumberItems.Text))).ToString();
            txtReward.Text = args[12];
            txtPunish.Text = args[13];
            chkSharedExperience.Checked = bool.Parse(args[14]);
            chkObstructItems.Checked = bool.Parse(args[15]);
            chkRandomItems.Checked = bool.Parse(args[16]);
            SetInfiniteItems(bool.Parse(args[17]));
            SetStaticExperience(bool.Parse(args[18]));
            canvas.Width = Int32.Parse(args[19]);
            canvas.Height = Int32.Parse(args[20]);
            duration = Int32.Parse(args[21]);
            txtCategory.Text = args[22];
            txtSolution.Text = args[23];
            txtSource.Text = args[24];
            txtRestartInterval.Text = args[25];
            txtRestartLoss.Text = args[26];
            txtMaxFactor.Text = args[27];
            txtMinFactor.Text = args[28];

            // start processing
            if (bool.Parse(args[29])) needToStart = true;
        }

        private void FormAgent_Load(object sender, EventArgs e)
        {
            if (needToStart) startLearn(false);
        }

        public void SetInstance(string inst)
        {
            instanceNumber = inst;
            netFile = Environment.CurrentDirectory + "\\deepQnet" + instanceNumber + ".dat";
        }

        public void SetExperience(int size)
        {
            experiencesize = size;
        }

        public void SetLearnThreshold(int size)
        {
            learnthreshold = size;
        }

        public void SetInfiniteItems(bool val)
        {
            infiniteItems = val;
        }

        public void SetStaticExperience(bool val)
        {
            staticExperience = val;
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            if (qAgent != null) {
                displayBox.Text = qAgent.draw_world(e.Graphics);

                // display category operations
                switch (qAgent.category)
                {
                    case "BA":
                        displayBox.Text += Environment.NewLine + "Mutation Operations: 0000-no mutation, 1000-addToMul, 0100-addToSub, 0010-addToDiv, 0001-addToRem";
                        break;

                    case "BS":
                        displayBox.Text += Environment.NewLine + "Mutation Operations: 0000-no mutation, 1000-subToMul, 0100-subToAdd, 0010-subToDiv, 0001-subToRem";
                        break;

                    case "BM":
                        displayBox.Text += Environment.NewLine + "Mutation Operations: 0000-no mutation, 1000-mulToAdd, 0100-mulToSub, 0010-mulToDiv, 0001-mulToRem";
                        break;

                    case "BD":
                        displayBox.Text += Environment.NewLine + "Mutation Operations: 0000-no mutation, 1000-divToAdd, 0100-divToSub, 0010-divToMul, 0001-divToRem";
                        break;

                    case "BO":
                        displayBox.Text += Environment.NewLine + "Mutation Operations: 0000-no mutation, 1000-remToAdd, 0100-remToSub, 0010-remToDiv, 0001-remToMul";
                        break;
                }

                // display speed
                switch (qAgent.simspeed)
                {
                    case 0:
                        displayBox.Text += Environment.NewLine + "Simulation speed: Slow";
                        break;

                    case 1:
                        displayBox.Text += Environment.NewLine + "Simulation speed: Normal";
                        break;

                    case 2:
                        displayBox.Text += Environment.NewLine + "Simulation speed: Fast";
                        break;

                    case 3:
                        displayBox.Text += Environment.NewLine + "Simulation speed: Very Fast";
                        break;
                }


            }

            canvas.Update();
            // refresh charts
            if (chkCharts.Checked) updateCharts();

            // pause process if duration reached
            if (needToStart && duration != 0 && qAgent != null && qAgent.getTickCount()>=duration)
                PauseBtn.PerformClick();

            // Object Agent criteria: reset process if average Q-learning loss is >=0.10 at check every 150000 intervals
            //if (qAgent != null && (qAgent.getTickCount() % 15000)==0 && qAgent.getAvgLoss() >= 0.10)

            // Mutation Agent criteria: reset process if average Q-learning loss is >=0.25 at check every 100 intervals
            //if (qAgent != null && (qAgent.getTickCount() % 100)==0 && qAgent.getAvgLoss() >= 0.35)
            if (qAgent != null && (qAgent.getTickCount() % Int32.Parse(txtRestartInterval.Text)) == 0 && qAgent.getAvgLoss() >= Double.Parse(txtRestartLoss.Text))
                RestartLearning.PerformClick();
        }

        #region // Button Controls
        private void StopLearning_Click(object sender, EventArgs e)
        {
            qAgent.stoplearn();
        }

        private void startLearning_Click(object sender, EventArgs e)
        {
            chkCharts.Checked = true;
            startLearn(false);
        }

        private void startLearn(bool delay)
        {
            if (qAgent == null)
            {
                //var num_inputs = 27; // 9 eyes, each sees 3 numbers (wall, green, red thing proximity)
                //var num_actions = 5; // 5 possible angles agent can turn
                var num_inputs = 3; // mutation metrics (class name word2vec, # lines, # methods) 
                var num_actions = Int32.Parse(txtActions.Text); // mutation operation combinations TODO: needs to be dynamic from agent
                var temporal_window = 4; // amount of temporal memory. 0 = agent lives in-the-moment :)
                var network_size = num_inputs * temporal_window + num_actions * temporal_window + num_inputs;

                var layer_defs = new List<LayerDefinition>();

                // the value function network computes a value of taking any of the possible actions
                // given an input state. Here we specify one explicitly the hard way
                // but user could also equivalently instead use opt.hidden_layer_sizes = [20,20]
                // to just insert simple relu hidden layers.
                layer_defs.Add(new LayerDefinition { type = "input", out_sx = 1, out_sy = 1, out_depth = network_size });
                layer_defs.Add(new LayerDefinition { type = "fc", num_neurons = 96, activation = "relu" });
                layer_defs.Add(new LayerDefinition { type = "fc", num_neurons = 96, activation = "relu" });
                layer_defs.Add(new LayerDefinition { type = "fc", num_neurons = 96, activation = "relu" });
                layer_defs.Add(new LayerDefinition { type = "regression", num_neurons = num_actions });

                // options for the Temporal Difference learner that trains the above net
                // by backpropping the temporal difference learning rule.
                //var opt = new Options { method="sgd", learning_rate=0.01, l2_decay=0.001, momentum=0.9, batch_size=10, l1_decay=0.001 };
                //var opt = new Options { method = "adadelta", l2_decay = 0.001, batch_size = 10 };
                var opt = new Options { method = cboLearningMethod.Text, learning_rate=Double.Parse(txtLearningRate.Text), momentum= Double.Parse(txtLearningMomentum.Text), l1_decay = Double.Parse(txtLearningL1Decay.Text), l2_decay =  Double.Parse(txtLearningL2Decay.Text), batch_size = Int32.Parse(txtLearningBatch.Text) };

                var tdtrainer_options = new TrainingOptions();
                tdtrainer_options.temporal_window = temporal_window;
                //tdtrainer_options.experience_size = 30000;
                tdtrainer_options.experience_size = experiencesize;
                //tdtrainer_options.start_learn_threshold = 1000;
                tdtrainer_options.start_learn_threshold = learnthreshold;
                tdtrainer_options.gamma = 0.7;
                //tdtrainer_options.learning_steps_total = 200000;
                tdtrainer_options.learning_steps_total = Int32.Parse(txtLearnTotal.Text);
                //tdtrainer_options.learning_steps_burnin = 3000;
                tdtrainer_options.learning_steps_burnin = Int32.Parse(txtLearnBurn.Text);
                tdtrainer_options.epsilon_min = 0.05;
                tdtrainer_options.epsilon_test_time = 0.00;
                tdtrainer_options.layer_defs = layer_defs;
                tdtrainer_options.options = opt;

                // determine when to use shared experience using static
                if (chkSharedExperience.Checked && staticExperience)
                {
                    var brain = new DeepQLearnShared(num_inputs, num_actions, tdtrainer_options);
                    brain.instance = this.instanceNumber;
                    qAgent = new QAgent(brain, canvas.Width, canvas.Height, txtCategory.Text, Int32.Parse(txtNumberItems.Text), txtSolution.Text, txtSource.Text, Double.Parse(txtMaxFactor.Text), Double.Parse(txtMinFactor.Text), chkRandomItems.Checked, chkObstructItems.Checked, infiniteItems, instanceNumber);
                }
                // determine when to use shared experience using singleton
                else if (chkSharedExperience.Checked && !staticExperience)
                {
                    var brain = new DeepQLearnSharedSingleton(num_inputs, num_actions, tdtrainer_options);
                    brain.instance = this.instanceNumber;
                    qAgent = new QAgent(brain, canvas.Width, canvas.Height, txtCategory.Text, Int32.Parse(txtNumberItems.Text), txtSolution.Text, txtSource.Text, Double.Parse(txtMaxFactor.Text), Double.Parse(txtMinFactor.Text), chkRandomItems.Checked, chkObstructItems.Checked, infiniteItems, instanceNumber);
                }
                // determine when to use nonshared experience
                else
                {
                    var brain = new DeepQLearn(num_inputs, num_actions, tdtrainer_options);
                    qAgent = new QAgent(brain, canvas.Width, canvas.Height, txtCategory.Text, Int32.Parse(txtNumberItems.Text), txtSolution.Text, txtSource.Text, Double.Parse(txtMaxFactor.Text), Double.Parse(txtMinFactor.Text), chkRandomItems.Checked, chkObstructItems.Checked, infiniteItems, instanceNumber);
                }
            }
            else
                qAgent.startlearn();

            if (!delay)
            {
                qAgent.goveryfast();
                interval = 0;
            }

            if (workerThread == null)
            {
                workerThread = new Thread(new ThreadStart(BackgroundThread));
                workerThread.Start();
            }
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            if(paused)
            {
                PauseBtn.Text = "Pause";
                paused = false;
                chkCharts.Checked = true;
            }
            else
            {
                PauseBtn.Text = "Continue";
                paused = true;
                chkCharts.Checked = false;
            }
        }

        private void RestartLearning_Click(object sender, EventArgs e)
        {
            initializeCharts();
            qAgent =null;
            startLearn(false);
        }


        private void saveNet_Click(object sender, EventArgs e)
        {
            // Create the network file
            using (FileStream fstream = new FileStream(netFile, FileMode.Create))
            {
                // REDO: save singleton to instance *** ExperienceShared has to be annotated as [Serializable]
                if (chkSharedExperience.Checked && staticExperience) qAgent.w.agents[0].brain.experience = ((DeepQLearning.DRLAgent.DeepQLearnShared)qAgent.w.agents[0].brain).List();
                if (chkSharedExperience.Checked && !staticExperience) qAgent.w.agents[0].brain.experience = ((DeepQLearning.DRLAgent.DeepQLearnSharedSingleton)qAgent.w.agents[0].brain).List();

                // Save the network to file
                new BinaryFormatter().Serialize(fstream, qAgent);
                fstream.Close();
            }

            displayBox.Text = "QNetwork saved successfully";
        }

        private void loadNet_Click(object sender, EventArgs e)
        {
            // Open the network file
            using (FileStream fstream = new FileStream(netFile, FileMode.Open))
            {
                // Load the network from file
                qAgent = new BinaryFormatter().Deserialize(fstream) as QAgent;

                // REDO: load instance to singleton *** ExperienceShared has to be annotated as [Serializable]
                if (chkSharedExperience.Checked && staticExperience) ((DeepQLearning.DRLAgent.DeepQLearnShared)qAgent.w.agents[0].brain).Init(qAgent.w.agents[0].brain.experience);
                if (chkSharedExperience.Checked && !staticExperience) ((DeepQLearning.DRLAgent.DeepQLearnSharedSingleton)qAgent.w.agents[0].brain).Init(qAgent.w.agents[0].brain.experience);

                qAgent.Reinitialize();
            }

            if (workerThread == null)
            {
                workerThread = new Thread(new ThreadStart(BackgroundThread));
                workerThread.Start();
            }

            displayBox.Text = "QNetwork loaded successfully";
        }

        private void FormAgent_FormClosed(object sender, FormClosedEventArgs e)
        {
            needToStop = true;

            if (workerThread != null)
            {
                // stop worker thread
                needToStop = true;
                while (!workerThread.Join(100))
                    Application.DoEvents();
                workerThread = null;
            }
        }

        private void goNormal_Click(object sender, EventArgs e)
        {
            qAgent.gonormal();
            interval = 25;
        }

        private void goFast_Click(object sender, EventArgs e)
        {
            qAgent.gofast();
            interval = 10;
        }

        private void goVeryFast_Click(object sender, EventArgs e)
        {
            qAgent.goveryfast();
            interval = 0;
        }

        private void goSlow_Click(object sender, EventArgs e)
        {
            qAgent.goslow();
            interval = 50;
        }
        #endregion

        // Delegates to enable async calls for setting controls properties
        private delegate void UpdateUICallback(Panel panel);

        // Thread safe updating of UI
        private void UpdateUI(Panel panel)
        {
            if(needToStop)
                return;

            if (panel.InvokeRequired)
            {
                UpdateUICallback d = new UpdateUICallback(UpdateUI);
                Invoke(d, new object[] { panel });
            }
            else
            {
                panel.Refresh();
            }
        }

        private void BackgroundThread()
        {
            while (!needToStop)
            {
                if (!paused)
                {
                    // pause when all reward items have been found
                    //if (txtReward.Text!="" && txtPunish.Text!="" && qAgent.tick(Double.Parse(txtReward.Text), Double.Parse(txtPunish.Text)) == 1) paused = true;

                    qAgent.tick(Double.Parse(txtReward.Text), Double.Parse(txtPunish.Text));
                    UpdateUI(canvas);
                }

                Thread.Sleep(interval);
            }
        }

        private void initializeCharts()
        {
            // initialize charts
            this.chart1.Series.Clear();
            this.chart1.Titles.Add("Agent Accuracy Over Time");
            series1a = this.chart1.Series.Add("Average\\nReward");
            series1a.ChartType = SeriesChartType.Spline;
            series1b = this.chart1.Series.Add("Average\\nLoss");
            series1b.ChartType = SeriesChartType.Spline;
            this.chart2.Series.Clear();
            this.chart2.Titles.Add("Agent Completeness Over Time");
            //series2 = this.chart2.Series.Add("Processed\\nItem Count");
            //series2.ChartType = SeriesChartType.Spline;
            //series2.Color = Color.Orange;
            series2a = this.chart2.Series.Add("Total\\nMutants\\nCount");
            series2a.ChartType = SeriesChartType.Spline;
            series2b = this.chart2.Series.Add("Killed\\nMutants\\nCount");
            series2b.ChartType = SeriesChartType.Spline;
            series2c = this.chart2.Series.Add("Live\\nMutants\\nCount");
            series2c.ChartType = SeriesChartType.Spline;
        }

        private void updateCharts()
        {
            // sporadically refresh agent charts
            //if (qAgent != null && qAgent.getTickCount()%100==0)
            if (qAgent != null && qAgent.getTickCount() % 10 == 0)
            {
                var tc = qAgent.getTickCount();
                //chart #1
                var ar = qAgent.getAvgReward();
                var al = qAgent.getAvgLoss();
                if (ar > -1) series1a.Points.AddXY(tc, ar);
                if (al > -1) series1b.Points.AddXY(tc, al);
                // chart #2
                //var pic = qAgent.getProcessedItemCount();
                //series2.Points.AddXY(tc, pic);
                var tmc = qAgent.getTotalMutantsCount();
                var kmc = qAgent.getKilledMutantsCount();
                var lmc = qAgent.getLiveMutantsCount();
                if (tmc > -1) series2a.Points.AddXY(tc, tmc);
                if (kmc > -1) series2b.Points.AddXY(tc, kmc);
                if (lmc > -1) series2c.Points.AddXY(tc, lmc);
                // refresh charts
                chart1.Update();
                chart2.Update();
            }
        }

    }
}
