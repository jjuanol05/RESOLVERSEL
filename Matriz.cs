using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEL
{
    public class Matriz 
    {
        public double[,] nuevaMatriz;
        string s;

        public Matriz(int n, int m)
        {
            nuevaMatriz = new double[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    nuevaMatriz[i, j] = 0;
        }

        public void SetElemento(int i, int j, double num)
        {
            nuevaMatriz[i, j] = num; //en dichos indices i,j y cambiamos el nuevo numero.
        }

        public Matriz Multiplicar(Matriz M2)
        {
            Matriz producto;
            if (nuevaMatriz.Length == M2.nuevaMatriz.Length)  //si las matrices son cuadradas del mismo orden hacemos el producto
            {
                int n = nuevaMatriz.GetLength(0);   //obtenemos el tamaño para la matriz producto
                producto = new Matriz(n,n);
                for (int i = 0; i < n; i++)                   //Debemos inicializar cada elemento en 0 de la matriz producto
                    for (int j = 0; j < n; j++)
                        producto.nuevaMatriz[i, j] = 0;
                for (int i = 0; i < n; i++)                  //Levamos acabo el producto de matrices con estos for's
                    for (int j = 0; j < n; j++)
                        for (int k = 0; k < n; k++)
                            producto.nuevaMatriz[i, k] += (nuevaMatriz[i, j] * M2.nuevaMatriz[j, k]);
                return producto;                            //regresamos la matriz producto
            }
            Matriz nula = new Matriz(1,1);            //si no son conformables las matrices regresamos una matriz nula de un elemento
            nula.nuevaMatriz[0, 0] = 0;
            return nula;
        }

        public void PorEscalar(double k)
        {
            int n = nuevaMatriz.GetLength(0);  //calculamos la el orden de la matriz 
            int m = nuevaMatriz.GetLength(1);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    nuevaMatriz[i, j] *= k;
        }

        public Matriz Transpuesta()
        {
            int n = nuevaMatriz.GetLength(0);    //orden de la matriz
            Matriz transpuesta = new Matriz(n,n);            //Como las matrices deben ser cuadradas, las transpuestas son del mismo orden de la matriz original
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    transpuesta.nuevaMatriz[i, j] = nuevaMatriz[j, i];  //Intercambiamos indices, columnas pasan a ser renglones y viceversa
            return transpuesta;
        }

        public double Determinante(int rank, double[,] nuevaMatriz) //caso general para matrices de nxn, usando recursividad
        {
            double det = 0;
            if (rank == 1) //caso base cuando el tmaño de la matriz es de un solo elemento, el mismo es su determinante.
                det = nuevaMatriz[0, 0];
            else
            {
                double[,] mataux = new double[rank - 1, rank - 1]; //matriz auxiliar para las matrices de orden menor a la matriz original

                for (int x = 0; x < rank; x++) //Este for llevara la cuenta de cofactores
                {
                    for (int i = 0; i < rank - 1; i++)
                        for (int j = 0; j < rank - 1; j++)
                        {
                            if (j >= x) //se van formando las matrices de orden menor que multiplican a cada cofactor
                                mataux[i, j] = nuevaMatriz[i + 1, j + 1];
                            else
                                mataux[i, j] = nuevaMatriz[i + 1, j];
                        }
                    det += (Math.Pow(-1, x + 2) * nuevaMatriz[0, x] * Determinante(rank - 1, mataux)); //calculamos el cofactor por su elemento menor.
                }
            }
            return det;
        }

        public override string ToString()
        {
            double n = nuevaMatriz.GetLength(0);
            double m = nuevaMatriz.GetLength(1);
            s = "\n";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    s += nuevaMatriz[i, j] + " ";
                s += "\n";
            }
            return s;
        }
    }
}
