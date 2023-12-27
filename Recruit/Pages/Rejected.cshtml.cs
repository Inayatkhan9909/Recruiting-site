using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Recruit.Pages
{
    public class RejectedModel : PageModel
    {
        private readonly string connectionstring;

        public RejectedModel(IConfiguration configation)
        {
            connectionstring = configation.GetConnectionString("DefaultConnection");
        }

        public List<Rejects> rejectlist = new List<Rejects>();

        public void OnGet(string Form_no)
        {

            try
            {



                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    string sql = "INSERT INTO Rejectedapplicants (Form_no, firstname, lastname, email, qualification, skills, job) " +
                        "SELECT Form_no, firstname, lastname, email, qualification, skills, job FROM Jobapplicants WHERE Form_no = @Form_no;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Form_no", Form_no);
                        command.ExecuteNonQuery();
                    }
                }



            }

            catch (Exception ex)
            
            {
             Console.Write(ex.ToString());
            
            
            }

            try
            { 


              using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sql2 = "select * from Rejectedapplicants";
                    using (SqlCommand command = new SqlCommand(sql2, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Rejects rejects = new Rejects();
                                rejects.Form_no = "" + reader.GetInt32(0);
                                rejects.firstname = reader.GetString(1);
                                rejects.lastname = reader.GetString(2);
                                rejects.email = reader.GetString(3);
                                rejects.qualification = reader.GetString(4);
                                rejects.skills = reader.GetString(5);
                                rejects.job = reader.GetString(6);
                                rejects.Created_at = reader.GetDateTime(7).ToString();
                                rejectlist.Add(rejects);

                            }
                        }

                    }

                }


            }
            
            
            
            
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }  


            try 
            { 
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open ();
                     
                    string sql3 = "Delete from Jobapplicants where Form_No = @Form_no";
                    using (SqlCommand command = new SqlCommand( sql3, connection))
                    
                    {
                        command.Parameters.AddWithValue("@Form_no",Form_no);
                        command.ExecuteNonQuery();
                    }

                    Response.Redirect("/Admin");
                   
                }

            }

            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

        }
    }
}
