
using System.Windows.Forms;

namespace GalleryManager {
    partial class MainWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.titlebarLayout = new System.Windows.Forms.TableLayoutPanel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.iconMinimize = new System.Windows.Forms.PictureBox();
            this.iconMaximize = new System.Windows.Forms.PictureBox();
            this.iconClose = new System.Windows.Forms.PictureBox();
            this.mainLayout.SuspendLayout();
            this.titlebarLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconMaximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconClose)).BeginInit();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.titlebarLayout, 0, 0);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Margin = new System.Windows.Forms.Padding(0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 2;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Size = new System.Drawing.Size(1000, 600);
            this.mainLayout.TabIndex = 0;
            // 
            // titlebarLayout
            // 
            this.titlebarLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.titlebarLayout.ColumnCount = 4;
            this.titlebarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.titlebarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.titlebarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.titlebarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.titlebarLayout.Controls.Add(this.titleLabel, 0, 0);
            this.titlebarLayout.Controls.Add(this.iconMinimize, 1, 0);
            this.titlebarLayout.Controls.Add(this.iconMaximize, 2, 0);
            this.titlebarLayout.Controls.Add(this.iconClose, 3, 0);
            this.titlebarLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titlebarLayout.Location = new System.Drawing.Point(0, 0);
            this.titlebarLayout.Margin = new System.Windows.Forms.Padding(0);
            this.titlebarLayout.Name = "titlebarLayout";
            this.titlebarLayout.RowCount = 1;
            this.titlebarLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.titlebarLayout.Size = new System.Drawing.Size(1000, 35);
            this.titlebarLayout.TabIndex = 0;
            this.titlebarLayout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Navbar_MouseDown);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.titleLabel.Location = new System.Drawing.Point(20, 0);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.titleLabel.Size = new System.Drawing.Size(875, 35);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Gallery Manager";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.titleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Navbar_MouseDown);
            // 
            // iconMinimize
            // 
            this.iconMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconMinimize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconMinimize.Image = global::GalleryManager.Properties.Resources.minus;
            this.iconMinimize.Location = new System.Drawing.Point(905, 10);
            this.iconMinimize.Margin = new System.Windows.Forms.Padding(10);
            this.iconMinimize.Name = "iconMinimize";
            this.iconMinimize.Size = new System.Drawing.Size(15, 15);
            this.iconMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconMinimize.TabIndex = 1;
            this.iconMinimize.TabStop = false;
            this.iconMinimize.Click += new System.EventHandler(this.IconMinimize_Click);
            // 
            // iconMaximize
            // 
            this.iconMaximize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconMaximize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconMaximize.Image = global::GalleryManager.Properties.Resources.menu_alt_4;
            this.iconMaximize.Location = new System.Drawing.Point(940, 10);
            this.iconMaximize.Margin = new System.Windows.Forms.Padding(10);
            this.iconMaximize.Name = "iconMaximize";
            this.iconMaximize.Size = new System.Drawing.Size(15, 15);
            this.iconMaximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconMaximize.TabIndex = 2;
            this.iconMaximize.TabStop = false;
            this.iconMaximize.Click += new System.EventHandler(this.IconMaximize_Click);
            // 
            // iconClose
            // 
            this.iconClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconClose.Image = global::GalleryManager.Properties.Resources.x;
            this.iconClose.Location = new System.Drawing.Point(975, 10);
            this.iconClose.Margin = new System.Windows.Forms.Padding(10);
            this.iconClose.Name = "iconClose";
            this.iconClose.Size = new System.Drawing.Size(15, 15);
            this.iconClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconClose.TabIndex = 3;
            this.iconClose.TabStop = false;
            this.iconClose.Click += new System.EventHandler(this.IconClose_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.ControlBox = false;
            this.Controls.Add(this.mainLayout);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.Text = "MainWindow";
            this.mainLayout.ResumeLayout(false);
            this.titlebarLayout.ResumeLayout(false);
            this.titlebarLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconMaximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel titlebarLayout;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.PictureBox iconMinimize;
        private PictureBox iconMaximize;
        private PictureBox iconClose;
    }
}