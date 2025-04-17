namespace PresentationLayer.Views.Custom_Message_Box
{
    partial class DialogBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogBox));
            panel1 = new Panel();
            panel4 = new Panel();
            lblMessage = new Label();
            panel3 = new Panel();
            pnlBtn1 = new Panel();
            btn1 = new MaterialSkin.Controls.MaterialButton();
            pnlBtn2 = new Panel();
            btn2 = new MaterialSkin.Controls.MaterialButton();
            pnlBtn3 = new Panel();
            btn3 = new MaterialSkin.Controls.MaterialButton();
            panel2 = new Panel();
            lblTitle = new Label();
            pbIcon = new PictureBox();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            pnlBtn1.SuspendLayout();
            pnlBtn2.SuspendLayout();
            pnlBtn3.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbIcon).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(20, 20);
            panel1.Name = "panel1";
            panel1.Size = new Size(434, 275);
            panel1.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.BackColor = Color.White;
            panel4.Controls.Add(lblMessage);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 80);
            panel4.Margin = new Padding(0, 10, 0, 10);
            panel4.Name = "panel4";
            panel4.Size = new Size(434, 148);
            panel4.TabIndex = 2;
            // 
            // lblMessage
            // 
            lblMessage.Font = new Font("Poppins", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMessage.Location = new Point(0, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(434, 140);
            lblMessage.TabIndex = 0;
            lblMessage.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(pnlBtn1);
            panel3.Controls.Add(pnlBtn2);
            panel3.Controls.Add(pnlBtn3);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 228);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(434, 47);
            panel3.TabIndex = 1;
            // 
            // pnlBtn1
            // 
            pnlBtn1.BackColor = Color.White;
            pnlBtn1.Controls.Add(btn1);
            pnlBtn1.Location = new Point(290, 0);
            pnlBtn1.Margin = new Padding(0);
            pnlBtn1.Name = "pnlBtn1";
            pnlBtn1.Padding = new Padding(5, 0, 5, 0);
            pnlBtn1.Size = new Size(144, 47);
            pnlBtn1.TabIndex = 2;
            // 
            // btn1
            // 
            btn1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btn1.Depth = 0;
            btn1.Dock = DockStyle.Fill;
            btn1.HighEmphasis = true;
            btn1.Icon = null;
            btn1.Location = new Point(5, 0);
            btn1.Margin = new Padding(4, 6, 4, 6);
            btn1.MouseState = MaterialSkin.MouseState.HOVER;
            btn1.Name = "btn1";
            btn1.NoAccentTextColor = Color.Empty;
            btn1.Size = new Size(134, 47);
            btn1.TabIndex = 1;
            btn1.Text = "Ok";
            btn1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btn1.UseAccentColor = false;
            btn1.UseVisualStyleBackColor = true;
            // 
            // pnlBtn2
            // 
            pnlBtn2.BackColor = Color.White;
            pnlBtn2.Controls.Add(btn2);
            pnlBtn2.Location = new Point(145, 0);
            pnlBtn2.Margin = new Padding(0);
            pnlBtn2.Name = "pnlBtn2";
            pnlBtn2.Padding = new Padding(5, 0, 5, 0);
            pnlBtn2.Size = new Size(144, 47);
            pnlBtn2.TabIndex = 1;
            // 
            // btn2
            // 
            btn2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn2.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btn2.Depth = 0;
            btn2.Dock = DockStyle.Fill;
            btn2.HighEmphasis = true;
            btn2.Icon = null;
            btn2.Location = new Point(5, 0);
            btn2.Margin = new Padding(4, 6, 4, 6);
            btn2.MouseState = MaterialSkin.MouseState.HOVER;
            btn2.Name = "btn2";
            btn2.NoAccentTextColor = Color.Empty;
            btn2.Size = new Size(134, 47);
            btn2.TabIndex = 1;
            btn2.Text = "Cancel";
            btn2.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btn2.UseAccentColor = false;
            btn2.UseVisualStyleBackColor = true;
            // 
            // pnlBtn3
            // 
            pnlBtn3.BackColor = Color.White;
            pnlBtn3.Controls.Add(btn3);
            pnlBtn3.Location = new Point(0, 0);
            pnlBtn3.Margin = new Padding(0);
            pnlBtn3.Name = "pnlBtn3";
            pnlBtn3.Padding = new Padding(5, 0, 5, 0);
            pnlBtn3.Size = new Size(144, 47);
            pnlBtn3.TabIndex = 0;
            // 
            // btn3
            // 
            btn3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn3.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btn3.Depth = 0;
            btn3.Dock = DockStyle.Fill;
            btn3.HighEmphasis = true;
            btn3.Icon = null;
            btn3.Location = new Point(5, 0);
            btn3.Margin = new Padding(4, 6, 4, 6);
            btn3.MouseState = MaterialSkin.MouseState.HOVER;
            btn3.Name = "btn3";
            btn3.NoAccentTextColor = Color.Empty;
            btn3.Size = new Size(134, 47);
            btn3.TabIndex = 0;
            btn3.Text = "Close";
            btn3.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btn3.UseAccentColor = false;
            btn3.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(lblTitle);
            panel2.Controls.Add(pbIcon);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(434, 80);
            panel2.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 33.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(66, -1);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(121, 61);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Title";
            // 
            // pbIcon
            // 
            pbIcon.Location = new Point(0, 0);
            pbIcon.Name = "pbIcon";
            pbIcon.Size = new Size(60, 60);
            pbIcon.SizeMode = PictureBoxSizeMode.Zoom;
            pbIcon.TabIndex = 0;
            pbIcon.TabStop = false;
            // 
            // DialogBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(474, 315);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DialogBox";
            Padding = new Padding(20);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Text";
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel3.ResumeLayout(false);
            pnlBtn1.ResumeLayout(false);
            pnlBtn1.PerformLayout();
            pnlBtn2.ResumeLayout(false);
            pnlBtn2.PerformLayout();
            pnlBtn3.ResumeLayout(false);
            pnlBtn3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel4;
        private Panel panel3;
        private Panel panel2;
        private Label lblTitle;
        private PictureBox pbIcon;
        private Label lblMessage;
        private Panel pnlBtn1;
        private Panel pnlBtn2;
        private Panel pnlBtn3;
        private MaterialSkin.Controls.MaterialButton btn2;
        private MaterialSkin.Controls.MaterialButton btn3;
        private MaterialSkin.Controls.MaterialButton btn1;
    }
}