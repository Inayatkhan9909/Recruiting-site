using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace Recruit.Pages
{

    public class ApplyModel : PageModel
    {
        public string errorMessage = "";
        public string successMessage = "";
        UserInfo ur = new UserInfo();
        private readonly string connectionstring;

        public ApplyModel(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection");
        }


        public void OnPost()
        {
            ur.firstname = Request.Form["firstname"];
            ur.lastname = Request.Form["lastname"];
            ur.email = Request.Form["email"];
            ur.qualification = Request.Form["qualification"];
            ur.skills = Request.Form["skills"];
            ur.job = Request.Form["job"];

            if (ur.firstname.Length == 0 || ur.lastname.Length == 0 || ur.email.Length == 0 ||
                ur.qualification.Length == 0 || ur.skills.Length == 0 || ur.job.Length == 0)
            {
                errorMessage = "All field are required";
                return;


            }


            try
            {
                
                using SqlConnection connection = new(connectionstring);
                connection.Open();
            

                string sql = "Insert into  Jobapplicants" + "(firstname,lastname,email,qualification,skills,job) Values" +
                    "(@firstname,@lastname,@email,@qualification,@skills,@job)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    
                    command.Parameters.AddWithValue("@firstname", ur.firstname);
                    command.Parameters.AddWithValue("@lastname", ur.lastname);
                    command.Parameters.AddWithValue("@email", ur.email);
                    command.Parameters.AddWithValue("@qualification", ur.qualification);
                    command.Parameters.AddWithValue("@skills", ur.skills);
                    command.Parameters.AddWithValue("@job", ur.job);

                    command.ExecuteNonQuery();
                }


            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            ur.firstname = "";
            ur.lastname = "";
            ur.email = "";
            ur.qualification = "";
            ur.skills = "";
            ur.job = "";

            Thread.Sleep(100);
            Response.Redirect("/Index");
        }
    }
}
