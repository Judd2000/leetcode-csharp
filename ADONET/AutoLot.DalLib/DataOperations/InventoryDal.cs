using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AutoLot.DalLib.DataOperations;

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
        OpenConnection();
        List<CarViewModel> inventory = [];

        string sqlStatement = @"SELECT i.Id, i.Color, i.PetName as Name, m.Name as Make FROM Inventory I INNER JOIN Makes m on m.Id = i.MakeId";

        using SqlCommand command = new(sqlStatement, _connection)
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

    public void InsertAuto(string color, int makeId, string name) {
        OpenConnection();
        string formattedSql = $"Insert Into Inventory (MakeId, Color, PetName) Values ('{makeId}', '{color}', '{name}')";

        using (SqlCommand command = new(formattedSql, _connection)) {
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
        }

        CloseConnection();
    }

    //overload the insert Car object method
    public void InsertAuto(Car car) {
        OpenConnection();


        string formattedSql = $"Insert Into Inventory (MakeId, Color, PetName) Values (@makeId, @color, @name)";

        // execute query
        using (SqlCommand command = new(formattedSql, _connection))
        {
            SqlParameter makeParam = new() { 
                ParameterName = "@makeId",
                Value = car.MakeId,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input
            };
            command.Parameters.Add(makeParam);

            SqlParameter colorParam = new()
            {
                ParameterName = "@color",
                Value = car.Color,
                SqlDbType = SqlDbType.NVarChar,
                Size = 50,
                Direction = ParameterDirection.Input
            };
            command.Parameters.Add(colorParam);

            SqlParameter nameParam = new()
            {
                ParameterName = "@name",
                Value = car.Name,
                SqlDbType = SqlDbType.NVarChar,
                Size = 50,
                Direction = ParameterDirection.Input
            };
            command.Parameters.Add(nameParam);

            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
        }

        CloseConnection();
    }

    public void DeleteCar(int id) {
        OpenConnection();

        SqlParameter param = new()
        {
            ParameterName = "@carId",
            Value = id,
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input,
        };

        string formattedSql = @"Delete from Inventory where Id = @carId";

        using (SqlCommand command = new(formattedSql, _connection)) {
            try
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add(param);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex) {
                Exception err = new("The car you are trying to delete is on order", ex);
                throw err;
            }
        }
    }

    public void UpdateName(int id, string newName) { 
        OpenConnection();

        SqlParameter paramId = new()
        {
            ParameterName = "@carId",
            Value = id,
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input,
        };

        SqlParameter paramName = new()
        {
            ParameterName = "@name",
            Value = newName,
            SqlDbType = SqlDbType.NVarChar,
            Size = 50,
            Direction = ParameterDirection.Input,
        };
        string sql = @"Update Inventory Set PetName = @name Where Id = @carId";

        using (SqlCommand command = new(sql, _connection)) {
            command.Parameters.Add(paramId);
            command.Parameters.Add(paramName);
            command.ExecuteNonQuery();
        }

        CloseConnection();
    }

    public string LookUpName(int carId) {
        OpenConnection();
        string carName = "";
        // GetPetName is stored procedure name in the db
        using (SqlCommand command = new("GetPetName", _connection)) { 
            command.CommandType = CommandType.StoredProcedure;

            // params
            SqlParameter inputParam = new()
            {
                ParameterName = "@carId",
                SqlDbType = SqlDbType.Int,
                Value = carId,
                Direction = ParameterDirection.Input
            };
            command.Parameters.Add(inputParam);

            // Output Param
            SqlParameter outputParam = new() { 
                ParameterName = "@name",
                SqlDbType = SqlDbType.NVarChar,
                Size = 50,
                Direction = ParameterDirection.Output,
            };

            command.Parameters.Add(outputParam);

            // Execute nonquery (even though it seems like a query, it is a procedure with an out param)

            command.ExecuteNonQuery();

            // return out poaram
            carName = (string) command.Parameters["@name"].Value;
            CloseConnection();
        }
        return carName;
    }

    public CarViewModel? GetCar(int id) {
        OpenConnection();
        CarViewModel? car = null;

        SqlParameter param = new()
        {
            ParameterName = "@carId",
            Value = id,
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input,
        };

        string sqlText = @"SELECT i.Id, i.Color, i.PetName as Name, m.Name as Make FROM Inventory i INNER JOIN Makes m on m.Id = i.MakeId WHERE i.Id = @carId";

        using SqlCommand command = new()
        {
            CommandType = CommandType.Text,
            CommandText = sqlText,
            Connection = _connection
        };

        command.Parameters.Add(param);

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
