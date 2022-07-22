using MyFirstProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyFirstProject.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                string EmpResponse = null;

                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage Res = await client.GetAsync("https://jsonplaceholder.typicode.com/todos/");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                }
                var stringJson = JsonConvert.DeserializeObject<IEnumerable<User>>(EmpResponse);
                
                SqlConnection con = null;
                using (con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UserDataBase; integrated security=SSPI"))
                {
                    con.Open();
                    string Deletequery = "delete from dbo.Even;delete from dbo.Odd;";
                    SqlCommand Deletecmd = new SqlCommand(Deletequery, con);
                    Deletecmd.ExecuteNonQuery();
                    foreach (var data in stringJson)
                    {
                        if (data.id%2==0)
                        {
                            string query= "INSERT INTO dbo.Even (id, title, completed) " +
                   "VALUES (@Id,@Title,@Completed)";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = data.id;
                            cmd.Parameters.Add("@Title", SqlDbType.VarChar, 40).Value = data.title;
                            cmd.Parameters.Add("@Completed", SqlDbType.VarChar,15).Value = data.completed;
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            string query = "INSERT INTO dbo.Odd (id, title, completed) " +
                   "VALUES (@Id,@Title,@Completed)";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = data.id;
                            cmd.Parameters.Add("@Title", SqlDbType.VarChar, 40).Value = data.title;
                            cmd.Parameters.Add("@Completed", SqlDbType.VarChar, 15).Value = data.completed;
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
                    
                // Update, Insert Delete Job in Table
                
                    
               // return Json(stringJson, JsonRequestBehavior.AllowGet);
                //returning the employee list to view
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}