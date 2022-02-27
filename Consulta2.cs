using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace Caso1NET
{
    internal class Consulta2
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
                constructorBD.Pooling = true;
                //asigna el maximo pool para la conexion de bd
                constructorBD.MaxPoolSize = 5;
                constructorBD.ConnectionString = "Server=NEWPORT\\SQLEXPRESS01;Database=ASENI;Trusted_Connection=True;TrustServerCertificate=True;";
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

        public void verResultado()
        {
            try
            {
                conectarBD();
                Stopwatch tiempo = Stopwatch.StartNew();
             
                SqlDataReader resultado = llamaConsulta();
                tiempo.Stop();
                TimeSpan duracion = tiempo.Elapsed;

                
                if (resultado != null && resultado.HasRows)
                {
                    Console.WriteLine("===============================");
                    Console.WriteLine("Terminado Listar cantones 25% de Partidos " +  " Tiempo: " + duracion.Milliseconds);

                    while (resultado.Read())
                    {
                        Console.WriteLine("{0}\t{1}", resultado.GetString(0),
                            resultado.GetInt32(1));
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron entregables");
                }
                desconectarBD();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public SqlDataReader llamaConsulta()
        {
            try
            {
                
                //Crea el string de ejecucion de procedimiento con el canton especificado
                string sql = "DECLARE @TOTAL INT " 
                            +"SELECT @TOTAL = COUNT(ID_PARTIDO) FROM PARTIDO "
                            +"SELECT  B.NOMBRE AS CANTON,"
                            +"COUNT(A.ID_CANTON)AS ENTREGABLES "
                            +"FROM ENTREGABLE A"
                            +"     INNER JOIN"
                            +"     CANTON B ON A.ID_CANTON = B.ID_CANTON "
                            +"GROUP BY B.NOMBRE "
                            +"HAVING @TOTAL * 0.25 >= COUNT(DISTINCT A.ID_PARTIDO) "
                            +"ORDER BY 1,2";
                //asigna el comando
                SqlCommand command = new SqlCommand(sql, sql_conexion);
                //abre la conexion
                sql_conexion.Open();
                //ejecuta el comando
                SqlDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch (SqlException)
            {
                return null;
            }

        }

    }
}
