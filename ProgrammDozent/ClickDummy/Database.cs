using System;
using System.Collections.Generic;
using Sybase.Data.AseClient;

namespace ProgrammDozent
{
   
    public class Database
    {
        public string ConnectionString =  "Data Source=141.56.20.2;Port=5200;" +
                                    "UID=case04;PWD=itcyisay;" +
                                    "Database=case04;";

        readonly AseConnection _conn;
        public Database()
        {
           _conn = new AseConnection(ConnectionString);
        }

        public void Connect()
        {
            
            try
            {
                _conn.Open();
            }
            catch (AseException ex)
            {
                Console.Write(
                   ex.Message,
                   "Failed to connect");
            }
        }
        
        public List<string[]> ExecuteQuery(string query)
        {
            Connect();
            var command = new AseCommand(query, _conn);
            using (var dataReader = command.ExecuteReader())
            {
                var col = dataReader.FieldCount;
                var strings = new List<string[]>();

                while (dataReader.Read())
                {
                    var array = new string[col];
                    for (var i = 0; i < col; i++)
                    {
                        array[i] = dataReader.GetString(i).Trim();
                    }
                    strings.Add(array);
                }
                _conn.Close();
                return strings;
            }
            
        }
    }
}
