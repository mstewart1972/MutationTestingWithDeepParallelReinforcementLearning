using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Timers;
using System.Threading;
using System.Diagnostics;

namespace DeepQLearning
{
    public partial class FormDriver : Form
    {
        // define variables
        int numInstances = 0;
        FormAgent[] formarray;
        string[] args = new string[30];
        bool paused = false;
        Series series1a, series1b, series1at, series1bt, series2, series2a, series2b, series2c, series2at, series2bt, series2ct;

        public FormDriver()
        {
            InitializeComponent();
        }

        private void frmDriver_Load(object sender, EventArgs e)
        {
            // intialize form
            cboLearningMethod.SelectedItem = "adadelta";
            txtLearningRate.Text = "0.01";
            txtLearningMomentum.Text = "0.9";
            txtLearningBatch.Text = "10";
            txtLearnTotal.Text = "1000";
            txtLearnBurn.Text = "100";
            txtLearningL1Decay.Text = "0";
            txtLearningL2Decay.Text = "0.001";
            txtRestartInterval.Text = "100";
            txtRestartLoss.Text = "0.5";
            txtNumInstances.Text = "1";
            rdoThread.Checked = true;
            txtExperienceMax.Text = "3000";
            txtExperienceMin.Text = "100";
            chkSharedExperience.Checked = true;
            rdoStaticExperience.Checked = true;
            txtExplorationMax.Text = "1";
            txtExplorationMin.Text = "0.05";
            txtCanvasWidth.Text = "710";
            txtCanvasHeight.Text = "500";
            txtReward.Text = "5.0";
            txtPunish.Text = "-6.0";
            txtDuration.Text = "1500";
            txtMaxFactor.Text = "0.01";
            txtMinFactor.Text = "0.75";
            txtChartFrequency.Text = "10";
            btnCreate.Select();
            toolStripStatusLabel1.Text = "";
            chkObstructItems.Checked = false;
            chkRandomItems.Checked = false;
            chkInfiniteItems.Checked = false;

            // mutation defaults
            // load mutation operator category combobox
            var items = new[] {
            new { Text = "A - All Replacements", Value = "56" },
            new { Text = "B - Arithmetic Basic (AORB)", Value = "20" },
            new { Text = "BA- Basic Addition", Value = "4" },
            new { Text = "BS- Basic Subtraction", Value = "4" },
            new { Text = "BM- Basic Multiplication", Value = "4" },
            new { Text = "BD- Basic Division", Value = "4" },
            new { Text = "BO- Basic Modulo", Value = "4" }
            };
            cboMutationCategory.DataSource = items;
            cboMutationCategory.DisplayMember = "Text";
            cboMutationCategory.ValueMember = "Value";

            // set basic addition as the default for mutation operations
            cboMutationCategory.Text = "BA- Basic Addition";
            txtNumberItems.Text = "4";

            // set the default .NET solution and source for mutation testing
            txtSolution.Text = "C:\\data\\MyPhD\\doctoral-research\\winter2021\\code-MT\\source\\BasicMath2\\TestExample.sln";
            txtSource.Text = "ClassLibrary1.dll";

            // initialize timer to refresh charts every 10s
            timer1.Interval = Int32.Parse(txtChartFrequency.Text) * 1000;
        }

        private void txtNumInstances_TextChanged(object sender, EventArgs e)
        {
            if (txtNumInstances.Text != "" && Int32.Parse(txtNumInstances.Text) == 1)
                chkSharedExperience.Checked = false;
            else
                chkSharedExperience.Checked = true;
        }
        private void chkRandomItems_CheckedChanged(object sender, EventArgs e)
        {
            // toggle random items
            for (int i = 0; i < numInstances; i++)
            {
                formarray[i].chkRandomItems.Checked = chkRandomItems.Checked;
            }
        }

        private void chkObstruct_CheckedChanged(object sender, EventArgs e)
        {
            // toggle obstruct items
            for (int i = 0; i < numInstances; i++)
            {
                formarray[i].chkObstructItems.Checked = chkObstructItems.Checked;
            }
        }

