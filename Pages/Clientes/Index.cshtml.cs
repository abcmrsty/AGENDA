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
    public class Index : PageModel
    {
        public List<ClientesInfo> ListaClientes { get; set; } = new List<ClientesInfo>();


        public void OnGet()
        {
            try {
                string connectionString = "Server=.;Database=AGENDA1;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)){
                    connection.Open();

                    string sql = "SELECT * FROM Clientes ORDER BY IdContacto DESC";

                    using (SqlCommand command = new SqlCommand(sql, connection)){
                        using (SqlDataReader reader = command.ExecuteReader()){
                            while (reader.Read()){
                                ClientesInfo clientesInfo = new ClientesInfo();
                                clientesInfo.IdContacto = reader.GetInt32(0);
                                clientesInfo.Nombres = reader.GetString(1);
                                clientesInfo.Apellidos = reader.GetString(2);
                                clientesInfo.Corre = reader.GetString(3);
                                clientesInfo.Telefono = reader.GetString(4);
                                clientesInfo.Direccion = reader.GetString(5);
                                clientesInfo.Compania = reader.GetString(6);
                                clientesInfo.Notas = reader.GetString(7);
                                clientesInfo.Fecha_Reg = reader.GetDateTime(8).ToString("MM/dd/yyyy");

                                ListaClientes.Add(clientesInfo);
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

    public class ClientesInfo{
        public int IdContacto { get; set; }
        public string Nombres { get; set; } = "";
        public string Apellidos { get; set; } = "";
        public string Corre { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Direccion { get; set; } = "";
        public string Compania { get; set; } = "";
        public string Notas { get; set; } = "";
        public string Fecha_Reg { get; set; } = "";

    }
}