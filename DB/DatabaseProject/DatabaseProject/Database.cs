using DatabaseProject.MyUtils;
using DatabaseProject.Queries;
using DatabaseProject.TableBuilding;

namespace DatabaseProject
{
    public static class Database
    {
        public const string DATABASE_PATH = "./db/";

        public static string GetTableMetaFilePath(string tableName)
        {
            return MyString.DirectoryConcat(DATABASE_PATH, tableName + ".meta");
        }
        public static string GetTableDataFilePath(string tableName)
        {
            return MyString.DirectoryConcat(DATABASE_PATH, tableName + ".data");
        }

        public static int GetRecordsCount(string dataFile)
        {
            int recordCount = 0;
            using (var dataReader = new StreamReader(dataFile))
            {
                while (dataReader.ReadLine() != null)
                {
                    recordCount++;
                }
            }
            return recordCount;    
        }

        public static MyList<string> GetColumnsFromMetaFile(string metaFilePath)
        {
            var columns = new MyList<string>();

            using (var reader = new StreamReader(metaFilePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    var parts = MyString.Split(line, ' ');
                    if (parts.Length > 0)
                    {
                        columns.Add(parts[0]);
                    }
                }
            }
            return columns;
        }

        public static MyList<MyList<string>> LoadDataFromFile(string dataFilePath)
        {
            var records = new MyList<MyList<string>>();

            using (var reader = new StreamReader(dataFilePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    var fields = MyString.Split(line, '\t'); 
                    records.Add(new MyList<string>(fields));
                }
            }
            return records;
        }

        public static MyList<Column> LoadTableColumns(string tableName)
        {
            var columns = new MyList<Column>();
            string metaFile = Database.GetTableMetaFilePath(tableName);

            using (var reader = new StreamReader(metaFile))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = MyString.Split(line, ' ');
                    string columnName = parts[0];
                    ColumnType columnType = MyString.ConvertStringToEnum(parts[1]);

                    string defaultValue = null;
                    int defaultIndex = MyString.IndexOf(line, "DEFAULT");
                    if (defaultIndex != -1)
                    {
                        defaultValue = MyString.Trim(MyString.Substring(line, defaultIndex + "DEFAULT".Length, line.Length - 1));
                    }

                    columns.Add(new Column(columnName, columnType, defaultValue));
                }
            }

            return columns;
        }

        public static string ExecuteQuery(string command)
        {
            try
            {
                var tokens = MyString.Split(command, '|');

                if (tokens.Length == 4 && tokens[0] == "CREATE" && tokens[1] == "TABLE")
                {
                    return CreateQuery.CreateTable(command);
                }
                else if (tokens.Length == 3 && tokens[0] == "DROP" && tokens[1] == "TABLE")
                {
                    return DropQuery.DropTable(command);
                }
                else if (tokens.Length == 3 && tokens[0] == "TABLE" && tokens[1] == "INFO")
                {
                    return InfoQuery.TableInfo(command);
                }
                else if (tokens.Length == 5 && tokens[0] == "GET" && tokens[1] == "ROW" && tokens[3] == "FROM")
                {
                    return GetQuery.GetRow(command);
                }
                else if (tokens.Length == 5 && tokens[0] == "DELETE" && tokens[1] == "FROM" && tokens[3] == "ROW")
                {
                    return DeleteQuery.DeleteFrom(command);
                }
                else if (tokens.Length == 6 && tokens[0] == "INSERT" && tokens[1] == "INTO" && tokens[4] == "VALUES")
                {
                    return InsertQuery.InsertInto(command);
                }
                else if ((tokens.Length >= 4 && tokens.Length <= 9) && tokens[0] == "SELECT" && tokens[2] == "FROM")
                {
                    return SelectQuery.SelectFrom(command);
                }
                else
                {
                    throw new Exception("Invalid command.");
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}\n";
            }
        }
    }
}

