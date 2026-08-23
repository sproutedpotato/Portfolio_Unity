using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.Data.Common;
using System.IO;
using System;

public class UIDB: MonoBehaviour
{
    private string dbname;
    private string connectionString;

    // Start is called before the first frame update
    void OnEnable()
    {
        dbname = "UIDB.db";
        string streamingPath = Application.streamingAssetsPath;
        if (!streamingPath.EndsWith("/"))
            streamingPath += "/";

        string dbPath = streamingPath + dbname;

        connectionString = "URI=file:" + dbPath;
    }

    //InsertData("PlayerInfo", new string[] {"Name", "Level"}, new object[] {"Bear", 5});
    public void InsertData(string tableName, string[] columns, object[] values)
    {
        if (columns.Length != values.Length)
        {
            Debug.LogError("Columns and values count mismatch.");
            return;
        }

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand cmd = dbConnection.CreateCommand())
            {
                string colNames = string.Join(", ", columns);
                string valPlaceholders = string.Join(", ", GetPlaceholders(values.Length));

                cmd.CommandText = $"INSERT INTO {tableName} ({colNames}) VALUES ({valPlaceholders})";
                AddParameters(cmd, values);
                cmd.ExecuteNonQuery();
            }
        }
    }

    // UPDATE
    //UpdateData("PlayerInfo", "Level", 10, "Name", "Bear");
    public void UpdateData(string tableName, string columnToUpdate, object newValue, string whereColumn, object whereValue)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand cmd = dbConnection.CreateCommand())
            {
                cmd.CommandText = $"UPDATE {tableName} SET {columnToUpdate} = @newValue WHERE {whereColumn} = @whereValue";
                cmd.Parameters.Add(new SqliteParameter("@newValue", newValue));
                cmd.Parameters.Add(new SqliteParameter("@whereValue", whereValue));
                cmd.ExecuteNonQuery();
            }
        }
    }

    // DELETE
    //DeleteData("PlayerInfo", "Name", "Bear");
    public void DeleteData(string tableName, string whereColumn, object whereValue)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand cmd = dbConnection.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM {tableName} WHERE {whereColumn} = @whereValue";
                cmd.Parameters.Add(new SqliteParameter("@whereValue", whereValue));
                cmd.ExecuteNonQuery();
            }
        }
    }

    // SELECT
    //SelectData("PlayerInfo", "*");
    //SelectData("PlayerInfo", "Name, Level", "Name = 'Bear'");
    public T SelectData<T>(string tableName, string column, string whereColumn, object whereValue)
    {
        T result = default(T);

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand cmd = dbConnection.CreateCommand())
            {
                cmd.CommandText = $"SELECT {column} FROM {tableName} WHERE {whereColumn} = @whereValue";

                var param = cmd.CreateParameter();
                param.ParameterName = "@whereValue";
                param.Value = whereValue;
                cmd.Parameters.Add(param);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        object value = reader.GetValue(0);
                        if (value != DBNull.Value)
                        {
                            result = (T)Convert.ChangeType(value, typeof(T));
                        }
                    }
                }
            }
        }

        return result;
    }

    private string[] GetPlaceholders(int count)
    {
        string[] placeholders = new string[count];
        for (int i = 0; i < count; i++)
        {
            placeholders[i] = "@param" + i;
        }
        return placeholders;
    }

    private void AddParameters(IDbCommand cmd, object[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            cmd.Parameters.Add(new SqliteParameter("@param" + i, values[i]));
        }
    }
}
