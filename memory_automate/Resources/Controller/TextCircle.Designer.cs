namespace memory_automate.Controller
{
    partial class TextCircle
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.txtAddHote = new WinFormSkin.Controls.WinSingleLineTextField();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtAddHote);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(187, 83);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Image = global::memory_automate.Properties.Resources.add1;
            this.button1.Location = new System.Drawing.Point(66, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 48);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtAddHote
            // 
            this.txtAddHote.Depth = 0;
            this.txtAddHote.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddHote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(30)))));
            this.txtAddHote.Hint = "";
            this.txtAddHote.Location = new System.Drawing.Point(0, 3);
            this.txtAddHote.MaxLength = 10;
            this.txtAddHote.MouseState = WinFormSkin.MouseState.HOVER;
            this.txtAddHote.Name = "txtAddHote";
            this.txtAddHote.PasswordChar = '\0';
            this.txtAddHote.SelectedText = "";
            this.txtAddHote.SelectionLength = 0;
            this.txtAddHote.SelectionStart = 0;
            this.txtAddHote.Size = new System.Drawing.Size(187, 23);
            this.txtAddHote.TabIndex = 0;
            this.txtAddHote.TabStop = false;
            this.txtAddHote.UseSystemPasswordChar = false;
            this.txtAddHote.Click += new System.EventHandler(this.txtAddHote_Click);
            this.txtAddHote.Enter += new System.EventHandler(this.txtAddHote_Enter);
            this.txtAddHote.TextChanged += new System.EventHandler(this.txtAddHote_TextChanged);
            // 
            // TextCircle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 163);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "TextCircle";
            this.Style = MetroFramework.MetroColorStyle.Brown;
            this.Text = "Ajouter un Hôte";
            this.Load += new System.EventHandler(this.TextCircle_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public WinFormSkin.Controls.WinSingleLineTextField txtAddHote;
        public System.Windows.Forms.Button button1;
    }
}