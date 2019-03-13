namespace ZiKoo
{
    partial class ComPort
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bitPerSecondCombo = new System.Windows.Forms.ComboBox();
            this.dataBitsCombo = new System.Windows.Forms.ComboBox();
            this.parityCombo = new System.Windows.Forms.ComboBox();
            this.stopBitsCombo = new System.Windows.Forms.ComboBox();
            this.flowControlCombo = new System.Windows.Forms.ComboBox();
            this.comPortOkBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bitova u sekundi:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bitova podataka:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Parnost: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Zaustavnih bitova: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Kontrola toka:";
            // 
            // bitPerSecondCombo
            // 
            this.bitPerSecondCombo.FormattingEnabled = true;
            this.bitPerSecondCombo.Location = new System.Drawing.Point(173, 24);
            this.bitPerSecondCombo.Name = "bitPerSecondCombo";
            this.bitPerSecondCombo.Size = new System.Drawing.Size(121, 21);
            this.bitPerSecondCombo.TabIndex = 9;
            // 
            // dataBitsCombo
            // 
            this.dataBitsCombo.FormattingEnabled = true;
            this.dataBitsCombo.Location = new System.Drawing.Point(173, 60);
            this.dataBitsCombo.Name = "dataBitsCombo";
            this.dataBitsCombo.Size = new System.Drawing.Size(121, 21);
            this.dataBitsCombo.TabIndex = 9;
            // 
            // parityCombo
            // 
            this.parityCombo.FormattingEnabled = true;
            this.parityCombo.Location = new System.Drawing.Point(173, 96);
            this.parityCombo.Name = "parityCombo";
            this.parityCombo.Size = new System.Drawing.Size(121, 21);
            this.parityCombo.TabIndex = 9;
            // 
            // stopBitsCombo
            // 
            this.stopBitsCombo.FormattingEnabled = true;
            this.stopBitsCombo.Location = new System.Drawing.Point(173, 132);
            this.stopBitsCombo.Name = "stopBitsCombo";
            this.stopBitsCombo.Size = new System.Drawing.Size(121, 21);
            this.stopBitsCombo.TabIndex = 9;
            // 
            // flowControlCombo
            // 
            this.flowControlCombo.FormattingEnabled = true;
            this.flowControlCombo.Location = new System.Drawing.Point(173, 169);
            this.flowControlCombo.Name = "flowControlCombo";
            this.flowControlCombo.Size = new System.Drawing.Size(121, 21);
            this.flowControlCombo.TabIndex = 9;
            // 
            // comPortOkBtn
            // 
            this.comPortOkBtn.Location = new System.Drawing.Point(231, 256);
            this.comPortOkBtn.Name = "comPortOkBtn";
            this.comPortOkBtn.Size = new System.Drawing.Size(75, 23);
            this.comPortOkBtn.TabIndex = 10;
            this.comPortOkBtn.Text = "U redu";
            this.comPortOkBtn.UseVisualStyleBackColor = true;
            this.comPortOkBtn.Click += new System.EventHandler(this.comPortOkBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowControlCombo);
            this.groupBox1.Controls.Add(this.stopBitsCombo);
            this.groupBox1.Controls.Add(this.parityCombo);
            this.groupBox1.Controls.Add(this.dataBitsCombo);
            this.groupBox1.Controls.Add(this.bitPerSecondCombo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 213);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 256);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Podrazumjevane vrijednosti";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ComPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 291);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comPortOkBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ComPort";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Postavke porta";
            this.Load += new System.EventHandler(this.ComPort_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox bitPerSecondCombo;
        private System.Windows.Forms.ComboBox dataBitsCombo;
        private System.Windows.Forms.ComboBox parityCombo;
        private System.Windows.Forms.ComboBox stopBitsCombo;
        private System.Windows.Forms.ComboBox flowControlCombo;
        private System.Windows.Forms.Button comPortOkBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
    }
}