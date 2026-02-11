using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AutoLot.Dal.DataOperations;

public class InventoryDal(string connectionString) : IDisposable
{
    private readonly string _connectionString = connectionString;

    private SqlConnection _connection = null;

    private bool _disposed = false;

    private void OpenConnection() {
        _connection = new() { ConnectionString = _connectionString };
        _connection.Open();
    }

    private void CloseConnection() {
        if (_connection?.State != ConnectionState.Closed) { 
            _connection?.Close();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing) { 
            _connection.Dispose();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public List<CarViewModel> GetAllInventory() {
        List<CarViewModel> inventory = [];

        string sqlStatement = @"SELECT i.Id, i.Color, i.PetName as Name, m.Name as Make FROM Inventory I INNER JOIN Makes m on m.Id = i.MakeId";

        using SqlCommand command = new(sqlStatement)
        {
            CommandType = CommandType.Text,
        };
        command.CommandType = CommandType.Text;

        // Sql data reader
        SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            inventory.Add(new CarViewModel { 
                Id = (int) dataReader["Id"],
                Color = (string) dataReader["Color"],
                Make = (string) dataReader["Make"],
                Name = (string) dataReader["Name"]
            });
        }
        dataReader.Close();
        return inventory;
    }

    public void InsertAuto(string color, string make, string name) {
        //"";
    }

    public CarViewModel? GetCar(int id) {
        OpenConnection();
        CarViewModel? car = null;

        string sqlText = $@"SELECT i.Id, i.Color, i.PetName as Name, m.Name as Make FROM Inventory i INNER JOIN Makes m on m.Id = i.MakeId WHERE i.Id = {id}";

        using SqlCommand command = new()
        {
            CommandType = CommandType.Text,
        };

        SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        while (reader.Read()) {
            car = new CarViewModel
            {
                Id = (int)reader["Id"],
                Color = (string)reader["Color"],
                Make = (string)reader["Make"],
                Name = (string)reader["Name"]
            };
        }

        reader.Close();
        return car;
    }

    public InventoryDal() : this(@"Data Source=.,5433;User Id=sa;Password=Th3N3wP@ssword_;Initial Catalog=AutoLot;Encrypt=False;") { }
}
