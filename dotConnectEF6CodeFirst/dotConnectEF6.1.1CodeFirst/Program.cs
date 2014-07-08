using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devart.Data.Oracle;
using System.Data.Entity.Infrastructure;

class Program
{
    static void Main(string[] args)
    {
        // Original tutorial: http://blog.devart.com/entity-framework-code-first-support-for-oracle-mysql-postgresql-and-sqlite.html

        //--------------------------------------------------------------
        // You use the capability for configuring the behavior of the EF-provider:
        /*
        Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig config =
            Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig.Instance;

        config.Workarounds.IgnoreSchemaName = true;
         * */
        //--------------------------------------------------------------

        /*--------------------------------------------------------------
        You can set up a connection string for DbContext in different ways.
        It can be placed into the app.config (web.config) file.
        The connection string name must be identical to the DbContext descendant name.
 
        <add name="MyDbContext" connectionString="Data Source=ora1020;
        User Id=test;Password=test;" providerName="Devart.Data.Oracle" />
 
        After that, you create a context instance, while a connection string is 
        enabled automatically:
        MyDbContext context = new MyDbContext();
        ---------------------------------------------------------------*/

        /*--------------------------------------------------------------
        You can choose one of database initialization
        strategies or turn off initialization:
        --------------------------------------------------------------*/
        System.Data.Entity.Database.SetInitializer<MyDbContext>(new MyDbContextDropCreateDatabaseAlways());

        /*System.Data.Entity.Database.SetInitializer
          <MyOracleContext>(new MyDbContextCreateDatabaseIfNotExists());
        System.Data.Entity.Database.SetInitializer
          <MyOracleContext>(new MyDbContextDropCreateDatabaseIfModelChanges());
        System.Data.Entity.Database.SetInitializer<MyOracleContext>(null);*/
        //--------------------------------------------------------------

        /*--------------------------------------------------------------
        Let's create MyDbContext and execute a database query.
        Depending on selected database initialization strategy,
        database tables can be deleted/added, and filled with source data.
        ---------------------------------------------------------------*/

        Console.WriteLine("Available account currencies in database:");
        using (MyDbContext context = new MyDbContext())
        {
            var list = (from d in context.AccountCurrencies
                        select d).ToList();

            foreach (var l in list)
            {
                Console.WriteLine(l.IsoCode);
            }
        }

        Console.ReadKey();
    }
}