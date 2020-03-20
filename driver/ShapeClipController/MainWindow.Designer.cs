namespace ShapeClipController
{
    partial class MainWindow
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.removeAnimButton = new System.Windows.Forms.Button();
            this.addAnimButton = new System.Windows.Forms.Button();
            this.animationList = new System.Windows.Forms.ListBox();
            this.comPortLabel = new System.Windows.Forms.Label();
            this.comSelectBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.delayText = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.uploadButton = new System.Windows.Forms.Button();
            this.toggleButton = new System.Windows.Forms.Button();
            this.serialPortRefreshButton = new System.Windows.Forms.Button();
            this.serialMarshal = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.clipCountInput = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.animSizeLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.durationLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.clipCountLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pauseButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.removeAnimButton);
            this.groupBox1.Controls.Add(this.addAnimButton);
            this.groupBox1.Controls.Add(this.animationList);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(717, 639);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Animations";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // removeAnimButton
            // 
            this.removeAnimButton.Enabled = false;
            this.removeAnimButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeAnimButton.Location = new System.Drawing.Point(508, 553);
            this.removeAnimButton.Name = "removeAnimButton";
            this.removeAnimButton.Size = new System.Drawing.Size(198, 63);
            this.removeAnimButton.TabIndex = 2;
            this.removeAnimButton.Text = "Delete Animation";
            this.removeAnimButton.UseVisualStyleBackColor = true;
            this.removeAnimButton.Click += new System.EventHandler(this.removeAnimButton_Click);
            // 
            // addAnimButton
            // 
            this.addAnimButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addAnimButton.Location = new System.Drawing.Point(12, 553);
            this.addAnimButton.Name = "addAnimButton";
            this.addAnimButton.Size = new System.Drawing.Size(198, 63);
            this.addAnimButton.TabIndex = 1;
            this.addAnimButton.Text = "Add Animation";
            this.addAnimButton.UseVisualStyleBackColor = true;
            this.addAnimButton.Click += new System.EventHandler(this.addAnimButton_Click);
            // 
            // animationList
            // 
            this.animationList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.animationList.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.animationList.FormattingEnabled = true;
            this.animationList.ItemHeight = 39;
            this.animationList.Location = new System.Drawing.Point(12, 19);
            this.animationList.Name = "animationList";
            this.animationList.Size = new System.Drawing.Size(694, 511);
            this.animationList.TabIndex = 0;
            this.animationList.SelectedIndexChanged += new System.EventHandler(this.animationList_SelectedIndexChanged);
            // 
            // comPortLabel
            // 
            this.comPortLabel.AutoSize = true;
            this.comPortLabel.Location = new System.Drawing.Point(45, 85);
            this.comPortLabel.Name = "comPortLabel";
            this.comPortLabel.Size = new System.Drawing.Size(55, 13);
            this.comPortLabel.TabIndex = 4;
            this.comPortLabel.Text = "Serial Port";
            this.comPortLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comSelectBox
            // 
            this.comSelectBox.FormattingEnabled = true;
            this.comSelectBox.Location = new System.Drawing.Point(46, 100);
            this.comSelectBox.Name = "comSelectBox";
            this.comSelectBox.Size = new System.Drawing.Size(116, 21);
            this.comSelectBox.TabIndex = 3;
            this.comSelectBox.SelectedIndexChanged += new System.EventHandler(this.comSelectBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pauseButton);
            this.groupBox2.Controls.Add(this.delayText);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.uploadButton);
            this.groupBox2.Controls.Add(this.toggleButton);
            this.groupBox2.Location = new System.Drawing.Point(735, 192);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(293, 279);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Control Options";
            // 
            // delayText
            // 
            this.delayText.AutoSize = true;
            this.delayText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delayText.Location = new System.Drawing.Point(177, 118);
            this.delayText.Name = "delayText";
            this.delayText.Size = new System.Drawing.Size(42, 20);
            this.delayText.TabIndex = 4;
            this.delayText.Text = "0ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Starting Delay";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(153, 384);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(293, 255);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Clip Settings";
            // 
            // uploadButton
            // 
            this.uploadButton.Enabled = false;
            this.uploadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadButton.Location = new System.Drawing.Point(18, 166);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(256, 76);
            this.uploadButton.TabIndex = 1;
            this.uploadButton.Text = "UPLOAD";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // toggleButton
            // 
            this.toggleButton.Enabled = false;
            this.toggleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleButton.Location = new System.Drawing.Point(52, 38);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(82, 76);
            this.toggleButton.TabIndex = 0;
            this.toggleButton.Text = " ▶";
            this.toggleButton.UseVisualStyleBackColor = true;
            this.toggleButton.Click += new System.EventHandler(this.toggleButton_Click);
            // 
            // serialPortRefreshButton
            // 
            this.serialPortRefreshButton.Location = new System.Drawing.Point(176, 99);
            this.serialPortRefreshButton.Name = "serialPortRefreshButton";
            this.serialPortRefreshButton.Size = new System.Drawing.Size(70, 23);
            this.serialPortRefreshButton.TabIndex = 5;
            this.serialPortRefreshButton.Text = "Refresh";
            this.serialPortRefreshButton.UseVisualStyleBackColor = true;
            this.serialPortRefreshButton.Click += new System.EventHandler(this.serialPortRefreshButton_Click);
            // 
            // serialMarshal
            // 
            this.serialMarshal.Interval = 1500;
            this.serialMarshal.Tick += new System.EventHandler(this.serialMarshal_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Connected Clips";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.clipCountInput);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.serialPortRefreshButton);
            this.groupBox4.Controls.Add(this.comPortLabel);
            this.groupBox4.Controls.Add(this.comSelectBox);
            this.groupBox4.Location = new System.Drawing.Point(735, 477);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(293, 174);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Clip Settings";
            // 
            // clipCountInput
            // 
            this.clipCountInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clipCountInput.Location = new System.Drawing.Point(196, 43);
            this.clipCountInput.Name = "clipCountInput";
            this.clipCountInput.Size = new System.Drawing.Size(49, 31);
            this.clipCountInput.TabIndex = 7;
            this.clipCountInput.Text = "1";
            this.clipCountInput.TextChanged += new System.EventHandler(this.clipCountInput_TextChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.animSizeLabel);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.durationLabel);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.clipCountLabel);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Location = new System.Drawing.Point(736, 13);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(292, 173);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Animation Information";
            // 
            // animSizeLabel
            // 
            this.animSizeLabel.AutoSize = true;
            this.animSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.animSizeLabel.Location = new System.Drawing.Point(204, 122);
            this.animSizeLabel.Name = "animSizeLabel";
            this.animSizeLabel.Size = new System.Drawing.Size(29, 20);
            this.animSizeLabel.TabIndex = 5;
            this.animSizeLabel.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Animation Size";
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.durationLabel.Location = new System.Drawing.Point(204, 82);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(38, 20);
            this.durationLabel.TabIndex = 3;
            this.durationLabel.Text = "23s";
            this.durationLabel.Click += new System.EventHandler(this.durationLabel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Approx. Duration";
            // 
            // clipCountLabel
            // 
            this.clipCountLabel.AutoSize = true;
            this.clipCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clipCountLabel.Location = new System.Drawing.Point(204, 41);
            this.clipCountLabel.Name = "clipCountLabel";
            this.clipCountLabel.Size = new System.Drawing.Size(19, 20);
            this.clipCountLabel.TabIndex = 1;
            this.clipCountLabel.Text = "0";
            this.clipCountLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Intended Clip Count";
            // 
            // pauseButton
            // 
            this.pauseButton.Enabled = false;
            this.pauseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pauseButton.Location = new System.Drawing.Point(155, 38);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(82, 76);
            this.pauseButton.TabIndex = 5;
            this.pauseButton.Text = "II";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 663);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShapeClip Controller | No Connected Clip";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button toggleButton;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.ListBox animationList;
        private System.Windows.Forms.Button removeAnimButton;
        private System.Windows.Forms.Button addAnimButton;
        private System.Windows.Forms.Label comPortLabel;
        private System.Windows.Forms.ComboBox comSelectBox;
        private System.Windows.Forms.Button serialPortRefreshButton;
        private System.Windows.Forms.Timer serialMarshal;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox clipCountInput;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label clipCountLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label delayText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label animSizeLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button pauseButton;
    }
}

