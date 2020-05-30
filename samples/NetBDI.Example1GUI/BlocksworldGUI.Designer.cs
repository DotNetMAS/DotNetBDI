using System.Drawing;
using System.Windows.Forms;

namespace NetBDI.Example1GUI
{
    partial class BlocksworldGUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private PictureBox _pictureBox1;
        private PictureBox _pictureBox2;

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
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Blocksworld";

            _pictureBox1 = new PictureBox
            {
                Size = new Size(375, 400),
                Location = new Point(25, 25)
            };

            _pictureBox2 = new PictureBox
            {
                Size = new Size(375, 400),
                Location = new Point(400, 25)
            };
            this.Controls.Add(_pictureBox1);
            this.Controls.Add(_pictureBox2);
        }

        #endregion
    }
}

