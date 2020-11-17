namespace AyanixEdit
{
    partial class DockTreeView
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
            this.DarkTV = new DarkUI.Controls.DarkTreeView();
            this.SuspendLayout();
            // 
            // DarkTV
            // 
            this.DarkTV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.DarkTV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DarkTV.Location = new System.Drawing.Point(0, 25);
            this.DarkTV.MaxDragChange = 20;
            this.DarkTV.Name = "DarkTV";
            this.DarkTV.ShowIcons = true;
            this.DarkTV.Size = new System.Drawing.Size(449, 339);
            this.DarkTV.TabIndex = 8;
            this.DarkTV.Text = "darkTreeView1";
            // 
            // DockTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CloseButton = false;
            this.Controls.Add(this.DarkTV);
            this.DefaultDockArea = DarkUI.Docking.DarkDockArea.Left;
            this.DockText = "My PC";
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DockTreeView";
            this.SerializationKey = "DockTreeView";
            this.Size = new System.Drawing.Size(449, 364);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkTreeView DarkTV;
    }
}
