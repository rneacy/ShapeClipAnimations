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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comPortLabel = new System.Windows.Forms.Label();
            this.comSelectBox = new System.Windows.Forms.ComboBox();
            this.removeAnimButton = new System.Windows.Forms.Button();
            this.addAnimButton = new System.Windows.Forms.Button();
            this.animationList = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.uploadButton = new System.Windows.Forms.Button();
            this.toggleButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            // 
            // comPortLabel
            // 
            this.comPortLabel.AutoSize = true;
            this.comPortLabel.Location = new System.Drawing.Point(85, 579);
            this.comPortLabel.Name = "comPortLabel";
            this.comPortLabel.Size = new System.Drawing.Size(55, 13);
            this.comPortLabel.TabIndex = 4;
            this.comPortLabel.Text = "Serial Port";
            this.comPortLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comSelectBox
            // 
            this.comSelectBox.FormattingEnabled = true;
            this.comSelectBox.Location = new System.Drawing.Point(86, 594);
            this.comSelectBox.Name = "comSelectBox";
            this.comSelectBox.Size = new System.Drawing.Size(121, 21);
            this.comSelectBox.TabIndex = 3;
            // 
            // removeAnimButton
            // 
            this.removeAnimButton.Enabled = false;
            this.removeAnimButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeAnimButton.Location = new System.Drawing.Point(508, 570);
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
            this.addAnimButton.Location = new System.Drawing.Point(12, 570);
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
            this.animationList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.animationList.FormattingEnabled = true;
            this.animationList.ItemHeight = 20;
            this.animationList.Location = new System.Drawing.Point(12, 20);
            this.animationList.Name = "animationList";
            this.animationList.Size = new System.Drawing.Size(694, 544);
            this.animationList.TabIndex = 0;
            this.animationList.SelectedIndexChanged += new System.EventHandler(this.animationList_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comPortLabel);
            this.groupBox2.Controls.Add(this.uploadButton);
            this.groupBox2.Controls.Add(this.comSelectBox);
            this.groupBox2.Controls.Add(this.toggleButton);
            this.groupBox2.Location = new System.Drawing.Point(735, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(293, 639);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Control Options";
            // 
            // uploadButton
            // 
            this.uploadButton.Enabled = false;
            this.uploadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadButton.Location = new System.Drawing.Point(19, 346);
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
            this.toggleButton.Location = new System.Drawing.Point(19, 216);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(256, 76);
            this.toggleButton.TabIndex = 0;
            this.toggleButton.Text = "START";
            this.toggleButton.UseVisualStyleBackColor = true;
            this.toggleButton.Click += new System.EventHandler(this.toggleButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 663);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShapeClip Controller";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
    }
}

