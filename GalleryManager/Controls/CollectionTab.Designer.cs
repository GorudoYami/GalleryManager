
namespace GalleryManager.Controls {
    partial class CollectionTab {
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
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("examplevid.mp4");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("2021-06", new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("examplepic.png");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("2021-05", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.contentPanelLayout0 = new System.Windows.Forms.TableLayoutPanel();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.infoPanelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cdateLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.label0 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolbarLayout = new System.Windows.Forms.TableLayoutPanel();
            this.iconPanel1 = new GalleryManager.Controls.IconPanel();
            this.iconPanel2 = new GalleryManager.Controls.IconPanel();
            this.iconPanel3 = new GalleryManager.Controls.IconPanel();
            this.iconPanel4 = new GalleryManager.Controls.IconPanel();
            this.iconPanel5 = new GalleryManager.Controls.IconPanel();
            this.iconPanel6 = new GalleryManager.Controls.IconPanel();
            this.treeView = new System.Windows.Forms.TreeView();
            this.contentPanelLayout0.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.infoPanelLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            this.toolbarLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // contentPanelLayout0
            // 
            this.contentPanelLayout0.ColumnCount = 2;
            this.contentPanelLayout0.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contentPanelLayout0.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.contentPanelLayout0.Controls.Add(this.infoPanel, 1, 0);
            this.contentPanelLayout0.Controls.Add(this.toolbarLayout, 0, 0);
            this.contentPanelLayout0.Controls.Add(this.treeView, 0, 1);
            this.contentPanelLayout0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanelLayout0.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.contentPanelLayout0.Location = new System.Drawing.Point(0, 0);
            this.contentPanelLayout0.Margin = new System.Windows.Forms.Padding(0);
            this.contentPanelLayout0.Name = "contentPanelLayout0";
            this.contentPanelLayout0.RowCount = 2;
            this.contentPanelLayout0.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.contentPanelLayout0.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contentPanelLayout0.Size = new System.Drawing.Size(800, 550);
            this.contentPanelLayout0.TabIndex = 1;
            // 
            // infoPanel
            // 
            this.infoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.infoPanel.Controls.Add(this.infoPanelLayout);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel.Location = new System.Drawing.Point(500, 0);
            this.infoPanel.Margin = new System.Windows.Forms.Padding(0);
            this.infoPanel.Name = "infoPanel";
            this.contentPanelLayout0.SetRowSpan(this.infoPanel, 2);
            this.infoPanel.Size = new System.Drawing.Size(300, 550);
            this.infoPanel.TabIndex = 0;
            // 
            // infoPanelLayout
            // 
            this.infoPanelLayout.AutoSize = true;
            this.infoPanelLayout.ColumnCount = 2;
            this.infoPanelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.infoPanelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.infoPanelLayout.Controls.Add(this.label7, 1, 7);
            this.infoPanelLayout.Controls.Add(this.label6, 0, 7);
            this.infoPanelLayout.Controls.Add(this.cdateLabel, 1, 6);
            this.infoPanelLayout.Controls.Add(this.label5, 0, 6);
            this.infoPanelLayout.Controls.Add(this.lengthLabel, 1, 5);
            this.infoPanelLayout.Controls.Add(this.sizeLabel, 1, 4);
            this.infoPanelLayout.Controls.Add(this.typeLabel, 1, 3);
            this.infoPanelLayout.Controls.Add(this.nameLabel, 1, 2);
            this.infoPanelLayout.Controls.Add(this.label4, 0, 5);
            this.infoPanelLayout.Controls.Add(this.label3, 0, 4);
            this.infoPanelLayout.Controls.Add(this.previewBox, 0, 1);
            this.infoPanelLayout.Controls.Add(this.label0, 0, 0);
            this.infoPanelLayout.Controls.Add(this.label1, 0, 2);
            this.infoPanelLayout.Controls.Add(this.label2, 0, 3);
            this.infoPanelLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanelLayout.Location = new System.Drawing.Point(0, 0);
            this.infoPanelLayout.Margin = new System.Windows.Forms.Padding(0);
            this.infoPanelLayout.Name = "infoPanelLayout";
            this.infoPanelLayout.Padding = new System.Windows.Forms.Padding(10);
            this.infoPanelLayout.RowCount = 9;
            this.infoPanelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.infoPanelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.infoPanelLayout.Size = new System.Drawing.Size(300, 550);
            this.infoPanelLayout.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(150, 470);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 35);
            this.label7.TabIndex = 13;
            this.label7.Text = "30.11.2021";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(10, 470);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 35);
            this.label6.TabIndex = 12;
            this.label6.Text = "Modification date:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdateLabel
            // 
            this.cdateLabel.AutoSize = true;
            this.cdateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cdateLabel.Location = new System.Drawing.Point(150, 435);
            this.cdateLabel.Margin = new System.Windows.Forms.Padding(0);
            this.cdateLabel.Name = "cdateLabel";
            this.cdateLabel.Size = new System.Drawing.Size(140, 35);
            this.cdateLabel.TabIndex = 11;
            this.cdateLabel.Text = "14.08.2020";
            this.cdateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(10, 435);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 35);
            this.label5.TabIndex = 10;
            this.label5.Text = "Creation date:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lengthLabel
            // 
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lengthLabel.Location = new System.Drawing.Point(150, 400);
            this.lengthLabel.Margin = new System.Windows.Forms.Padding(0);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size(140, 35);
            this.lengthLabel.TabIndex = 9;
            this.lengthLabel.Text = "00:00:00";
            this.lengthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sizeLabel.Location = new System.Drawing.Point(150, 365);
            this.sizeLabel.Margin = new System.Windows.Forms.Padding(0);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(140, 35);
            this.sizeLabel.TabIndex = 8;
            this.sizeLabel.Text = "104 MB";
            this.sizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeLabel.Location = new System.Drawing.Point(150, 330);
            this.typeLabel.Margin = new System.Windows.Forms.Padding(0);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(140, 35);
            this.typeLabel.TabIndex = 7;
            this.typeLabel.Text = "MP4 Video";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameLabel.Location = new System.Drawing.Point(150, 295);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(140, 35);
            this.nameLabel.TabIndex = 6;
            this.nameLabel.Text = "example.mp4";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(10, 400);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 35);
            this.label4.TabIndex = 5;
            this.label4.Text = "Length:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(10, 365);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 35);
            this.label3.TabIndex = 4;
            this.label3.Text = "Size:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // previewBox
            // 
            this.infoPanelLayout.SetColumnSpan(this.previewBox, 2);
            this.previewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewBox.Image = global::GalleryManager.Properties.Resources.example;
            this.previewBox.Location = new System.Drawing.Point(10, 45);
            this.previewBox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(280, 240);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewBox.TabIndex = 0;
            this.previewBox.TabStop = false;
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.infoPanelLayout.SetColumnSpan(this.label0, 2);
            this.label0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label0.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.label0.Location = new System.Drawing.Point(10, 10);
            this.label0.Margin = new System.Windows.Forms.Padding(0);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(280, 35);
            this.label0.TabIndex = 1;
            this.label0.Text = "Preview";
            this.label0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(10, 295);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(10, 330);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 35);
            this.label2.TabIndex = 3;
            this.label2.Text = "Type:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolbarLayout
            // 
            this.toolbarLayout.ColumnCount = 10;
            this.toolbarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.toolbarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.toolbarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.toolbarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.toolbarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.toolbarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.toolbarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.toolbarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.toolbarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.toolbarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.toolbarLayout.Controls.Add(this.iconPanel1, 0, 0);
            this.toolbarLayout.Controls.Add(this.iconPanel2, 1, 0);
            this.toolbarLayout.Controls.Add(this.iconPanel3, 2, 0);
            this.toolbarLayout.Controls.Add(this.iconPanel4, 3, 0);
            this.toolbarLayout.Controls.Add(this.iconPanel5, 4, 0);
            this.toolbarLayout.Controls.Add(this.iconPanel6, 5, 0);
            this.toolbarLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolbarLayout.Location = new System.Drawing.Point(6, 0);
            this.toolbarLayout.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.toolbarLayout.Name = "toolbarLayout";
            this.toolbarLayout.RowCount = 1;
            this.toolbarLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.toolbarLayout.Size = new System.Drawing.Size(494, 35);
            this.toolbarLayout.TabIndex = 1;
            // 
            // iconPanel1
            // 
            this.iconPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.iconPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconPanel1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.iconPanel1.Image = global::GalleryManager.Properties.Resources.folder_add;
            this.iconPanel1.Location = new System.Drawing.Point(0, 0);
            this.iconPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.iconPanel1.Name = "iconPanel1";
            this.iconPanel1.Padding = new System.Windows.Forms.Padding(8);
            this.iconPanel1.Size = new System.Drawing.Size(35, 35);
            this.iconPanel1.TabIndex = 5;
            // 
            // iconPanel2
            // 
            this.iconPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.iconPanel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconPanel2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.iconPanel2.Image = global::GalleryManager.Properties.Resources.trash;
            this.iconPanel2.Location = new System.Drawing.Point(35, 0);
            this.iconPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.iconPanel2.Name = "iconPanel2";
            this.iconPanel2.Padding = new System.Windows.Forms.Padding(8);
            this.iconPanel2.Size = new System.Drawing.Size(35, 35);
            this.iconPanel2.TabIndex = 6;
            // 
            // iconPanel3
            // 
            this.iconPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.iconPanel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconPanel3.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.iconPanel3.Image = global::GalleryManager.Properties.Resources.document_duplicate;
            this.iconPanel3.Location = new System.Drawing.Point(70, 0);
            this.iconPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.iconPanel3.Name = "iconPanel3";
            this.iconPanel3.Padding = new System.Windows.Forms.Padding(8);
            this.iconPanel3.Size = new System.Drawing.Size(35, 35);
            this.iconPanel3.TabIndex = 7;
            // 
            // iconPanel4
            // 
            this.iconPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.iconPanel4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconPanel4.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.iconPanel4.Image = global::GalleryManager.Properties.Resources.search;
            this.iconPanel4.Location = new System.Drawing.Point(105, 0);
            this.iconPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.iconPanel4.Name = "iconPanel4";
            this.iconPanel4.Padding = new System.Windows.Forms.Padding(8);
            this.iconPanel4.Size = new System.Drawing.Size(35, 35);
            this.iconPanel4.TabIndex = 8;
            // 
            // iconPanel5
            // 
            this.iconPanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.iconPanel5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconPanel5.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.iconPanel5.Image = global::GalleryManager.Properties.Resources.star;
            this.iconPanel5.Location = new System.Drawing.Point(140, 0);
            this.iconPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.iconPanel5.Name = "iconPanel5";
            this.iconPanel5.Padding = new System.Windows.Forms.Padding(8);
            this.iconPanel5.Size = new System.Drawing.Size(35, 35);
            this.iconPanel5.TabIndex = 9;
            // 
            // iconPanel6
            // 
            this.iconPanel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.iconPanel6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconPanel6.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.iconPanel6.Image = global::GalleryManager.Properties.Resources.refresh;
            this.iconPanel6.Location = new System.Drawing.Point(175, 0);
            this.iconPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.iconPanel6.Name = "iconPanel6";
            this.iconPanel6.Padding = new System.Windows.Forms.Padding(8);
            this.iconPanel6.Size = new System.Drawing.Size(35, 35);
            this.iconPanel6.TabIndex = 10;
            // 
            // treeView
            // 
            this.treeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.treeView.ForeColor = System.Drawing.Color.White;
            this.treeView.FullRowSelect = true;
            this.treeView.Indent = 20;
            this.treeView.ItemHeight = 25;
            this.treeView.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.treeView.Location = new System.Drawing.Point(10, 35);
            this.treeView.Margin = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.treeView.Name = "treeView";
            treeNode5.Name = "Node1";
            treeNode5.Text = "examplevid.mp4";
            treeNode6.Name = "Node0";
            treeNode6.Text = "2021-06";
            treeNode7.Name = "Node2";
            treeNode7.Text = "examplepic.png";
            treeNode8.Name = "Node5";
            treeNode8.Text = "2021-05";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode8});
            this.treeView.ShowLines = false;
            this.treeView.Size = new System.Drawing.Size(480, 505);
            this.treeView.TabIndex = 2;
            // 
            // CollectionTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.Controls.Add(this.contentPanelLayout0);
            this.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "CollectionTab";
            this.Size = new System.Drawing.Size(800, 550);
            this.contentPanelLayout0.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.infoPanelLayout.ResumeLayout(false);
            this.infoPanelLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.toolbarLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel contentPanelLayout0;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.TableLayoutPanel infoPanelLayout;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label cdateLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel toolbarLayout;
        private System.Windows.Forms.TreeView treeView;
        private IconPanel iconPanel1;
        private IconPanel iconPanel2;
        private IconPanel iconPanel3;
        private IconPanel iconPanel4;
        private IconPanel iconPanel5;
        private IconPanel iconPanel6;
    }
}
