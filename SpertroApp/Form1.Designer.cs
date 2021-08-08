namespace SpertroApp
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CCDImage = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.DrawCanvas = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ROIImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CCDImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrawCanvas)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ROIImage)).BeginInit();
            this.SuspendLayout();
            // 
            // CCDImage
            // 
            this.CCDImage.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CCDImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CCDImage.Cursor = System.Windows.Forms.Cursors.Cross;
            this.CCDImage.Location = new System.Drawing.Point(2, 2);
            this.CCDImage.Margin = new System.Windows.Forms.Padding(0);
            this.CCDImage.Name = "CCDImage";
            this.CCDImage.Size = new System.Drawing.Size(480, 288);
            this.CCDImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CCDImage.TabIndex = 1;
            this.CCDImage.TabStop = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(2, 401);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(96, 53);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DrawCanvas
            // 
            this.DrawCanvas.BackColor = System.Drawing.Color.Transparent;
            this.DrawCanvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DrawCanvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DrawCanvas.Location = new System.Drawing.Point(36, 38);
            this.DrawCanvas.Margin = new System.Windows.Forms.Padding(0);
            this.DrawCanvas.MinimumSize = new System.Drawing.Size(8, 9);
            this.DrawCanvas.Name = "DrawCanvas";
            this.DrawCanvas.Size = new System.Drawing.Size(63, 38);
            this.DrawCanvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DrawCanvas.TabIndex = 4;
            this.DrawCanvas.TabStop = false;
            this.DrawCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawCanvas_MouseDown);
            this.DrawCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawCanvas_MouseMove);
            this.DrawCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawCanvas_MouseUp);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.ROIImage);
            this.panel1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Location = new System.Drawing.Point(2, 458);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 316);
            this.panel1.TabIndex = 6;
            // 
            // ROIImage
            // 
            this.ROIImage.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ROIImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ROIImage.Cursor = System.Windows.Forms.Cursors.Cross;
            this.ROIImage.Location = new System.Drawing.Point(-44, 14);
            this.ROIImage.Margin = new System.Windows.Forms.Padding(0);
            this.ROIImage.Name = "ROIImage";
            this.ROIImage.Size = new System.Drawing.Size(480, 288);
            this.ROIImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ROIImage.TabIndex = 6;
            this.ROIImage.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1171, 801);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DrawCanvas);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.CCDImage);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.CCDImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrawCanvas)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ROIImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox CCDImage;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox DrawCanvas;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox ROIImage;

    }
}

