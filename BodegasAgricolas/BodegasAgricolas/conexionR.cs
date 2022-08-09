using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace BodegasAgricolas
{
    class conexionR
    {

        public static MySqlConnection conexion()
        {
            string servidor = "localhost";
            string bd = "sic";
            string usuario = "admin";
            string password = "admin12345";

            string cadenaConexion = "Database=" + bd + "; Data Source=" + servidor +
                "; User Id=" + usuario + "; Password=" + password + "";
            try
            {
                MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);

                return conexionBD;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }

        }
    }
}
