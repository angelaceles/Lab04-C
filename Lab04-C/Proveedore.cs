using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04_C
{
    public class Proveedore
    {
        public static string connectionString = "Data Source=LAB1504-30\\SQLEXPRESS;Initial Catalog=Neptuno3;User ID=userTecsup;Password=123456";

        public string? Nombrecontacto { get; private set; }
        public string? Ciudad { get; private set; }

        private static List<Proveedore> ListarProveedoresP(string nombrecontacto, string ciudad)
        {
            List<Proveedore> proveedores = new List<Proveedore>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();
                string query = "ListarProveedoresP";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    SqlParameter parameter = new SqlParameter("@Nombrecontacto", nombrecontacto);
                    SqlParameter parameter2 = new SqlParameter("@Ciudad", ciudad);
                    command.Parameters.Add(parameter);
                    command.Parameters.Add(parameter2);
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Verificar si hay filas
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Leer los datos de cada fila
                                proveedores.Add(new Proveedore
                                {
                                    Nombrecontacto = reader["Nombrecontacto"].ToString(),
                                    Ciudad = reader["Ciudad"].ToString(),
                                });

                            }
                        }
                    }
                }

                // Cerrar la conexión
                connection.Close();


            }
            return proveedores;

        }
    }
}