        private void chkSharedExperience_CheckedChanged(object sender, EventArgs e)
        {
            // toggle shared experience
            for (int i = 0; i < numInstances; i++)
            {
                formarray[i].chkSharedExperience.Checked = chkSharedExperience.Checked;
            }
            // disable or enable experience types
            if (chkSharedExperience.Checked)
            {
                rdoStaticExperience.Enabled = true;
                rdoSingletonExperience.Enabled = true;
            }
            else
            {
                rdoStaticExperience.Enabled = false;
                rdoSingletonExperience.Enabled = false;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            // get input num of instances
            numInstances = Int32.Parse(txtNumInstances.Text);
            formarray = new FormAgent[numInstances];

            // create forms and threads or processes
            for (int i = 0; i < numInstances; i++)
            {
                args[0] = i.ToString();
                args[1] = txtExperienceMax.Text;
                args[2] = txtExperienceMin.Text;
                args[3] = cboLearningMethod.SelectedItem.ToString();
                args[4] = txtLearningRate.Text;
                args[5] = txtLearningMomentum.Text;
                args[6] = txtLearningBatch.Text;
                args[7] = txtLearnTotal.Text;
                args[8] = txtLearnBurn.Text;
                args[9] = txtLearningL1Decay.Text;
                args[10] = txtLearningL2Decay.Text;
                args[11] = txtNumberItems.Text;
                args[12] = txtReward.Text;
                args[13] = txtPunish.Text;
                args[14] = chkSharedExperience.Checked.ToString();
                args[15] = chkObstructItems.Checked.ToString();
                args[16] = chkRandomItems.Checked.ToString();
                args[17] = chkInfiniteItems.Checked.ToString();
                args[18] = rdoStaticExperience.Checked.ToString();
                args[19] = txtCanvasWidth.Text;
                args[20] = txtCanvasHeight.Text;
                args[21] = (txtDuration.Text!="") ? txtDuration.Text : "0";
                args[22] = cboMutationCategory.Text.Split('-')[0];
                args[23] = txtSolution.Text;
                args[24] = txtSource.Text;
                args[25] = txtRestartInterval.Text;
                args[26] = txtRestartLoss.Text;
                args[27] = txtMaxFactor.Text;
                args[28] = txtMinFactor.Text;


                // create thread
                if (rdoThread.Checked)
                {
                    args[29] = "false";
                    formarray[i] = new FormAgent(args);
                    formarray[i].Show();
                }

                // create process
                if (rdoProcess.Checked)
                {
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = "DeepQLearning.exe";
                    info.Arguments = string.Join(" ", args) + " true";
                    info.UseShellExecute = true;
                    Process.Start(info);
                }

                // wait 10 milliseconds between instances to guarantee random seed
                Thread.Sleep(10);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // display start
            toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            // start instances
            for (int i = 0; i < numInstances; i++)
            {
                formarray[i].startLearning.PerformClick();

                // wait 10 milliseconds between instances
                Thread.Sleep(10);
            }

            // start charts
            initializeCharts();
            timer1.Enabled = true;
        }

        private void btnPauseContinue_Click(object sender, EventArgs e)
        {
            // pause or continue forms
            for (int i = 0; i < numInstances; i++)
            {
                formarray[i].PauseBtn.PerformClick();
            }

            // set button state
            if (paused)
            {
                btnPauseContinue.Text = "Pause";
                paused = false;
                timer1.Enabled = true;
            }
            else
            {
                btnPauseContinue.Text = "Continue";
                paused = true;
                timer1.Enabled = false;
            }

            // display elapsed
            DateTime start;
            var end = DateTime.Now;
            if (DateTime.TryParse(toolStripStatusLabel1.Text, out start))
            {
                var sb = new StringBuilder().AppendFormat("START(year-month-day_hours:mins:secs):{0} | END(year-month-day_hours:mins:secs):{1} | ELAPSED(day.hours_mins:secs):{2}", start.ToString("yyyy-MM-dd_HH:mm:ss"), end.ToString("yyyy-MM-dd_HH:mm:ss"), (end - start).ToString(@"dd\.hh\_mm\:ss"));
                toolStripStatusLabel1.Text = sb.ToString();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            // stop forms
            for (int i = 0; i < numInstances; i++)
            {
                formarray[i].StopLearning.PerformClick();
            }
            timer1.Enabled = false;
        }

        private void btnDestroy_Click(object sender, EventArgs e)
        {
            // close forms
            for (int i = 0; i < numInstances; i++)
            {
                formarray[i].Close();
            }
        }

        private void btnFaster_Click(object sender, EventArgs e)
        {
            // faster forms
            for (int i = 0; i < numInstances; i++)
            {
                formarray[i].goVeryFast.PerformClick();
            }
        }

        private void btnFast_Click(object sender, EventArgs e)
        {
            // fast forms
            for (int i = 0; i < numInstances; i++)
            {
                formarray[i].goFast.PerformClick();
            }
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            // normal forms
            for (int i = 0; i < numInstances; i++)
            {
                formarray[i].goNormal.PerformClick();
            }
        }

        private void btnSlow_Click(object sender, EventArgs e)
        {
            // slow forms
            for (int i = 0; i < numInstances; i++)
            {
                formarray[i].goSlow.PerformClick();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // save forms
            for (int i = 0; i < numInstances; i++)
            {
                formarray[i].saveNet.PerformClick();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // load forms
            for (int i = 0; i < numInstances; i++)
            {
                formarray[i].loadNet.PerformClick();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.btnDestroy.PerformClick();
            this.Close();
        }

        private void rdoThread_CheckedChanged(object sender, EventArgs e)
        {
                // can control processes and use shared memory with threads
                if (rdoThread.Checked)
                {
                    btnDestroy.Enabled = true;
                    btnFast.Enabled = true;
                    btnFastest.Enabled = true;
                    btnLoad.Enabled = true;
                    btnNormal.Enabled = true;
                    btnPauseContinue.Enabled = true;
                    btnSave.Enabled = true;
                    btnSlow.Enabled = true;
                    btnStart.Enabled = true;
                    btnStop.Enabled = true;
                    chkSharedExperience.Enabled = true;
                }
        }

        private void cboMutationOperators_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNumberItems.Text = cboMutationCategory.SelectedValue.ToString();
        }

        private void rdoProcess_CheckedChanged(object sender, EventArgs e)
        {
            // cannot control processes or use shared memory with processes, no interprocess communication
            if (rdoProcess.Checked)
            {
                btnDestroy.Enabled = false;
                btnFast.Enabled = false;
                btnFastest.Enabled = false;
                btnLoad.Enabled = false;
                btnNormal.Enabled = false;
                btnPauseContinue.Enabled = false;
                btnSave.Enabled = false;
                btnSlow.Enabled = false;
                btnStart.Enabled = false;
                btnStop.Enabled = false;
                chkSharedExperience.Enabled = false;
                chkSharedExperience.Checked = false;
            }
        }

        private void initializeCharts()
        {
            // create static charts
            // chart #1
            this.chart1.Series.Clear();
            this.chart1.Titles.Clear();
            this.chart1.Titles.Add("Agent Accuracy Over Time");
            series1a = this.chart1.Series.Add("Average\\nReward");
            series1a.ChartType = SeriesChartType.Spline;
            series1a.Color = Color.Blue;
            series1b = this.chart1.Series.Add("Average\\nLoss");
            series1b.ChartType = SeriesChartType.Spline;
            series1b.Color = Color.Orange;
            // add points for trendline
            series1a.Points.AddXY(0, 0.5);
            series1a.Points.AddXY(0, 0.5);
            series1b.Points.AddXY(0, 0.5);
            series1b.Points.AddXY(0, 0.5);
            // create trend lines
            series1at = chart1.Series.Add("Reward\\nTrend");
            series1at.ChartType = SeriesChartType.Line;
            series1at.Color = Color.Blue;
            series1at.BorderDashStyle = ChartDashStyle.Dash;
            chart1.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, "Linear,1,false,false", series1a, series1at);
            series1bt = chart1.Series.Add("Loss\\nTrend");
            series1bt.ChartType = SeriesChartType.Line;
            series1bt.Color = Color.Orange;
            series1bt.BorderDashStyle = ChartDashStyle.Dash;
            chart1.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, "Linear,1,false,false", series1b, series1bt);

            // chart #2
            this.chart2.Titles.Clear();
            this.chart2.Series.Clear();
            this.chart2.Titles.Add("Agent Completeness Over Time");
            //series2 = this.chart2.Series.Add("Processed\\nItem Count");
            //series2.ChartType = SeriesChartType.Spline;
            //series2.Color = Color.Orange;
            ////this.chart2.Series["Processed\\nItem Count"].IsValueShownAsLabel = true;
            ////this.chart1.Series["Average\\nReward"].IsValueShownAsLabel = true;
            ////this.chart1.Series["Average\\nLoss"].IsValueShownAsLabel = true;
            series2a = this.chart2.Series.Add("Average\\nMutants\\nCount");
            series2a.ChartType = SeriesChartType.Spline;
            series2a.Color = Color.LightBlue;
            series2b = this.chart2.Series.Add("Average\\nKilled\\nCount");
            series2b.ChartType = SeriesChartType.Spline;
            series2b.Color = Color.PaleGoldenrod;
            series2c = this.chart2.Series.Add("Average\\nLive\\nCount");
            series2c.ChartType = SeriesChartType.Spline;
            series2c.Color = Color.LightSalmon;
            // add points for trendline
            series2a.Points.AddXY(0, 0.5);
            series2a.Points.AddXY(0, 0.5);
            series2b.Points.AddXY(0, 0.5);
            series2b.Points.AddXY(0, 0.5);
            series2c.Points.AddXY(0, 0.5);
            series2c.Points.AddXY(0, 0.5);
            // create trend lines
            series2at = chart2.Series.Add("Mutants\\nTrend");
            series2at.ChartType = SeriesChartType.Line;
            series2at.BorderDashStyle = ChartDashStyle.Dash;
            series2at.Color = Color.Blue;
            chart2.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, "Linear,1,false,false", series2a, series2at);
            series2bt = chart2.Series.Add("Killed\\nTrend");
            series2bt.ChartType = SeriesChartType.Line;
            series2bt.BorderDashStyle = ChartDashStyle.Dash;
            series2bt.Color = Color.Orange;
            chart2.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, "Linear,1,false,false", series2b, series2bt);
            series2ct = chart2.Series.Add("Live\\nTrend");
            series2ct.ChartType = SeriesChartType.Line;
            series2ct.BorderDashStyle = ChartDashStyle.Dash;
            series2ct.Color = Color.Red;
            chart2.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, "Linear,1,false,false", series2c, series2ct);

            // create dynamic charts
            // chart #3
            this.chart3.Series.Clear();
            this.chart3.Titles.Clear();
            this.chart3.Titles.Add("Experience Allocation Over Time by Instance");
            for (int i = 0; i < numInstances; i++)
            {
                // for each instance add a new series
                this.chart3.Series.Add(i.ToString());
                this.chart3.Series[i.ToString()].ChartType = SeriesChartType.StackedArea;
                //this.chart3.Series[i.ToString()].IsValueShownAsLabel = true;
            }
        }

