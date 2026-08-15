using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eduda.Forms
{
    public partial class USWorker : DevExpress.XtraEditors.XtraUserControl
    {
        public USWorker()
        {
            InitializeComponent();
        }

        private void USWorker_Load(object sender, EventArgs e)
        {
            DT dt = new DT();
            string state1 = @"SELECT *from Teacher ";
          

            DataTable t = dt.QuaryValue(state1);
            this.gridControl.DataSource = t;
            gridView1.Columns["id_teacher"].Caption = "رقم المدرس";
            gridView1.Columns["name_teacher"].Caption = "اسم المدرس";
            gridView1.Columns["Birthday"].Caption = "تاريخ الميلاد";
            gridView1.Columns["City"].Caption = "المدينة";
            gridView1.Columns["Gender"].Caption = "الجنس";
            gridView1.Columns["Mobile"].Caption = "رقم الموبايل";
            gridView1.Columns["Email"].Caption = "البريد الإلكتروني";
            gridView1.Columns["Circle"].Caption = "الدائرة";
            gridView1.Columns["Graduate"].Caption = "الخريج";
            gridView1.Columns["university"].Caption = "الجامعة";
            gridView1.Columns["college"].Caption = "الكلية";
            gridView1.Columns["number_of_children"].Caption = "عدد الأطفال";
            gridView1.Columns["Date_and_order_of_the_first_appointment"].Caption = "تاريخ وأمر التعيين الأول";
            gridView1.Columns["At_the_current_school"].Caption = "في المدرسة الحالية";
            gridView1.Columns["Jurisdiction"].Caption = "الجهة التابعة";
            gridView1.Columns["National_ID_number"].Caption = "رقم الهوية الوطنية";
            gridView1.Columns["National_ID_date"].Caption = "تاريخ إصدار الهوية";
            gridView1.Columns["Graduation_Year"].Caption = "سنة التخرج";
            gridView1.Columns["teacher_status"].Caption = "حالة المدرس";

            gridView1.OptionsBehavior.Editable = false; // عدم السماح بالتحرير
            gridView1.OptionsBehavior.ReadOnly = true;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dt = (DataTable)gridControl.DataSource;

            // إيجاد آخر قيمة للعمود "رقم الطالب"
            int lastId = 0;
            if (dt.Rows.Count > 0)
            {
                lastId = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["id_teacher"]);
            }

            // إضافة صف جديد مع زيادة 1 على آخر ID
            DataRow newRow = dt.NewRow();
            newRow["id_teacher"] = lastId + 1;

            // ضع قيم الأعمدة الأخرى إذا أردت، أو اتركها فارغة
            dt.Rows.Add(newRow);



        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Globals.typevv == "Manager")
            {
                MessageBox.Show("لقد تم فتح التعديل ");
                gridView1.OptionsBehavior.Editable = true; // السماح بالتحرير
                gridView1.OptionsBehavior.ReadOnly = false;
            }
            else
            {
                MessageBox.Show("ليست لديك صلاحيه التعديل");
            }

        }
        private void SaveChangesToDatabase(DevExpress.XtraGrid.GridControl gridControl, string tableName)
        {
            // الحصول على الـ DataTable المرتبط بـ GridControl
            DataTable dt = (DataTable)gridControl.DataSource;


            // إنشاء SqlDataAdapter للتعامل مع قاعدة البيانات
            SqlDataAdapter dataAdapter = new SqlDataAdapter($"SELECT * FROM {tableName}", DT.constring);

            // بناء SqlCommandBuilder لتوليد الاستعلامات (UPDATE, INSERT, DELETE) تلقائيًا
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

            // محاولة تحديث البيانات في قاعدة البيانات
            try
            {
                // استخدام SqlDataAdapter لتحديث البيانات في قاعدة البيانات
                dataAdapter.Update(dt);  // هذا سيقوم بتحديث التعديلات في قاعدة البيانات

                // تأكيد الحفظ
                MessageBox.Show("تم حفظ التعديلات بنجاح!", "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                // إذا حدث خطأ أثناء التحديث
                MessageBox.Show($"حدث خطأ أثناء حفظ البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Globals.typevv == "Manager")
            {// الحصول على الصف المحدد
                int rowHandle = gridView1.FocusedRowHandle;
                if (rowHandle >= 0)
                {
                    // تأكيد الحذف من المستخدم (اختياري)
                    DialogResult result = MessageBox.Show("هل تريد حذف هذا الصف؟", "تأكيد الحذف", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // حذف الصف من الـ DataTable
                        DataRow row = gridView1.GetDataRow(rowHandle);
                        row.Delete();  // أو gridView1.DeleteRow(rowHandle);

                        // تحديث GridControl تلقائيًا
                        gridView1.RefreshData();
                    }
                }
            }
            else
            {
                MessageBox.Show("معذرة ليست لديك صلاحية الحذف");
            }
        }

        private void gridControl_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveChangesToDatabase(gridControl, "Teacher");
        }
    }
}
