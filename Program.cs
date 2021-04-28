using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final
{
    class Program
    {
        //Instancias
        static Fechas mis_Fechas = new Fechas();
        static Atributos mis_atributos = new Atributos();

        //Declaracion de Listas
       static List<Atributos> ListaAtributos = new List<Atributos>();

     
        static void Main(string[] args)
        {
           

            //Asignacion de variables
            mis_Fechas.mes = int.Parse(mis_Fechas.thisDay.ToString("MM"));
            mis_Fechas.anio = int.Parse(mis_Fechas.thisDay.ToString("yyyy"));


           

            try
            {


                //Lamada de Metodos
                menu();


                GenerarTabla();

                Console.WriteLine();

               
             

            }catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }

            Console.WriteLine("Enter para salir");
            Console.WriteLine("Hecho Por: Luis Perez");
            Console.ReadKey();


        }









        //Metodos
      static void ObtenerUltimaFechaMes(int anio, int mes)
        {
    
            var ulimoLunesDelMes = new DateTime(anio, mes, DateTime.DaysInMonth(anio, mes));
        
          
            //Obtengo el Ultmo Viernes del Mes
            while (ulimoLunesDelMes.DayOfWeek != DayOfWeek.Friday)
                ulimoLunesDelMes = ulimoLunesDelMes.AddDays(-1);



      
              //Completar atributos faltantes
            mis_atributos.diacobro = ulimoLunesDelMes.Day;
            mis_atributos.mescobro = Convert.ToString(ulimoLunesDelMes.ToString("MMMM"));
            mis_atributos.aniocobro = ulimoLunesDelMes.Year;

        }

    
     static void menu()
      {
            
            Console.WriteLine("******************************************");
            Console.WriteLine("*--------------- Bienvenido -------------*");
            Console.WriteLine("*--- Sistema Calculador de Prestamos  ---*");
            Console.WriteLine("*----------------------------------------*");
            Console.WriteLine("******************************************");

            Console.WriteLine();

            Console.Write("- Digite el Prestamo a Tomar(Sin Guiones o Comas): ");
            mis_atributos.capital = double.Parse(Console.ReadLine());

            Console.WriteLine();

            Console.Write("- Digite el interes del Prestamo: ");
            mis_atributos.tasaporcentajeanual = double.Parse(Console.ReadLine()) / 1200;

            Console.WriteLine();

            Console.Write("- Digite el Lapso de Tiempo del Prestamo (En meses): ");
            mis_atributos.tiempoprestamo = int.Parse(Console.ReadLine());

            Console.WriteLine();

            Console.WriteLine("---- Enter Para Continuar ----");
            Console.ReadKey();
            Console.Clear();


            Console.WriteLine("Cargando Tabla de Amortización.");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Cargando Tabla de Amortización..");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Cargando Tabla de Amortización...");
            System.Threading.Thread.Sleep(5000);

            Console.Clear();

        }


     static void GenerarTabla()
        {
            //Calculo de couta
           mis_atributos.Couta  = mis_atributos.capital * (mis_atributos.tasaporcentajeanual / (double)(1 - Math.Pow(1 + (double)mis_atributos.tasaporcentajeanual, -mis_atributos.tiempoprestamo)));




            for (int i = 1; i <= mis_atributos.tiempoprestamo; i++)
            {
                mis_atributos.interes_mensual = Math.Round((mis_atributos.tasaporcentajeanual * mis_atributos.capital), 2);
                mis_atributos.capital = Math.Round(mis_atributos.capital - mis_atributos.Couta + mis_atributos.interes_mensual, 2);

                //Amortizacion
               mis_atributos.amortizacion_total += Math.Round(mis_atributos.Couta - mis_atributos.interes_mensual, 2);
                mis_atributos.amortizacion = mis_atributos.Couta - mis_atributos.interes_mensual;
                
                //Dia
             
              ObtenerUltimaFechaMes(mis_Fechas.anio, mis_Fechas.mes);
              
                if (mis_Fechas.mes >= 12)
                    {
                    mis_Fechas.mes = 1;
                    mis_Fechas.anio += 1;
                    }
       
                mis_Fechas.mes += 1;
                





                ListaAtributos.Add(new Atributos
                {
                    capital = mis_atributos.capital,
                    amortizacion =Math.Round(mis_atributos.amortizacion,2),
                    amortizacion_total = mis_atributos.amortizacion_total,
                    coutas = Math.Round(mis_atributos.Couta,2),
                    interes_mensual = mis_atributos.interes_mensual,
                    diacobro = mis_atributos.diacobro,
                    mescobro = mis_atributos.mescobro,
                    aniocobro = mis_atributos.aniocobro
                }) ;
              
            }
            Console.WriteLine("   Pagos   |       Fechas de Pago      |     Cuota    |     Capital     |     Interes     |     Balance      | Total pagado");
            int contador = 1;
            foreach (Atributos lista in ListaAtributos)
            {
              
                Console.WriteLine($"      {contador}    |      {Convert.ToString(lista.diacobro)}-{Convert.ToString(lista.mescobro)}-{Convert.ToString(lista.aniocobro)}       |    {Convert.ToString(lista.coutas)}    |     {Convert.ToString(lista.amortizacion)}     |        {Convert.ToString(lista.interes_mensual)}    |      {Convert.ToString(lista.capital)}   |     {Convert.ToString(lista.amortizacion_total)} ");
                contador += 1; 
            }
            Console.ReadKey();

        }
    }

}
