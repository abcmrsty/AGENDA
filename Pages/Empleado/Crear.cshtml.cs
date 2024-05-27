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

namespace AGENDA.Pages.Empleado
{
    public class Crear : PageModel
    {
        [BindProperty, Required]
        public int Id { get; set; }

        [BindProperty, Required]
        public string Cedula { get; set; } = "";

        [BindProperty, Required]
        public string Nombres{get; set;} = "";

        [BindProperty, Required]
        public string Apellidos{get; set;} = "";

        [BindProperty, Required]
        public DateTime Fecha_de_Nacimiento { get; set; } = DateTime.Now;

        [BindProperty, Required]
        public string Departamento { get; set; } = "";

        [BindProperty, Required]
        public string Municipio { get; set; } = "";

        [BindProperty, Required]
        public string Dirección { get; set; } = "";

        [BindProperty, Required, Phone]
        public string Teléfono{get; set;} = "";

        [BindProperty, Required, Phone]
        public string Celular { get; set; } = "";

        [BindProperty,Required, EmailAddress]
        public string Correo{get; set;} = "";

        public DateTime Fecha_de_Ingreso { get; set; } = DateTime.Now;

        [BindProperty, Required]
        public string Profesión{get; set;} = "";

        public string Puesto { get; set; } = "";

        [BindProperty, Required]
        public Decimal Salario{get; set;} 

        public void OnGet()
        {
        }
        public String ErrorMessage { get; set; } = "";
        public void OnPost()
        {
            
            //Crear Empleados           
            try {
                string connectionString = "Server=.;Database=AGENDA1;Trusted_Connection=True;TrustServerCertificate=True;";
                
                using(SqlConnection connection = new SqlConnection(connectionString)){
                    connection.Open();

                    string sql = "INSERT INTO Empleado " +
                    "(Id, Cedula, Nombres, Apellidos, Fecha_de_Nacimiento, Departamento, Municipio, Dirección, Teléfono, Celular, Correo, Fecha_de_Ingreso, Profesión, Puesto, Salario) VALUES " +
                    "(@Id, @Cedula, @Nombres, @Apellidos, @Fecha_de_Nacimiento, @Departamento, @Municipio, @Dirección, @Teléfono, @Celular, @Correo, @Fecha_de_Ingreso, @Profesión, @Puesto, @Salario)";

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@Id", Id);
                        command.Parameters.AddWithValue("@Cedula", Cedula);
                        command.Parameters.AddWithValue("@Nombres", Nombres);
                        command.Parameters.AddWithValue("@Apellidos", Apellidos);
                        command.Parameters.AddWithValue("@Fecha_de_Nacimiento", Fecha_de_Nacimiento);
                        command.Parameters.AddWithValue("@Departamento", Departamento);
                        command.Parameters.AddWithValue("@Municipio", Municipio);
                        command.Parameters.AddWithValue("@Dirección", Dirección);
                        command.Parameters.AddWithValue("@Teléfono", Teléfono);
                        command.Parameters.AddWithValue("@Celular", Celular);
                        command.Parameters.AddWithValue("@Correo", Correo);
                        command.Parameters.AddWithValue("@Fecha_de_Ingreso", Fecha_de_Ingreso);
                        command.Parameters.AddWithValue("@Profesión", Profesión);
                        command.Parameters.AddWithValue("@Puesto", Puesto);
                        command.Parameters.AddWithValue("@Salario", Salario);

                        command.ExecuteNonQuery();                      
                    }
                }
            }
            catch(Exception ex) {
                ErrorMessage = ex.Message;
                return;
            }
            
            Response.Redirect("/Empleado/Index");
            
        }
    }
}