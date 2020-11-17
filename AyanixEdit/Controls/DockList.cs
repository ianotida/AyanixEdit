using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DarkUI.Docking;

namespace AyanixEdit
{
    public partial class DockList : DarkToolWindow
    {
        public DockList()
        {
            InitializeComponent();
        }

        public ObservableCollection<DarkUI.Controls.DarkListItem> ListItems => DarkLV.Items;


        public MouseEventHandler DarkList_DoubleClick
        {
            set => DarkLV.MouseDoubleClick += value; 
        }

    }
}
