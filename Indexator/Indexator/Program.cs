using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Schema;

namespace Indexator
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            Console.WriteLine("Исходный список книг:\n");
            foreach (var book in library)
            {
                Console.WriteLine(book);
            }

            Console.WriteLine("\nLINQ EXAMPLE\n");
            // LINQ EXAMPLE
            string it = " - ";
            var selectedBooks = from b in library
                                where b.Author == "Мак-Дональд"
                                select new { b.Name, it, b.Author };


            var selectedBooks1 = library.Where(b => b.Author == "Мак-Дональд").Select(b => new { b.Name, it, b.Author });


            var selectedBooks2 = library.Where(b => b.Author == "Мак-Дональд").Select(b => b.Name);

            foreach (var item in selectedBooks1)
            {
                Console.WriteLine(item.Name + it + item.Author);
            }

            Console.WriteLine("\nИспользуем свой перегруженный индексатор:\n");
            for (int i = 0; i < library.Count; i++)
            {
                Console.WriteLine(library[i]);
            }

            Library libr = new Library();

            Console.WriteLine("\nIEnumerable   vs   IQueryable:\n");

            // IEnumerable и IQueryable выглядят похоже, но работают по разному
            IEnumerable<Book> enumerable = libr;
            IQueryable<Book> queryable = libr.AsQueryable();    // IQueryable так используется только для примера, обычно это для БД

            Console.WriteLine("\nIEnumerable:\n");

            foreach (var item in enumerable)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nIQueryable:\n");

            foreach (var item in queryable)
            {
                Console.WriteLine(item);
            }

            // LINQ запрос вернет коллекцию в foreach
            var eSet = enumerable.Where(p => p.Author.Contains("Э"));
            var qSet = queryable.Where(p => p.Author.Contains("Э"));

            // добавляем после LINQ запроса
            libr.books.Add(new Book() {Author = "Эндрю Энрювич", Name = "Новая книга"});

            Console.WriteLine("\nIEnumerable:\n");
            // новая книга попала в результаті запроса
            foreach (var item in eSet)
            {
                Console.WriteLine("eSet: " + item.ToString());
            }

            Console.WriteLine("\nIQueryable:\n");
            foreach (var item in qSet)
            {
                Console.WriteLine("qSet: " + item.ToString());
            }

            Console.ReadLine();
        }
    }

    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }

        public override string ToString()
        {
            return Name + " - \tAuthor: " + Author;
        }
    }

    public class Library: IEnumerable<Book>
    {
        public List<Book> books;

        public Library()
        {
            books = new List<Book>()
            {
                new Book() {Name = "Язык программирования С# 5 и платформа NET", Author = "Эндрю Троелсон"},
                new Book() {Name = "WPF 4 Подробное руководство", Author = "Адам Натан "},
                new Book() {Name = "Windows Presentation Foundation в .NET", Author = "Мак-Дональд"},
                new Book() {Name = "ASP NET MVC Framework", Author = "Стивен Сандерсон"},
                new Book() {Name = "Сборник лучших практик", Author = "Эндрю Троелсон"},
                new Book() {Name = "Сборник лучших практик", Author = "Адам Натан"},
                new Book() {Name = "Сборник лучших практик", Author = "Мак-Дональд"},
                new Book() {Name = "Сборник лучших практик", Author = "Стивен Сандерсон"},
                new Book() {Name = "Лучшее в WPF", Author = "Адам Натан"},
                new Book() {Name = "Лучшее в WPF", Author = "Мак-Дональд"},
                new Book() {Name = "Книга от Адама Фримена", Author = "Адам Фримен"}
            };
        }

        public int Count
        {
            get
            {
                return books.Count;
            }
        }

        // Cодаем индексатор 
        public string this[int idx]
        {
            get
            {
                return "Автор: " + books[idx].Author + "\tКнига: " + books[idx].Name;
            }
        }

        // Можно так:
        IEnumerator IEnumerable.GetEnumerator()
        {
            return books.GetEnumerator();
        }

        // А можно и так:
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    // throw new NotImplementedException();
        //    for (int i = 0; i < books.Length; i++)
        //    {
        //        yield return books[i] + " - с использованием yield";
        //    }
        //}

        // А можно и так:
        IEnumerator<Book> IEnumerable<Book>.GetEnumerator()
        {
            // throw new NotImplementedException();
            for (int i = 0; i < books.Count; i++)
            {
                yield return books[i];
            }
        }
    }

}

