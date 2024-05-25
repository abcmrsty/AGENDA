using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace AGENDA.Pages.Clientes
{
    public class Crear : PageModel
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

        public void OnGet()
        {
        }
        public String ErrorMessage { get; set; } = "";
        public void OnPost()
        {
            if (!ModelState.IsValid) {
                return;
            }

            if (Telefono == null) Telefono = "";
            if (Direccion == null) Direccion = "";
            if (Notas == null) Notas = "";

            //Crear Clientes           
            try {
                string connectionString = "Server=.;Database=AGENDA1;Trusted_Connection=True;TrustServerCertificate=True;";
                
                using(SqlConnection connection = new SqlConnection(connectionString)){
                    connection.Open();

                    string sql = "INSERT INTO Clientes " +
                    "(IdContacto,Nombres, Apellidos, Corre, Telefono, Direccion, Compania, Notas, Fecha_reg) VALUES " +
                    "(@IdContacto, @Nombres, @Apellidos, @Corre, @Telefono, @Direccion, @Compania, @Notas, @Fecha_reg);";

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@IdContacto", IdContacto);
                        command.Parameters.AddWithValue("@Nombres", Nombres);
                        command.Parameters.AddWithValue("@Apellidos", Apellidos);
                        command.Parameters.AddWithValue("@Corre", Corre);
                        command.Parameters.AddWithValue("@Telefono", Telefono);
                        command.Parameters.AddWithValue("@Direccion", Direccion);
                        command.Parameters.AddWithValue("@Compania", Compania);
                        command.Parameters.AddWithValue("@Notas", Notas);
                        command.Parameters.AddWithValue("@Fecha_reg", Fecha_reg);


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