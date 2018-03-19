namespace ConfereEstoque.Migrations
{
    using ConfereEstoque.Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ConfereEstoque.Data.ConfereEstoqueContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new SqlGenerator());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ConfereEstoqueContext>());
        }

        protected override void Seed(ConfereEstoque.Data.ConfereEstoqueContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
