using GalleryManager.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalleryManager.Controls {
    public partial class CollectionTab : UserControl {
        public CollectionTab() {
            InitializeComponent();

            ImageList imageList = new();
            imageList.ColorDepth = ColorDepth.Depth32Bit;

            imageList.Images.Add(Properties.Resources.folder);
            imageList.Images.Add(Properties.Resources.folder_open);
            imageList.Images.Add(Properties.Resources.photograph);
            imageList.Images.Add(Properties.Resources.film);
            imageList.Images.Add(Properties.Resources.question_mark_circle);

            treeView.ImageList = imageList;

            LoadNodes();
        }

        public async void LoadNodes() {
            treeView.Nodes.Clear();
            treeView.Nodes.Add("Loading...");

            DirectoryInfo rootDirectory = new(Properties.Settings.Default.GalleryPath);
            TreeNode rootNode = new(rootDirectory.Name) {
                Tag = rootDirectory
            };

            await Task.Run(() => {
                Stack<TreeNode> stack = new();
                stack.Push(rootNode);

                while (stack.Count > 0) {
                    TreeNode currentNode = stack.Pop();
                    DirectoryInfo directoryInfo = (DirectoryInfo)currentNode.Tag;

                    foreach (var directory in directoryInfo.GetDirectories()) {
                        TreeNode childNode = new(directory.Name) {
                            Tag = directory,
                            ImageIndex = 0
                        };
                        currentNode.Nodes.Add(childNode);
                        stack.Push(childNode);
                    }

                    foreach (var file in directoryInfo.GetFiles()) {
                        int imageIndex;
                        if (Media.IsPicture(file))
                            imageIndex = 2;
                        else if (Media.IsVideo(file))
                            imageIndex = 3;
                        else
                            imageIndex = 4;

                        currentNode.Nodes.Add(new TreeNode(file.Name) {
                            ImageIndex = imageIndex,
                            SelectedImageIndex = imageIndex
                        });
                    }
                }
            });

            treeView.Nodes.Clear();
            foreach (TreeNode node in rootNode.Nodes)
                treeView.Nodes.Add(node);
        }
    }
}
