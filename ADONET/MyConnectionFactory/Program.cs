using System.Data;
using System.Data.Common;
using System.Data.Odbc;
#if PC
using System.Data.OleDb;
#endif
using Microsoft.Data.SqlClient;
using MyConnectionFactory;

Console.WriteLine("**** Connnection Factory Example ****");
Setup(DataProviderEnum.SqlServer);

#if PC
Setup(DataProviderEnum.OleDb); // conditional checks are because Mac OS does not support OleDb.
#endif
Setup(DataProviderEnum.Odbc);
Setup(DataProviderEnum.None);

static void Setup(DataProviderEnum type) {
    IDbConnection? connection = GetConnection(type);
    Console.WriteLine($"Connection Type: {connection?.GetType().Name ?? "not found"}");
}

static IDbConnection? GetConnection(DataProviderEnum type) {
    return type switch
    {
        DataProviderEnum.SqlServer => new SqlConnection(),
#if PC
        DataProviderEnum.OleDb => new OleDbConnection(),
#endif
        DataProviderEnum.Odbc => new OdbcConnection(),
        _ => null
    };
}
