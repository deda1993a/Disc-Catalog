using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Management;
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

        public int index = 0;
        public int allnodes = 0;
        public int labelA = 0;
        public ulong i = 0;
        public ulong fileIndex = 0;
        public string selected;
        public string dataurl;
        List<FolderInfo> flder = new List<FolderInfo>();
        List<FileInfo> filnfo = new List<FileInfo>();
        List<FolderInfo> TempInfo = new List<FolderInfo>();
        List<FileInfo> TempFile = new List<FileInfo>();
        List<LabelInfo> labels = new List<LabelInfo>();


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
                cmd.CommandText = "INSERT INTO FolderInfo(`id`, `name`, `parent`, `labelinfo`, `size`, `description`, `attr`, `crtdate`, `mdfdate`, `fullpath`)VALUES(@id, @name, @parent, @labelinfo, @size, @description, @attr, @crtdate, @mdfdate, @fullpath)";
                cmd.Parameters.AddWithValue("@id", i);
                cmd.Parameters.AddWithValue("@name", d.Name);
                cmd.Parameters.AddWithValue("@parent", rootF);
                cmd.Parameters.AddWithValue("@labelinfo", "disclabel" + labelA);
                cmd.Parameters.AddWithValue("@size", 0);
                cmd.Parameters.AddWithValue("@description", "");
                cmd.Parameters.AddWithValue("@attr", "Mappa");
                cmd.Parameters.AddWithValue("@crtdate", d.CreationTime);
                cmd.Parameters.AddWithValue("@mdfdate", d.LastWriteTime);
                cmd.Parameters.AddWithValue("@fullpath", d.FullName);


                cmd.ExecuteNonQuery();
                flder.Add(new FolderInfo(i.ToString(), d.Name, rootF, i, "disclabel" + labelA, 0, "", "Mappa", d.CreationTime.ToString(), d.LastWriteTime.ToString(), d.FullName));
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

                Console.WriteLine(fi.CreationTime);
                var cmd2 = new SQLiteCommand(con2);
                cmd2.CommandText = "INSERT INTO FileInfo(`id`, `name`, `parent`, `labelinfo`, `size`, `description`, `attr`, `crtdate`, `mdfdate`, `fullpath`)VALUES(@id, @name, @parent, @labelinfo, @size, @description, @attr, @crtdate, @mdfdate, @fullpath)";
                cmd2.Parameters.AddWithValue("@id", fileIndex);
                cmd2.Parameters.AddWithValue("@name", fi.Name);
                cmd2.Parameters.AddWithValue("@parent", rootF);
                cmd2.Parameters.AddWithValue("@labelinfo", "disclabel" + labelA);
                cmd2.Parameters.AddWithValue("@size", fi.Length);
                cmd2.Parameters.AddWithValue("@description", "");
                cmd2.Parameters.AddWithValue("@attr", fi.Extension);
                cmd2.Parameters.AddWithValue("@crtdate", fi.CreationTime);
                cmd2.Parameters.AddWithValue("@mdfdate", fi.LastWriteTime);
                cmd2.Parameters.AddWithValue("@fullpath", fi.FullName);

                cmd2.ExecuteNonQuery();

                filnfo.Add(new FileInfo("file" + fileIndex.ToString(), fi.Name, rootF, fileIndex, "disclabel" + labelA, fi.Length, "", fi.Extension, fi.CreationTime.ToString(), fi.LastWriteTime.ToString(), fi.FullName));
                TreeNode[] tempnode = treeView1.Nodes.Find(rootF, true);
                tempnode[0].Nodes.Add("file" + fileIndex.ToString(), fi.Name);
                fileIndex++;

            }
            con.Close();
            con2.Close();
        }

        void driveInsertEvent(object sender, EventArrivedEventArgs e)
        {
            // Get the Event object and display it
            PropertyData pd = e.NewEvent.Properties["TargetInstance"];

            if (pd != null)
            {
                ManagementBaseObject mbo = pd.Value as ManagementBaseObject;
                // if CD removed VolumeName == null
                if (mbo.Properties["VolumeName"].Value != null)
                {
                    Console.WriteLine("valtozik");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            labelA++;
            var con = new SQLiteConnection("Data Source=database.db");
            con.Open();
            var cmd = new SQLiteCommand(con);
            drive();
            cmd.CommandText = "INSERT INTO LabelInfo(`id`, `labelnum`, `labelname`, `labelkey`) VALUES(@id, @labelnum, @labelname, @labelkey)";
            cmd.Parameters.AddWithValue("@id", labelA);
            cmd.Parameters.AddWithValue("@labelnum", labelA);
            cmd.Parameters.AddWithValue("@labelname", textBox1.Text);
            cmd.Parameters.AddWithValue("@labelkey", "disclabel" + labelA);

            cmd.ExecuteNonQuery();
            con.Close();
           
            treeView1.Nodes.Add("disclabel" + labelA, textBox1.Text);

            selected = "disclabel" + labelA;

            listall(comboBox1.Items[index].ToString(), selected);

            allnodes++;

            
           


        }

        public void drive(){
                        SelectQuery query =
    new SelectQuery("select * from win32_logicaldisk where drivetype=5");
        ManagementObjectSearcher searcher =
            new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get())
            {
                // If both properties are null I suppose there's no CD
                if ((mo["volumename"] != null) || (mo["volumeserialnumber"] != null))
                {
                    Console.WriteLine("CD is named: {0}", mo["volumename"]);
                    Console.WriteLine("CD Serial Number: {0}", mo["volumeserialnumber"]);
                    Console.WriteLine("CD filesystem: {0}", mo["FileSystem"]);
                    Console.WriteLine("CD mediatype: {0}", Convert.ToString(mo["MediaType"]));
                    Console.WriteLine("CD size: {0}", mo["Size"]);
                    string vname = mo["volumename"].ToString();
                    string dsize = mo["Size"].ToString();
                    long tsize = Convert.ToInt64(dsize);
                    string serial = mo["volumeserialnumber"].ToString();
                    string fs = mo["FileSystem"].ToString();
                    Console.WriteLine(dsize);
                    labels.Add(new LabelInfo("disclabel" + labelA, 
                     labelA, textBox1.Text, vname,
                     comboBox1.Text, tsize, textBox27.Text,
                     serial,
                     dtype(tsize), fs));
                }
                else
                {
                    Console.WriteLine("No CD in Unit");
                }
            }
            }



        public string[] input;

        
        public string dtype(long dsize)
        {
            if (dsize > 800 * Math.Pow(1024, 2))
            {
                return "Dvd";
            }
            return "s";
        }


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

                        flder.Add(new FolderInfo(reader.GetInt64(0).ToString(), reader.GetString(1), reader.GetString(2), ((ulong)reader.GetInt64(0)), reader.GetString(3), (long)reader.GetInt64(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9)));
                        TreeNode[] tempnode = treeView1.Nodes.Find(reader.GetString(2), true);


                        tempnode[0].Nodes.Add(reader.GetInt64(0).ToString(), reader.GetString(1));
                        i++;
                    }

                    reader.Close();

                    cmd.CommandText = @"SELECT * FROM FileInfo";

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        filnfo.Add(new FileInfo(reader.GetInt64(0).ToString(), reader.GetString(1), reader.GetString(2), ((ulong)reader.GetInt64(0)), reader.GetString(3), (long)reader.GetInt64(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9)));
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
            TempInfo.Clear();
            TempFile.Clear();
            listView1.Items.Clear();
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
                            TempInfo.Add(tmpfolder2);
                            ListViewItem ls = new ListViewItem(tmpfolder2.Name());
                            ls.SubItems.Add("ez");
                            ls.SubItems.Add(tmpfolder2.Size().ToString());
                            ls.SubItems.Add(tmpfolder2.Attr());
                            ls.SubItems.Add(tmpfolder2.Mdfdate());
                            listView1.Items.Add(ls);
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
                            TempFile.Add(tmpfile2);
                            ListViewItem ls= new ListViewItem(tmpfile2.Name());
                            ls.SubItems.Add("ez");
                            ls.SubItems.Add(sizecalculate(tmpfile2.Size()));
                            ls.SubItems.Add(tmpfile2.Attr());
                            ls.SubItems.Add(tmpfile2.Mdfdate());
                            listView1.Items.Add(ls);
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

        private string sizecalculate(long size)
        {
            if (size < 1024)
            {
                return size.ToString() + "b";
            }
            else if (size > 1024 && size<1024*1024)
            {
                return Math.Round(size / Math.Pow(1024, 1), 2).ToString()+"kb";
            }else if (size> Math.Pow(1024, 2) && size< Math.Pow(1024, 3))
            {
                return Math.Round(size / Math.Pow(1024, 2),2).ToString() + "mb";
            }
            else if (size > Math.Pow(1024, 3) && size < Math.Pow(1024,4))
            {
                return Math.Round(size / Math.Pow(1024, 3),2).ToString() + "Gb";
            }
            else if (size > Math.Pow(1024, 4) && size < Math.Pow(1024, 5))
            {
                return Math.Round(size / Math.Pow(1024, 4),2).ToString() + "Tb";
            }
            else
            {
                return size.ToString() + "b";
            }
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

                    flder.Add(new FolderInfo(reader.GetInt64(0).ToString(), reader.GetString(1), reader.GetString(2), ((ulong)reader.GetInt64(0)), reader.GetString(3), (long)reader.GetInt64(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9)));
                    TreeNode[] tempnode = treeView1.Nodes.Find(reader.GetString(2), true);


                    tempnode[0].Nodes.Add(reader.GetInt64(0).ToString(), reader.GetString(1));
                    i++;
                }

                reader.Close();

                cmd.CommandText = @"SELECT * FROM FileInfo";

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    filnfo.Add(new FileInfo(reader.GetInt64(0).ToString(), reader.GetString(1), reader.GetString(2), ((ulong)reader.GetInt64(0)), reader.GetString(3), (long)reader.GetInt64(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9)));
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
            name TEXT, parent TEXT, labelinfo TEXT, size BIGINT, description TEXT, attr TEXT, crtdate TEXT, mdfdate TEXT, fullpath TEXT)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE FileInfo(id BIGINT [UNSIGNED] PRIMARY KEY,
            name TEXT, parent TEXT, labelinfo TEXT, size BIGINT, description TEXT, attr TEXT, crtdate TEXT, mdfdate TEXT, fullpath TEXT)";
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)

        {

            if (listView1.SelectedItems.Count != 0 && TempInfo.Count()!=0 )
            {
               
                textBox3.Text = TempInfo[listView1.Items.IndexOf(listView1.SelectedItems[0])].Name();
                textBox5.Text = TempInfo[listView1.Items.IndexOf(listView1.SelectedItems[0])].Attr();
                textBox7.Text = sizecalculate (TempInfo[listView1.Items.IndexOf(listView1.SelectedItems[0])].Size());
                textBox9.Text = TempInfo[listView1.Items.IndexOf(listView1.SelectedItems[0])].Fullpath();
                textBox11.Text = TempInfo[listView1.Items.IndexOf(listView1.SelectedItems[0])].Crtdate();
                textBox13.Text = TempInfo[listView1.Items.IndexOf(listView1.SelectedItems[0])].Crtdate();
            }
            else if (listView1.SelectedItems.Count != 0 && TempFile.Count() != 0 )
            {

                textBox3.Text = TempFile[listView1.Items.IndexOf(listView1.SelectedItems[0])-TempInfo.Count()].Name();
                textBox5.Text = TempFile[listView1.Items.IndexOf(listView1.SelectedItems[0]) - TempInfo.Count()].Attr();
                textBox7.Text = sizecalculate(TempFile[listView1.Items.IndexOf(listView1.SelectedItems[0])].Size());
                textBox9.Text = TempFile[listView1.Items.IndexOf(listView1.SelectedItems[0])].Fullpath();
                textBox11.Text = TempFile[listView1.Items.IndexOf(listView1.SelectedItems[0])].Crtdate();
                textBox13.Text = TempFile[listView1.Items.IndexOf(listView1.SelectedItems[0])].Crtdate();
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
