
namespace GalleryManager.Controls {
    partial class TabMenu {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.layout0 = new System.Windows.Forms.TableLayoutPanel();
            this.iconPanel0 = new GalleryManager.Controls.IconPanel();
            this.menuOption4 = new GalleryManager.Controls.MenuOption();
            this.menuOption3 = new GalleryManager.Controls.MenuOption();
            this.menuOption2 = new GalleryManager.Controls.MenuOption();
            this.menuOption1 = new GalleryManager.Controls.MenuOption();
            this.menuOption0 = new GalleryManager.Controls.MenuOption();
            this.mainLayout.SuspendLayout();
            this.layout0.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.mainLayout.ColumnCount = 2;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.layout0, 0, 0);
            this.mainLayout.Controls.Add(this.menuOption4, 0, 5);
            this.mainLayout.Controls.Add(this.menuOption3, 0, 4);
            this.mainLayout.Controls.Add(this.menuOption2, 0, 3);
            this.mainLayout.Controls.Add(this.menuOption1, 0, 2);
            this.mainLayout.Controls.Add(this.menuOption0, 0, 1);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Margin = new System.Windows.Forms.Padding(0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 7;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Size = new System.Drawing.Size(1000, 565);
            this.mainLayout.TabIndex = 1;
            // 
            // layout0
            // 
            this.layout0.ColumnCount = 2;
            this.layout0.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.layout0.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout0.Controls.Add(this.iconPanel0, 0, 0);
            this.layout0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.layout0.Location = new System.Drawing.Point(0, 0);
            this.layout0.Margin = new System.Windows.Forms.Padding(0);
            this.layout0.Name = "layout0";
            this.layout0.RowCount = 1;
            this.layout0.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout0.Size = new System.Drawing.Size(160, 60);
            this.layout0.TabIndex = 2;
            // 
            // iconPanel0
            // 
            this.iconPanel0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.iconPanel0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconPanel0.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.iconPanel0.Image = global::GalleryManager.Properties.Resources.menu;
            this.iconPanel0.Location = new System.Drawing.Point(0, 0);
            this.iconPanel0.Margin = new System.Windows.Forms.Padding(0);
            this.iconPanel0.Name = "iconPanel0";
            this.iconPanel0.Padding = new System.Windows.Forms.Padding(10);
            this.iconPanel0.Size = new System.Drawing.Size(60, 60);
            this.iconPanel0.TabIndex = 0;
            this.iconPanel0.Click += new System.EventHandler(this.MenuIcon_Click);
            this.iconPanel0.DoubleClick += new System.EventHandler(this.MenuIcon_Click);
            // 
            // menuOption4
            // 
            this.menuOption4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.menuOption4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuOption4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuOption4.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.menuOption4.ForeColor = System.Drawing.Color.White;
            this.menuOption4.Image = global::GalleryManager.Properties.Resources.information_circle;
            this.menuOption4.Index = 4;
            this.menuOption4.Location = new System.Drawing.Point(0, 300);
            this.menuOption4.Margin = new System.Windows.Forms.Padding(0);
            this.menuOption4.Name = "menuOption4";
            this.menuOption4.OptionText = "Info";
            this.menuOption4.Size = new System.Drawing.Size(160, 60);
            this.menuOption4.TabIndex = 3;
            this.menuOption4.Click += new System.EventHandler(this.MenuOption_Click);
            this.menuOption4.DoubleClick += new System.EventHandler(this.MenuOption_Click);
            // 
            // menuOption3
            // 
            this.menuOption3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.menuOption3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuOption3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuOption3.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.menuOption3.ForeColor = System.Drawing.Color.White;
            this.menuOption3.Image = global::GalleryManager.Properties.Resources.cog;
            this.menuOption3.Index = 3;
            this.menuOption3.Location = new System.Drawing.Point(0, 240);
            this.menuOption3.Margin = new System.Windows.Forms.Padding(0);
            this.menuOption3.Name = "menuOption3";
            this.menuOption3.OptionText = "Options";
            this.menuOption3.Size = new System.Drawing.Size(160, 60);
            this.menuOption3.TabIndex = 4;
            this.menuOption3.Click += new System.EventHandler(this.MenuOption_Click);
            this.menuOption3.DoubleClick += new System.EventHandler(this.MenuOption_Click);
            // 
            // menuOption2
            // 
            this.menuOption2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.menuOption2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuOption2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuOption2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.menuOption2.ForeColor = System.Drawing.Color.White;
            this.menuOption2.Image = global::GalleryManager.Properties.Resources.document_search;
            this.menuOption2.Index = 2;
            this.menuOption2.Location = new System.Drawing.Point(0, 180);
            this.menuOption2.Margin = new System.Windows.Forms.Padding(0);
            this.menuOption2.Name = "menuOption2";
            this.menuOption2.OptionText = "Duplicates";
            this.menuOption2.Size = new System.Drawing.Size(160, 60);
            this.menuOption2.TabIndex = 5;
            this.menuOption2.Click += new System.EventHandler(this.MenuOption_Click);
            this.menuOption2.DoubleClick += new System.EventHandler(this.MenuOption_Click);
            // 
            // menuOption1
            // 
            this.menuOption1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.menuOption1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuOption1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuOption1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.menuOption1.ForeColor = System.Drawing.Color.White;
            this.menuOption1.Image = global::GalleryManager.Properties.Resources.save_as;
            this.menuOption1.Index = 1;
            this.menuOption1.Location = new System.Drawing.Point(0, 120);
            this.menuOption1.Margin = new System.Windows.Forms.Padding(0);
            this.menuOption1.Name = "menuOption1";
            this.menuOption1.OptionText = "Import";
            this.menuOption1.Size = new System.Drawing.Size(160, 60);
            this.menuOption1.TabIndex = 6;
            this.menuOption1.Click += new System.EventHandler(this.MenuOption_Click);
            this.menuOption1.DoubleClick += new System.EventHandler(this.MenuOption_Click);
            // 
            // menuOption0
            // 
            this.menuOption0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.menuOption0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuOption0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuOption0.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.menuOption0.ForeColor = System.Drawing.Color.White;
            this.menuOption0.Image = global::GalleryManager.Properties.Resources.collection;
            this.menuOption0.Index = 0;
            this.menuOption0.Location = new System.Drawing.Point(0, 60);
            this.menuOption0.Margin = new System.Windows.Forms.Padding(0);
            this.menuOption0.Name = "menuOption0";
            this.menuOption0.OptionText = "Collection";
            this.menuOption0.Size = new System.Drawing.Size(160, 60);
            this.menuOption0.TabIndex = 7;
            this.menuOption0.Click += new System.EventHandler(this.MenuOption_Click);
            this.menuOption0.DoubleClick += new System.EventHandler(this.MenuOption_Click);
            // 
            // TabMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.Controls.Add(this.mainLayout);
            this.Name = "TabMenu";
            this.Size = new System.Drawing.Size(1000, 565);
            this.mainLayout.ResumeLayout(false);
            this.layout0.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel layout0;
        private IconPanel iconPanel0;
        private MenuOption menuOption4;
        private MenuOption menuOption3;
        private MenuOption menuOption2;
        private MenuOption menuOption1;
        private MenuOption menuOption0;
    }
}
