using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab11
{
    public partial class Form1 : Form
    {
        public static StudentiEntities db;
        public Form1()
        {
            db = new StudentiEntities();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;





        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedItem == "Studenti")
            {
                updateList(0);
            }
            else if (cb.SelectedItem == "Predmeti")
            {
                updateList(1);
            }

        }

        private void updateList(int t)
        {
            listView1.Clear();

            if (t == 0)
            {
                listView1.Columns.Add("ID").Width = 100;
                listView1.Columns.Add("Ime").Width = 100;
                listView1.Columns.Add("Prezime").Width = 100;
                foreach (var item in db.Studentis)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = item.Id.ToString();
                    lvi.SubItems.Add(item.Ime);
                    lvi.SubItems.Add(item.Prezime);
                    lvi.Tag = item;
                    listView1.Items.Add(lvi);

                }
            }
            else if (t == 1)
            {
                listView1.Columns.Add("ID").Width = 100;
                listView1.Columns.Add("Naziv").Width = 200;
                foreach (var item in db.Predmetis)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = item.Id.ToString();
                    lvi.SubItems.Add(item.Naziv);
                    lvi.Tag = item;
                    listView1.Items.Add(lvi);

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem == "Studenti")
            {

                NewStudent form = new NewStudent();
                Studenti student = form.student;
                form.Text = "Dodaj studenta";
                if (form.ShowDialog() == DialogResult.OK)
                {

                    db.Studentis.Add(student);
                    db.SaveChanges();
                  

                    updateList(0);
                }

            }
            else if (comboBox1.SelectedItem == "Predmeti")
            {

                NewSubject form = new NewSubject();
                Predmeti predmet = form.predmet;
                form.Text = "Dodaj predmet";
                if (form.ShowDialog() == DialogResult.OK)
                {

                    db.Predmetis.Add(predmet);
                    db.SaveChanges();

                    updateList(1);
                }
            }

        }

        private void urediToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Studenti")
            {
                Studenti s = (Studenti)listView1.SelectedItems[0].Tag;
                // var student = db.Studentis.First<Studenti>(q => q.Id == s.Id);
                NewStudent form = new NewStudent(s);
                if (form.ShowDialog() == DialogResult.OK)
                {


                    try
                    {
                        db.SaveChanges();
                        updateList(0);
                    }
                    catch (Exception ex) { }

                }
            }
            else if(comboBox1.SelectedItem == "Predmeti")
            {
                Predmeti p = (Predmeti)listView1.SelectedItems[0].Tag;
                NewSubject form = new NewSubject(p);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        db.SaveChanges();
                        updateList(1);
                    }
                    catch (Exception ex) { }
                    
                }
            }
        }

        private void izbrišiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Studenti")
            {
                Studenti s = (Studenti)listView1.SelectedItems[0].Tag;
                foreach(var predmet in s.Predmetis.ToList<Predmeti>())
                {
                    s.Predmetis.Remove(predmet);
                }
                db.Studentis.Remove(s);
                db.SaveChanges();
                updateList(0);
            }
            else if(comboBox1.SelectedItem == "Predmeti")
            {
                Predmeti p = (Predmeti)listView1.SelectedItems[0].Tag;
                foreach(var student in p.Studentis.ToList<Studenti>())
                {
                    p.Studentis.Remove(student);
                }
                db.Predmetis.Remove(p);
                db.SaveChanges();
                updateList(1);
            }
        }
    }
}
