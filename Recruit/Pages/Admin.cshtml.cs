using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Recruit.Pages
{
    public class AdminModel : PageModel
    {
        private string connectionstring;

        public AdminModel(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Applicants> applicantlist = new List<Applicants>();
        public void OnGet()
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    string sql = "select * from Jobapplicants";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                       
                        using (SqlDataReader reader = command.ExecuteReader())
                        { 
                            while (reader.Read()) 
                            { 
                             Applicants applicants = new Applicants();
                                applicants.Form_no = "" + reader.GetInt32(0);
                                applicants.firstname = reader.GetString(1);
                                applicants.lastname = reader.GetString(2);
                                applicants.email = reader.GetString(3);
                                applicants.qualification = reader.GetString(4);
                                applicants.skills = reader.GetString(5);
                                applicants.job = reader.GetString(6);
                                applicantlist.Add(applicants);
                            
                            }
                        }

                    }

                }


            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
