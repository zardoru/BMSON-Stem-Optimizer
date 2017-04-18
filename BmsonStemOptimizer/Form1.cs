using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BmsonStemOptimizer
{
    public partial class BsoForm : Form
    {
        public BsoForm()
        {
            InitializeComponent();
        }

        private void AddMessage(string ev)
        {
            messageBox.AppendText(ev + Environment.NewLine);
        }

        private void BsoForm_Load(object sender, EventArgs e)
        {
            Logger.Log("@@@ Starting execution");

            StemOptimizer.UseLoudEpsilon = epsilonCheckbox.Checked;
            StemOptimizer.UseMinimumPlayback = true;

            Logger.OnLogEvent += (ev) =>
            {
                if (messageBox.InvokeRequired)
                {
                    this.Invoke((Action)delegate() { AddMessage(ev); });
                } else
                    AddMessage(ev);
            };
        }
        

        private void searchBmsonButton_Click(object sender, EventArgs e)
        {
            DialogResult res = openBmsonDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                bmsonTextBox.Text = openBmsonDialog.FileName;
            }
        }

        private void searchOutputDirButton_Click(object sender, EventArgs e)
        {
            DialogResult res = selectOutputFolderDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                outputDirTextBox.Text = selectOutputFolderDialog.SelectedPath;
            }
        }

        private static string CheckOutputDir(string bmson, string output_dir)
        {
            if (output_dir != "")
                Directory.CreateDirectory(output_dir);
            else
            {
                output_dir = Path.GetDirectoryName(bmson) + Path.DirectorySeparatorChar + "optimized";
                Logger.Log("Unspecified output dir, using {0}", output_dir);
                Directory.CreateDirectory(output_dir);
            }

            if (!(Directory.Exists(output_dir) && File.Exists(bmson)))
            {
                throw new ArgumentException("Either the input file or output dir does not exist!");
            }

            return output_dir;
        }

        private void performOptimizationButtonn_Click(object sender, EventArgs e)
        {
            PerformOptimization();
        }

        private void SetText(TextBox text, string str)
        {
            if (text.InvokeRequired)
                Invoke((Action)(() => { text.Text = str; }));
            else
                text.Text = str;
        }

        private async Task<Dictionary<string, StemTimeMap[]>> PerformOptimization()
        {
            Logger.Log("Starting optimization process on bmson file {0} to {1}",
                bmsonTextBox.Text, outputDirTextBox.Text);

            try
            {

                //BlockButton(optimizeAndRemapButton);
                BlockButton(performOptimizationButton);
                BlockButton(remapNotesButton);
                string outdir = CheckOutputDir(bmsonTextBox.Text, outputDirTextBox.Text);

                SetText(outputDirTextBox, outdir);

                var task = StemOptimizer.OptimizeFromBMSON(bmsonTextBox.Text, outdir);

                await task.ContinueWith(val =>
                {
                    StemOptimizer.WriteNewStemsFromRemappingData(bmsonTextBox.Text, val.Result, outdir);

                    string remapfile = outdir + Path.DirectorySeparatorChar + "remap.json";
                    StemOptimizer.SerializeRemappingDataToJSON(val.Result, remapfile);

                    if (remappingFileTextBox.InvokeRequired)
                    {
                        Invoke((Action)(() =>
                        {
                            remappingFileTextBox.Text = remapfile;
                        }));
                    }
                    else
                    {
                        remappingFileTextBox.Text = remapfile;
                    }

                    //UnblockButton(optimizeAndRemapButton);
                    UnblockButton(performOptimizationButton);
                    UnblockButton(remapNotesButton);
                });

                return task.Result;
            }
            catch (Exception ex)
            {
                Logger.Log("An error has ocurred while optimizing: {0}", ex.ToString());
                MessageBox.Show("An error has ocurred while optimizing stems. Check the log for details.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        private void RemapNotes(Dictionary<string, StemTimeMap[]> tmap)
        {
            string outdir = CheckOutputDir(bmsonTextBox.Text, outputDirTextBox.Text);

            SetText(outputDirTextBox, outdir);

            Logger.Log("Deserializing remapping data...");
            var timemap = tmap != null ? tmap : StemOptimizer.DeserializeRemappingDataToJSON(remappingFileTextBox.Text);

            Logger.Log("Remapping notes...");
            NoteRemapper remap = new NoteRemapper(StemOptimizer.GetBMSONRoot(bmsonTextBox.Text), timemap);

            string write = remap.GetRemappedJSON();

            string file = outputDirTextBox.Text
                + Path.DirectorySeparatorChar
                + Path.GetFileName(bmsonTextBox.Text);
            Logger.Log("Writing bmson to {0}...", file);
            StreamWriter output = new StreamWriter(file);

            output.WriteLine(write);
            output.Close();
            Logger.Log("Done!");
        }

        private void UnblockButton(Button btn)
        {
            if (btn.InvokeRequired)
            {
                btn.Invoke((Action)(() => { btn.Enabled = true; }));
            } else
            {
                btn.Enabled = true;
            }
        }

        private void BlockButton(Button btn)
        {
            if (btn.InvokeRequired)
            {
                btn.Invoke((Action)(() => { btn.Enabled = false; }));
            }
            else
            {
                btn.Enabled = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void outputDirTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void remapNotesButton_Click(object sender, EventArgs e)
        {
            //performOptimizationButtonn_Click(sender, e);
            RemapNotes(null);

        }

        private void messageBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void epsilonCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            StemOptimizer.UseLoudEpsilon = epsilonCheckbox.Checked;
        }

        private void minPlaybackCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            // StemOptimizer.UseMinimumPlayback = minPlaybackCheckbox.Checked;
        }

        private void searchRemappingFileButton_Click(object sender, EventArgs e)
        {
            DialogResult res = openRemappingFileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                remappingFileTextBox.Text = openRemappingFileDialog.FileName;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lblPlaybackTime.Text = string.Format("{0}ms", tbMinPlayback.Value);
            StemOptimizer.MinPlaybackTime = tbMinPlayback.Value / 1000.0;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            lblSilencePeriod.Text = string.Format("{0}ms", tbMinSilence.Value);
            StemOptimizer.MinSilenceTime = tbMinSilence.Value / 1000.0;
        }
    }
}
