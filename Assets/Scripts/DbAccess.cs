using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using System.Text;

public class DbAccess
{
    private SqliteConnection dbConnection;
    private SqliteCommand dbCommand;
    private SqliteDataReader reader;

    public DbAccess(string connectString)
    {
        OpenDB(connectString);
    }

    public DbAccess()
    {

    }

    public void OpenDB(string connectString)
    {
        try
        {
            dbConnection = new SqliteConnection(connectString);
            dbConnection.Open();
            Debug.Log("Connect to db");
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    public void CloseSqlConnection()
    {
        if (dbCommand != null)
        {
            dbCommand.Dispose();
        }
        dbCommand = null;
        if (reader != null)
        {
            reader.Dispose();
        }
        reader = null;
        if (dbConnection != null)
        {
            dbConnection.Dispose();
        }
        dbConnection = null;
    }

    public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = sqlQuery;
        reader = dbCommand.ExecuteReader();
        return reader;
    }

    public SqliteDataReader ReadFullTable(string tableName)
    {
        string query = "SELECT * FROM " + tableName;
        return ExecuteQuery(query);
    }

    public SqliteDataReader InsertInto(string tableName, string[] values)
    {
        StringBuilder queryStr = new StringBuilder();
        
        queryStr.Append(string.Format("INSERT INTO {0} VALUES ({1}", tableName, values[0]));
        
        for (int i = 1; i < values.Length; ++i)
        {
            queryStr.Append(", ");
            queryStr.Append(values[i]);
        }
        
        queryStr.Append(")");

        Debug.Log(queryStr.ToString());
        return ExecuteQuery(queryStr.ToString());
    }

    public SqliteDataReader UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {
        StringBuilder queryStr = new StringBuilder();

        queryStr.Append(string.Format("UPDATE {0} SET {1} = {2}", tableName, cols[0], colsvalues[0]));

        for (int i = 1; i < colsvalues.Length; ++i)
        {
            queryStr.Append(string.Format(", {0} = {1}", cols[i], colsvalues[i]));
        }

        queryStr.Append(string.Format(" WHERE {0} = {1}", selectkey, selectvalue));

        Debug.Log(queryStr.ToString());
        return ExecuteQuery(queryStr.ToString());
    }

    public SqliteDataReader Delete(string tableName, string[] cols, string[] colsvalues)
    {
        StringBuilder queryStr = new StringBuilder();

        queryStr.Append(string.Format("DELETE FROM {0} WHERE {1} = {2}", tableName, cols[0], colsvalues[0]));

        for (int i = 1; i < colsvalues.Length; ++i)
        {
            queryStr.Append(string.Format(" or {0} = {1}", cols[i], colsvalues[i]));
        }

        Debug.Log(queryStr.ToString());
        return ExecuteQuery(queryStr.ToString());
    }

    public SqliteDataReader InsertIntoSpecific(string tableName, string[] cols, string[] values)
    {
        if (cols.Length != values.Length)
        {
            throw new SqliteException("columns.Length != values.Length");
        }

        StringBuilder queryStr = new StringBuilder();

        queryStr.Append(string.Format("INSERT INTO {0} ({1}", tableName, cols[0]));

        for (int i = 1; i < cols.Length; ++i)
        {
            queryStr.Append(string.Format(", {0}", cols[i]));
        }

        queryStr.Append(string.Format(") VALUES ({0}", values[0]));

        for (int i = 1; i < values.Length; ++i)
        {
            queryStr.Append(string.Format(", {0}", values[i]));
        }

        queryStr.Append(")");

        Debug.Log(queryStr.ToString());
        return ExecuteQuery(queryStr.ToString());
    }

    public SqliteDataReader DeleteContents(string tableName)
    {
        string query = string.Format("DELETE FROM ", tableName);

        return ExecuteQuery(query);
    }

    public SqliteDataReader CreateTable(string name, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {
            throw new SqliteException("columns.Length != colType.Length");
        }

        StringBuilder queryStr = new StringBuilder();
        queryStr.Append(
                    string.Format("CREATE TABLE {0} ({1} {2}", name, col[0], colType[0])
                    );

        for (int i = 1; i < col.Length; ++i)
        {
            queryStr.Append(string.Format(", {0} {1}", col[i], colType[i])
                );
        }

        queryStr.Append(")");

        return ExecuteQuery(queryStr.ToString());
    }

    public SqliteDataReader SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
    {
        if (col.Length != operation.Length || operation.Length != values.Length)
        {
            throw new SqliteException("col.Length != operation.Length != values.Length");
        }

        StringBuilder queryStr = new StringBuilder();
        queryStr.Append(string.Format("SELECT {0}", items[0]));

        for (int i = 1; i < items.Length; ++i)
        {
            queryStr.Append(string.Format(", {0}", items[i]));
        }

        queryStr.Append(string.Format(" FROM {0} WHERE {1} {2} {3}", tableName, col[0], operation[0], values[0]));

        for (int i = 1; i < col.Length; ++i)
        {
            queryStr.Append(string.Format(" AND {0} {1} {2}", col[i], operation[i], values[i]));
        }

        Debug.Log(queryStr.ToString());
        return ExecuteQuery(queryStr.ToString());
    }
}
