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
    public partial class NewStudent : Form
    {
        public Studenti student { get; private set; }
        
        public NewStudent()
        {
            InitializeComponent();
            student = new Studenti();
            foreach (var predmet in Form1.db.Predmetis)
            {
                checkedListBox1.Items.Add(predmet.Naziv.Trim());
            }

        }

        public NewStudent(Studenti s)
        {
            InitializeComponent();
            foreach (var predmet in Form1.db.Predmetis)
            {
                checkedListBox1.Items.Add(predmet.Naziv);
            }
            this.student = s;
            this.textBox1.Text = s.Id.ToString();
            this.textBox2.Text = s.Ime.Trim();
            this.textBox3.Text = s.Prezime.Trim();

            var studentiResult = from studenti in Form1.db.Studentis
                                 where s.Id == studenti.Id
                                 select studenti.Predmetis;
            foreach(var items in studentiResult)
            {
                foreach(var predmet in items)
                {
                    for(int i=0; i<checkedListBox1.Items.Count; i++)
                    {
                        if(checkedListBox1.Items[i].ToString().Trim() == predmet.Naziv.Trim())
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }
            }
        
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            student.Ime = textBox2.Text.Trim();
            student.Prezime = textBox3.Text.Trim();
                   
                    IEnumerable<Predmeti> predmetiResult=null;
                    foreach (var item in checkedListBox1.CheckedItems)
                    {
                       predmetiResult = from predmet in Form1.db.Predmetis
                                             where item.ToString() == predmet.Naziv
                                             select predmet;
                    foreach(Predmeti p in predmetiResult)
                                        {
                                            student.Predmetis.Add(p);
                                        }
                    }

                    
                    


                

            
                
           
            
        }

       
    }
}
