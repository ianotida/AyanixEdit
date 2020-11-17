using DarkUI.Controls;
using DarkUI.Forms;
using DarkUI.Win32;
using System;
using System.Drawing;
using System.Management;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace AyanixEdit
{
    public partial class frmMain : DarkForm
    {
        private DockTreeView _dockTreeView;
        private DockList _doctList;
        private DockItem _dockItem;

        public frmMain()
        {
            InitializeComponent();

            DrawingControl.SetDoubleBuffered(DarkUIPanel);

            Application.AddMessageFilter(new ControlScrollFilter());
            Application.AddMessageFilter(DarkUIPanel.DockContentDragFilter);
            Application.AddMessageFilter(DarkUIPanel.DockResizeFilter);

            _dockTreeView = new DockTreeView();
            _dockTreeView.Icon = new Bitmap(IMG_TV.Images[0]);
            _dockTreeView.CloseButton = false;
            _dockTreeView.DarkNode_DoubleClick = _dockTreeView_MouseDoubleClick;

            _doctList = new DockList();
            _doctList.Icon = new Bitmap(IMG_TV.Images[8]);
            _doctList.DarkList_DoubleClick = _dockList_MouseDoubleClick;

            _dockItem = new DockItem();
            _dockItem.Icon = new Bitmap(IMG_TV.Images[1]);

            DarkUIPanel.AddContent(_dockTreeView, _dockTreeView.DockGroup);
            DarkUIPanel.AddContent(_doctList, _doctList.DockGroup);
            DarkUIPanel.AddContent(_dockItem, _dockItem.DockGroup);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            timer_startup.Enabled = true; // RUN ONCE ON STARTUP
        }

        private void LoadWMI_Data()
        {
            DarkTreeNode DN_PC;
            DarkTreeNode DN_Drv;

            int iDrv = 0, iIcon = 2;
            string sDrv = "", sName = "";

            DN_PC = new DarkTreeNode
            {
                Text = Environment.MachineName.ToUpper(),
                Name = "MyPC",
                Icon = new Bitmap(IMG_TV.Images[0]),
                ExpandedIcon = new Bitmap(IMG_TV.Images[0]),
                Key = "PC",
                Tag = "PC"
            };

            ConnectionOptions Opt = new ConnectionOptions();
            Opt.Impersonation = ImpersonationLevel.Impersonate;

            ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2", Opt);
            scope.Connect();

            ObjectQuery OQ_Drv = new ObjectQuery("SELECT * FROM Win32_LogicalDisk");

            ManagementObjectSearcher ObjSch = new ManagementObjectSearcher(scope, OQ_Drv);
            ManagementObjectCollection QbjCol = ObjSch.Get();

            foreach (ManagementObject m in QbjCol)
            {
                iDrv = Convert.ToInt32(m["DriveType"].ToString());
                sDrv = m["Caption"].ToString().Substring(0,1);

                if (m["VolumeName"] != null) sName = m["VolumeName"].ToString();
                if (m["ProviderName"] != null) sName = m["ProviderName"].ToString();

                switch (iDrv)
                {
                    case 2:     if (sName == "") sName = "Removable ";    iIcon = 5;  break;
                    case 3:     if (sName == "") sName = "Local Disk ";   iIcon = 2;  break;
                    case 4:     if (sName == "") sName = "Net Drive ";    iIcon = 3;  break;
                    case 5:     if (sName == "") sName = "CD Drive ";     iIcon = 2;  break;
                    case 6:     if (sName == "") sName = "RAM Drive ";    iIcon = 6;  break;
                    default:    if (sName == "") sName = "Unknown ";      iIcon = 6;  break;
                }

                DN_Drv = new DarkTreeNode
                {
                    Text = sName + " (" + sDrv + ":)",
                    Name = "Drv" + sDrv,
                    Icon = new Bitmap(IMG_TV.Images[iIcon]),
                    ExpandedIcon = new Bitmap(IMG_TV.Images[iIcon]),
                    Key = sDrv,
                    Tag = "Drive"
                };

                DN_PC.Nodes.Add(DN_Drv);
            }

            DN_PC.Expanded = true;

            _dockTreeView.DarkNodes.Add(DN_PC);
        }

        private void Load_Folders()
        {
            DarkTreeNode DTN_Folder;
            DarkTreeNode DTN_FolderL2;

            DirectoryInfo[] DirInfoL1;
            DirectoryInfo[] DirInfoL2;

            Bitmap bFolder = new Bitmap(IMG_TV.Images[7]);

            foreach(DarkTreeNode DTN in _dockTreeView.DarkNodes[0].Nodes)
            {
                if (DTN.Tag.ToString() == "Drive")
                {
                    try
                    {
                        DirInfoL1 = new DirectoryInfo(DTN.Key + ":\\").GetDirectories(); 

                        foreach(DirectoryInfo Dri in DirInfoL1)
                        {
                            DirInfoL2 = Dri.GetDirectories(); 

                            DTN_Folder = new DarkTreeNode
                            {
                                Text = Dri.Name,
                                Name = Dri.Name,
                                Icon = bFolder,
                                ExpandedIcon = bFolder,
                                Key = Dri.FullName,
                                Tag = "Folder"
                            };

                            foreach(DirectoryInfo Dri2 in DirInfoL2)
                            {
                                DTN_FolderL2 = new DarkTreeNode
                                {
                                    Text = Dri2.Name,
                                    Name = Dri2.Name,
                                    Icon = bFolder,
                                    ExpandedIcon = bFolder,
                                    Key = Dri2.FullName,
                                    Tag = "Folder"
                                };

                                DTN_Folder.Nodes.Add(DTN_FolderL2);
                            }

                            DTN.Nodes.Add(DTN_Folder);
                        }
                    }
                    catch (Exception)
                    {
                       // _dockItem.TextArea += "Error : Loading " + DTN.Text + " >> " + x.Message + Environment.NewLine;
                    }
                }
            }
        }

        private void _dockTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_dockTreeView.DarkNode_Selected.Count > 0)
            {
                DarkTreeNode DTN = _dockTreeView.DarkNode_Selected[0];

                if (DTN.Tag.ToString() == "Folder")
                {
                    var files = Directory.EnumerateFiles(DTN.Key.ToString(), "*.*", SearchOption.TopDirectoryOnly)
                                .Where(s => s.EndsWith(".txt") || s.EndsWith(".csv") || s.EndsWith(".xml") || s.EndsWith(".log") || 
                                            s.EndsWith(".htm") || s.EndsWith(".html") || s.EndsWith(".css") || s.EndsWith(".js") ||
                                            s.EndsWith(".sql"));

                    _doctList.ListItems.Clear();

                    DarkListItem Ditm;
                    FileInfo FilInfo;

                    foreach(string str in files)
                    {
                        FilInfo = new FileInfo(str);

                        Ditm = new DarkListItem(FilInfo.Name);
                        Ditm.Tag = FilInfo.FullName;

                        _doctList.ListItems.Add(Ditm);
                    }
                }
            }
        }


        private void _dockList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DarkListView DkList = (DarkListView)sender;

            if (DkList.SelectedIndices.Count > 0)
            {
                DarkListItem Dkitm = DkList.Items[DkList.SelectedIndices[0]];

                string sValue = File.ReadAllText(Dkitm.Tag.ToString());

                _dockItem = new DockItem();
                _dockItem.FilePath = Dkitm.Tag.ToString();
                _dockItem.FileName = Dkitm.Text;
                _dockItem.DockText = Dkitm.Text;

                DarkUIPanel.AddContent(_dockItem, _dockItem.DockGroup);

                _dockItem.TextArea = sValue;
            }
        }

        private void mnuNewDoc_Click(object sender, EventArgs e)
        {
            _dockItem = new DockItem();
            _dockItem.DockText = "Document " + (DarkUIPanel.GetDocuments().Count + 1);
            _dockItem.FilePath = "";

            DarkUIPanel.AddContent(_dockItem, _dockItem.DockGroup);
        }


        private void mnuOpenDoc_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpnFD = new OpenFileDialog
            {
                Filter = "Text files |*.txt;*.log;*.csv;*.xml|" +
                         "HTML files |*.html;*.htm;*.js;*.css|" +
                         "SQL files (*.sql)|*.sql|" +
                         "All Files|*.*"
            };

            OpnFD.ShowDialog();

            if (OpnFD.FileName != "")
            {
                try
                {
                    string sValue = File.ReadAllText(OpnFD.FileName);

                    _dockItem = new DockItem();
                    _dockItem.FilePath = OpnFD.FileName;
                    _dockItem.FileName = OpnFD.SafeFileName;
                    _dockItem.DockText = OpnFD.SafeFileName;

                    DarkUIPanel.AddContent(_dockItem, _dockItem.DockGroup);

                    _dockItem.TextArea = sValue;
                }
                catch (Exception x)
                {
                    MessageBox.Show("Error: " + x.Message, "Opening File " + OpnFD.SafeFileName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void mnuSaveDoc_Click(object sender, EventArgs e)
        {
            if(DarkUIPanel.GetDocuments().Count > 0)
            {
                DockItem DocItm = (DockItem)DarkUIPanel.ActiveDocument;

                try
                {
                    if (DocItm.FilePath != null && DocItm.FilePath != "")
                    {
                        File.WriteAllText(DocItm.FilePath, DocItm.TextArea);
                        DocItm.Accept();        // Trigger to Update Control 
                    }
                    else
                    {
                        SaveFileDialog SaveFD = new SaveFileDialog
                        {
                            Filter = "Text file |*.txt|" +
                                     "SQL file |*.sql|" +
                                     "All Files|*.*"
                        };

                        SaveFD.ShowDialog();

                        if (SaveFD.FileName != "")
                        {
                            File.WriteAllText(SaveFD.FileName, DocItm.TextArea);

                            FileInfo fil = new FileInfo(SaveFD.FileName);

                            DocItm.DockText = fil.Name;
                            DocItm.FileName = fil.Name;
                            DocItm.FilePath = fil.FullName;
                            DocItm.Accept();  // Trigger to Update Control 
                        }
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show("Error: " + x.Message, "Saving File " + DocItm.DockText, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void mnuExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void timer_startup_Tick(object sender, EventArgs e)
        {
            timer_startup.Enabled = false;

            LoadWMI_Data();
            Load_Folders();
        }
    }
}
