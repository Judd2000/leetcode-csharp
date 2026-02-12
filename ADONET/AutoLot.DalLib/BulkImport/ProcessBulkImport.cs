using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.DalLib.BulkImport;

public static class ProcessBulkImport
{
    private const string ConnectString = @"Data Source=.,5433;User Id=sa;Password=Th3N3wP@ssword_;Initial Catalog=AutoLot;Encrypt=False;";
    private static SqlConnection _sqlConnection = null;

    private static void OpenConnection() { 
        _sqlConnection = new SqlConnection(ConnectString);
        _sqlConnection.Open();
    }

    private static void CloseConnection() {
        if (_sqlConnection?.State != ConnectionState.Closed) { 
            _sqlConnection?.Close();
        }
    }

    public static void ExecuteBulkImport<T>(IEnumerable<T> records, string tableName) {
        OpenConnection();
        using SqlConnection conn = _sqlConnection;
        SqlBulkCopy bulkCopy = new(conn)
        {
            DestinationTableName = tableName
        };

        MyDataReader<T> reader = new([.. records], _sqlConnection, "dbo", tableName);

        try
        {
            bulkCopy.WriteToServer(reader);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured adding the bulk items, {ex}");
            // smth
        }
        finally {
            CloseConnection();
        }    
    }
}
