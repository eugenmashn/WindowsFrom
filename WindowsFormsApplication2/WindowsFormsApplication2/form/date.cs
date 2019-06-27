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
    
    
    public partial class date : Form
    {
        int _Id;
        WorkerContext _db;
        public class HollydayTwo {
            public int Id;
            public DateTime FirstDate;
            public DateTime SecondDate;
        }
        public date(int Id)
        {
           
            _db = new WorkerContext();
            InitializeComponent();
            _Id = Id;
            _db.HolyDays.Load();
            BindingSource DatedbOne = new BindingSource();
            var DatedbOneK=from w in _db.HolyDays.Local
                       where (w.Peopleid== _Id)
                       select w;
            var qieryAsList = new BindingList<HolyDay>(DatedbOneK.ToList());
            DatedbOne.DataSource = qieryAsList;
            dataGridView1.DataSource = DatedbOne;
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void date_Load(object sender, EventArgs e)
        {

        }
   

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                HolyDay peoplday = _db.HolyDays.FirstOrDefault(c => c.Id == id);

                BindingSource DatedbOne = new BindingSource();
                var DatedbOneK = from w in _db.HolyDays.Local
                                 where (w.Peopleid == _Id)
                                 select w;
             

                _db.HolyDays.Remove(peoplday);

                _db.SaveChanges();

                 DatedbOneK = from w in _db.HolyDays.Local
                                 where (w.Peopleid == _Id)
                                 select w;
                var qieryAsList = new BindingList<HolyDay>(DatedbOneK.ToList());
                DatedbOne.DataSource = qieryAsList;
                dataGridView1.DataSource = DatedbOne;
                MessageBox.Show("object delete");

            }
        }
    }
}
