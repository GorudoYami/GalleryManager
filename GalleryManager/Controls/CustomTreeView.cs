using GalleryManager.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalleryManager.Controls {
    public class CollectionView : TreeView {
        public DirectoryInfo BaseDirectory { get; set; }
        public List<TreeNode> Selection { get; set; }
        public Color SelectColor { get; set; }

        public CollectionView() {
            ImageList = new ImageList {
                ColorDepth = ColorDepth.Depth32Bit
            };
            
            ImageList.Images.Add(Properties.Resources.folder);
            ImageList.Images.Add(Properties.Resources.folder_open);
            ImageList.Images.Add(Properties.Resources.photograph);
            ImageList.Images.Add(Properties.Resources.film);
            ImageList.Images.Add(Properties.Resources.question_mark_circle);
            ShowLines = false;
            FullRowSelect = true;
            Indent = 20;
            ItemHeight = 25;

            Selection = new List<TreeNode>();

            DrawNode += CollectionView_DrawNode;
            BeforeSelect += CollectionView_BeforeSelect;
        }

        private void CollectionView_BeforeSelect(object sender, TreeViewCancelEventArgs e) {
            Selection.Add(e.Node);
        }

        private void CollectionView_DrawNode(object sender, DrawTreeNodeEventArgs e) {
            if (Selection.Contains(e.Node)) {
                using SolidBrush fgBrush = new(ForeColor);
                using SolidBrush bgBrush = new(SelectColor);
                e.Graphics.FillRectangle(bgBrush, e.Bounds);
                //e.Graphics.DrawString(e.Node.Text, Font, fgBrush, e.Bounds);
            }
        }

        private async void LoadNodes() {
            Nodes.Clear();
            Nodes.Add("Loading...");

            TreeNode rootNode = new(BaseDirectory.Name) {
                Tag = BaseDirectory
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

            Nodes.Clear();
            foreach (TreeNode node in rootNode.Nodes)
                Nodes.Add(node);
        }

        public void Reload() {
            if (BaseDirectory != null && BaseDirectory.Exists)
                LoadNodes();
        }
    }
}
