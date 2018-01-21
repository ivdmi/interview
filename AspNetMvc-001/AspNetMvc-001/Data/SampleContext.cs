using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AspNetMvc_001.Models;

namespace AspNetMvc_001.Data
{
    public class SampleContext: DbContext
    {
        // Строку подключения или имя будущей БД можно указать через вызов конструктора базового класса
        public SampleContext() :base("name=SampleContextConnectionString") { }

        public DbSet<Customer2> Customers { get; set; }
        public DbSet<Order2> Orders { get; set; }
    }
}