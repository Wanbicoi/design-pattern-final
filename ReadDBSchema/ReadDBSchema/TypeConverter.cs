using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDBSchema
{
    public static class TypeConverter
    {
        public static Type MapMySqlTypeToCSharp(string mySqlType)
        {
            mySqlType = mySqlType.ToLower(); 

            return mySqlType switch
            {
                "char" or "varchar" or "text" or "tinytext" or "mediumtext" or "longtext" => typeof(string),
                "tinyint" => typeof(sbyte), 
                "smallint" => typeof(short),
                "mediumint" => typeof(int),
                "int" or "integer" => typeof(int),
                "bigint" => typeof(long),
                "float" => typeof(float),
                "double" => typeof(double),
                "decimal" or "numeric" => typeof(decimal),
                "date" or "datetime" or "timestamp" => typeof(DateTime),
                "time" => typeof(TimeSpan),
                "blob" or "tinyblob" or "mediumblob" or "longblob" => typeof(byte[]),
                "boolean" or "bool" => typeof(bool),
                "enum" or "set" => typeof(string), 
                "bit" => typeof(bool), 
                "uuid" => typeof(Guid),
                "json" or "jsonb" => typeof(string), 
                _ => typeof(object), 
            };
        }


        public static Type MapPostgreSqlTypeToCSharp(string postgreSqlType)
        {

            postgreSqlType = postgreSqlType.ToLower();
            return postgreSqlType switch
            {
                "character" or "character varying" or "text" => typeof(string),
                "smallint" or "int2" => typeof(short),
                "integer" or "int" or "int4" => typeof(int),
                "bigint" or "int8" => typeof(long),
                "serial" or "serial4" => typeof(int),
                "bigserial" or "serial8" => typeof(long),
                "real" or "float4" => typeof(float),
                "double precision" or "float8" => typeof(double),
                "numeric" or "decimal" => typeof(decimal),
                "boolean" or "bool" => typeof(bool),
                "date" => typeof(DateTime),
                "timestamp" => typeof(DateTime),
                "timestamp with time zone" or "timestamptz" => typeof(DateTimeOffset),
                "time" => typeof(TimeSpan),
                "time with time zone" or "timetz" => typeof(TimeSpan),
                "bytea" => typeof(byte[]),
                "uuid" => typeof(Guid),
                "json" or "jsonb" => typeof(string),
                "money" => typeof(decimal),
                "cidr" or "inet" or "macaddr" or "macaddr8" => typeof(string),
                "point" or "line" or "lseg" or "box" or "circle" or "path" or "polygon" => typeof(string),
                _ => typeof(object), 
            };
        }
    }
}
