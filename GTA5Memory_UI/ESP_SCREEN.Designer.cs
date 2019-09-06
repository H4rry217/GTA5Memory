namespace GTA5Memory_UI
{
    partial class ESP_SCREEN
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
            this.SuspendLayout();
            // 
            // ESP_SCREEN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ESP_SCREEN";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ESP_SCREEN";
            this.TopMost = true;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ESP_SCREEN_Paint);
            this.ResumeLayout(false);

        }

        #endregion
    }
}