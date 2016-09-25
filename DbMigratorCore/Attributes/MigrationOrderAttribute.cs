using System;

namespace DbMigratorCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MigrationOrderAttribute : Attribute
    {
        public long MigrationOrder { get; set; }
    }
}
