namespace BmsonStemOptimizer
{
    partial class BsoForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.searchBmsonButton = new System.Windows.Forms.Button();
            this.bmsonTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.outputDirTextBox = new System.Windows.Forms.TextBox();
            this.performOptimizationButton = new System.Windows.Forms.Button();
            this.searchOutputDirButton = new System.Windows.Forms.Button();
            this.openBmsonDialog = new System.Windows.Forms.OpenFileDialog();
            this.selectOutputFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.remapNotesButton = new System.Windows.Forms.Button();
            this.remappingFileTextBox = new System.Windows.Forms.TextBox();
            this.searchRemappingFileButton = new System.Windows.Forms.Button();
            this.epsilonCheckbox = new System.Windows.Forms.CheckBox();
            this.openRemappingFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tbMinPlayback = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPlaybackTime = new System.Windows.Forms.Label();
            this.tbMinSilence = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSilencePeriod = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbMinPlayback)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMinSilence)).BeginInit();
            this.SuspendLayout();
            // 
            // searchBmsonButton
            // 
            this.searchBmsonButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBmsonButton.AutoSize = true;
            this.searchBmsonButton.Location = new System.Drawing.Point(483, 33);
            this.searchBmsonButton.Name = "searchBmsonButton";
            this.searchBmsonButton.Size = new System.Drawing.Size(75, 23);
            this.searchBmsonButton.TabIndex = 0;
            this.searchBmsonButton.Text = "Search...";
            this.searchBmsonButton.UseVisualStyleBackColor = true;
            this.searchBmsonButton.Click += new System.EventHandler(this.searchBmsonButton_Click);
            // 
            // bmsonTextBox
            // 
            this.bmsonTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bmsonTextBox.Location = new System.Drawing.Point(12, 36);
            this.bmsonTextBox.Name = "bmsonTextBox";
            this.bmsonTextBox.Size = new System.Drawing.Size(465, 20);
            this.bmsonTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select reference BMSON file...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output Directory...";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // outputDirTextBox
            // 
            this.outputDirTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputDirTextBox.Location = new System.Drawing.Point(12, 80);
            this.outputDirTextBox.Name = "outputDirTextBox";
            this.outputDirTextBox.Size = new System.Drawing.Size(465, 20);
            this.outputDirTextBox.TabIndex = 4;
            this.outputDirTextBox.TextChanged += new System.EventHandler(this.outputDirTextBox_TextChanged);
            // 
            // performOptimizationButton
            // 
            this.performOptimizationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.performOptimizationButton.Location = new System.Drawing.Point(19, 425);
            this.performOptimizationButton.Name = "performOptimizationButton";
            this.performOptimizationButton.Size = new System.Drawing.Size(223, 28);
            this.performOptimizationButton.TabIndex = 5;
            this.performOptimizationButton.Text = "Optimize";
            this.performOptimizationButton.UseVisualStyleBackColor = true;
            this.performOptimizationButton.Click += new System.EventHandler(this.performOptimizationButtonn_Click);
            // 
            // searchOutputDirButton
            // 
            this.searchOutputDirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchOutputDirButton.Location = new System.Drawing.Point(484, 80);
            this.searchOutputDirButton.Name = "searchOutputDirButton";
            this.searchOutputDirButton.Size = new System.Drawing.Size(75, 23);
            this.searchOutputDirButton.TabIndex = 6;
            this.searchOutputDirButton.Text = "Search...";
            this.searchOutputDirButton.UseVisualStyleBackColor = true;
            this.searchOutputDirButton.Click += new System.EventHandler(this.searchOutputDirButton_Click);
            // 
            // openBmsonDialog
            // 
            this.openBmsonDialog.Filter = "BMSON file|*.bmson";
            this.openBmsonDialog.Title = "Select BMSON file";
            // 
            // messageBox
            // 
            this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageBox.Location = new System.Drawing.Point(11, 297);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.messageBox.Size = new System.Drawing.Size(546, 122);
            this.messageBox.TabIndex = 7;
            this.messageBox.WordWrap = false;
            this.messageBox.TextChanged += new System.EventHandler(this.messageBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 281);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Messages";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Remapping File";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // remapNotesButton
            // 
            this.remapNotesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.remapNotesButton.Location = new System.Drawing.Point(350, 426);
            this.remapNotesButton.Name = "remapNotesButton";
            this.remapNotesButton.Size = new System.Drawing.Size(209, 28);
            this.remapNotesButton.TabIndex = 10;
            this.remapNotesButton.Text = "Remap";
            this.remapNotesButton.UseVisualStyleBackColor = true;
            this.remapNotesButton.Click += new System.EventHandler(this.remapNotesButton_Click);
            // 
            // remappingFileTextBox
            // 
            this.remappingFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remappingFileTextBox.Location = new System.Drawing.Point(12, 127);
            this.remappingFileTextBox.Name = "remappingFileTextBox";
            this.remappingFileTextBox.Size = new System.Drawing.Size(464, 20);
            this.remappingFileTextBox.TabIndex = 12;
            // 
            // searchRemappingFileButton
            // 
            this.searchRemappingFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchRemappingFileButton.Location = new System.Drawing.Point(483, 127);
            this.searchRemappingFileButton.Name = "searchRemappingFileButton";
            this.searchRemappingFileButton.Size = new System.Drawing.Size(75, 23);
            this.searchRemappingFileButton.TabIndex = 13;
            this.searchRemappingFileButton.Text = "Search...";
            this.searchRemappingFileButton.UseVisualStyleBackColor = true;
            this.searchRemappingFileButton.Click += new System.EventHandler(this.searchRemappingFileButton_Click);
            // 
            // epsilonCheckbox
            // 
            this.epsilonCheckbox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.epsilonCheckbox.AutoSize = true;
            this.epsilonCheckbox.Checked = true;
            this.epsilonCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.epsilonCheckbox.Location = new System.Drawing.Point(12, 155);
            this.epsilonCheckbox.Name = "epsilonCheckbox";
            this.epsilonCheckbox.Size = new System.Drawing.Size(299, 17);
            this.epsilonCheckbox.TabIndex = 14;
            this.epsilonCheckbox.Text = "Use Minimum Loudness (0.01f) instead of absolute silence";
            this.epsilonCheckbox.UseVisualStyleBackColor = true;
            this.epsilonCheckbox.CheckedChanged += new System.EventHandler(this.epsilonCheckbox_CheckedChanged);
            // 
            // openRemappingFileDialog
            // 
            this.openRemappingFileDialog.FileName = "remap.json";
            this.openRemappingFileDialog.Filter = "Remapping Files|*.json";
            this.openRemappingFileDialog.Title = "Select remapping file...";
            this.openRemappingFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // tbMinPlayback
            // 
            this.tbMinPlayback.LargeChange = 25;
            this.tbMinPlayback.Location = new System.Drawing.Point(125, 178);
            this.tbMinPlayback.Maximum = 200;
            this.tbMinPlayback.Minimum = 10;
            this.tbMinPlayback.Name = "tbMinPlayback";
            this.tbMinPlayback.Size = new System.Drawing.Size(432, 45);
            this.tbMinPlayback.SmallChange = 10;
            this.tbMinPlayback.TabIndex = 16;
            this.tbMinPlayback.TickFrequency = 10;
            this.tbMinPlayback.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbMinPlayback.Value = 150;
            this.tbMinPlayback.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Minimum playback time";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // lblPlaybackTime
            // 
            this.lblPlaybackTime.AutoSize = true;
            this.lblPlaybackTime.Location = new System.Drawing.Point(90, 191);
            this.lblPlaybackTime.Name = "lblPlaybackTime";
            this.lblPlaybackTime.Size = new System.Drawing.Size(38, 13);
            this.lblPlaybackTime.TabIndex = 18;
            this.lblPlaybackTime.Text = "150ms";
            // 
            // tbMinSilence
            // 
            this.tbMinSilence.LargeChange = 100;
            this.tbMinSilence.Location = new System.Drawing.Point(125, 230);
            this.tbMinSilence.Maximum = 1000;
            this.tbMinSilence.Minimum = 100;
            this.tbMinSilence.Name = "tbMinSilence";
            this.tbMinSilence.Size = new System.Drawing.Size(432, 45);
            this.tbMinSilence.SmallChange = 20;
            this.tbMinSilence.TabIndex = 19;
            this.tbMinSilence.TickFrequency = 100;
            this.tbMinSilence.Value = 200;
            this.tbMinSilence.Scroll += new System.EventHandler(this.trackBar1_Scroll_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Minimum silence period";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // lblSilencePeriod
            // 
            this.lblSilencePeriod.AutoSize = true;
            this.lblSilencePeriod.Location = new System.Drawing.Point(92, 247);
            this.lblSilencePeriod.Name = "lblSilencePeriod";
            this.lblSilencePeriod.Size = new System.Drawing.Size(38, 13);
            this.lblSilencePeriod.TabIndex = 21;
            this.lblSilencePeriod.Text = "200ms";
            // 
            // BsoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(570, 493);
            this.Controls.Add(this.lblSilencePeriod);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbMinSilence);
            this.Controls.Add(this.lblPlaybackTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbMinPlayback);
            this.Controls.Add(this.epsilonCheckbox);
            this.Controls.Add(this.searchRemappingFileButton);
            this.Controls.Add(this.remappingFileTextBox);
            this.Controls.Add(this.remapNotesButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.searchOutputDirButton);
            this.Controls.Add(this.performOptimizationButton);
            this.Controls.Add(this.outputDirTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bmsonTextBox);
            this.Controls.Add(this.searchBmsonButton);
            this.MinimumSize = new System.Drawing.Size(580, 440);
            this.Name = "BsoForm";
            this.Text = "Bmson Stem Optimizer";
            this.Load += new System.EventHandler(this.BsoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbMinPlayback)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMinSilence)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button searchBmsonButton;
        private System.Windows.Forms.TextBox bmsonTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox outputDirTextBox;
        private System.Windows.Forms.Button performOptimizationButton;
        private System.Windows.Forms.Button searchOutputDirButton;
        private System.Windows.Forms.OpenFileDialog openBmsonDialog;
        private System.Windows.Forms.FolderBrowserDialog selectOutputFolderDialog;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button remapNotesButton;
        private System.Windows.Forms.TextBox remappingFileTextBox;
        private System.Windows.Forms.Button searchRemappingFileButton;
        private System.Windows.Forms.CheckBox epsilonCheckbox;
        private System.Windows.Forms.OpenFileDialog openRemappingFileDialog;
        private System.Windows.Forms.TrackBar tbMinPlayback;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPlaybackTime;
        private System.Windows.Forms.TrackBar tbMinSilence;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSilencePeriod;
    }
}

