
namespace TestIFilter
{
    partial class TestRes
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
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.icon = new System.Windows.Forms.PictureBox();
            this.ext = new System.Windows.Forms.Label();
            this.errorText = new System.Windows.Forms.Label();
            this.table.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            this.SuspendLayout();
            // 
            // table
            // 
            this.table.AutoSize = true;
            this.table.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.table.ColumnCount = 3;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table.Controls.Add(this.icon, 0, 0);
            this.table.Controls.Add(this.ext, 1, 0);
            this.table.Controls.Add(this.errorText, 2, 0);
            this.table.Location = new System.Drawing.Point(0, 0);
            this.table.Name = "table";
            this.table.RowCount = 1;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Size = new System.Drawing.Size(56, 22);
            this.table.TabIndex = 0;
            // 
            // icon
            // 
            this.icon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.icon.Location = new System.Drawing.Point(3, 3);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(16, 16);
            this.icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.icon.TabIndex = 0;
            this.icon.TabStop = false;
            // 
            // ext
            // 
            this.ext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ext.AutoSize = true;
            this.ext.Location = new System.Drawing.Point(25, 5);
            this.ext.Name = "ext";
            this.ext.Size = new System.Drawing.Size(11, 12);
            this.ext.TabIndex = 1;
            this.ext.Text = "...";
            // 
            // errorText
            // 
            this.errorText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.errorText.AutoSize = true;
            this.errorText.Location = new System.Drawing.Point(42, 5);
            this.errorText.Name = "errorText";
            this.errorText.Size = new System.Drawing.Size(11, 12);
            this.errorText.TabIndex = 2;
            this.errorText.Text = "...";
            // 
            // TestRes
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.table);
            this.Name = "TestRes";
            this.Size = new System.Drawing.Size(59, 25);
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.PictureBox icon;
        private System.Windows.Forms.Label ext;
        private System.Windows.Forms.Label errorText;
    }
}
