namespace Wisej.SimpleDemo
{
    partial class MainPage
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

        #region Wisej Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openWindow = new Wisej.Web.Button();
            this.SuspendLayout();
            // 
            // openWindow
            // 
            this.openWindow.Location = new System.Drawing.Point(33, 31);
            this.openWindow.Name = "openWindow";
            this.openWindow.Size = new System.Drawing.Size(180, 40);
            this.openWindow.TabIndex = 0;
            this.openWindow.Text = "Open First Window";
            this.openWindow.Click += new System.EventHandler(this.openWindow_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
            this.Controls.Add(this.openWindow);
            this.Name = "MainPage";
            this.Size = new System.Drawing.Size(810, 547);
            this.Text = "Main Page";
            this.ResumeLayout(false);

        }

        #endregion

        private Web.Button openWindow;
    }
}
