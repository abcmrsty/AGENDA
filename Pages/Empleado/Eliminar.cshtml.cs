using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace AGENDA.Pages.Empleado
{
    public class Eliminar : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost( int Id){
            EliminarEmpleado(Id);
            Response.Redirect("/Empleado/Index");
        }

        private void EliminarEmpleado(int Id){
            try {
                string connectionString = "Server=.;Database=AGENDA1;Trusted_Connection=True;TrustServerCertificate=True;";
                
                using(SqlConnection connection = new SqlConnection(connectionString)){
                    connection.Open();

                    //Eliminar Empleado

                    string sql = "DELETE FROM Empleado WHERE Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        command.ExecuteNonQuery();

                    }

                }

            }
            catch(Exception ex) {
                Console.WriteLine("No se puede eliminar a este empleado: " + ex.Message);
            }

        }
    }
}