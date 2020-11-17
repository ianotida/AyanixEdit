using System.Windows.Forms;
using DarkUI.Docking;
using DarkUI.Controls;
using DarkUI.Collections;
using System.Collections.ObjectModel;


namespace AyanixEdit
{
    public partial class DockTreeView : DarkToolWindow
    {
        public DockTreeView()
        {
            InitializeComponent();

            DrawingControl.SetDoubleBuffered(DarkTV);
        }


        // DARKTV FORWARDED EVENTS
        public MouseEventHandler DarkNode_Click
        {
            set => DarkTV.MouseClick += value;
        }

        public MouseEventHandler DarkNode_DoubleClick
        {
            set => DarkTV.MouseDoubleClick += value; 
        }

        public ObservableCollection<DarkTreeNode> DarkNode_Selected => DarkTV.SelectedNodes;

        public ObservableList<DarkTreeNode> DarkNodes => DarkTV.Nodes;







    }
}
