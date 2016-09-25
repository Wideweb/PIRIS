using DbMigratorCore.Attributes;
using DbMigratorCore.Gateway;
using System;
using System.Reflection;

namespace DbMigratorCore.Utils
{
    public static class RowDataGatewayHelper
    {
        public static string GetTableName(Type gatewayType)
        {
            if (!typeof(RowDataGatewayBase).IsAssignableFrom(gatewayType))
            {
                throw new ArgumentException($"Wrong gateway type {gatewayType.Name}");
            }

            var attr = gatewayType.GetTypeInfo().GetCustomAttribute<TableAttribute>();
            return attr == null ? gatewayType.Name : attr.Name;
        }
    }
}
