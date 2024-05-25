using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace AGENDA.Pages.Clientes
{
    public class Eliminar : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost( int IdContacto){
            EliminarCliente(IdContacto);
            Response.Redirect("/Clientes/Index");
        }

        private void EliminarCliente(int IdContacto){
            try {
                string connectionString = "Server=.;Database=AGENDA1;Trusted_Connection=True;TrustServerCertificate=True;";
                
                using(SqlConnection connection = new SqlConnection(connectionString)){
                    connection.Open();

                    //Eliminar Clientes

                    string sql = "DELETE FROM Clientes WHERE IdContacto=@IdContacto";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@IdContacto", IdContacto);
                        command.ExecuteNonQuery();

                    }

                }

            }
            catch(Exception ex) {
                Console.WriteLine("No se puede eliminar a este cliente: " + ex.Message);
            }

        }
    }
}