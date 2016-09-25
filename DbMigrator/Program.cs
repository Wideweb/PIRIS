using DbMigratorCore.Utils;

namespace DbMigrator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var migrationsAssemblyName = @"C:\Users\alcke\Documents\Visual Studio 2015\Projects\Piris\Infrastructure.DbMigrations\bin\Debug\netstandard1.6\Infrastructure.DbMigrations.dll";
            var connectionString = @"Server=.;Database=PirisDev;Trusted_Connection=True;MultipleActiveResultSets=true";
            new Migrator(migrationsAssemblyName, connectionString).ApplyMigrations();
        }
    }
}
