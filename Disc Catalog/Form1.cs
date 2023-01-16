using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;



namespace Disc_Catalog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public int index=0;
        public int allnodes = 0;
        public int labelA = 0;
        public ulong i = 0;
        public ulong fileIndex = 0;
        public string selected;
        public string dataurl;
        List<FolderInfo> flder = new List<FolderInfo>();
        List<FileInfo> filnfo = new List<FileInfo>();
        List<string> labelnfo = new List<string>();


        private void listall(string tmp, string rootF)
        {
            DirectoryInfo a = new System.IO.DirectoryInfo(tmp);

            var con = new SQLiteConnection("Data Source=database.db");
            con.Open();

            var con2 = new SQLiteConnection("Data Source=database.db");
            con2.Open();


            //int size=a.GetDirectories().Count();
            foreach (DirectoryInfo d in a.GetDirectories())
            {
                //if (i==1)
                //{
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "INSERT INTO FolderInfo(`id`, `name`, `parent`, `labelinfo`) VALUES(@id, @name, @parent, @labelinfo)";
                cmd.Parameters.AddWithValue("@id", i);
                cmd.Parameters.AddWithValue("@name", d.Name);
                cmd.Parameters.AddWithValue("@parent", rootF);
                cmd.Parameters.AddWithValue("@labelinfo", "disclabel" + labelA);

                cmd.ExecuteNonQuery();
                flder.Add(new FolderInfo(i.ToString(), d.Name, rootF, i, "disclabel"+labelA));
                Console.WriteLine(flder[Convert.ToInt32(i)].Id());
                
                TreeNode[] tempnode = treeView1.Nodes.Find(rootF, true);
                //Console.WriteLine("ez: "+tempnode[0].Nodes.Count);

                    tempnode[0].Nodes.Add(i.ToString(), d.Name);
                    //treeView1.Nodes.Add(i.ToString(), d.Name);
                     //selected = i.ToString();
                    //treeView1.SelectedNode=(TreeNodeCollection)treeView1.Nodes.Count;
                    i++;
                //if(d.)
                ulong tp = i - 1;
                  listall(tmp + d.Name + "\\", tp.ToString());
                //}
                /* else
                 {
                     treeView1.SelectedNode = treeView1.SelectedNode.Nodes.Add(i.ToString(), d.Name);
                     i++;
                     Console.WriteLine(tmp + d.Name + "\\");

                     listall(tmp + d.Name + "\\");
                 }*/

            }

            foreach (var fi in a.GetFiles())
            {
                var cmd2 = new SQLiteCommand(con2);
                cmd2.CommandText = "INSERT INTO FileInfo(`id`, `name`, `parent`, `labelinfo`) VALUES(@id, @name, @parent, @labelinfo)";
                cmd2.Parameters.AddWithValue("@id", fileIndex);
                cmd2.Parameters.AddWithValue("@name", fi.Name);
                cmd2.Parameters.AddWithValue("@parent", rootF);
                cmd2.Parameters.AddWithValue("@labelinfo", "disclabel" + labelA);

                cmd2.ExecuteNonQuery();

                filnfo.Add(new FileInfo("file" + fileIndex.ToString(), fi.Name, rootF, fileIndex, "disclabel" + labelA));
                TreeNode[] tempnode = treeView1.Nodes.Find(rootF, true);
                tempnode[0].Nodes.Add("file"+fileIndex.ToString(), fi.Name);
                fileIndex++;

            }
                con.Close();
                con2.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            labelA++;
            var con = new SQLiteConnection("Data Source=database.db");
            con.Open();
            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "INSERT INTO LabelInfo(`id`, `labelnum`, `labelname`, `labelkey`) VALUES(@id, @labelnum, @labelname, @labelkey)";
            cmd.Parameters.AddWithValue("@id", labelA);
            cmd.Parameters.AddWithValue("@labelnum", labelA);
            cmd.Parameters.AddWithValue("@labelname", textBox1.Text);
            cmd.Parameters.AddWithValue("@labelkey", "disclabel" + labelA);

            cmd.ExecuteNonQuery();
            con.Close();
            labelnfo.Add("disclabel" + labelA);
            treeView1.Nodes.Add("disclabel"+labelA, textBox1.Text);

            selected = "disclabel" + labelA;

            listall(comboBox1.Items[index].ToString(), selected);
           
            allnodes++;
        }

        public string[] input;
        private void Form1_Load(object sender, EventArgs e)
        {
            //Console.WriteLine("ez: "+ AppContext.BaseDirectory);
            
            if (!File.Exists("options.ini"))
            {
                using (StreamWriter sw = new StreamWriter("options.ini"))
                {
                   
                        sw.WriteLine("database=");
                    
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader("options.ini"))
                {
                    string line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        input = line.Split('=');
                        Console.WriteLine(line);
                    }
                    
                }

                if (!input[1].Equals(""))
                {
                    var con = new SQLiteConnection("Data Source=" + input[1]);
                    con.Open();
                    var cmd = new SQLiteCommand(con);
                    cmd.CommandText = @"SELECT * FROM LabelInfo";

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        labelA++;
                        treeView1.Nodes.Add(reader.GetString(3), reader.GetString(2));
                    }
                    reader.Close();

                    cmd.CommandText = @"SELECT * FROM FolderInfo";

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        flder.Add(new FolderInfo(reader.GetInt64(0).ToString(), reader.GetString(1), reader.GetString(2), ((ulong)reader.GetInt64(0)), reader.GetString(3)));
                        TreeNode[] tempnode = treeView1.Nodes.Find(reader.GetString(2), true);


                        tempnode[0].Nodes.Add(reader.GetInt64(0).ToString(), reader.GetString(1));
                        i++;
                    }

                    reader.Close();

                    cmd.CommandText = @"SELECT * FROM FileInfo";

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        filnfo.Add(new FileInfo(reader.GetInt64(0).ToString(), reader.GetString(1), reader.GetString(2), ((ulong)reader.GetInt64(0)), reader.GetString(3)));
                        TreeNode[] tempnode = treeView1.Nodes.Find(reader.GetString(2), true);


                        tempnode[0].Nodes.Add("label" + reader.GetInt64(0).ToString(), reader.GetString(1));
                        fileIndex++;
                    }

                    con.Close();
                }
            }
            /*foreach (var drive in DriveInfo.GetDrives()
                               .Where(d => d.DriveType == DriveType.CDRom))
                MessageBox.Show(drive.Name + " " + drive.IsReady.ToString());*/
            foreach (var drive in DriveInfo.GetDrives()
                               .Where(d => d.DriveType == DriveType.CDRom))
                comboBox1.Items.Add(drive);

           

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            index= comboBox1.SelectedIndex;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listBox1.Items.Clear();
            string selectedNodeText = e.Node.Name;
            int selectedindex = (treeView1.SelectedNode.Index + 1);
            Console.WriteLine("selec: "+selectedNodeText+"index:");
            foreach (var tmpfolder in flder)
            {

             
                
                Console.WriteLine("parent: " + tmpfolder.Parent()+" onames: "+ tmpfolder.Key()+"name: "+ tmpfolder.Name());
                if (selectedNodeText.Equals(tmpfolder.Key()) || selectedNodeText.Equals("disclabel" + selectedindex))
                {

                    foreach (var tmpfolder2 in flder)
                    {

                        if (selectedNodeText.Equals(tmpfolder2.Parent()))
                        {
                            listBox1.Items.Add(tmpfolder2.Name());
                            //Console.WriteLine("oke: "+ selectedNodeText+" masik: "+ tmpfolder2.Parent());
                        }
                        else
                        {
                            //Console.WriteLine("oke: " + selectedNodeText + " masik: " + tmpfolder2.Parent());
                        }
                    }


                    foreach (var tmpfile2 in filnfo)
                    {

                        if (selectedNodeText.Equals(tmpfile2.Parent()))
                        {
                            listBox1.Items.Add(tmpfile2.Name());
                            //Console.WriteLine("oke: "+ selectedNodeText+" masik: "+ tmpfolder2.Parent());
                        }
                        else
                        {
                            //Console.WriteLine("oke: " + selectedNodeText + " masik: " + tmpfolder2.Parent());
                        }
                    }
                    selectedindex = -1;

                }
                
            }

            /*foreach (var tmpfile in filnfo)
            {
                Console.WriteLine("sel: "+);
                Console.WriteLine("parent: " + tmpfile.Parent() + " onames: " + tmpfile.Key() + "name: " + tmpfile.Name());
                if (selectedNodeText.Equals(tmpfile.Key()) || "0".Equals("disclabel" + labelA))
                {

                    foreach (var tmpfile2 in filnfo)
                    {

                        if (selectedNodeText.Equals(tmpfile2.Parent()))
                        {
                            listBox1.Items.Add(tmpfile2.Name());
                            //Console.WriteLine("oke: "+ selectedNodeText+" masik: "+ tmpfolder2.Parent());
                        }
                        else
                        {
                            //Console.WriteLine("oke: " + selectedNodeText + " masik: " + tmpfolder2.Parent());
                        }
                    }


                }
            }*/


            }

        private void megnyitásToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void megnyitásToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (database1.ShowDialog() == DialogResult.OK)
            {
                treeView1.Nodes.Clear();
                i = 0;
                fileIndex = 0;
                labelA = 0;
                flder.Clear();
                filnfo.Clear();

                //Get the path of specified file
                string filePath = database1.FileName;
                dataurl=database1.FileName;





                var con = new SQLiteConnection("Data Source=" + filePath);
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = @"SELECT * FROM LabelInfo";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    labelA++;
                    treeView1.Nodes.Add(reader.GetString(3), reader.GetString(2));
                }
                reader.Close();

                cmd.CommandText = @"SELECT * FROM FolderInfo";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    flder.Add(new FolderInfo(reader.GetInt64(0).ToString(), reader.GetString(1), reader.GetString(2), ((ulong)reader.GetInt64(0)), reader.GetString(3)));
                    TreeNode[] tempnode = treeView1.Nodes.Find(reader.GetString(2), true);


                    tempnode[0].Nodes.Add(reader.GetInt64(0).ToString(), reader.GetString(1));
                    i++;
                }

                reader.Close();

                cmd.CommandText = @"SELECT * FROM FileInfo";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    filnfo.Add(new FileInfo(reader.GetInt64(0).ToString(), reader.GetString(1), reader.GetString(2), ((ulong)reader.GetInt64(0)), reader.GetString(3)));
                    TreeNode[] tempnode = treeView1.Nodes.Find(reader.GetString(2), true);


                    tempnode[0].Nodes.Add("label" + reader.GetInt64(0).ToString(), reader.GetString(1));
                    fileIndex++;
                }

                con.Close();

            }
        }

        private void újToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cs = @"URI=file:C:\Users\Jano\Documents\test.db";

            var con = new SQLiteConnection("Data Source=database.db");
            con.Open();
            var cmd = new SQLiteCommand(con);
            cmd.CommandText = @"CREATE TABLE FolderInfo(id BIGINT [UNSIGNED] PRIMARY KEY,
            name TEXT, parent TEXT, labelinfo TEXT)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE FileInfo(id BIGINT [UNSIGNED] PRIMARY KEY,
            name TEXT, parent TEXT, labelinfo TEXT)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE LabelInfo(id INT PRIMARY KEY,
            labelnum TEXT, labelname TEXT, labelkey TEXT)";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void kilépésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!input[1].Equals(null))
            {
                using (StreamWriter sw = new StreamWriter("options.ini"))
                {


                    sw.WriteLine("database=" + dataurl);


                }

            }
    
             Application.Exit();
            
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
