## 1. `DatabaseManagement` Class

### Constructor:

```csharp
public DatabaseManagement(string username, string password, string address, string port, string databaseName, string databaseType)
```

### Methods:

```csharp
public bool CheckConnection()
	returns: true if connection is successful, false otherwise
public string GetConnectionString()
	returns the connection string
public DatabaseSchema GetDatabaseSchema()
	returns a DatabaseSchema object containing the structure of the database.
public string GetDatabaseType()
	returns the type of the database
```

## 2. `TypeConverter` Class

### Methods:

```csharp
public static Type MapMySqlTypeToCSharp(string mySqlType)
public static Type MapPostgreSqlTypeToCSharp(string postgreSqlType)
	returns: The corresponding C# type (Type).
```

| C# Type          | MySQL Type                                                      | PostgreSQL Type                                                                                    |
| ---------------- | --------------------------------------------------------------- | -------------------------------------------------------------------------------------------------- |
| `string`         | `CHAR`, `VARCHAR`, `TEXT`, `TINYTEXT`, `MEDIUMTEXT`, `LONGTEXT` | `character`, `character varying`, `text`                                                           |
| `sbyte`          | `TINYINT`                                                       |                                                                                                    |
| `short`          | `SMALLINT`                                                      | `smallint`, `int2`                                                                                 |
| `int`            | `INT`, `INTEGER`, `MEDIUMINT`                                   | `integer`, `int`, `int4`                                                                           |
| `long`           | `BIGINT`                                                        | `bigint`, `int8`                                                                                   |
| `int`            | `TINYINT`                                                       | `serial`, `serial4`                                                                                |
| `long`           | `BIGSERIAL`                                                     | `bigserial`, `serial8`                                                                             |
| `float`          | `FLOAT`                                                         | `real`, `float4`                                                                                   |
| `double`         | `DOUBLE`, `DECIMAL`                                             | `double precision`, `float8`                                                                       |
| `decimal`        | `DECIMAL`, `NUMERIC`                                            | `numeric`, `money`                                                                                 |
| `DateTime`       | `DATE`, `DATETIME`, `TIMESTAMP`                                 | `date`, `timestamp`                                                                                |
| `TimeSpan`       | `TIME`                                                          | `time`, `time with time zone`, `timetz`                                                            |
| `byte[]`         | `BLOB`, `TINYBLOB`, `MEDIUMBLOB`, `LONGBLOB`                    | `bytea`                                                                                            |
| `bool`           | `BOOLEAN`, `BOOL`, `BIT`                                        | `boolean`, `bool`                                                                                  |
| `Guid`           | `UUID`                                                          | `uuid`                                                                                             |
| `string`         | `ENUM`, `SET`, `JSON`, `JSONB`                                  | `json`, `jsonb`                                                                                    |
| `DateTimeOffset` |                                                                 | `timestamp with time zone`, `timestamptz`                                                          |
| `string`         |                                                                 | `cidr`, `inet`, `macaddr`, `macaddr8`, `point`, `line`, `lseg`, `box`, `circle`, `path`, `polygon` |

### Docker

#### Start the container

```bash
docker-compose up -d
```

#### Stop the container

```bash
docker-compose down -v
```
