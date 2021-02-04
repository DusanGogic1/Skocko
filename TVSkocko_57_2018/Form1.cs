using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TVSkocko_57_2018
{
    public partial class Form1 : Form
    {
        Participant p;
        Stopwatch sw = new Stopwatch();
        int[] combination = new int[4];
        int[] current = new int[4];
        int nom = 0;
        ForSerialization fs = new ForSerialization();

        public Form1()
        {
            InitializeComponent();
            p = new Participant();
            nom = 0;
        }

        public void convertBitmapToIco()
        {
            Bitmap bmp;
            using(bmp = new Bitmap(@"skocko.bmp"))
            {
                bmp.MakeTransparent(Color.White);
                IntPtr icH = bmp.GetHicon();
                Icon ico = Icon.FromHandle(icH);
                this.Icon = ico;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.convertBitmapToIco();

            //serijalizacija podataka
            string filePath = "data.save";
            DataSerializer dataSerializer = new DataSerializer();
            fs = dataSerializer.BinaryDeserialize(filePath) as ForSerialization;

            //nema sta da se serijalizuje
            if (fs == null)
            {
                this.nom = 0;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;

                //priprema za mogucu buducu serijalizaciju
                fs = new ForSerialization();
                Random rand = new Random();

                combination[0] = rand.Next(0, 6);
                combination[1] = rand.Next(0, 6);
                combination[2] = rand.Next(0, 6);
                combination[3] = rand.Next(0, 6);
                fs.combination = this.combination;

                //popunjavanje levog DataGridViewa
                dataGridView1.Columns.Add("column1", "Column 1");
                dataGridView1.Columns.Add("column2", "Column 2");
                dataGridView1.Columns.Add("column3", "Column 3");
                dataGridView1.Columns.Add("column4", "Column 4");

                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[1].Width = 40;
                dataGridView1.Columns[2].Width = 40;
                dataGridView1.Columns[3].Width = 40;

                for (int i = 0; i < 6; i++)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.Height = 40;
                    row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"praznolevo.bmp") });
                    row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"praznolevo.bmp") });
                    row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"praznolevo.bmp") });
                    row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"praznolevo.bmp") });
                    dataGridView1.Rows.Add(row);
                }

                //popunjavanje srednjeg DataGridViewa
                dataGridView2.Columns.Add("column1", "Column 1");
                dataGridView2.Columns.Add("column2", "Column 2");
                dataGridView2.Columns.Add("column3", "Column 3");
                dataGridView2.Columns.Add("column4", "Column 4");

                dataGridView2.Columns[0].Width = 40;
                dataGridView2.Columns[1].Width = 40;
                dataGridView2.Columns[2].Width = 40;
                dataGridView2.Columns[3].Width = 40;

                for (int i = 0; i < 6; i++)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.Height = 40;
                    row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"praznodesno.bmp") });
                    row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"praznodesno.bmp") });
                    row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"praznodesno.bmp") });
                    row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"praznodesno.bmp") });
                    dataGridView2.Rows.Add(row);
                }

            }
            else
            {
                //imalo je sta da se serijalizuje
                textBox1.Visible = false;
                label1.Visible = false;
                button7.Visible = false;
                this.nom = fs.nom;
                this.combination = fs.combination;

                //preslikavanje levog DataGridViewa iz fajla
                dataGridView1.Columns.Add("column1", "Column 1");
                dataGridView1.Columns.Add("column2", "Column 2");
                dataGridView1.Columns.Add("column3", "Column 3");
                dataGridView1.Columns.Add("column4", "Column 4");

                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[1].Width = 40;
                dataGridView1.Columns[2].Width = 40;
                dataGridView1.Columns[3].Width = 40;

                for (int i = 0; i < 6; i++)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.Height = 40;
                    for (int j = 0; j < 4; j++)
                    {
                        if(fs.dg1[i,j] == 0)
                            row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"praznolevo.bmp") });

                        if (fs.dg1[i,j] == 1)
                            row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"hearts.bmp") });

                        if (fs.dg1[i,j] == 2)
                            row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"karo.bmp") });

                        if (fs.dg1[i,j] == 3)
                            row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"pik.bmp") });

                        if (fs.dg1[i,j] == 4)
                            row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"tref.bmp") });

                        if (fs.dg1[i,j] == 5)
                            row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"zvezda.bmp") });

                        if (fs.dg1[i,j] == 6)
                            row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"skocko.bmp") });
                    }
                    dataGridView1.Rows.Add(row);
                }

                //preslikavanje desnog DataGridViewa iz fajla
                dataGridView2.Columns.Add("column1", "Column 1");
                dataGridView2.Columns.Add("column2", "Column 2");
                dataGridView2.Columns.Add("column3", "Column 3");
                dataGridView2.Columns.Add("column4", "Column 4");

                dataGridView2.Columns[0].Width = 40;
                dataGridView2.Columns[1].Width = 40;
                dataGridView2.Columns[2].Width = 40;
                dataGridView2.Columns[3].Width = 40;

                for (int i = 0; i < 6; i++)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.Height = 40;

                    for (int j = 0; j < 4; j++)
                    {
                        if (fs.dg2[i,j] == 0)
                            row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"praznodesno.bmp") });
                        if (fs.dg2[i,j] == 1)
                            row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"pogodak.bmp") });
                        if (fs.dg2[i,j] == 2)
                            row.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"polupokodak.bmp") });
                    }
                    dataGridView2.Rows.Add(row);
                }
            }

            //popunjavanje donjeg DataGridViewa
            dataGridView4.Columns.Add("column1", "Column 1");
            dataGridView4.Columns.Add("column2", "Column 2");
            dataGridView4.Columns.Add("column3", "Column 3");
            dataGridView4.Columns.Add("column4", "Column 4");

            dataGridView4.Columns[0].Width = 40;
            dataGridView4.Columns[1].Width = 40;
            dataGridView4.Columns[2].Width = 40;
            dataGridView4.Columns[3].Width = 40;

            DataGridViewRow row1 = new DataGridViewRow();
            row1.Height = 40;
            row1.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"praznolevo.bmp") });
            row1.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"praznolevo.bmp") });
            row1.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"praznolevo.bmp") });
            row1.Cells.Add(new DataGridViewImageCell { Value = new Bitmap(@"praznolevo.bmp") });
            dataGridView4.Rows.Add(row1);

            //Popunjavanje desno DataGridViewa podacima iz baze podataka
            dataGridView3.Rows.Clear();
            List<Participant> p = this.giveMeTop10();

            foreach (var part in p)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView3.Rows[0].Clone();
                row.Cells[0].Value = part.Username;
                row.Cells[1].Value = part.Time;
                row.Cells[2].Value = part.NumberOfMoves;
                dataGridView3.Rows.Add(row);
            }
            sw.Start();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Unos imena igraca i omogucavanje dalje igre
            if (textBox1.Text != "")
            {
                this.p.Username = textBox1.Text;
                this.fs.Username = textBox1.Text;

                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;

                textBox1.Visible = false;
                button7.Visible = false;
                label1.Visible = false;

                sw.Start();
            }
            else
                MessageBox.Show("Niste uneli username igraca");
        }
        //provera koliko njih je na pravom, a koliko njih na pogresnom mestu
        public int[] checkAccuracy()
        {
            int[] placed = new int[2];
            placed[0] = 0;
            placed[1] = 0;

            int[] indicator = new int[4];
            indicator[0] = 0;
            indicator[1] = 0;
            indicator[2] = 0;
            indicator[3] = 0;

            int[] indicator1 = new int[4];
            indicator1[0] = 0;
            indicator1[1] = 0;
            indicator1[2] = 0;
            indicator1[3] = 0;

            //the ones on right place
            for (int i = 0; i < 4; i++)
                if (this.current[i] == this.combination[i])
                {
                    placed[0]++;
                    indicator[i] = 1;
                    indicator1[i] = 1;
                }
            //the ones on wrong place
            for (int i = 0; i < 4; i++)
                for(int j = 0; j < 4; j++)
                    if(this.combination[i] == this.current[j] && indicator[i] == 0 && indicator1[j] == 0)
                    {
                        indicator1[j] = 1;
                        indicator[i] = 1;
                        placed[1]++;
                        break;
                    }

            if (placed[0] == 4)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;

                this.giveMeSolution();
                this.insertIntoDatabase();
            }
            return placed;
        }

        //unos igraca u bazu podataka
        private void insertIntoDatabase()
        {
            string conn = @"Data Source=(localdb)\seminarski;Initial Catalog=seminarski;Integrated Security=True";
            SqlConnection instance = new SqlConnection(conn);
            instance.Open();
            sw.Stop();
            float time = sw.ElapsedMilliseconds / 1000 + fs.time;

            string upit = @"INSERT INTO Spisak_igraca(username, time, number_of_moves) VALUES ('" + p.Username + "', " + time + ", " + (this.nom / 4) + ")";

            SqlCommand command = new SqlCommand(upit, instance);
            command.ExecuteNonQuery();
            instance.Close();
        }

        //brisanje svih igraca iz baze podataka
        public void deleteFromDatabase()
        {
            string conn = @"Data Source=(localdb)\seminarski;Initial Catalog=seminarski;Integrated Security=True";
            SqlConnection instance = new SqlConnection(conn);
            instance.Open();

            string upit = @"DELETE FROM Spisak_igraca WHERE 1=1";

            SqlCommand command = new SqlCommand(upit, instance);
            command.ExecuteNonQuery();
            instance.Close();
        }

        //lista top 10 najboljih igraca sortirana po vremenu i broju poteza
        private List<Participant> giveMeTop10()
        {
            List<Participant> p = new List<Participant>();

            string conn = @"Data Source=(localdb)\seminarski;Initial Catalog=seminarski;Integrated Security=True";
            SqlConnection instance = new SqlConnection(conn);
            instance.Open();

            string upit = @"select top 10 username, time, number_of_moves from Spisak_igraca order by time, number_of_moves";
            SqlCommand command = new SqlCommand(upit, instance);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Participant part = new Participant();
                part.Username = reader["username"].ToString();
                part.Time = int.Parse(reader["time"].ToString());
                part.NumberOfMoves = int.Parse(reader["number_of_moves"].ToString());
                p.Add(part);
            }

            instance.Close();
            return p;
        }

        //ispis resenja u donji DataGridView
        private void giveMeSolution()
        {
            for (int i = 0; i < 4; i++)
            {
                if (this.combination[i] == 0)
                    dataGridView4.Rows[0].Cells[i] = new DataGridViewImageCell { Value = new Bitmap(@"hearts.bmp") };
                if (this.combination[i] == 1)
                    dataGridView4.Rows[0].Cells[i] = new DataGridViewImageCell { Value = new Bitmap(@"karo.bmp") };
                if (this.combination[i] == 2)
                    dataGridView4.Rows[0].Cells[i] = new DataGridViewImageCell { Value = new Bitmap(@"pik.bmp") };
                if (this.combination[i] == 3)
                    dataGridView4.Rows[0].Cells[i] = new DataGridViewImageCell { Value = new Bitmap(@"tref.bmp") };
                if (this.combination[i] == 4)
                    dataGridView4.Rows[0].Cells[i] = new DataGridViewImageCell { Value = new Bitmap(@"zvezda.bmp") };
                if (this.combination[i] == 5)
                    dataGridView4.Rows[0].Cells[i] = new DataGridViewImageCell { Value = new Bitmap(@"skocko.bmp") };
            }
        }

        //popunjavanje srednjeg DataGridViewa na osnovu unetih simbola u levom DataGridViewu
        public void fillRight(int[] placed)
        {
            int i = 0;
            for(; i < placed[0]; i++)
            {
                dataGridView2.Rows[this.nom / 4 - 1].Cells[i] = new DataGridViewImageCell { Value = new Bitmap(@"pogodak.bmp") };
                fs.dg2[this.nom / 4 - 1,i] = 1;
            }
            for (; i < placed[0] + placed[1]; i++)
            {
                dataGridView2.Rows[this.nom / 4 - 1].Cells[i] = new DataGridViewImageCell { Value = new Bitmap(@"polupokodak.bmp") };
                fs.dg2[this.nom / 4 - 1,i] = 2;
            }
        }

        //unos simbola u levi DGV i u trenutnu kombinaciju + provera da li se doslo do kraja i kako su popunjeni
        public void insertCurrent(int znak)
        {
            fs.dg1[this.nom / 4, this.nom % 4] = znak;
            this.current[this.nom % 4] = znak - 1;

            if (znak == 1)
                dataGridView1.Rows[this.nom / 4].Cells[this.nom % 4] = new DataGridViewImageCell { Value = new Bitmap(@"hearts.bmp") };
            else if (znak == 2)
                dataGridView1.Rows[this.nom / 4].Cells[this.nom % 4] = new DataGridViewImageCell { Value = new Bitmap(@"karo.bmp") };
            else if (znak == 3)
                dataGridView1.Rows[this.nom / 4].Cells[this.nom % 4] = new DataGridViewImageCell { Value = new Bitmap(@"pik.bmp") };
            else if (znak == 4)
                dataGridView1.Rows[this.nom / 4].Cells[this.nom % 4] = new DataGridViewImageCell { Value = new Bitmap(@"tref.bmp") };
            else if (znak == 5)
                dataGridView1.Rows[this.nom / 4].Cells[this.nom % 4] = new DataGridViewImageCell { Value = new Bitmap(@"zvezda.bmp") };
            else if (znak == 6)
                dataGridView1.Rows[this.nom / 4].Cells[this.nom % 4] = new DataGridViewImageCell { Value = new Bitmap(@"skocko.bmp") };

            this.nom++;

            if (this.nom % 4 == 0)
            {
                int[] placed = this.checkAccuracy();
                this.fillRight(placed);
            }

            if (this.nom >= 24)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.insertCurrent(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.insertCurrent(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.insertCurrent(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.insertCurrent(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.insertCurrent(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.insertCurrent(6);
        }

        //ispis resenja
        private void button8_Click(object sender, EventArgs e)
        {
            this.giveMeSolution();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
        }

        //stvaranje nove igre
        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView4.Rows.Clear();

            label1.Visible = true;
            button7.Visible = true;
            textBox1.Visible = true;
            textBox1.Text = "";

            this.Form1_Load(sender, e);
        }

        //zatvaranje igre
        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //serijalizacija igre
        private void button11_Click(object sender, EventArgs e)
        {
            sw.Stop();
            string filePath = "data.save";
            DataSerializer dataSerializer = new DataSerializer();
            fs.nom = this.nom;
            fs.Username = this.p.Username;
            fs.time = sw.ElapsedMilliseconds / 1000;
            dataSerializer.BinarySerialize(fs, filePath);
            MessageBox.Show("Igrica sacuvana uspesno");
        }

        //brisanje poslednje sacuvane igre
        private void button12_Click(object sender, EventArgs e)
        {

            if (File.Exists("data.save"))
            {
                File.Delete("data.save");
                MessageBox.Show("Poslednja sacuvana igrica uspesno obrisana");
            }
            else
                MessageBox.Show("Nemate sta da obrisete");
        }

        //brisanje podataka iz baze podataka
        private void button13_Click(object sender, EventArgs e)
        {
            this.deleteFromDatabase();
            dataGridView3.Rows.Clear();
            List<Participant> p = this.giveMeTop10();

            foreach (var part in p)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView3.Rows[0].Clone();
                row.Cells[0].Value = part.Username;
                row.Cells[1].Value = part.Time;
                row.Cells[2].Value = part.NumberOfMoves;
                dataGridView3.Rows.Add(row);
            }

        }
    }
}
