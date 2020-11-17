using System;
using DarkUI.Docking;
using ScintillaNET;
using System.Windows.Forms;

namespace AyanixEdit
{
    public partial class DockItem : DarkDocument
    {
        private string sFilePath;
        private string sFileName;
        private string sOrigContent = "";
        private string sOrigTitle = "";


        Scintilla txtQuery;

        public DockItem()
        {
            InitializeComponent();

            txtQuery = new Scintilla();

            this.Controls.Add(txtQuery);
            txtQuery.Dock = DockStyle.Fill;
            txtQuery.BorderStyle = BorderStyle.None;
            txtQuery.TextChanged += TxtQuery_TextChanged;


            SC.Set_NoZoom(txtQuery);
            SC.Set_Coloring_Text(txtQuery);
           // SC.Set_Coloring_Cpp(txtQuery);
           // SC.Set_Coloring_SQL(txtQuery);

            DrawingControl.SetDoubleBuffered(txtQuery);
        }

        private void TxtQuery_TextChanged(object sender, EventArgs e)
        {
            this.DockText = sOrigContent.Length != txtQuery.Text.Length ? sOrigTitle + " *" : sOrigTitle;
        }

        public string FileName
        {
            get { return sFileName; }
            set { 
                sFileName = value; 
                sOrigTitle = value; 
            }
        }

        public string FilePath
        {
            get { return sFilePath; }
            set 
            {   
                sFilePath = value; 
            
                if (sFilePath.ToLower().Contains(".sql"))
                {
                    SC.Set_Coloring_SQL(txtQuery);
                }
            
                if (sFilePath.ToLower().Contains(".cs") || sFilePath.ToLower().Contains(".c") || sFilePath.ToLower().Contains(".js"))
                {
                    SC.Set_Coloring_Cpp(txtQuery);
                }


            }
        }

        public string TextArea
        {
            get { return txtQuery.Text; }
            set { 
                txtQuery.Text = value;
                sOrigContent = value;
                
                TxtQuery_TextChanged(null, null);
            }
        }

        public void Accept()
        {
            sOrigContent = txtQuery.Text;

            TxtQuery_TextChanged(null, null);
        }

        public string TextArea_Selected => txtQuery.SelectedText;


        private void DockItem_Load(object sender, EventArgs e)
        {
            sOrigTitle = this.DockText;
            sOrigContent = "";
            txtQuery.Text = "";
        }
    }
}
