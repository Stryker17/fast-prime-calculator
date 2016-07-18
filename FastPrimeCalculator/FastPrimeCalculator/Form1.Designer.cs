namespace FastPrimeCalculator
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CalculationTimeout = new System.Windows.Forms.TextBox();
            this.HighestPrimeNumber = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.AbortButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TimerValueErrorMsg = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.NumberOfPrimes = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(484, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fast Prime Calculator";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Time Remaining:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(41, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Highest Prime Calculated:";
            // 
            // CalculationTimeout
            // 
            this.CalculationTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CalculationTimeout.Location = new System.Drawing.Point(173, 120);
            this.CalculationTimeout.Name = "CalculationTimeout";
            this.CalculationTimeout.Size = new System.Drawing.Size(59, 29);
            this.CalculationTimeout.TabIndex = 3;
            this.CalculationTimeout.Text = "60";
            // 
            // HighestPrimeNumber
            // 
            this.HighestPrimeNumber.Enabled = false;
            this.HighestPrimeNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HighestPrimeNumber.Location = new System.Drawing.Point(238, 173);
            this.HighestPrimeNumber.Name = "HighestPrimeNumber";
            this.HighestPrimeNumber.Size = new System.Drawing.Size(372, 29);
            this.HighestPrimeNumber.TabIndex = 4;
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Location = new System.Drawing.Point(354, 228);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(106, 43);
            this.StartButton.TabIndex = 5;
            this.StartButton.Text = "Start!";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // AbortButton
            // 
            this.AbortButton.Enabled = false;
            this.AbortButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AbortButton.Location = new System.Drawing.Point(504, 228);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(106, 43);
            this.AbortButton.TabIndex = 5;
            this.AbortButton.Text = "Abort";
            this.AbortButton.UseVisualStyleBackColor = true;
            this.AbortButton.Click += new System.EventHandler(this.AbortButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(238, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "seconds";
            // 
            // TimerValueErrorMsg
            // 
            this.TimerValueErrorMsg.AutoSize = true;
            this.TimerValueErrorMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimerValueErrorMsg.ForeColor = System.Drawing.SystemColors.Highlight;
            this.TimerValueErrorMsg.Location = new System.Drawing.Point(337, 120);
            this.TimerValueErrorMsg.Name = "TimerValueErrorMsg";
            this.TimerValueErrorMsg.Size = new System.Drawing.Size(283, 24);
            this.TimerValueErrorMsg.TabIndex = 7;
            this.TimerValueErrorMsg.Text = "Timer must be greater than zero!";
            this.TimerValueErrorMsg.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(45, 242);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Number of Primes:";
            // 
            // NumberOfPrimes
            // 
            this.NumberOfPrimes.Enabled = false;
            this.NumberOfPrimes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberOfPrimes.Location = new System.Drawing.Point(190, 236);
            this.NumberOfPrimes.Name = "NumberOfPrimes";
            this.NumberOfPrimes.Size = new System.Drawing.Size(143, 26);
            this.NumberOfPrimes.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 294);
            this.Controls.Add(this.NumberOfPrimes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TimerValueErrorMsg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.HighestPrimeNumber);
            this.Controls.Add(this.CalculationTimeout);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Fast Prime Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox CalculationTimeout;
        private System.Windows.Forms.TextBox HighestPrimeNumber;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button AbortButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label TimerValueErrorMsg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox NumberOfPrimes;
    }
}

