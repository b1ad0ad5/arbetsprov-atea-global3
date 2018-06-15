using System;
using System.Data.SqlClient;

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
                        endMessage = "Text too long, 50 characters max!";
                        if (text.Length <= 50)
                        {
                            using (SqlConnection sqlCon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + fullFilePath))
                            {
                                sqlCon.Open();
                                SqlCommand cmd = sqlCon.CreateCommand();
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = "insert into table1 values(@date, @text)";
                                cmd.Parameters.Add(new SqlParameter("@date", date));
                                cmd.Parameters.Add(new SqlParameter("@text", text));
                                cmd.ExecuteNonQuery();
                                endMessage = string.Format("Success! saved: '{0} - {1}' to database", date, text);
                            }
                        }
                    }
                    Console.WriteLine(endMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong! error message: " + ex.Message);
                }
            }
        }
    }
}
