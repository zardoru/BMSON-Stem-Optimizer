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
            StemOptimizer.UseMinimumPlayback = minPlaybackCheckbox.Checked;

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
            Logger.Log("Starting optimization process on bmson file {0} to {1}",
                bmsonTextBox.Text, outputDirTextBox.Text);

            try
            {

                BlockButton(optimizeAndRemapButton);
                BlockButton(performOptimizationButton);
                BlockButton(remapNotesButton);
                string outdir = CheckOutputDir(bmsonTextBox.Text, outputDirTextBox.Text);

                outputDirTextBox.Text = outdir;

                var task = StemOptimizer.OptimizeFromBMSON(bmsonTextBox.Text, outdir);

                task.ContinueWith(val =>
                {
                    StemOptimizer.WriteNewStemsFromRemappingData(bmsonTextBox.Text, val.Result, outdir);
                    StemOptimizer.SerializeRemappingDataToJSON(val.Result, outdir + System.IO.Path.DirectorySeparatorChar + "remap.json");
                    UnblockButton(optimizeAndRemapButton);
                    UnblockButton(performOptimizationButton);
                    UnblockButton(remapNotesButton);
                });
            } catch(Exception ex)
            {
                Logger.Log("An error has ocurred while optimizing: {0}", ex.ToString());
                MessageBox.Show("An error has ocurred while optimizing stems. Check the log for details.", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void button1_Click(object sender, EventArgs e)
        {

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
            StemOptimizer.UseMinimumPlayback = minPlaybackCheckbox.Checked;
        }
    }
}
