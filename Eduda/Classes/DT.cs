using DevExpress.Xpo.DB.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace Eduda
{
    public class DT
    {
      public  static string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;
      AttachDbFilename=|DataDirectory|\Shcool Management System.mdf;
      Integrated Security=True";
    /*  public  static string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;
      AttachDbFilename=|DataDirectory|\EdudaDB.mdf;
      Integrated Security=True";*/
        private int Cmd;
        private SqlConnection Con = new SqlConnection(constring);
        public string Conne()
        { return constring; }
        public void OpenCon()
        {
            if (Con.State != ConnectionState.Open)
            {
                try
                {
                    Con.Open();

                }
                catch (Exception)
                {
                    MessageBox.Show("Error");

                }
               
            }
           
        }
        public void CloseCon()
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }
        }
        public string Int2String(string id)
        {
            string y = "";
            SqlConnection Con = new SqlConnection(constring);
            SqlCommand Cmd = Con.CreateCommand();
            try
            {
                OpenCon();
                Cmd.Connection = Con;
                Cmd.CommandText = id;
                y = Cmd.ExecuteScalar().ToString();
                return y;
            }
            catch (Exception)
            {

                return y;
            }
            finally
            {
                CloseCon();
            }
        }

        public bool QuaryNon(string statement)
        {
            try
            {
                OpenCon();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = statement;
                cmd.Connection = Con;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            finally
            {
                CloseCon();
                
            }
        }
        public DataTable AllData_Student()
        {
           
            string stat = $"SELECT\r\n    Student_id,\r\n" +
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
          $"FROM Student;\r\n";

            // استدعاء الدالة لتمرير الاستعلام وتنفيذها
            DataTable q = QuaryValue(stat);

            return q;
        }

        public DataTable QuaryValue(string statement)
        {
            try
            {
                // إنشاء كائن DataTable لتحميل البيانات
                DataTable db = new DataTable();

                // إنشاء الاتصال
                using (SqlConnection Con = new SqlConnection(constring))
                {
                    Con.Open();  // فتح الاتصال

                    // إعداد الأمر
                    using (SqlCommand cmd = new SqlCommand(statement, Con))
                    {
                        cmd.CommandType = CommandType.Text;

                        // تنفيذ الاستعلام وملء DataTable بالنتيجة
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            db.Load(reader);
                        }
                    }
                }

                return db;
            }
            catch (Exception ex)
            {
                // يمكنك تسجيل الخطأ هنا إذا أردت
                Console.WriteLine($"Error: {ex.Message}");
                return new DataTable();  // إعادة DataTable فارغ في حالة حدوث خطأ
            }
            finally { CloseCon(); } 
        }/*
        public DataTable QuaryValue2(string statement)
        {
            try
            {
                // إنشاء كائن DataTable لتحميل البيانات
                DataTable db = new DataTable();

                // إنشاء الاتصال
                using (SqlConnection Con = new SqlConnection(constring))
                {
                    Con.Open();  // فتح الاتصال

                    // إعداد الأمر
                    using (SqlCommand cmd = new SqlCommand(statement, Con))
                    {
                        cmd.CommandType = CommandType.Text;

                        // تنفيذ الاستعلام وملء DataTable بالنتيجة
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            db.Load(reader);
                        }
                    }
                }

                return db;
            }
            catch (Exception ex)
            {
                // يمكنك تسجيل الخطأ هنا إذا أردت
                Console.WriteLine($"Error: {ex.Message}");
                return new DataTable();  // إعادة DataTable فارغ في حالة حدوث خطأ
            }
        }
       */

       

        public int String2Int(String str)
        {
            int x = 0;
                SqlConnection Con = new SqlConnection( constring );
                SqlCommand Cmd = new SqlCommand();
            try
            {
               
                OpenCon();
                Cmd.Connection = Con;
                Cmd.CommandText = str;
                x = Convert.ToInt32(Cmd.ExecuteScalar());
                return x;

            }
            catch (Exception)
            {

                return x;
            }
            finally
            {
                CloseCon( );
            }
        }
        public bool checkUser(string use)
        {
            SqlConnection connection = new SqlConnection(constring);
            string query = "SELECT * FROM Users WHERE username = @UserName";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@UserName", use);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close();
                    return true;

                }
                else
                {
                    reader.Close();

                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

            public bool checkallow(string user)
        {
           
            string querycheck = "SELECT allow FROM Users WHERE username = '" + user + "';";
            DataTable dt = QuaryValue(querycheck);

            string now = "";
            if (dt.Rows.Count > 0)
            {
                now = dt.Rows[0][0].ToString();  // الصف الأول، العمود الأول
            }

           
            if (now == "True")

                return true;
            else
                return false;
        }


        public string checktype(string user)
        {
            string querycheck2 = "SELECT Type FROM Users WHERE username = '" + user + "';";
            DataTable ta = QuaryValue(querycheck2);

            // تعيين قيمة افتراضية
            string b = "";

            if (ta.Rows.Count > 0)
            {
                b = ta.Rows[0][0].ToString();  // الصف الأول، العمود الأول
            }

            if (b == "Manager")
            {
                return "Manager";
            }
            else if (b == "Teacher")
            {
                return "Teacher";
            }
            else
            {
                return "vis";
            }
        }


        public bool checkPass(string use)
        {
            SqlConnection connection = new SqlConnection(constring);
            string query = "SELECT * FROM Users WHERE password = @UserName"; 
            SqlCommand cmd = new SqlCommand(query, connection); 

            cmd.Parameters.AddWithValue("@UserName", use);

            try
            {
                connection.Open(); 

                SqlDataReader reader = cmd.ExecuteReader(); 

                if (reader.HasRows) 
                {
                    reader.Close(); 
                    return true;

                }
                else
                {
                    reader.Close();

                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }




        }
    }
}