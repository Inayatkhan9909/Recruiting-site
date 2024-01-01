using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Recruit.Pages
{
    public class Applicationstatus_searchModel : PageModel
    {
        public Search sr = new Search();
        public string errorMessage = "";

        private readonly string connectionString;

        public Applicationstatus_searchModel(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IActionResult OnPost()   

        {
            Console.WriteLine("Inside post at top");
            sr.email = Request.Form["email"];
            sr.dob = Request.Form["dob"];



            if (string.IsNullOrEmpty(sr.email) || string.IsNullOrEmpty(sr.dob))
            {
                errorMessage = "All fields are required";
                return Page();
            }


            try
            {

                Console.WriteLine("Inside try at top");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT Form_no, email, dob FROM Jobapplicants WHERE email = @email";



                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        Console.WriteLine("Inside Using command at top");

                        command.Parameters.AddWithValue("@email", sr.email);


 
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Inside reader at top");
                            if (reader.Read())
                            {
                                String storeddob = reader["dob"].ToString();
                                Console.WriteLine("If lapasa at sr.dob==storeddob");
                               

                                Console.WriteLine($"sr.dob: {sr.dob}, storeddob: {storeddob}");
                                if (sr.dob== storeddob)
                                {
                                    string form_no = reader["Form_no"].ToString();
                                    Console.WriteLine("Inside redirect at top");
                                  return   RedirectToPage("/Applicationstatus_mainpage", new { Form_no = form_no });
                                }

                             
                                else
                                {
                                    errorMessage = "Invalid DOB";
                                    return Page();
                                }
                            }

                        }
                    }
                }

                errorMessage = "Invalid email";
                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Page();
            }
        }
    }
}
