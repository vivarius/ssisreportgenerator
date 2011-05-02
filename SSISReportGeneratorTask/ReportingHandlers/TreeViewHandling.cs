using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SSISReportGeneratorTask100.ReportService2005;

namespace SSISReportGeneratorTask100.ReportingHandlers
{
    internal class TreeViewHandling
    {
        /// <summary>
        /// Gets the folder as nodes.
        /// </summary>
        /// <param name="reportingService2005">The reporting service2005.</param>
        /// <returns></returns>
        internal static TreeNode GetFolderAsNodes(ReportingService2005 reportingService2005)
        {
            return GetFolderAsNodes(reportingService2005, false);
        }

        /// <summary>
        /// Gets the folder as nodes.
        /// </summary>
        /// <param name="reportingService2005">The reporting service2005.</param>
        /// <param name="showDataSource">if set to <c>true</c> [show data source].</param>
        /// <returns></returns>
        internal static TreeNode GetFolderAsNodes(ReportingService2005 reportingService2005, bool showDataSource)
        {
            TreeNode treeNode = new TreeNode(reportingService2005.Url)
            {
                Tag = reportingService2005.Url,
                ImageIndex = 3
            };

            return (FillTreeView("/", treeNode, reportingService2005, showDataSource));
        }

        /// <summary>
        /// Fills the tree view.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="reportingService2005">The reporting service2005.</param>
        /// <param name="showDataSource">if set to <c>true</c> [show data source].</param>
        /// <returns></returns>
        private static TreeNode FillTreeView(string path, TreeNode parentNode, ReportingService2005 reportingService2005, bool showDataSource)
        {
            try
            {
                CatalogItem[] catalogItems = reportingService2005.ListChildren(path, false);
                foreach (CatalogItem catalogItem in catalogItems)
                {
                    switch (catalogItem.Type)
                    {
                        case ItemTypeEnum.Folder:
                            TreeNode folderNode = new TreeNode(catalogItem.Name)
                                                      {
                                                          ImageIndex = 0,
                                                          // Tag = catalogItem,
                                                          Name = catalogItem.Path,
                                                          Tag = catalogItem.Type,
                                                      };

                            folderNode.SelectedImageIndex = folderNode.ImageIndex;
                            parentNode.Nodes.Add(FillTreeView(catalogItem.Path, folderNode, reportingService2005, showDataSource));
                            break;
                        case ItemTypeEnum.Report:
                            if (showDataSource)
                                break;
                            TreeNode reportNode = new TreeNode(catalogItem.Name)
                                                      {
                                                          ImageIndex = 1,
                                                          //Tag = catalogItem,
                                                          Name = catalogItem.Path,
                                                          Tag = catalogItem.Type
                                                      };

                            reportNode.SelectedImageIndex = reportNode.ImageIndex;
                            parentNode.Nodes.Add(reportNode);
                            break;
                        case ItemTypeEnum.DataSource:
                            if (showDataSource)
                            {
                                TreeNode dataSourceNode = new TreeNode(catalogItem.Name)
                                {
                                    ImageIndex = 2,
                                    //Tag = catalogItem,
                                    Name = catalogItem.Path,
                                    Tag = catalogItem.Type,
                                };

                                dataSourceNode.SelectedImageIndex = dataSourceNode.ImageIndex;
                                parentNode.Nodes.Add(dataSourceNode);
                            }
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format(@"The Reports Server {0} is not available. Exception details: {1}", reportingService2005.Url, exception.Message));
            }

            return parentNode;
        }

        /// <summary>
        /// Gets the nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <returns></returns>
        public static List<TreeNode> GetNodes(TreeNodeCollection nodes)
        {
            var newNodes = new List<TreeNode>();

            foreach (TreeNode node in nodes)
            {
                newNodes.Add(node);
                newNodes.AddRange(GetNodes(node.Nodes));
            }

            return newNodes;
        }

        /// <summary>
        /// Gets the checked nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <returns></returns>
        public static List<TreeNode> GetCheckedNodes(TreeNodeCollection nodes)
        {
            var newNodes = new List<TreeNode>();

            foreach (TreeNode node in nodes)
            {
                newNodes.Add(node);
                newNodes.AddRange(GetCheckedNodes(node.Nodes));
            }

            return newNodes;
        }

        /// <summary>
        /// Checks the nodes.
        /// </summary>
        /// <param name="node">The node.</param>
        public static void CheckNodes(TreeNode node)
        {
            if (node.Level == 0)
                return;

            if (node.Tag != null)
                if (((ItemTypeEnum)(node.Tag)) != ItemTypeEnum.Folder)
                    return;

            List<TreeNode> listTreeNodes = GetNodes(node.Nodes);

            foreach (TreeNode treeNode in listTreeNodes)
            {
                treeNode.Checked = node.Checked;
            }
        }


        public static TreeNode FindRecursive(TreeNode treeNode, string nodeText, string path)
        {
            TreeNode retTreeNode = null;

            if (treeNode.Text == nodeText && treeNode.FullPath.Contains(path.Replace(@"/", @"\")))
                retTreeNode = treeNode;
            else
            {
                foreach (TreeNode node in treeNode.Nodes)
                {
                    retTreeNode = FindRecursive(node, nodeText, path);
                    if (retTreeNode != null)
                        break;
                }
            }

            return retTreeNode;
        }

        public static void SelectTreeNodeFromPath(TreeView treeView, string path)
        {

            var delimiters = new[] { '\\' };
            string[] pathArray = path.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            int numberOfLvlsToProbe = pathArray.Length;
            int currentLevel = 0;
            TreeNodeCollection globalTreeNCollection = treeView.Nodes;

            do
            {
                foreach (TreeNode rootNode in globalTreeNCollection)
                {
                    if (rootNode.Level < pathArray.Length)
                    {
                        currentLevel = rootNode.Level;

                        if (rootNode.Text == pathArray[currentLevel])
                        {
                            globalTreeNCollection = rootNode.Nodes;
                            break;
                        }
                    }
                    else
                    {
                        treeView.SelectedNode = rootNode;
                        currentLevel = numberOfLvlsToProbe;
                        treeView.SelectedNode.EnsureVisible();
                        break;
                    }
                }
            } while (currentLevel < numberOfLvlsToProbe);
        }
    }
}
