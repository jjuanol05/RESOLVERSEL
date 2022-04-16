using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEL
{
    class Program
    {
        static void Main(string[] args)
        {
            bool ecuaciones, otro = true;
            int num_ec = 0, opt = 0, i = 0, j = 0, k = 0;
            while (otro)
            {
                ecuaciones = true;
                while (ecuaciones)
                {
                    Console.Write("Numero de ecuaciones: ");
                    num_ec = int.Parse(Console.ReadLine());

                    if (num_ec <= 0)
                        Console.WriteLine("Número de ecuaciones no válido");
                    else if (num_ec > 0)
                        ecuaciones = false;
                }

                Matriz MS = new Matriz(num_ec, num_ec);         //Matriz de coeficientes del sistema
                Matriz vector = new Matriz(num_ec, 1);          //Matriz de terminos independientes
                double[] variables = new double[num_ec];
                List<Matriz> mvar = new List<Matriz>();
                for (int ki = 0; ki <= num_ec; ki++)
                    mvar.Add(new Matriz(num_ec, num_ec));

                for (i = 0; i < num_ec; i++)
                {
                    Console.Clear();
                    Console.WriteLine("*********************** M E T O D O   D E   C R A M E R **********************");
                    Console.WriteLine("Coeficientes de la ecuación " + (i + 1) + ":");
                    for (j = 0; j <= num_ec; j++)
                    {
                        if (j == num_ec)
                        {
                            Console.Write("\tDame el termino independiente: ");
                            vector.SetElemento(i, 0, double.Parse(Console.ReadLine()));
                        }
                        else
                        {
                            Console.Write("\tDame el coeficiente " + (j + 1) + ": ");
                            MS.SetElemento(i, j, double.Parse(Console.ReadLine()));
                        }
                    }
                }
                int cont = 0;
                //Se generan las matrices asociadas a cada variable y al sistema a partir de MS
                foreach (var item in mvar)
                {

                    for (i = 0; i < num_ec; i++)
                        for (j = 0; j < num_ec; j++)
                        {
                            if (j == cont)
                                item.SetElemento(i, j, vector.nuevaMatriz[i, 0]);
                            else
                                item.SetElemento(i, j, MS.nuevaMatriz[i, j]);
                        }
                    cont++;
                }
                Console.Clear();
                Console.WriteLine("*********************** M E T O D O   D E   C R A M E R **********************");
                double d_sistema = mvar[num_ec].Determinante(num_ec, mvar[num_ec].nuevaMatriz);
                if (d_sistema != 0)
                {                                                                           //Se valida si el sistema es compatible determinado
                    Console.WriteLine("Resultados:");
                    for (i = 0; i < num_ec; i++)
                    {
                        variables[i] = (mvar[i].Determinante(num_ec, mvar[i].nuevaMatriz)) / d_sistema; //se calcula cada variable como delta(xn)/delta(sistema)
                        Console.WriteLine("\t\tX" + (i + 1) + " = " + variables[i]);
                    }
                }
                else
                    Console.WriteLine("\nEl sistema introducido es incompatible o compatible indetermiando...");

                Console.WriteLine("Desea resolver otro sistema:\n(1) Si\n(2) No");
                opt = int.Parse(Console.ReadLine());
                if (opt != 1)
                    otro = false;
                Console.Clear();
            }
        }
    }
}