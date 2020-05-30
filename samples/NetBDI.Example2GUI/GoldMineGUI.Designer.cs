using System.Drawing;
using System.Windows.Forms;

namespace NetBDI.Example2GUI
{
    partial class GoldMineGUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private PictureBox _pictureBox1;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 820);
            this.Text = "GoldMineGUI";
            _pictureBox1 = new PictureBox
            {
                Size = new Size(800, 800),
                Location = new Point(10, 10)
            };
            this.Controls.Add(_pictureBox1);
        }

        #endregion
    }
}

