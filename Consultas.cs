using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace Copia
{
    internal class Consultas
    {
        //Define la conexion a usar
        public static SqlConnection sql_conexion;

        public void conectarBD()
        {
            //if (sql_conexion!= null && sql_conexion.State != ConnectionState.Open)
            // {
            try
            {
                SqlConnectionStringBuilder constructorBD = new SqlConnectionStringBuilder();

                //Trusted_Connection=True;TrustServerCertificate=True;
                // constructorBD.ConnectionString = "Server=NEWPORT\\SQLEXPRESS01;Database=ASENI;";
                constructorBD.Pooling = true;
                //asigna el maximo pool para la conexion de bd
                constructorBD.MaxPoolSize = 10;
                //asigna el minimo pool
                constructorBD.MinPoolSize = 2;
                //Habilita multiples resultados activos (esto ya que se trabajara en paralelo y habran
                //varios ResultSet Activos en la misma conexion
                constructorBD.MultipleActiveResultSets = true;
                constructorBD.UserID = "sa";
                constructorBD.Password = "1234";
                constructorBD.ConnectTimeout = 500;
                constructorBD.InitialCatalog = "ASENI";
                constructorBD.DataSource = "NEWPORT\\SQLEXPRESS01";
                constructorBD.TrustServerCertificate = true;
                // Console.WriteLine(constructorBD.ConnectionString);
                //asigna la conexion a base de datos
                sql_conexion = new SqlConnection(constructorBD.ConnectionString);
            }
            catch (SqlException excepcion)
            {
                Console.WriteLine("Ha ocurrido un error conectandose a la Base de Datos." + excepcion.Message);
            }
            //}


        }

        public void desconectarBD()
        {
            Console.WriteLine("Cierra conexion");
            if (sql_conexion.State != ConnectionState.Closed)
            {
                try
                {
                    sql_conexion.Close();
                }
                catch (SqlException excepcion)
                {
                    Console.WriteLine("Ha ocurrido un error desconectandose a la Base de Datos." + excepcion.Message);
                }
            }
        }

        public void verResultado(string canton)
        {
            try
            {
                //Conecta a la base de datos
                conectarBD();
                //Varible para registrar el tiempo transcurrido
                Stopwatch tiempo = Stopwatch.StartNew();
                //ejecuta la llamada a la consulta
                SqlDataReader resultado = llamaConsulta(canton);
                //Detiene el contador de tiempo
                tiempo.Stop();
                //Obtine el tiempo de duracion
                TimeSpan duracion = tiempo.Elapsed;
                //Registra string de salida de datos
                String salida;
                //Verifica que existan datos
                if (resultado != null && resultado.HasRows)
                {
                    salida = "===============================";
                    salida = salida + "Terminado canton " + canton + " Tiempo: " + duracion.Milliseconds.ToString();
                    //Recorre el Reader
                    while (resultado.Read())
                    {
                        salida = salida + "\n" + String.Join("\t", resultado.GetString(1),
                            resultado.GetString(3), resultado.GetInt32(4), resultado.GetString(5));

                    }
                }
                else
                {
                    salida = "No se encontraron entregables";
                }
                Console.WriteLine(salida);
                //Desconecta la conexion
                desconectarBD();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public SqlDataReader llamaConsulta(string canton)
        {
            try
            {
                //Crea el string de ejecucion de procedimiento con el canton especificado
                string sql = "EXEC dbo.ListarEntregablesXCanton @canton = N'" + canton + "'; ";
                //asigna el comando
                SqlCommand command = new SqlCommand(sql, sql_conexion);
                if (sql_conexion.State != ConnectionState.Open && sql_conexion.State != ConnectionState.Connecting)
                {
                    sql_conexion.Open();
                }
                var attempts = 0;
                while (sql_conexion.State == ConnectionState.Connecting && attempts < 10)
                {
                    attempts++;
                    Thread.Sleep(100);
                }
                //abre la conexion
                //sql_conexion.Open();
                Console.WriteLine("Abre conexion base datos " + canton);
                //ejecuta el comando
                SqlDataReader reader = command.ExecuteReader();
                // DataSet ds = new DataSet();
                return reader;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
    }
}
