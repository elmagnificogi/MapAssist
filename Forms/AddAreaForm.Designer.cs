
namespace MapAssist
{
    partial class AddAreaForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAreaForm));
            this.lstAreas = new System.Windows.Forms.ListBox();
            this.btnAddArea = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstAreas
            // 
            this.lstAreas.FormattingEnabled = true;
            this.lstAreas.ItemHeight = 12;
            this.lstAreas.Location = new System.Drawing.Point(12, 13);
            this.lstAreas.Name = "lstAreas";
            this.lstAreas.Size = new System.Drawing.Size(287, 160);
            this.lstAreas.TabIndex = 0;
            this.lstAreas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstAreas_MouseDoubleClick);
            // 
            // btnAddArea
            // 
            this.btnAddArea.Location = new System.Drawing.Point(12, 178);
            this.btnAddArea.Name = "btnAddArea";
            this.btnAddArea.Size = new System.Drawing.Size(287, 21);
            this.btnAddArea.TabIndex = 1;
            this.btnAddArea.Text = "添加场景";
            this.btnAddArea.UseVisualStyleBackColor = true;
            this.btnAddArea.Click += new System.EventHandler(this.btnAddArea_Click);
            // 
            // AddAreaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(311, 207);
            this.Controls.Add(this.btnAddArea);
            this.Controls.Add(this.lstAreas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddAreaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Areas - MapAssist";
            this.Load += new System.EventHandler(this.AddAreaForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstAreas;
        private System.Windows.Forms.Button btnAddArea;
    }
}
