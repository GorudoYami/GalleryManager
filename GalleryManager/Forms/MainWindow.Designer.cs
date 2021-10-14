﻿
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
            this.navbarLayout = new System.Windows.Forms.TableLayoutPanel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.iconMinimize = new System.Windows.Forms.PictureBox();
            this.iconMaximize = new System.Windows.Forms.PictureBox();
            this.iconClose = new System.Windows.Forms.PictureBox();
            this.contentLayout = new System.Windows.Forms.TableLayoutPanel();
            this.menuLayout = new System.Windows.Forms.TableLayoutPanel();
            this.infoIconPanel = new System.Windows.Forms.Panel();
            this.infoIcon = new System.Windows.Forms.PictureBox();
            this.optionsIconPanel = new System.Windows.Forms.Panel();
            this.optionsIcon = new System.Windows.Forms.PictureBox();
            this.duplicatesIconPanel = new System.Windows.Forms.Panel();
            this.duplicatesIcon = new System.Windows.Forms.PictureBox();
            this.importIconPanel = new System.Windows.Forms.Panel();
            this.importIcon = new System.Windows.Forms.PictureBox();
            this.collectionLabel = new System.Windows.Forms.Label();
            this.importLabel = new System.Windows.Forms.Label();
            this.duplicatesLabel = new System.Windows.Forms.Label();
            this.optionsLabel = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.collectionIconPanel = new System.Windows.Forms.Panel();
            this.collectionIcon = new System.Windows.Forms.PictureBox();
            this.menuIconPanel = new System.Windows.Forms.Panel();
            this.menuIcon = new System.Windows.Forms.PictureBox();
            this.contentPanel0 = new System.Windows.Forms.Panel();
            this.contentPanelLayout0 = new System.Windows.Forms.TableLayoutPanel();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.toolbarLayout = new System.Windows.Forms.TableLayoutPanel();
            this.iconAddDirectory = new System.Windows.Forms.PictureBox();
            this.iconDelete = new System.Windows.Forms.PictureBox();
            this.iconCopy = new System.Windows.Forms.PictureBox();
            this.iconSearch = new System.Windows.Forms.PictureBox();
            this.iconStar = new System.Windows.Forms.PictureBox();
            this.mainLayout.SuspendLayout();
            this.navbarLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconMaximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconClose)).BeginInit();
            this.contentLayout.SuspendLayout();
            this.menuLayout.SuspendLayout();
            this.infoIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoIcon)).BeginInit();
            this.optionsIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optionsIcon)).BeginInit();
            this.duplicatesIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.duplicatesIcon)).BeginInit();
            this.importIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.importIcon)).BeginInit();
            this.collectionIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.collectionIcon)).BeginInit();
            this.menuIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuIcon)).BeginInit();
            this.contentPanel0.SuspendLayout();
            this.contentPanelLayout0.SuspendLayout();
            this.toolbarLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconAddDirectory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconStar)).BeginInit();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.navbarLayout, 0, 0);
            this.mainLayout.Controls.Add(this.contentLayout, 0, 1);
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
            // navbarLayout
            // 
            this.navbarLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.navbarLayout.ColumnCount = 4;
            this.navbarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.navbarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.navbarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.navbarLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.navbarLayout.Controls.Add(this.titleLabel, 0, 0);
            this.navbarLayout.Controls.Add(this.iconMinimize, 1, 0);
            this.navbarLayout.Controls.Add(this.iconMaximize, 2, 0);
            this.navbarLayout.Controls.Add(this.iconClose, 3, 0);
            this.navbarLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navbarLayout.Location = new System.Drawing.Point(0, 0);
            this.navbarLayout.Margin = new System.Windows.Forms.Padding(0);
            this.navbarLayout.Name = "navbarLayout";
            this.navbarLayout.RowCount = 1;
            this.navbarLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.navbarLayout.Size = new System.Drawing.Size(1000, 35);
            this.navbarLayout.TabIndex = 0;
            this.navbarLayout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Navbar_MouseDown);
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
            // contentLayout
            // 
            this.contentLayout.ColumnCount = 2;
            this.contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.contentLayout.Controls.Add(this.menuLayout, 0, 0);
            this.contentLayout.Controls.Add(this.contentPanel0, 1, 0);
            this.contentLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentLayout.Location = new System.Drawing.Point(0, 35);
            this.contentLayout.Margin = new System.Windows.Forms.Padding(0);
            this.contentLayout.Name = "contentLayout";
            this.contentLayout.RowCount = 1;
            this.contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contentLayout.Size = new System.Drawing.Size(1000, 565);
            this.contentLayout.TabIndex = 1;
            // 
            // menuLayout
            // 
            this.menuLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.menuLayout.ColumnCount = 2;
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.menuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.menuLayout.Controls.Add(this.infoIconPanel, 0, 5);
            this.menuLayout.Controls.Add(this.optionsIconPanel, 0, 4);
            this.menuLayout.Controls.Add(this.duplicatesIconPanel, 0, 3);
            this.menuLayout.Controls.Add(this.importIconPanel, 0, 2);
            this.menuLayout.Controls.Add(this.collectionLabel, 1, 1);
            this.menuLayout.Controls.Add(this.importLabel, 1, 2);
            this.menuLayout.Controls.Add(this.duplicatesLabel, 1, 3);
            this.menuLayout.Controls.Add(this.optionsLabel, 1, 4);
            this.menuLayout.Controls.Add(this.infoLabel, 1, 5);
            this.menuLayout.Controls.Add(this.collectionIconPanel, 0, 1);
            this.menuLayout.Controls.Add(this.menuIconPanel, 0, 0);
            this.menuLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuLayout.Location = new System.Drawing.Point(0, 0);
            this.menuLayout.Margin = new System.Windows.Forms.Padding(0);
            this.menuLayout.Name = "menuLayout";
            this.menuLayout.RowCount = 7;
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.menuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.menuLayout.Size = new System.Drawing.Size(65, 565);
            this.menuLayout.TabIndex = 0;
            // 
            // infoIconPanel
            // 
            this.infoIconPanel.Controls.Add(this.infoIcon);
            this.infoIconPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.infoIconPanel.Location = new System.Drawing.Point(0, 325);
            this.infoIconPanel.Margin = new System.Windows.Forms.Padding(0);
            this.infoIconPanel.Name = "infoIconPanel";
            this.infoIconPanel.Padding = new System.Windows.Forms.Padding(15);
            this.infoIconPanel.Size = new System.Drawing.Size(65, 65);
            this.infoIconPanel.TabIndex = 7;
            this.infoIconPanel.Tag = "4";
            this.infoIconPanel.Click += new System.EventHandler(this.Menu_Click);
            this.infoIconPanel.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.infoIconPanel.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // infoIcon
            // 
            this.infoIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoIcon.Image = global::GalleryManager.Properties.Resources.information_circle;
            this.infoIcon.Location = new System.Drawing.Point(15, 15);
            this.infoIcon.Margin = new System.Windows.Forms.Padding(0);
            this.infoIcon.Name = "infoIcon";
            this.infoIcon.Size = new System.Drawing.Size(35, 35);
            this.infoIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.infoIcon.TabIndex = 5;
            this.infoIcon.TabStop = false;
            this.infoIcon.Tag = "4";
            this.infoIcon.Click += new System.EventHandler(this.Menu_Click);
            this.infoIcon.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.infoIcon.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // optionsIconPanel
            // 
            this.optionsIconPanel.Controls.Add(this.optionsIcon);
            this.optionsIconPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.optionsIconPanel.Location = new System.Drawing.Point(0, 260);
            this.optionsIconPanel.Margin = new System.Windows.Forms.Padding(0);
            this.optionsIconPanel.Name = "optionsIconPanel";
            this.optionsIconPanel.Padding = new System.Windows.Forms.Padding(15);
            this.optionsIconPanel.Size = new System.Drawing.Size(65, 65);
            this.optionsIconPanel.TabIndex = 7;
            this.optionsIconPanel.Tag = "3";
            this.optionsIconPanel.Click += new System.EventHandler(this.Menu_Click);
            this.optionsIconPanel.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.optionsIconPanel.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // optionsIcon
            // 
            this.optionsIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsIcon.Image = global::GalleryManager.Properties.Resources.cog;
            this.optionsIcon.Location = new System.Drawing.Point(15, 15);
            this.optionsIcon.Margin = new System.Windows.Forms.Padding(0);
            this.optionsIcon.Name = "optionsIcon";
            this.optionsIcon.Size = new System.Drawing.Size(35, 35);
            this.optionsIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.optionsIcon.TabIndex = 4;
            this.optionsIcon.TabStop = false;
            this.optionsIcon.Tag = "3";
            this.optionsIcon.Click += new System.EventHandler(this.Menu_Click);
            this.optionsIcon.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.optionsIcon.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // duplicatesIconPanel
            // 
            this.duplicatesIconPanel.Controls.Add(this.duplicatesIcon);
            this.duplicatesIconPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.duplicatesIconPanel.Location = new System.Drawing.Point(0, 195);
            this.duplicatesIconPanel.Margin = new System.Windows.Forms.Padding(0);
            this.duplicatesIconPanel.Name = "duplicatesIconPanel";
            this.duplicatesIconPanel.Padding = new System.Windows.Forms.Padding(15);
            this.duplicatesIconPanel.Size = new System.Drawing.Size(65, 65);
            this.duplicatesIconPanel.TabIndex = 7;
            this.duplicatesIconPanel.Tag = "2";
            this.duplicatesIconPanel.Click += new System.EventHandler(this.Menu_Click);
            this.duplicatesIconPanel.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.duplicatesIconPanel.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // duplicatesIcon
            // 
            this.duplicatesIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.duplicatesIcon.Image = global::GalleryManager.Properties.Resources.document_search;
            this.duplicatesIcon.Location = new System.Drawing.Point(15, 15);
            this.duplicatesIcon.Margin = new System.Windows.Forms.Padding(0);
            this.duplicatesIcon.Name = "duplicatesIcon";
            this.duplicatesIcon.Size = new System.Drawing.Size(35, 35);
            this.duplicatesIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.duplicatesIcon.TabIndex = 3;
            this.duplicatesIcon.TabStop = false;
            this.duplicatesIcon.Tag = "2";
            this.duplicatesIcon.Click += new System.EventHandler(this.Menu_Click);
            this.duplicatesIcon.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.duplicatesIcon.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // importIconPanel
            // 
            this.importIconPanel.Controls.Add(this.importIcon);
            this.importIconPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.importIconPanel.Location = new System.Drawing.Point(0, 130);
            this.importIconPanel.Margin = new System.Windows.Forms.Padding(0);
            this.importIconPanel.Name = "importIconPanel";
            this.importIconPanel.Padding = new System.Windows.Forms.Padding(15);
            this.importIconPanel.Size = new System.Drawing.Size(65, 65);
            this.importIconPanel.TabIndex = 7;
            this.importIconPanel.Tag = "1";
            this.importIconPanel.Click += new System.EventHandler(this.Menu_Click);
            this.importIconPanel.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.importIconPanel.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // importIcon
            // 
            this.importIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importIcon.Image = global::GalleryManager.Properties.Resources.save_as;
            this.importIcon.Location = new System.Drawing.Point(15, 15);
            this.importIcon.Margin = new System.Windows.Forms.Padding(0);
            this.importIcon.Name = "importIcon";
            this.importIcon.Size = new System.Drawing.Size(35, 35);
            this.importIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.importIcon.TabIndex = 2;
            this.importIcon.TabStop = false;
            this.importIcon.Tag = "1";
            this.importIcon.Click += new System.EventHandler(this.Menu_Click);
            this.importIcon.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.importIcon.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // collectionLabel
            // 
            this.collectionLabel.AutoSize = true;
            this.collectionLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.collectionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collectionLabel.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.collectionLabel.Location = new System.Drawing.Point(65, 65);
            this.collectionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.collectionLabel.Name = "collectionLabel";
            this.collectionLabel.Size = new System.Drawing.Size(135, 65);
            this.collectionLabel.TabIndex = 9;
            this.collectionLabel.Tag = "0";
            this.collectionLabel.Text = "Collection";
            this.collectionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.collectionLabel.Click += new System.EventHandler(this.Menu_Click);
            this.collectionLabel.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.collectionLabel.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // importLabel
            // 
            this.importLabel.AutoSize = true;
            this.importLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.importLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importLabel.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.importLabel.Location = new System.Drawing.Point(65, 130);
            this.importLabel.Margin = new System.Windows.Forms.Padding(0);
            this.importLabel.Name = "importLabel";
            this.importLabel.Size = new System.Drawing.Size(135, 65);
            this.importLabel.TabIndex = 10;
            this.importLabel.Tag = "1";
            this.importLabel.Text = "Import";
            this.importLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.importLabel.Click += new System.EventHandler(this.Menu_Click);
            this.importLabel.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.importLabel.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // duplicatesLabel
            // 
            this.duplicatesLabel.AutoSize = true;
            this.duplicatesLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.duplicatesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.duplicatesLabel.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.duplicatesLabel.Location = new System.Drawing.Point(65, 195);
            this.duplicatesLabel.Margin = new System.Windows.Forms.Padding(0);
            this.duplicatesLabel.Name = "duplicatesLabel";
            this.duplicatesLabel.Size = new System.Drawing.Size(135, 65);
            this.duplicatesLabel.TabIndex = 11;
            this.duplicatesLabel.Tag = "2";
            this.duplicatesLabel.Text = "Duplicates";
            this.duplicatesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.duplicatesLabel.Click += new System.EventHandler(this.Menu_Click);
            this.duplicatesLabel.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.duplicatesLabel.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // optionsLabel
            // 
            this.optionsLabel.AutoSize = true;
            this.optionsLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.optionsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsLabel.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.optionsLabel.Location = new System.Drawing.Point(65, 260);
            this.optionsLabel.Margin = new System.Windows.Forms.Padding(0);
            this.optionsLabel.Name = "optionsLabel";
            this.optionsLabel.Size = new System.Drawing.Size(135, 65);
            this.optionsLabel.TabIndex = 12;
            this.optionsLabel.Tag = "3";
            this.optionsLabel.Text = "Options";
            this.optionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optionsLabel.Click += new System.EventHandler(this.Menu_Click);
            this.optionsLabel.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.optionsLabel.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.infoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoLabel.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.infoLabel.Location = new System.Drawing.Point(65, 325);
            this.infoLabel.Margin = new System.Windows.Forms.Padding(0);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(135, 65);
            this.infoLabel.TabIndex = 13;
            this.infoLabel.Tag = "4";
            this.infoLabel.Text = "Info";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.infoLabel.Click += new System.EventHandler(this.Menu_Click);
            this.infoLabel.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.infoLabel.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // collectionIconPanel
            // 
            this.collectionIconPanel.Controls.Add(this.collectionIcon);
            this.collectionIconPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.collectionIconPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collectionIconPanel.Location = new System.Drawing.Point(0, 65);
            this.collectionIconPanel.Margin = new System.Windows.Forms.Padding(0);
            this.collectionIconPanel.Name = "collectionIconPanel";
            this.collectionIconPanel.Padding = new System.Windows.Forms.Padding(15);
            this.collectionIconPanel.Size = new System.Drawing.Size(65, 65);
            this.collectionIconPanel.TabIndex = 6;
            this.collectionIconPanel.Tag = "0";
            this.collectionIconPanel.Click += new System.EventHandler(this.Menu_Click);
            this.collectionIconPanel.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.collectionIconPanel.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // collectionIcon
            // 
            this.collectionIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collectionIcon.Image = global::GalleryManager.Properties.Resources.collection;
            this.collectionIcon.Location = new System.Drawing.Point(15, 15);
            this.collectionIcon.Margin = new System.Windows.Forms.Padding(0);
            this.collectionIcon.Name = "collectionIcon";
            this.collectionIcon.Size = new System.Drawing.Size(35, 35);
            this.collectionIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.collectionIcon.TabIndex = 1;
            this.collectionIcon.TabStop = false;
            this.collectionIcon.Tag = "0";
            this.collectionIcon.Click += new System.EventHandler(this.Menu_Click);
            this.collectionIcon.MouseEnter += new System.EventHandler(this.Menu_MouseEnter);
            this.collectionIcon.MouseLeave += new System.EventHandler(this.Menu_MouseLeave);
            // 
            // menuIconPanel
            // 
            this.menuIconPanel.Controls.Add(this.menuIcon);
            this.menuIconPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuIconPanel.Location = new System.Drawing.Point(0, 0);
            this.menuIconPanel.Margin = new System.Windows.Forms.Padding(0);
            this.menuIconPanel.Name = "menuIconPanel";
            this.menuIconPanel.Padding = new System.Windows.Forms.Padding(15);
            this.menuIconPanel.Size = new System.Drawing.Size(65, 65);
            this.menuIconPanel.TabIndex = 14;
            // 
            // menuIcon
            // 
            this.menuIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuIcon.Image = global::GalleryManager.Properties.Resources.menu;
            this.menuIcon.Location = new System.Drawing.Point(15, 15);
            this.menuIcon.Margin = new System.Windows.Forms.Padding(0);
            this.menuIcon.Name = "menuIcon";
            this.menuIcon.Size = new System.Drawing.Size(35, 35);
            this.menuIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.menuIcon.TabIndex = 0;
            this.menuIcon.TabStop = false;
            this.menuIcon.Click += new System.EventHandler(this.MenuIcon_Click);
            this.menuIcon.DoubleClick += new System.EventHandler(this.MenuIcon_Click);
            // 
            // contentPanel0
            // 
            this.contentPanel0.Controls.Add(this.contentPanelLayout0);
            this.contentPanel0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel0.Location = new System.Drawing.Point(65, 0);
            this.contentPanel0.Margin = new System.Windows.Forms.Padding(0);
            this.contentPanel0.Name = "contentPanel0";
            this.contentPanel0.Size = new System.Drawing.Size(935, 565);
            this.contentPanel0.TabIndex = 1;
            this.contentPanel0.Visible = false;
            // 
            // contentPanelLayout0
            // 
            this.contentPanelLayout0.ColumnCount = 2;
            this.contentPanelLayout0.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contentPanelLayout0.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.contentPanelLayout0.Controls.Add(this.infoPanel, 1, 0);
            this.contentPanelLayout0.Controls.Add(this.toolbarLayout, 0, 0);
            this.contentPanelLayout0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanelLayout0.Location = new System.Drawing.Point(0, 0);
            this.contentPanelLayout0.Margin = new System.Windows.Forms.Padding(0);
            this.contentPanelLayout0.Name = "contentPanelLayout0";
            this.contentPanelLayout0.RowCount = 2;
            this.contentPanelLayout0.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.contentPanelLayout0.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contentPanelLayout0.Size = new System.Drawing.Size(935, 565);
            this.contentPanelLayout0.TabIndex = 0;
            // 
            // infoPanel
            // 
            this.infoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel.Location = new System.Drawing.Point(635, 0);
            this.infoPanel.Margin = new System.Windows.Forms.Padding(0);
            this.infoPanel.Name = "infoPanel";
            this.contentPanelLayout0.SetRowSpan(this.infoPanel, 2);
            this.infoPanel.Size = new System.Drawing.Size(300, 565);
            this.infoPanel.TabIndex = 0;
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
            this.toolbarLayout.Controls.Add(this.iconAddDirectory, 0, 0);
            this.toolbarLayout.Controls.Add(this.iconDelete, 1, 0);
            this.toolbarLayout.Controls.Add(this.iconCopy, 2, 0);
            this.toolbarLayout.Controls.Add(this.iconSearch, 3, 0);
            this.toolbarLayout.Controls.Add(this.iconStar, 4, 0);
            this.toolbarLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolbarLayout.Location = new System.Drawing.Point(6, 0);
            this.toolbarLayout.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.toolbarLayout.Name = "toolbarLayout";
            this.toolbarLayout.RowCount = 1;
            this.toolbarLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.toolbarLayout.Size = new System.Drawing.Size(629, 35);
            this.toolbarLayout.TabIndex = 1;
            // 
            // iconAddDirectory
            // 
            this.iconAddDirectory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconAddDirectory.Image = global::GalleryManager.Properties.Resources.folder_add;
            this.iconAddDirectory.Location = new System.Drawing.Point(6, 6);
            this.iconAddDirectory.Margin = new System.Windows.Forms.Padding(6);
            this.iconAddDirectory.Name = "iconAddDirectory";
            this.iconAddDirectory.Size = new System.Drawing.Size(23, 23);
            this.iconAddDirectory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconAddDirectory.TabIndex = 0;
            this.iconAddDirectory.TabStop = false;
            // 
            // iconDelete
            // 
            this.iconDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconDelete.Image = global::GalleryManager.Properties.Resources.trash;
            this.iconDelete.Location = new System.Drawing.Point(43, 8);
            this.iconDelete.Margin = new System.Windows.Forms.Padding(8);
            this.iconDelete.Name = "iconDelete";
            this.iconDelete.Size = new System.Drawing.Size(19, 19);
            this.iconDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconDelete.TabIndex = 1;
            this.iconDelete.TabStop = false;
            // 
            // iconCopy
            // 
            this.iconCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconCopy.Image = global::GalleryManager.Properties.Resources.document_duplicate;
            this.iconCopy.Location = new System.Drawing.Point(78, 8);
            this.iconCopy.Margin = new System.Windows.Forms.Padding(8);
            this.iconCopy.Name = "iconCopy";
            this.iconCopy.Size = new System.Drawing.Size(19, 19);
            this.iconCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconCopy.TabIndex = 2;
            this.iconCopy.TabStop = false;
            // 
            // iconSearch
            // 
            this.iconSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconSearch.Image = global::GalleryManager.Properties.Resources.search;
            this.iconSearch.Location = new System.Drawing.Point(113, 8);
            this.iconSearch.Margin = new System.Windows.Forms.Padding(8);
            this.iconSearch.Name = "iconSearch";
            this.iconSearch.Size = new System.Drawing.Size(19, 19);
            this.iconSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconSearch.TabIndex = 3;
            this.iconSearch.TabStop = false;
            // 
            // iconStar
            // 
            this.iconStar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconStar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconStar.Image = global::GalleryManager.Properties.Resources.star;
            this.iconStar.Location = new System.Drawing.Point(148, 8);
            this.iconStar.Margin = new System.Windows.Forms.Padding(8);
            this.iconStar.Name = "iconStar";
            this.iconStar.Size = new System.Drawing.Size(19, 19);
            this.iconStar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconStar.TabIndex = 4;
            this.iconStar.TabStop = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.ControlBox = false;
            this.Controls.Add(this.mainLayout);
            this.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.Text = "MainWindow";
            this.mainLayout.ResumeLayout(false);
            this.navbarLayout.ResumeLayout(false);
            this.navbarLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconMaximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconClose)).EndInit();
            this.contentLayout.ResumeLayout(false);
            this.menuLayout.ResumeLayout(false);
            this.menuLayout.PerformLayout();
            this.infoIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.infoIcon)).EndInit();
            this.optionsIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.optionsIcon)).EndInit();
            this.duplicatesIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.duplicatesIcon)).EndInit();
            this.importIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.importIcon)).EndInit();
            this.collectionIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.collectionIcon)).EndInit();
            this.menuIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.menuIcon)).EndInit();
            this.contentPanel0.ResumeLayout(false);
            this.contentPanelLayout0.ResumeLayout(false);
            this.toolbarLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconAddDirectory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconStar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel navbarLayout;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.PictureBox iconMinimize;
        private PictureBox iconMaximize;
        private PictureBox iconClose;
        private TableLayoutPanel contentLayout;
        private TableLayoutPanel menuLayout;
        private Panel infoIconPanel;
        private Panel optionsIconPanel;
        private Panel duplicatesIconPanel;
        private Panel importIconPanel;
        private Panel collectionIconPanel;
        private PictureBox infoIcon;
        private PictureBox optionsIcon;
        private PictureBox duplicatesIcon;
        private PictureBox importIcon;
        private PictureBox menuIcon;
        private PictureBox collectionIcon;
        private Label collectionLabel;
        private Label importLabel;
        private Label duplicatesLabel;
        private Label optionsLabel;
        private Label infoLabel;
        private Panel menuIconPanel;
        private Panel contentPanel0;
        private TableLayoutPanel contentPanelLayout0;
        private Panel infoPanel;
        private TableLayoutPanel toolbarLayout;
        private PictureBox iconAddDirectory;
        private PictureBox iconDelete;
        private PictureBox iconCopy;
        private PictureBox iconSearch;
        private PictureBox iconStar;
    }
}