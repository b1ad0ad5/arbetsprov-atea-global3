using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string fullFilePath = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.Length - 22, 22) + "website\\App_Data\\Database1.mdf";

            int repeaterValue = 1;
            while (repeaterValue != 0)
            {
                string endMessage = "You inputed blank, try again!";
                try
                {
                    Console.WriteLine("Type message to send to website here:");
                    string text = Console.ReadLine();
                    DateTime date = DateTime.Now;
                    if (text != "")
                    {
                        endMessage = string.Format("Success! saved: '{0} - {1}' to database", date, text);
                        using (SqlConnection sqlCon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + fullFilePath))
                        {
                            sqlCon.Open();
                            SqlCommand cmd = sqlCon.CreateCommand();
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.CommandText = "insert into table1 values(@date, @text)";
                            cmd.Parameters.Add(new SqlParameter("@date", date));
                            cmd.Parameters.Add(new SqlParameter("@text", text));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    Console.WriteLine(endMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("n�got gick snett! felmeddelande: " + ex.Message);
                }
            }
        }
    }
}
