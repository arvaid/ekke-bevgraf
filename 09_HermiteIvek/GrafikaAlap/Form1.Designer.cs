namespace GrafikaAlap
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
            this.canvas = new System.Windows.Forms.PictureBox();
            this.isClosedArc = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.isWeightGlobal = new System.Windows.Forms.CheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.isFilled = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.Location = new System.Drawing.Point(12, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(733, 540);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // isClosedArc
            // 
            this.isClosedArc.AutoSize = true;
            this.isClosedArc.Location = new System.Drawing.Point(752, 13);
            this.isClosedArc.Name = "isClosedArc";
            this.isClosedArc.Size = new System.Drawing.Size(58, 17);
            this.isClosedArc.TabIndex = 1;
            this.isClosedArc.Text = "Closed";
            this.isClosedArc.UseVisualStyleBackColor = true;
            this.isClosedArc.CheckedChanged += new System.EventHandler(this.isClosed_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(767, 62);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(764, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(767, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Remove last";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // isWeightGlobal
            // 
            this.isWeightGlobal.AutoSize = true;
            this.isWeightGlobal.Checked = true;
            this.isWeightGlobal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isWeightGlobal.Location = new System.Drawing.Point(761, 188);
            this.isWeightGlobal.Name = "isWeightGlobal";
            this.isWeightGlobal.Size = new System.Drawing.Size(98, 17);
            this.isWeightGlobal.TabIndex = 5;
            this.isWeightGlobal.Text = "Global Weights";
            this.isWeightGlobal.UseVisualStyleBackColor = true;
            this.isWeightGlobal.CheckedChanged += new System.EventHandler(this.isWeightGlobal_CheckedChanged);
            // 
            // listBox1
            // 
            this.listBox1.Enabled = false;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(761, 211);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(98, 342);
            this.listBox1.TabIndex = 6;
            // 
            // isFilled
            // 
            this.isFilled.AutoSize = true;
            this.isFilled.Enabled = false;
            this.isFilled.Location = new System.Drawing.Point(752, 37);
            this.isFilled.Name = "isFilled";
            this.isFilled.Size = new System.Drawing.Size(50, 17);
            this.isFilled.TabIndex = 7;
            this.isFilled.Text = "Filled";
            this.isFilled.UseVisualStyleBackColor = true;
            this.isFilled.CheckedChanged += new System.EventHandler(this.isFilled_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 564);
            this.Controls.Add(this.isFilled);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.isWeightGlobal);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.isClosedArc);
            this.Controls.Add(this.canvas);
            this.Name = "Form1";
            this.Text = "Hermite Ívek";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.CheckBox isClosedArc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox isWeightGlobal;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.CheckBox isFilled;
    }
}

