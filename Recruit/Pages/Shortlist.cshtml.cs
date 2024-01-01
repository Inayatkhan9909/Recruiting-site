using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Recruit.Pages
{
    public class ShortlistModel : PageModel
    {
        
        
            private readonly string connectionstring;

            public ShortlistModel(IConfiguration configation)
            {
                connectionstring = configation.GetConnectionString("DefaultConnection");
            }

            public List<Shortlists> shortlisted = new List<Shortlists>();

            public void OnGet(string Form_no)
            {

                try
                {



                    using (SqlConnection connection = new SqlConnection(connectionstring))
                    {
                        connection.Open();

                        string sql = "INSERT INTO Shortlistedapplicants (Form_no, firstname, lastname, email,dob, qualification, skills, job) " +
                            "SELECT Form_no, firstname, lastname, email,dob, qualification, skills, job FROM Jobapplicants WHERE Form_no = @Form_no;";

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
                        string sql2 = "select * from Shortlistedapplicants";
                        using (SqlCommand command = new SqlCommand(sql2, connection))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                Shortlists shortlists = new Shortlists();
                                    shortlists.Form_no = "" + reader.GetInt32(0);
                                    shortlists.firstname = reader.GetString(1);
                                    shortlists.lastname = reader.GetString(2);
                                    shortlists.email = reader.GetString(3);
                                    shortlists.dob = reader.GetString(4);
                                    shortlists.qualification = reader.GetString(5);
                                    shortlists.skills = reader.GetString(6);
                                    shortlists.job = reader.GetString(7);
                                    shortlists.Created_at = reader.GetDateTime(8).ToString();
                                    shortlisted.Add(shortlists);

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
                        connection.Open();

                        string sql3 = "Delete from Jobapplicants where Form_No = @Form_no";
                        using (SqlCommand command = new SqlCommand(sql3, connection))

                        {
                            command.Parameters.AddWithValue("@Form_no", Form_no);
                            command.ExecuteNonQuery();
                        }

                        Response.Redirect("/Admin");

                    }

                }

                catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            }
        }
}
