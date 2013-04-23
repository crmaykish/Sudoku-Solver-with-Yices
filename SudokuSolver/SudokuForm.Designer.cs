namespace SudokuSolver
{
    partial class SudokuForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.randomButton = new System.Windows.Forms.Button();
            this.slider = new System.Windows.Forms.TrackBar();
            this.demoCheckBox = new System.Windows.Forms.CheckBox();
            this.countDown = new System.Windows.Forms.TextBox();
            this.sliderLabel = new System.Windows.Forms.Label();
            this.starting = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.slider)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(84, 447);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Solve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(84, 476);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // randomButton
            // 
            this.randomButton.Location = new System.Drawing.Point(165, 447);
            this.randomButton.Name = "randomButton";
            this.randomButton.Size = new System.Drawing.Size(75, 23);
            this.randomButton.TabIndex = 2;
            this.randomButton.Text = "Generate";
            this.randomButton.UseVisualStyleBackColor = true;
            this.randomButton.Click += new System.EventHandler(this.randomButton_Click);
            // 
            // slider
            // 
            this.slider.Location = new System.Drawing.Point(249, 463);
            this.slider.Maximum = 35;
            this.slider.Minimum = 23;
            this.slider.Name = "slider";
            this.slider.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.slider.Size = new System.Drawing.Size(104, 45);
            this.slider.TabIndex = 3;
            this.slider.Value = 35;
            this.slider.Scroll += new System.EventHandler(this.slider_Scroll);
            this.slider.ValueChanged += new System.EventHandler(this.slider_ValueChanged);
            // 
            // demoCheckBox
            // 
            this.demoCheckBox.AutoSize = true;
            this.demoCheckBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.demoCheckBox.Location = new System.Drawing.Point(165, 476);
            this.demoCheckBox.Name = "demoCheckBox";
            this.demoCheckBox.Size = new System.Drawing.Size(54, 17);
            this.demoCheckBox.TabIndex = 4;
            this.demoCheckBox.Text = "Demo";
            this.demoCheckBox.UseVisualStyleBackColor = false;
            this.demoCheckBox.CheckedChanged += new System.EventHandler(this.demoCheckBox_CheckedChanged);
            // 
            // countDown
            // 
            this.countDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countDown.Location = new System.Drawing.Point(359, 458);
            this.countDown.Name = "countDown";
            this.countDown.Size = new System.Drawing.Size(30, 35);
            this.countDown.TabIndex = 5;
            this.countDown.TextChanged += new System.EventHandler(this.countDown_TextChanged);
            // 
            // sliderLabel
            // 
            this.sliderLabel.AutoSize = true;
            this.sliderLabel.Location = new System.Drawing.Point(323, 444);
            this.sliderLabel.Name = "sliderLabel";
            this.sliderLabel.Size = new System.Drawing.Size(19, 13);
            this.sliderLabel.TabIndex = 6;
            this.sliderLabel.Text = "35";
            // 
            // starting
            // 
            this.starting.AutoSize = true;
            this.starting.Location = new System.Drawing.Point(246, 447);
            this.starting.Name = "starting";
            this.starting.Size = new System.Drawing.Size(71, 13);
            this.starting.TabIndex = 7;
            this.starting.Text = "Starting Tiles:";
            this.starting.Click += new System.EventHandler(this.starting_Click);
            // 
            // SudokuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 507);
            this.Controls.Add(this.starting);
            this.Controls.Add(this.sliderLabel);
            this.Controls.Add(this.countDown);
            this.Controls.Add(this.demoCheckBox);
            this.Controls.Add(this.slider);
            this.Controls.Add(this.randomButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "SudokuForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.slider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button randomButton;
        private System.Windows.Forms.TrackBar slider;
        private System.Windows.Forms.CheckBox demoCheckBox;
        private System.Windows.Forms.TextBox countDown;
        private System.Windows.Forms.Label sliderLabel;
        private System.Windows.Forms.Label starting;
    }
}

