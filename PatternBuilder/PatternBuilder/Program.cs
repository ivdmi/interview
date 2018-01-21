using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// <add name="pspEntities" 

// connectionString
//@metadata=res: //*/PSPModel.csdl|res://*/PSPModel.ssdl|res://*/PSPModel.msl;
/*
provider=MySql.Data.MySqlClient;
provider connection string=&quot;
server=localhost;
user id=root;
persistsecurityinfo=True;
Charset=utf8;
database=psp;
providerName="System.Data.EntityClient"


    name="ScriptContext" 
    connectionString=
    Data Source=PC;
    Initial Catalog=ScriptDelegatorWeb;
    Integrated Security=True
    providerName="System.Data.SqlClient"/>

*/

namespace PatternBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pattern Builder:");
            Console.WriteLine("Создает в несколько шагов один сложный (составной) объект.");
            Console.WriteLine("Скрывает процесс создания объекта, порождает требуемую реализацию.\n");

            var builder = new ConcreteBuilder();
            Director director = new Director(builder);

            director.Construct();
            string result = builder.GetResult();

            Console.WriteLine(result);
            Console.ReadLine();
        }
    }


    // Director (распорядитель) – класс для управления процессом. создает объект, используя объекты Builder
    // Функции: 
    // сокрытие стратегии сборки (это позволит, модифицировать или полностью менять ее, не затрагивая остальной код).
    // получение данных для конструирования (уже потом Builder преобразовывает их в вид, необходимый для порождаемого объекта)
    // Такое разделение связано с тем, что создаваемый объект скрыт от Directora и может не уметь работать с форматом исходных данных.
    class Director
    {
        AbstractBuilder builder;

        public Director(AbstractBuilder builder)
        {
            this.builder = builder;
        }

        public void Construct()
        {
            builder.SetConnectionName("MyConnection");
            builder.SetDataSource("PC");
            builder.SetInitialCatalog("ScriptDelegatorWeb");
            builder.SetIntegratedSecurity(true);
            builder.SetMetadata(null);
            builder.SetProviderName("System.Data.SqlClient");
        }
    }

    // Builder: определяет интерфейс для создания различных частей объекта Product
    abstract class AbstractBuilder
    {
        public abstract void SetConnectionName(string connectionName);
        public abstract void SetMetadata(string metadata);
        public abstract void SetDataSource(string dataSource);
        public abstract void SetInitialCatalog(string initialCatalog);
        public abstract void SetIntegratedSecurity(bool integratedSecurity);
        public abstract void SetProviderName(string providerName);

        public abstract string GetResult();
    }


    // Builder: - реализация
    class ConcreteBuilder : AbstractBuilder
    {
        ConnectionString connectionString = new ConnectionString();

        public override void SetConnectionName(string connectionName)
        {
            connectionString.ConnectionName = connectionName;
        }

        public override void SetDataSource(string dataSource)
        {
            connectionString.DataSource = dataSource;
        }

        public override void SetMetadata(string metadata)
        {
            connectionString.Metadata = metadata;
        }

        public override void SetInitialCatalog(string initialCatalog)
        {
            connectionString.InitialCatalog = initialCatalog;
        }

        public override void SetIntegratedSecurity(bool integratedSecurity)
        {
            connectionString.IntegratedSecurity = integratedSecurity;
        }

        public override void SetProviderName(string providerName)
        {
            connectionString.ProviderName = providerName;
        }

        public override string GetResult()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(connectionString.DataSource) &&
                !string.IsNullOrEmpty(connectionString.InitialCatalog) &&
                !string.IsNullOrEmpty(connectionString.ProviderName))
            {
                if (connectionString.Metadata != null)
                    sb.Append("metadata =").Append(connectionString.Metadata).Append("; ");

                sb.Append("connectionString =\"Data Source=")
                    .Append(connectionString.DataSource)
                    .Append("; Initial Catalog=")
                    .Append(connectionString.InitialCatalog)
                    .Append("; Integrated Security=")
                    .Append(connectionString.IntegratedSecurity.ToString())
                    .Append("\"");

                sb.Append("\nproviderName = \"").Append(connectionString.ProviderName).Append("\"");
            }

            return sb.ToString();
        }
    }


    // Product: представляет объект, который должен быть создан
    class ConnectionString                                                          // Product
    {
        public string ConnectionName { get; set; }
        public string Metadata { get; set; }
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string ProviderName { get; set; }
    }
}
