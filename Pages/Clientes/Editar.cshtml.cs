using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace AGENDA.Pages.Clientes
{
    public class Editar : PageModel
    {
        [BindProperty]
        public int IdContacto{get; set;}

        [BindProperty, Required(ErrorMessage = "Este espacio no puede quedar en blanco")]
        public string Nombres{get; set;} = "";

        [BindProperty, Required(ErrorMessage = "Este espacio no puede quedar en blanco")]
        public string Apellidos{get; set;} = "";

        [BindProperty, Required, EmailAddress]
        public string Corre{get; set;} = "";

        [BindProperty,  Phone]
        public string? Telefono{get; set;} 

        [BindProperty]
        public string? Direccion{get; set;}

        [BindProperty, Required]
        public string Compania{get; set;} = "";

        [BindProperty, Required]
        public string? Notas{get; set;} = "";

        [BindProperty, Required]
        public DateTime Fecha_reg { get; set; } = DateTime.Now;

        public string ErrorMessage {get; set;} = "";
        public void OnGet(int IdContacto)
        {
            try{
                string connectionString = "Server=.;Database=AGENDA1;Trusted_Connection=True;TrustServerCertificate=True;";
                
                using(SqlConnection connection = new SqlConnection(connectionString)){
                    connection.Open();

                    string sql = "SELECT * FROM Clientes WHERE IdContacto=@IdContacto";
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@IdContacto", IdContacto);

                        using (SqlDataReader reader = command.ExecuteReader()){
                            if(reader.Read())
                            {
                                IdContacto = reader.GetInt32(0);
                                Nombres = reader.GetString(1);
                                Apellidos = reader.GetString(2);
                                Corre = reader.GetString(3);
                                Telefono = reader.GetString(4);
                                Direccion = reader.GetString(5);
                                Compania = reader.GetString(6);
                                Notas = reader.GetString(7);
                                Fecha_reg= reader.GetDateTime(8);
                            }
                            else
                            {
                                Response.Redirect("/Clientes/Index");
                            }
                        }
                    }
                }


            }
            catch(Exception ex) {
                ErrorMessage = ex.Message;
            }
        }

        public void OnPost(){
            if(!ModelState.IsValid){
                return;
            }

            if(Telefono == null) Telefono = "";
            if(Direccion == null) Direccion = "";
            if(Notas == null) Notas = "";

            //Actualizar Clientes

            try {
                string connectionString = "Server=.;Database=AGENDA1;Trusted_Connection=True;TrustServerCertificate=True;";
                
                using(SqlConnection connection = new SqlConnection(connectionString)){
                    connection.Open();

                    string sql = "UPDATE Clientes SET Nombres=@Nombres, Apellidos=@Apellidos, Corre=@Corre, " +
                    "Telefono=@Telefono, Direccion=@Direccion, Compania=@Compania,Notas=@Notas WHERE IdContacto=@IdContacto";

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@Nombres", Nombres);
                        command.Parameters.AddWithValue("@Apellidos", Apellidos);
                        command.Parameters.AddWithValue("@Corre", Corre);
                        command.Parameters.AddWithValue("@Telefono", Telefono);
                        command.Parameters.AddWithValue("@Direccion", Direccion);
                        command.Parameters.AddWithValue("@Compania", Compania);
                        command.Parameters.AddWithValue("@Notas", Notas);
                        command.Parameters.AddWithValue("@IdContacto", IdContacto);

                        command.ExecuteNonQuery();                      
                    }
                }
            }
            catch(Exception ex) {
                ErrorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clientes/Index");
        }
    }
}