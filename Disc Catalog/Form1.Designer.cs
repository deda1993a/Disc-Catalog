namespace Disc_Catalog
{
    partial class Form1
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Lemez infó", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.megnyitásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.újToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.megnyitásToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.kilépésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.database1 = new System.Windows.Forms.OpenFileDialog();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(239, 120);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(197, 251);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(31, 118);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(193, 249);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(265, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(31, 34);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(159, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.megnyitásToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // megnyitásToolStripMenuItem
            // 
            this.megnyitásToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.újToolStripMenuItem,
            this.megnyitásToolStripMenuItem1,
            this.kilépésToolStripMenuItem});
            this.megnyitásToolStripMenuItem.Name = "megnyitásToolStripMenuItem";
            this.megnyitásToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.megnyitásToolStripMenuItem.Text = "Fájl";
            this.megnyitásToolStripMenuItem.Click += new System.EventHandler(this.megnyitásToolStripMenuItem_Click);
            // 
            // újToolStripMenuItem
            // 
            this.újToolStripMenuItem.Name = "újToolStripMenuItem";
            this.újToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.újToolStripMenuItem.Text = "Új";
            this.újToolStripMenuItem.Click += new System.EventHandler(this.újToolStripMenuItem_Click);
            // 
            // megnyitásToolStripMenuItem1
            // 
            this.megnyitásToolStripMenuItem1.Name = "megnyitásToolStripMenuItem1";
            this.megnyitásToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.megnyitásToolStripMenuItem1.Text = "Megnyitás";
            this.megnyitásToolStripMenuItem1.Click += new System.EventHandler(this.megnyitásToolStripMenuItem1_Click);
            // 
            // kilépésToolStripMenuItem
            // 
            this.kilépésToolStripMenuItem.Name = "kilépésToolStripMenuItem";
            this.kilépésToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.kilépésToolStripMenuItem.Text = "Kilépés";
            this.kilépésToolStripMenuItem.Click += new System.EventHandler(this.kilépésToolStripMenuItem_Click);
            // 
            // database1
            // 
            this.database1.FileName = "openFileDialog1";
            // 
            // treeView2
            // 
            this.treeView2.Location = new System.Drawing.Point(556, 120);
            this.treeView2.Name = "treeView2";
            treeNode1.Name = "Node4";
            treeNode1.Tag = "euu";
            treeNode1.Text = "Node4";
            treeNode2.Name = "Node3";
            treeNode2.Text = "Lemez infó";
            this.treeView2.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeView2.Size = new System.Drawing.Size(209, 247);
            this.treeView2.TabIndex = 6;
            this.treeView2.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView2_AfterSelect);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.treeView2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Disc Catalog";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem megnyitásToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem megnyitásToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem kilépésToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog database1;
        private System.Windows.Forms.ToolStripMenuItem újToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView2;
    }
}

