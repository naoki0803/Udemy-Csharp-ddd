using Microsoft.Data.Sqlite;

namespace DDD.Infrastructure.SQLite;

public static class SQLiteHelper
{
    public const string ConnectionString = @"Data Source=/Users/shiratorinaoki/DataBase/sqlite/Udemy-DDD-Part1.db";

    internal static IReadOnlyList<T> Query<T>(string sql, Func<SqliteDataReader, T> createEntity)
    {
        var results = new List<T>();
        try
        {
            using (var connection = new SqliteConnection(ConnectionString))
            using (var command = new SqliteCommand(sql, connection))
            {
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(createEntity(reader));
                    }
                }
                return results.AsReadOnly();
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("データベース接続エラー", ex);
        }
    }

    internal static T QuerySingle<T>(string sql, SqliteParameter[] parameters, Func<SqliteDataReader, T> createEntity, T nullEntity)
    {
        try
        {
            using (var connection = new SqliteConnection(ConnectionString))
            using (var command = new SqliteCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.AddRange(parameters);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return createEntity(reader);
                    }
                }
                return nullEntity;
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("データベース接続エラー", ex);
        }
    }
};