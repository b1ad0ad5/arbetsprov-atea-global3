using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using website.Models;

namespace website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                List<Message> messageList = new List<Message>();
                //db file path
                string fullFilePath = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.Length - 8, 8) + "website\\App_Data\\Database1.mdf";
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + fullFilePath + ";"))
                {
                    sqlCon.Open();
                    SqlCommand cmd = sqlCon.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    
                    cmd.CommandText = "select * from table1 order by 2 desc OFFSET " + 15 + " ROWS FETCH NEXT 15 ROWS ONLY";
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Message message = new Message
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Date = Convert.ToDateTime(dr["date"]),
                            Text = dr["text"].ToString()
                        };
                        messageList.Add(message);
                    }
                    ViewBag.Message = messageList;
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }
        public ActionResult Index2(int? pageNumber)
        {
            if (pageNumber != null)
            {
                
                int offsetRows = Convert.ToInt32(pageNumber) * 15;
                try
                {
                    List<Message> messageList = new List<Message>();
                    //db file path
                    string fullFilePath = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.Length - 8, 8) + "website\\App_Data\\Database1.mdf";
                    using (SqlConnection sqlCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + fullFilePath + ";"))
                    {
                        sqlCon.Open();
                        SqlCommand cmd = sqlCon.CreateCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select * from table1 order by 2 desc OFFSET " + offsetRows + " ROWS FETCH NEXT 15 ROWS ONLY";
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Message message = new Message
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Date = Convert.ToDateTime(dr["date"]),
                                Text = dr["text"].ToString()
                            };
                            messageList.Add(message);
                        }
                        ViewBag.Message = messageList;
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    ViewBag.message = ex.Message;
                    return View();
                }
            }
            else
            {
                return Redirect("/Home/Index2?pageNumber=1");
            }
        }
    }
}