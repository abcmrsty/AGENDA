using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace AGENDA.Pages.Empleado
{
    public class Editar : PageModel
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
        
        public String ErrorMessage { get; set; } = "";

        public void OnGet(int Id)
        {
                 
            try {
                string connectionString = "Server=.;Database=AGENDA1;Trusted_Connection=True;TrustServerCertificate=True;";
                
                using(SqlConnection connection = new SqlConnection(connectionString)){
                    connection.Open();

                    string sql = "SELECT * FROM Empleado WHERE Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@Id", Id);

                        using (SqlDataReader reader = command.ExecuteReader()){
                            if(reader.Read())
                            {
                                Id = reader.GetInt32(0);
                                Cedula = reader.GetString(1);
                                Nombres = reader.GetString(2);
                                Apellidos = reader.GetString(3);
                                Fecha_de_Nacimiento = reader.GetDateTime(4);
                                Departamento = reader.GetString(5);
                                Municipio = reader.GetString(6);
                                Dirección = reader.GetString(7);
                                Teléfono = reader.GetString(8);
                                Celular = reader.GetString(9);
                                Correo = reader.GetString(10);
                                Fecha_de_Ingreso = reader.GetDateTime(11);
                                Profesión = reader.GetString(12);
                                Puesto = reader.GetString(13);
                                Salario = reader.GetDecimal(14);
                            }
                            else{
                                Response.Redirect("/Empleado/Index");
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
            
            //Actualizar Empleados

            try {
                string connectionString = "Server=.;Database=AGENDA1;Trusted_Connection=True;TrustServerCertificate=True;";
                
                using(SqlConnection connection = new SqlConnection(connectionString)){
                    connection.Open();

                    string sql = "UPDATE Empleado SET Cedula=@Cedula, Nombres=@Nombres, Apellidos=@Apellidos, " +
                    "Fecha_de_Nacimiento=@Fecha_de_Nacimiento, Departamento=@Departamento, Municipio=@Municipio, " +
                    "Dirección=@Dirección, Teléfono=@Teléfono, Celular=@Celular, Correo=@Correo, " +
                    "Fecha_de_Ingreso=@Fecha_de_Ingreso, Profesión=@Profesión, Puesto=@Puesto, Salario=@Salario WHERE Id=@Id";

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
                 
