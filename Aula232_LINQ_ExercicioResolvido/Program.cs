/* Fazer um programa para ler um conjunto de produtos a partir de um arquivo em formato .csv (suponha que exista pelo menos 
 * um produto). Em seguida mostrar o preco medio dos produtos. Depois, mostrar os nomes, em ordem decrescente, dos produtos
 * que possuem preco inferior ao preco medio. 
 * 
 * Exemplo de Saida:
 * Enter full file path: c:\temp\in.txt
 * Average price: 420.23
 * Tablet
 * Mouse
 * Monitor
 * HD Case
 */

/* >>> PROGRAMA PRINCIPAL <<< */
using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using Aula232_LINQ_ExercicioResolvido.Entities;

namespace Aula232_LINQ_ExercicioResolvido
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            List<Product> list = new List<Product>();

            using (StreamReader sr = File.OpenText(path)) // StreamReader para abrir o arquivo do "path"
            {
                while (!sr.EndOfStream) // Enquanto nao chegar ao fim do texto no arquivo
                {
                    string[] fields = sr.ReadLine().Split(','); // Cria um vetor de strings e divide por virgula
                    string name = fields[0]; // Atribui o valor da posicao 0 a "name"
                    double price = double.Parse(fields[1], CultureInfo.InvariantCulture); // Atribui o valor da posicao 1 a "price"
                    list.Add(new Product(name, price)); // Adiciona "name" e "price" a "list"
                }
            }

            // Verificar a media dos precos
            //var avg = list.Select(p => p.Price).Average(); // Transforma a lista de produtos em de "double", so com os precos
            var avg = list.Select(p => p.Price).DefaultIfEmpty(0.0).Average(); // Evita falhas, caso o Source nao tenha precos
            Console.WriteLine("Average price = " + avg.ToString("F2", CultureInfo.InvariantCulture));

            /* Filtra a lista para os obter apenas produtos com precos abaixo da media e organiza em ordem decrescente de nomes e
             * obtem, apenas, o nome do produto */
            var names = list.Where(p => p.Price < avg).OrderByDescending(p => p.Name).Select(p => p.Name);
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
