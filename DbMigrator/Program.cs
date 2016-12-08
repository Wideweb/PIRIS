using DbMigratorCore.Utils;
using EternityFramework.Utils;

namespace DbMigrator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var migrationsAssemblyName = @"C:\Users\alcke\Documents\Piris\Infrastructure.DbMigrations\bin\Debug\netstandard1.6\Infrastructure.DbMigrations.dll";
            var connectionString = @"Server=.;Database=PirisDev;Trusted_Connection=True;MultipleActiveResultSets=true";
            new Migrator(migrationsAssemblyName, connectionString).ApplyMigrations();
            //new Migrator(migrationsAssemblyName, connectionString).Downgrade(0);
        }
    }
}
