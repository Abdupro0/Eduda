using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using DevExpress.XtraGrid;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Eduda.Forms
{
    public partial class USstudents : DevExpress.DXperience.Demos.TutorialControlBase
    {
        public USstudents()
        {
            InitializeComponent();
        }

        private void USstudents_Load(object sender, EventArgs e)
        {
            DT dt = new DT();
           DataTable t= dt.AllData_Student();
           this.gridControl1.DataSource = t;
            gridView1.OptionsBehavior.Editable = false; // عدم السماح بالتحرير
            gridView1.OptionsBehavior.ReadOnly = true;
           

            gridView1.Columns["Student_id"].Caption = "رقم الطالب";
            gridView1.Columns["Student_Name"].Caption = "اسم الطالب";
            gridView1.Columns["Birthday"].Caption = "تاريخ الميلاد";
            gridView1.Columns["city"].Caption = "المدينة";
            gridView1.Columns["Gender"].Caption = "الجنس";
            gridView1.Columns["phone"].Caption = "رقم الموبايل";
            gridView1.Columns["study_Level"].Caption = "المرحلة الدراسية";
            gridView1.Columns["Class"].Caption = "الشعبه";

            gridView1.Columns["image"].Caption = "الصورة";
            gridView1.Columns["Guardian"].Caption = "ولي الأمر";
            gridView1.Columns["Neighborhood"].Caption = "الحي";

            gridView1.Columns["page"].Caption = "الصفحة";
            gridView1.Columns["Old_school"].Caption = "المدرسة السابقة";
            gridView1.Columns["Recruitment_Department"].Caption = "قسم الاستقدام";

            gridView1.Columns["Transfer_document_total"].Caption = "عدد مستندات النقل";
            gridView1.Columns["Document_date"].Caption = "تاريخ المستند";

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dt = (DataTable)gridControl1.DataSource;

            // إيجاد آخر قيمة للعمود "رقم الطالب"
            int lastId = 0;
            if (dt.Rows.Count > 0)
            {
                lastId = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Student_id"]);
            }

            // إضافة صف جديد مع زيادة 1 على آخر ID
            DataRow newRow = dt.NewRow();
            newRow["Student_id"] = lastId + 1;

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

        private void button1_Click(object sender, EventArgs e)
        {
            SaveChangesToDatabase(gridControl1, "Student");
        }
        private void SaveChangesToDatabase(DevExpress.XtraGrid.GridControl gridControl, string tableName)
        {
            // الحصول على الـ DataTable المرتبط بـ GridControl
            DataTable dt = (DataTable)gridControl.DataSource;


            // إنشاء SqlDataAdapter للتعامل مع قاعدة البيانات
            SqlDataAdapter dataAdapter = new SqlDataAdapter($"SELECT\r\n    Student_id,\r\n" +
          $"    Student_Name,\r\n" +
          $"    Gender,\r\n" +
          $"    Birthday,\r\n" +
          $"    study_Level,\r\n" +
          $"    city,\r\n" +
          $"    phone,\r\n" +
          $"    Class,\r\n" +
          $"    image,\r\n" +
          $"    Guardian,\r\n" +
          $"    Neighborhood,\r\n" +
          $"    Alley_and_number,\r\n" +
          $"    Nationality,\r\n" +
          $"    page,\r\n" +
          $"    Old_school,\r\n" +
          $"    Recruitment_Department,\r\n" +
          $"    Transfer_document_total,\r\n" +
          $"    Document_date\r\n" +
          $"FROM \r\n { tableName}", DT.constring);

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
    }

}
