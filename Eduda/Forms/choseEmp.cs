using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eduda.Forms
{
    public partial class choseEmp : Form
    {
        public choseEmp()
        {
            InitializeComponent();
        }

        private void choseEmp_Load(object sender, EventArgs e)
        {
            DT dt = new DT();
            string state1 = @"
SELECT
    *
from Teacher;
";

            DataTable t = dt.QuaryValue(state1);
            this.gridControl1.DataSource = t;

           
        }
      public   string name,  type;
   
        private void button1_Click(object sender, EventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            if (rowHandle >= 0)
            {
                DialogResult result = MessageBox.Show("هل تريد هذا الموظف؟", "تأكيد الموظف", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    name = gridView1.GetRowCellValue(rowHandle, "name_teacher").ToString();
                    MessageBox.Show("تم الحفظ");
                    doit = true;
                    gridView1.RefreshData();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("حدث خطأ");
                    doit = false;
                    this.Close();
                }
            }
        }

        public bool doit =false;
       

        private void gridControl1_Click(object sender, EventArgs e)
        {
            

        }
    }
}
