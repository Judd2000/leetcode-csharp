using Microsoft.Data.SqlClient;

Console.WriteLine("***** Fun with Data Readers *****\n");

// Create / open connection

using (SqlConnection connection = new SqlConnection()) {
    //connection.ConnectionString = @"Data Source=.,5433;User Id=sa;Password=Th3N3wP@ssword_;Initial Catalog=AutoLot;Encrypt=False;";
    //

    SqlConnectionStringBuilder builder = new() { 
        InitialCatalog = "AutoLot",
        Encrypt = false,
        DataSource = ".,5433",
        UserID = "sa",
        Password= "Th3N3wP@ssword_",
        ConnectTimeout = 30
    };

    connection.ConnectionString = builder.ConnectionString;

    // Consider a connection string builder
    connection.Open();
    // SQL Command Object

    string sql = @"Select i.id, m.Name as Make, i.Color, i.PetName FROM Inventory i INNER JOIN Makes m on m.Id = i.MakeId";
    SqlCommand command = new(sql, connection);

    // Other ways to spin up a sql command
    SqlCommand anotherCommand = new();
    anotherCommand.CommandText = sql;
    anotherCommand.Connection = connection;

    // data reader
    using (SqlDataReader reader = command.ExecuteReader()) {
        while (reader.Read()) {
            //Console.WriteLine($"-> Make: {reader["Make"]}, PetName: {reader["PetName"]}, Color: {reader["Color"]}");

            // Non-hardcoded property approach.
            for (int i = 0; i < reader.FieldCount; i++) {
                //For each field...
                string keyVal = $"{reader.GetName(i)} = {reader.GetValue(i)}";
                Console.Write(i != reader.FieldCount - 1 ? $"{keyVal}, " : keyVal);
            }
            Console.WriteLine();
        }
        
        // alternate iteration approach: use do while NextResult outside of this loop (for multiple sql queries sparated by ';' in the command string)
    }
}

