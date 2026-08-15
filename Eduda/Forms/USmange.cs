using DevExpress.CodeParser;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
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
using static DevExpress.XtraEditors.Mask.MaskSettings;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Eduda.Forms
{
    public partial class USmange : DevExpress.XtraEditors.XtraUserControl
    {
        public USmange()
        {
            InitializeComponent();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // تحديث السطر الحالي لضمان حفظ التعديلات
                gridView1.UpdateCurrentRow();

                // هنا نقوم بتحديث البيانات في قاعدة البيانات
                UpdateDatabase();

                // عرض رسالة للمستخدم بعد حفظ التعديلات
                MessageBox.Show("تم حفظ التعديلات بنجاح!", "حفظ البيانات", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // عرض رسالة خطأ إذا حدث
                MessageBox.Show("حدث خطأ أثناء الحفظ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void UpdateDatabase()
        {
            // افترض أنك تستخدم DataTable مرتبطة بـ GridControl
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Users", DT.constring);

            // إنشاء SqlCommandBuilder لإنشاء استعلامات التحديث (INSERT/UPDATE/DELETE)
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

            DataSet Users = null;
            // تحديث قاعدة البيانات
            dataAdapter.Update(Users);
        }
        

   
        private void USmange_Load(object sender, EventArgs e)
        {            // السماح بالتحرير للـ gridView2 فقط
            gridView1.OptionsBehavior.Editable = false;  // لا يسمح بالتحرير في gridView1
            gridView1.OptionsBehavior.ReadOnly = true;   // قراءة فقط في gridView1
            gridView2.OptionsBehavior.Editable = true;   // السماح بالتحرير في gridView2
            gridView2.OptionsBehavior.ReadOnly = false;  // ليس قراءة فقط في gridView2

            // استعلام لإحضار بيانات المستخدمين
            DT dt = new DT();
            string state1 = @"
    SELECT
        id_user,
        username,
        password,
        type,
        allow
    FROM Users;
    ";

            DataTable t = dt.QuaryValue(state1);
            this.gridControl1.DataSource = t;  // ربط الـ GridControl بالـ DataTable

            // تعيين الـ Caption للأعمدة باللغة العربية فقط للعرض في الـ GridView
            gridView1.Columns["id_user"].Caption = "رقم اليوزر";
            gridView1.Columns["username"].Caption = "اسم المستخدم";
            gridView1.Columns["password"].Caption = "كلمة المرور";
            gridView1.Columns["type"].Caption = "الرتبة";
            gridView1.Columns["allow"].Caption = "هل مسموح الدخول؟";
            string state2 = @"
SELECT
   id,
teacher,
salary,
Reward,
type,
Total
FROM Salary;
";


            DataTable b = dt.QuaryValue(state2);
            this.gridControl2.DataSource = b;
            // تعيين الأسماء المعروضة باللغة العربية فقط للعرض في الـ GridView
            gridView2.Columns["id"].Caption = "رقم الراتب";
            gridView2.Columns["teacher"].Caption = "اسم الموظف";
            gridView2.Columns["salary"].Caption = "الراتب";
            gridView2.Columns["Reward"].Caption = "المكافأة";
            gridView2.Columns["type"].Caption = "الوظيفه";
            gridView2.Columns["Total"].Caption = "الإجمالي";

        }


        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dt = (DataTable)gridControl1.DataSource;

            int lastId = 0;
            if (dt.Rows.Count > 0)
            {
                lastId = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["id_user"]);
            }

            // إضافة صف جديد مع زيادة 1 على آخر ID
            DataRow newRow = dt.NewRow();
            newRow["id_user"] = lastId + 1;

            if (string.IsNullOrEmpty(newRow["username"].ToString()))
            {
                newRow["username"] = "admin";  // استبدل هذه القيمة بالقيمة الافتراضية التي تريدها
            }
            if (string.IsNullOrEmpty(newRow["password"].ToString()))
            {
                newRow["password"] = "admin";  // استبدل هذه القيمة بالقيمة الافتراضية التي تريدها
            }
            if (string.IsNullOrEmpty(newRow["allow"].ToString()))
            {
                newRow["allow"] = "False";  // استبدل هذه القيمة بالقيمة الافتراضية التي تريدها
            }

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


            // الحصول على الصف المحدد
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

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
           SaveChangesToDatabase(gridControl1, "Users");

           




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

        private void button1_Click(object sender, EventArgs e)
        {
            choseEmp choseEmp = new choseEmp();
            choseEmp.ShowDialog();

            try
            {
                if (choseEmp.doit)
                {
                    DataTable dt = (DataTable)gridControl2.DataSource;

                    int lastId = 0;

                    if (dt.Rows.Count > 0)
                    {
                        lastId = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["id"]);
                    }

                    DataRow newRow = dt.NewRow();
                    newRow["id"] = lastId + 1;


                    if (string.IsNullOrEmpty(newRow["salary"].ToString()))
                    {
                        newRow["salary"] = 1000;
                    }
                    if (string.IsNullOrEmpty(newRow["Reward"].ToString()))
                    {
                        newRow["Reward"] = 0;
                    }

                    if (string.IsNullOrEmpty(newRow["teacher"].ToString()))
                    {
                        newRow["teacher"] = choseEmp.name;
                    }
                    dt.Rows.Add(newRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Globals.typevv == "Manager")
            {
                MessageBox.Show("لقد تم فتح التعديل ");
                gridView2.OptionsBehavior.Editable = true; // السماح بالتحرير
                gridView2.OptionsBehavior.ReadOnly = false;
            }
            else
            {
                MessageBox.Show("ليست لديك صلاحيه التعديل");
            }
        }
    }
}
