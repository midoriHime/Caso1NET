using System;
using System.Text;
using System.Threading;
using Microsoft.Data.SqlClient;


namespace Copia
{
    class Program
    {


        static void Main(string[] args)
        {
            try
            {

                Consultas consultas = new Consultas();
                //  consultas.conectarBD();
                //lista de cantones a consultar
                string[] listaCantones = new string[] { "BAGASES", "ESCAZU", "BARVA", "LA UNION", "LIMON", "PURISCAL" };
                //lista de hilos creados
                Thread[] hilosCreados = new Thread[listaCantones.Length];
                //recorre los cantones para crear los hilos
                for (int i = 0; i < listaCantones.Length - 1; i++)
                {
                    //crea un hilo nuevo con el metodo de consulta
                    var hilo = new Thread(() => consultas.verResultado(listaCantones[i]));
                    //Nombra el hilo
                    hilo.Name = "Hilo " + i.ToString() + "Canton: " + listaCantones[i];
                    //Registra el hilo en el arreglo
                    hilosCreados[i] = hilo;
                    Thread.Sleep(50);
                    Console.WriteLine(hilo.Name);
                }
                //Recorrelos hilos creados para ejecutarlos
                for (int i = 0; i < listaCantones.Length - 1; i++)
                {
                    hilosCreados[i].Start();
                    //asigna tiempo de espera entre ejecucion de hilos para evitar la saturacion al inicio
                    hilosCreados[i].Join(10);
                }

                Consulta2 query2 = new Consulta2();
                query2.conectarBD();
                query2.verResultado();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}



