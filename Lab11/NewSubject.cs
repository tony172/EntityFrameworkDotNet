using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab11
{
    public partial class NewSubject : Form
    {
        public Predmeti predmet { get; }
        public NewSubject()
        {
            predmet = new Predmeti();
            InitializeComponent();
        }

        public NewSubject(Predmeti p)
        {
            InitializeComponent();
            this.predmet = p;
            this.textBox1.Text = p.Id.ToString();
            this.textBox2.Text = p.Naziv.Trim();
            foreach (var student in Form1.db.Studentis)
            {
                checkedListBox1.Items.Add(student.ToString());
            }

            var predmetiResult = from predmet in Form1.db.Predmetis
                                 where predmet.Id == p.Id
                                 select predmet.Studentis;
            foreach (var items in predmetiResult)
            {
                foreach(var student in items)
                {
                    for(int i=0; i<checkedListBox1.Items.Count; i++)
                    {
                        string[] id = checkedListBox1.Items[i].ToString().Split(new char[] { '(', ')'});
                        if(student.Id.ToString() == id[1])
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.predmet.Naziv = textBox2.Text;
            foreach (var student in this.predmet.Studentis.ToList<Studenti>())
            {
                this.predmet.Studentis.Remove(student);
            }
            IEnumerable<Studenti> studentiResult = null;
            foreach(var item in checkedListBox1.CheckedItems)
            {
                string[] id = item.ToString().Split(new char[] { '(', ')' });
                var tmp = id[1];
                studentiResult = from student in Form1.db.Studentis
                                     where tmp == student.Id.ToString()
                                     select student;
                foreach(Studenti student in studentiResult)
                {
                    this.predmet.Studentis.Add(student);
                }
            }

            
        }
    }
}
