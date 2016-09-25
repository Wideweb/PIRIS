using DbMigratorCore.Gateway;
using System;
using System.Collections.Generic;

namespace DbMigratorCore.Utils
{
    public class DbRegistry : IDisposable
    {
        private Dictionary<string, RowDataGatewayBase> resources;

        public DbRegistry()
        {
            resources = new Dictionary<string, RowDataGatewayBase>();
        }

        public T GetById<T>(long id) where T : RowDataGatewayBase
        {
            var key = $"{RowDataGatewayHelper.GetTableName(typeof(T))}_{id.ToString()}";
            if(resources.ContainsKey(key)){
                return (T)resources[key];
            }
            return null;
        }

        public void Add<T>(T instance) where T : RowDataGatewayBase
        {
            var key = $"{RowDataGatewayHelper.GetTableName(typeof(T))}_{instance.Id.ToString()}";
            if (resources.ContainsKey(key))
            {
                resources[key] = instance;
            }
            resources.Add(key, instance);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (resources != null) resources.Clear();
            }
        }
    }
}
