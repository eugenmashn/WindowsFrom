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
    public partial class Sort : Form
    {
        People peopl;
        HolyDay holydays;
        WorkerContext _db;
        int _Id;
        int YearIndex;
        public Sort(int Id, string name,string lastName)
        {
            _Id = Id;
            InitializeComponent();
            _db = new WorkerContext();
            _db.Peoples.Load();
            _db.HolyDays.Load();
            linkLabel2.Text = name;
            linkLabel3.Text = lastName;
            peopl = _db.Peoples.Find(_Id);
            YearIndex =peopl.Year;
            numericUpDown1.Value = YearIndex;
            if (_db.HolyDays.Count(i => i.FirstDate.Year == YearIndex)<1) {
                    MessageBox.Show("Don`t booked");
               
            }
            else {

                BindingSource DatedbOne = new BindingSource();
                var DatedbOnek = from w in _db.HolyDays.Local
                                where w.FirstDate.Year == YearIndex
                                where w.People.Id==_Id
                                select w;
                var qieryAsList = new BindingList<HolyDay>(DatedbOnek.ToList());
                DatedbOne.DataSource = qieryAsList;
                dataGridView1.DataSource = DatedbOne;

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            YearIndex = (int)numericUpDown1.Value;
            if (_db.HolyDays.Count(i => i.FirstDate.Year == YearIndex) < 1)
            {
                MessageBox.Show("Don`t booked");

            }
            else
            {

                BindingSource DatedbOne = new BindingSource();
                var DatedbOnek = from w in _db.HolyDays.Local
                                 where w.FirstDate.Year == YearIndex
                                 where w.People.Id==_Id
                                 select w;
                var qieryAsList = new BindingList<HolyDay>(DatedbOnek.ToList());
                DatedbOne.DataSource = qieryAsList;
                dataGridView1.DataSource = DatedbOne;

            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {


                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;

                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                date addHolForm = new date(id);
                DialogResult result = addHolForm.ShowDialog(this);

                if (result == DialogResult.OK)
                    return;

            }
        }

        private void Sort_Load(object sender, EventArgs e)
        {

        }
    }
}
