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
    public class Index : PageModel
    {
        public List<EmpleadoInfo> ListarEmpleado { get; set; } = new List<EmpleadoInfo>();


        public void OnGet()
        {
            try {
                string connectionString = "Server=.;Database=AGENDA1;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)){
                    connection.Open();

                    string sql = "SELECT * FROM Empleado ORDER BY Id DESC";

                    using (SqlCommand command = new SqlCommand(sql, connection)){
                        using (SqlDataReader reader = command.ExecuteReader()){
                            while (reader.Read()){
                                EmpleadoInfo EmpleadoInfo = new EmpleadoInfo();
                                EmpleadoInfo.Id = reader.GetInt32(0);
                                EmpleadoInfo.Cedula = reader.GetString(1);
                                EmpleadoInfo.Nombres = reader.GetString(2);
                                EmpleadoInfo.Apellidos = reader.GetString(3);
                                EmpleadoInfo.Fecha_de_Nacimiento = reader.GetDateTime(4);
                                EmpleadoInfo.Departamento = reader.GetString(5);
                                EmpleadoInfo.Municipio = reader.GetString(6);
                                EmpleadoInfo.Dirección = reader.GetString(7);
                                EmpleadoInfo.Teléfono = reader.GetString(8);
                                EmpleadoInfo.Celular = reader.GetString(9);
                                EmpleadoInfo.Correo = reader.GetString(10);
                                EmpleadoInfo.Fecha_de_Ingreso = reader.GetDateTime(11);
                                EmpleadoInfo.Profesión = reader.GetString(12);
                                EmpleadoInfo.Puesto = reader.GetString(13);
                                EmpleadoInfo.Salario = reader.GetDecimal(14);

                                ListarEmpleado.Add(EmpleadoInfo);
                            }
                        }
                    }

                }

            }
            catch(Exception ex) {
                Console.WriteLine("Tenemos un error: " + ex.Message);
            }
        }
    }

    public class EmpleadoInfo{
        public int Id { get; set; }
        public string Cedula { get; set; } = "";
        public string Nombres { get; set; } = "";
        public string Apellidos { get; set; } = "";
        public DateTime Fecha_de_Nacimiento { get; set; }
        public string Departamento { get; set; } = "";
        public string Municipio { get; set; } = "";
        public string Dirección { get; set; } = "";
        public string Teléfono { get; set; } = "";
        public string Celular { get; set; } = "";
        public string Correo { get; set; } = "";
        public DateTime Fecha_de_Ingreso { get; set; }
        public string Profesión { get; set; } = "";
        public string Puesto { get; set; } = "";
        public Decimal Salario { get; set; }
    }
}