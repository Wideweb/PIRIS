using Infrastructure.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Models
{
    public class Entity
    {
        public long Id { get; set; }

        public bool IsNew => Id <= 0;

        public IEnumerable<PropertyInfo> GetProperties(bool excludeId = true)
        {
            return GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(it => it.CanWrite)
                .Where(it => it.Name != nameof(Id) || !excludeId);
        }

        public IEnumerable<SqlParameter> GetSqlParameters(bool excludeId = true)
        {
            return GetProperties(excludeId)
                .Select(it => new SqlParameter
                {
                    ParameterName = $"@{it.Name}",
                    Value = it.GetValue(this, null)
                });
        }

        public string GetPropertiesString()
        {
            return string.Join(", ", GetProperties().Select(it => it.Name));
        }

        public string GetUpdatePropertiesString()
        {
            var pairs = GetProperties()
                .Zip(GetSqlParameters(), (prop, param) => $"{prop}={param.ParameterName}");

            return string.Join(", ", pairs);
        }

        public string GetSqlParametersString()
        {
            return string.Join(", ", GetSqlParameters().Select(it => it.ParameterName));
        }

        public string GetDbTableName()
        {
            var entityType = GetType();
            var attr = entityType.GetTypeInfo().GetCustomAttribute<TableAttribute>();
            return attr == null ? entityType.Name : attr.Name;
        }

        public bool Load(SqlDataReader sqlDataReader)
        {
            if (!sqlDataReader.Read())
            {
                return false;
            }

            try
            {
                var properties = GetProperties(excludeId: false);

                foreach (var property in properties)
                {
                    property.SetValue(this, sqlDataReader[property.Name]);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
