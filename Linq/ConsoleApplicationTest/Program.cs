using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
//            var a = new ClassA();
            var b = new ClassB("Test ------");

            string[] travelTypes = {"Воздушный", "Морской", "Наземный", "Речной"};

            
            Transport[] transport =
            {
                new Transport("велосипед", "Наземный"),
                new Transport("автомобиль", "Наземный"),
                new Transport("лошадь", "Наземный"),
                new Transport("автобус", "Наземный"),
                new Transport("катамаран", "Морской"),
                new Transport("лодка", "Речной"),
                new Transport("лайнер", "Морской"),
                new Transport("каное", "Речной"),
                new Transport("байдарка", "Речной"),
                new Transport("А320", "Воздушный"),
                new Transport("B747", "Воздушный"),
                new Transport("Ан-2", "Воздушный"),
                new Transport("Мрия", "Воздушный")
            };


            Console.WriteLine(" --------- Простейший Linq ------------");

            var linq = from item in transport
                where item.How == "Воздушный"
                select new { Name=item.Name , Date=DateTime.Now};

            // то же что и linq
            var linq2 = transport.Where(item => item.How == "Воздушный")
                                .Select(item => new { Name = item.Name, Date = DateTime.Now });

            foreach (var item in linq)
            {
                Console.WriteLine(item.Date + " - " + item.Name);
            }

            Console.WriteLine("\n");
            Console.ReadKey();

            Console.WriteLine(" --------- Join используя операторы запроса ------------");
            // используя операторы запроса
            var byHow = from types in travelTypes
                join tr in transport 
                on types equals tr.How
                select new {NAME = tr.Name, TYPE=types};

            foreach (var item in byHow)
            {
                Console.WriteLine(item.NAME + " - " + item.TYPE);
            }
            Console.WriteLine("\n");

            // То же используя методы запроса
            Console.WriteLine(" ----------- Join используя методы запроса -------------");
            var byHow1 = travelTypes.Join(transport,
                it1 => it1,
                it2 => it2.How,
                (it1, it2) => new {TYPE=it1, NAME=it2.Name, RAZD=" = "}
                );

            Console.WriteLine("Колличество - " + byHow1.Count());

            foreach (var item in byHow1)
            {
                Console.WriteLine(item.NAME + item.RAZD + item.TYPE);
            }

            Console.WriteLine("\n");
            Console.ReadKey();

            Console.WriteLine(" ------------ Группировка по категориям -------------");
            var byHow2 = from types in travelTypes
                        join tr in transport 
                        on types equals tr.How
                        into lst
                        select new { TYPE = types, Tlist = lst};

            foreach (var item in byHow2)
            {
                Console.WriteLine("Категория - " + item.TYPE);
                foreach (var it in item.Tlist)
                {
                    Console.WriteLine("     " + it.Name);
                }
            }


            Console.WriteLine(" ------------ Обобщенные методы ------------------- ");
            InfoParametrType("Строковый тип");
            InfoParametrType('f');
            InfoParametrType(123);
            InfoParametrType(12.7);
            InfoParametrType(DateTime.Now);

            
            Console.ReadKey();
        }

        static void InfoParametrType<T>(T arg)
        {
            Console.WriteLine("Обобщенный метод - InfoParametrType; Параметр типа - " + arg.GetType() + " " +  arg.ToString());
        }
    }

    class ClassA
    {
        public ClassA(string st)
        {
            Console.WriteLine("ClassA Start" + st);
        }
    }

    class ClassB : ClassA
    {
        public ClassB(string st) : base(st)
        {
            Console.WriteLine("ClassB " + st);
        }
    }

    public class Transport
    {
        public string Name { get; set; }
        public string How { get; set; }

        public Transport(string name, string how)
        {
            Name = name;
            How = how;
        }
    }
}
