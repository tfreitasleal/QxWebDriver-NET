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
            this.customerEditor = new Wisej.Web.Button();
            this.sayGoodBye = new Wisej.Web.Button();
            this.SuspendLayout();
            // 
            // customerEditor
            // 
            this.customerEditor.Location = new System.Drawing.Point(35, 30);
            this.customerEditor.Name = "customerEditor";
            this.customerEditor.Size = new System.Drawing.Size(180, 40);
            this.customerEditor.TabIndex = 0;
            this.customerEditor.Text = "Open Buttons Window";
            this.customerEditor.Click += new System.EventHandler(this.customerEditor_Click);
            // 
            // sayGoodBye
            // 
            this.sayGoodBye.Location = new System.Drawing.Point(35, 130);
            this.sayGoodBye.Name = "sayGoodBye";
            this.sayGoodBye.Size = new System.Drawing.Size(180, 40);
            this.sayGoodBye.TabIndex = 1;
            this.sayGoodBye.Text = "Say Good-bye";
            this.sayGoodBye.Click += new System.EventHandler(this.sayGoodBye_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
            this.Controls.Add(this.sayGoodBye);
            this.Controls.Add(this.customerEditor);
            this.Name = "MainPage";
            this.Size = new System.Drawing.Size(1024, 548);
            this.Text = "Main Page";
            this.ResumeLayout(false);

        }

        #endregion

        private Web.Button customerEditor;
        private Web.Button sayGoodBye;
    }
}
