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
            this.minPlaybackCheckbox = new System.Windows.Forms.CheckBox();
            this.openRemappingFileDialog = new System.Windows.Forms.OpenFileDialog();
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
            this.performOptimizationButton.Location = new System.Drawing.Point(19, 334);
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
            this.messageBox.Location = new System.Drawing.Point(11, 221);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.messageBox.Size = new System.Drawing.Size(546, 107);
            this.messageBox.TabIndex = 7;
            this.messageBox.WordWrap = false;
            this.messageBox.TextChanged += new System.EventHandler(this.messageBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 205);
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
            this.remapNotesButton.Location = new System.Drawing.Point(350, 335);
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
            this.epsilonCheckbox.Location = new System.Drawing.Point(125, 153);
            this.epsilonCheckbox.Name = "epsilonCheckbox";
            this.epsilonCheckbox.Size = new System.Drawing.Size(299, 17);
            this.epsilonCheckbox.TabIndex = 14;
            this.epsilonCheckbox.Text = "Use Minimum Loudness (0.01f) instead of absolute silence";
            this.epsilonCheckbox.UseVisualStyleBackColor = true;
            this.epsilonCheckbox.CheckedChanged += new System.EventHandler(this.epsilonCheckbox_CheckedChanged);
            // 
            // minPlaybackCheckbox
            // 
            this.minPlaybackCheckbox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.minPlaybackCheckbox.AutoSize = true;
            this.minPlaybackCheckbox.Checked = true;
            this.minPlaybackCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.minPlaybackCheckbox.Location = new System.Drawing.Point(125, 177);
            this.minPlaybackCheckbox.Name = "minPlaybackCheckbox";
            this.minPlaybackCheckbox.Size = new System.Drawing.Size(276, 17);
            this.minPlaybackCheckbox.TabIndex = 15;
            this.minPlaybackCheckbox.Text = "Use minimum playback length to discard files (150ms)";
            this.minPlaybackCheckbox.UseVisualStyleBackColor = true;
            this.minPlaybackCheckbox.CheckedChanged += new System.EventHandler(this.minPlaybackCheckbox_CheckedChanged);
            // 
            // openRemappingFileDialog
            // 
            this.openRemappingFileDialog.FileName = "remap.json";
            this.openRemappingFileDialog.Filter = "Remapping Files|*.json";
            this.openRemappingFileDialog.Title = "Select remapping file...";
            this.openRemappingFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // BsoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(570, 402);
            this.Controls.Add(this.minPlaybackCheckbox);
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
        private System.Windows.Forms.CheckBox minPlaybackCheckbox;
        private System.Windows.Forms.OpenFileDialog openRemappingFileDialog;
    }
}

