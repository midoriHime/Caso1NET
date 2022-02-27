using System;
using System.Text;
using System.Threading;
using Microsoft.Data.SqlClient;


namespace Caso1NET
{
    class Program
    {
        

        static void Main(string[] args)
        {
            try
            {

              //  Consultas consultas = new Consultas();
              ////  consultas.conectarBD();

              //  string[] listaCantones = new string[] { "BAGASES", "ESCAZU", "BARVA" , "LA UNION", "LIMON", "PURISCAL" };
              //  Thread[] hilosCreados = new Thread[listaCantones.Length];
              //  for (int i = 0; i < listaCantones.Length-1; i++) {
              //      var hilo = new Thread(() => consultas.verResultado(listaCantones[i]));
              //      hilo.Name = "Hilo " + i.ToString()+ "Canton: "+ listaCantones[i];
              //      hilosCreados[i] = hilo;
              //  }  
                
              //  for(int i = 0; i < listaCantones.Length-1; i++) 
              //  {
              //      hilosCreados[i].Start();
              //      hilosCreados[i].Join(10);
              //  }

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

