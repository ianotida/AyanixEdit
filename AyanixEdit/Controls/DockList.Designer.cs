namespace AyanixEdit
{
    partial class DockList
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DarkLV = new DarkUI.Controls.DarkListView();
            this.SuspendLayout();
            // 
            // DarkLV
            // 
            this.DarkLV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DarkLV.Location = new System.Drawing.Point(0, 25);
            this.DarkLV.Name = "DarkLV";
            this.DarkLV.Size = new System.Drawing.Size(355, 488);
            this.DarkLV.TabIndex = 0;
            this.DarkLV.Text = "darkListView1";
            // 
            // DockList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CloseButton = false;
            this.Controls.Add(this.DarkLV);
            this.DefaultDockArea = DarkUI.Docking.DarkDockArea.Left;
            this.DockText = "Files";
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DockList";
            this.SerializationKey = "DockList";
            this.Size = new System.Drawing.Size(355, 513);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkListView DarkLV;
    }
}
