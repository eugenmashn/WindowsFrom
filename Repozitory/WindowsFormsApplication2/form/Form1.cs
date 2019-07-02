using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        static WorkerContext db;
        public Form1()
        {
            db = new WorkerContext();
            //_db = new WorkerContext();
            InitializeComponent();
            db.Peoples.Load();
            dataGridView1.DataSource = db.Peoples.Local.ToBindingList();
     
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add addForm = new Add();
            DialogResult result = addForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            People people = new People();
            people.Name = addForm.textBox1.Text;
            people.LastName = addForm.textBox2.Text;
            people.Day = (int)addForm.numericUpDown1.Value;
            people.Year = (int)addForm.numericUpDown2.Value;
            db.Peoples.Add(people);
            db.SaveChanges();
            dataGridView1.DataSource = null;
            dataGridView1.Update();
            db.Peoples.Load();
            dataGridView1.DataSource = db.Peoples.AsNoTracking().ToList();
            dataGridView1.Refresh();

        }

        public void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                HolyDay holydayn = new HolyDay();

                int Daysu;
              
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                  if (converted == false)
                    return;
                
               
                holydayn.People = db.Peoples.Find(id);
                AddHol addHolForm = new AddHol(holydayn.People.Day);

                if (holydayn.People.Day < 1) {
                    MessageBox.Show("don`t have weekend!!!");
                    return;
                }

                DialogResult result = addHolForm.ShowDialog(this);
                holydayn.FirstDate = addHolForm.dateTimePicker1.Value;


                holydayn.IndexDate = false;
                addHolForm.dateTimePicker2.MaxDate= addHolForm.dateTimePicker1.Value.AddDays(holydayn.People.Day);
              holydayn.SecontDate = addHolForm.dateTimePicker2.Value;
                 if (result == DialogResult.Cancel)
                    return;
                Daysu = holydayn.People.Day-holydayn.SecontDate.Subtract(holydayn.FirstDate).Days;
                holydayn.People.Day = Daysu;
                holydayn.Days = holydayn.SecontDate.Subtract(holydayn.FirstDate).Days;
                db.HolyDays.Add(holydayn);
                db.SaveChanges();
                dataGridView1.DataSource = null;
                dataGridView1.Update();
                db.Peoples.Load();
                dataGridView1.DataSource = db.Peoples.AsNoTracking().ToList();
                dataGridView1.Refresh();
               
           
            }
                
               
               
        }
      
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                People peopl = db.Peoples.FirstOrDefault(c=> c.Id==id);

                
                
                db.Peoples.Remove(peopl);
                   
                    db.SaveChanges();
                dataGridView1.DataSource = null;
                dataGridView1.Update();
                db.Peoples.Load();
                dataGridView1.DataSource = db.Peoples.AsNoTracking().ToList();
                dataGridView1.Refresh();




            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
               
                
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
              
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                date addHolForm = new date(id);

                addHolForm.onupdate += new ONupdate(evenstb);
                DialogResult result = addHolForm.ShowDialog(this);
              dataGridView1.Update();
              dataGridView1.Refresh();
                if (result == DialogResult.OK) {
                    addHolForm.Close();
                   
                    return;
                }
             
            }
             
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Add addForm = new Add();
           
                People people = new People();
                People peopleOne = new People();
                peopleOne = db.Peoples.Find(id);
                people.Name = peopleOne.Name;
                people.LastName = peopleOne.LastName;
                people.Day = (int)peopleOne.Day+18;
                people.Year = (int)peopleOne.Year+1;
                db.Peoples.Add(people);
                db.SaveChanges();

                dataGridView1.DataSource = null;
                dataGridView1.Update();
                db.Peoples.Load();
                dataGridView1.DataSource = db.Peoples.AsNoTracking().ToList();
                dataGridView1.Refresh();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sort sort = new Sort();
            DialogResult result = sort.ShowDialog(this);
            if (result == DialogResult.OK)
                return;

        }
        private void update(object sender, EventArgs e) {

            this.Refresh();
        }
        void evenstb()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Update();
            db.Peoples.Load();
            dataGridView1.DataSource = db.Peoples.AsNoTracking().ToList();
            dataGridView1.Refresh();
        }
    }
}
