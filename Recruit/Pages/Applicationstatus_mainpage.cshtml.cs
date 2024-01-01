using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Common;
using System.Data.SqlClient;

namespace Recruit.Pages
{
    public class Applicationstatus_mainpageModel : PageModel
    {
        public string errorMessage = "";
        public string successMessage="";

        public UserInfo ur =new();

        private  readonly string connectionstring;

        public Applicationstatus_mainpageModel(IConfiguration configration)
        {
            connectionstring = configration.GetConnectionString("DefaultConnection");
        }

        public string Formno { get; set; }
        

        public void OnGet(string Form_no)
        {
            Formno = Form_no;

            try   
            {
            
              using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sql = "select * from Jobapplicants where Form_no = @Formno";
                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Formno", Formno);
                       
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine( "Inside the Apphomepage cs reader");
                            if (reader.Read())
                            {
                                Formno = ""+ reader.GetInt32(0);
                                ur.firstname = reader.GetString(1);
                                ur.lastname = reader.GetString(2);
                                ur.email = reader.GetString(3);
                                ur.dob = reader.GetString(4);
                                Console.WriteLine("Inside the Apphomepage cs list reader");
                                ur.qualification = reader.GetString(5);
                                ur.skills = reader.GetString(6);
                                ur.job = reader.GetString(7);
                            }

                        }
                    }


                }

            }
            catch (Exception ex)
            { Console.WriteLine(ex.ToString); }

        }

        public void OnPost(string Form_no)
        {
            Formno = Form_no;
            ur.firstname = Request.Form["firstname"];
            ur.lastname = Request.Form["lastname"];
            ur.email = Request.Form["email"];
            ur.dob = Request.Form["dob"];
            ur.qualification = Request.Form["qualification"];
            ur.skills = Request.Form["skills"];
            ur.job = Request.Form["job"];
            Console.WriteLine("Inside the Onpost aphomepage bleow request.form");

            if (ur.firstname.Length==0|| ur.lastname.Length == 0 || ur.email.Length == 0 ||
                ur.dob.Length == 0 || ur.qualification.Length == 0 || ur.skills.Length == 0 ||
               ur.job.Length == 0  )
            {
                errorMessage = "All feilds are necessary";
                return;
            }

            try
            {

                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    Console.WriteLine("Inside the Onpost aphomepage bleow con.open();");

                    string sql = 
                       
                        "  Update Jobapplicants SET firstname=@firstname, " +
                        "lastname=@lastname,email=@email,dob=@dob,qualification=@qualification," +
                        "skills=@skills,job=@job where Form_no =@Formno";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("Inside the Onpost aphomepage top of cmd ");
                        cmd.Parameters.AddWithValue("@Formno", Formno);
                        cmd.Parameters.AddWithValue("firstname", ur.firstname);
                        cmd.Parameters.AddWithValue("lastname", ur.lastname);
                        cmd.Parameters.AddWithValue("email", ur.email);
                        cmd.Parameters.AddWithValue("dob", ur.dob);
                        cmd.Parameters.AddWithValue("qualification", ur.qualification);
                        cmd.Parameters.AddWithValue("skills", ur.skills);
                        cmd.Parameters.AddWithValue("job", ur.job);
                        Console.WriteLine("Inside the Onpost aphomepage above cmd.ExecuteNonQuery");
                        Console.WriteLine("************************************************************");
                        Console.WriteLine("SQL Query: " + sql);
                        cmd.ExecuteNonQuery();

                    }
                }
                successMessage = "Intern information updated successfully.";
                Console.WriteLine("successmessage khwa ki");



            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }



        }







    }
}