        private void updateChart(bool label)
        {


            if (formarray?[0].qAgent != null && (chkChartExperience.Checked || chkChartItems.Checked || chkChartReward.Checked))
            {
                double sumOfAgentAvgReward = 0, sumOfAgentAvgLoss = 0, sumOfAgentTotalMutations = 0, sumOfAgentKilledMutations = 0, sumOfAgentLiveMutations = 0;
                var tc = formarray[0].qAgent.getTickCount();

                if (chkChartItems.Checked || chkChartReward.Checked)
                {
                    for (int i = 0; i < numInstances; i++)
                    {
                        sumOfAgentAvgReward = sumOfAgentAvgReward + formarray[i].qAgent.getAvgReward();
                        sumOfAgentAvgLoss = sumOfAgentAvgLoss + formarray[i].qAgent.getAvgLoss();
                        sumOfAgentTotalMutations = sumOfAgentTotalMutations + formarray[i].qAgent.getTotalMutantsCount();
                        sumOfAgentKilledMutations = sumOfAgentKilledMutations + formarray[i].qAgent.getKilledMutantsCount();
                        sumOfAgentLiveMutations = sumOfAgentLiveMutations + formarray[i].qAgent.getLiveMutantsCount();
                    }
                }

                // average agent average rewards and loss
                if (chkChartReward.Checked)
                {
                    DataPoint dp1 = new DataPoint(tc, sumOfAgentAvgReward / numInstances);
                    dp1.IsValueShownAsLabel = label;
                    if (sumOfAgentAvgReward > -1) series1a.Points.Add(dp1);
                    DataPoint dp2 = new DataPoint(tc, sumOfAgentAvgLoss / numInstances);
                    dp2.IsValueShownAsLabel = label;
                    if (sumOfAgentAvgLoss > -1) series1b.Points.Add(dp2);
                    chart1.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, "Linear,1,false,false", series1a, series1at);
                    chart1.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, "Linear,1,false,false", series1b, series1bt);
                    chart1.Update();
                }
                // sum of agent items processed
                if (chkChartItems.Checked)
                {
                    //DataPoint dp3 = new DataPoint(tc, sumOfAgentItemsProcessed);
                    //dp3.IsValueShownAsLabel = label;
                    //series2.Points.Add(dp3);
                    //chart2.Update();
                    DataPoint dp3a = new DataPoint(tc, sumOfAgentTotalMutations / numInstances);
                    dp3a.IsValueShownAsLabel = label;
                    if (sumOfAgentTotalMutations > -1) series2a.Points.Add(dp3a);
                    DataPoint dp3b = new DataPoint(tc, sumOfAgentKilledMutations / numInstances);
                    dp3b.IsValueShownAsLabel = label;
                    if (sumOfAgentKilledMutations > -1) series2b.Points.Add(dp3b);
                    DataPoint dp3c = new DataPoint(tc, sumOfAgentLiveMutations / numInstances);
                    dp3c.IsValueShownAsLabel = label;
                    if (sumOfAgentLiveMutations > -1) series2c.Points.Add(dp3c);
                    chart2.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, "Linear,1,false,false", series2a, series2at);
                    chart2.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, "Linear,1,false,false", series2b, series2bt);
                    chart2.DataManipulator.FinancialFormula(FinancialFormula.Forecasting, "Linear,1,false,false", series2c, series2ct);
                    chart2.Update();
                }

                // percent of agents shared experience
                if (chkChartExperience.Checked)
                {
                    DataPoint dp4;
                    for (int i = 0; i < numInstances; i++)
                    {
                        dp4 = new DataPoint(tc, this.formarray[i].qAgent.getExperienceCount());
                        dp4.IsValueShownAsLabel = label;
                        this.chart3.Series[i.ToString()].Points.Add(dp4);
                    }
                    chart3.Update();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // update chart w/o labels
            updateChart(false);

            // pause threads if duration reached
            if (txtDuration.Text != "" && formarray[0].qAgent.getTickCount() > Int32.Parse(txtDuration.Text))
            {
                // update chart w/ labels
                updateChart(true);

                // pause threads
                btnPauseContinue.PerformClick();
            }
        }
    }

    public class ComboboxItem
    {
        public ComboboxItem(string Text, object Value)
        {
            this.Text = Text;
            this.Value = Value;
        }
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

}
