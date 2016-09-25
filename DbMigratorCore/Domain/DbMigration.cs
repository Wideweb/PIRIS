﻿using DbMigratorCore.Utils;

namespace DbMigratorCore.Domain
{
    public abstract class DbMigration
    {
        public DbQuery DbQuery { get; set; }

        public abstract void Up();

        public abstract void Down();

        protected void Sql(string sql)
        {
            DbQuery.ExecuteCommand(sql);
        }
    }
}
