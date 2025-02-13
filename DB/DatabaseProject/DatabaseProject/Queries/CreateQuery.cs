using DatabaseProject.MyUtils;
using DatabaseProject.TableBuilding;

namespace DatabaseProject.Queries
{
    public static class CreateQuery
    {
        public static string CreateTable(string command)
        {
                var (tableName, columns) = ParseCreateTableCommand(command);

                string metaFile = Database.GetTableMetaFilePath(tableName);
                string dataFile = Database.GetTableDataFilePath(tableName);

                Directory.CreateDirectory(Database.DATABASE_PATH);

                if (File.Exists(metaFile) || File.Exists(dataFile))
                {
                    throw new Exception($"The table '{tableName}' already exists.");
                }

                using (var meta = new StreamWriter(metaFile))
                {
                    foreach (var col in columns)
                    {
                        var defaultPart = col.DefaultValue != null
                            ? " DEFAULT " + col.DefaultValue
                            : "";

                        meta.WriteLine(MyString.Join([col.Name, MyString.ConvertEnumToString(col.Type) + defaultPart], ' '));
                    }
                }
                File.Create(dataFile).Close();
                return $"The table '{tableName}' is created successfully.\n";
        }

        private static (string tableName, MyList<Column> columns) ParseCreateTableCommand(string command)
        {
                var tokens = MyString.Split(command, '|');
                string tableName = tokens[2];
                var columns = new MyList<Column>();

                int columnsStartIndex = MyString.IndexOf(command, "(");
                int columnsEndIndex = MyString.IndexOf(command, ")");

                if (columnsStartIndex == -1 || columnsEndIndex == -1 || columnsEndIndex < columnsStartIndex)
                {
                    throw new Exception("Invalid command. Make sure that the columns are properly defined with brackets.");
                }

                string columnsPart = MyString.Substring(command, columnsStartIndex + 1, columnsEndIndex - 1);

                foreach (var columnDef in MyString.Split(columnsPart, ','))
                {
                    var columnDetails = MyString.Trim(columnDef);

                    int typeSeparatorIndex = MyString.IndexOf(columnDetails, ":");
                    if (typeSeparatorIndex == -1)
                    {
                        throw new Exception($"Invalid definition for column: '{columnDetails}'. The type is missing.");
                    }

                    string columnName = MyString.Trim(MyString.Substring(columnDetails, 0, typeSeparatorIndex - 1));
                    string typeAndDefault = MyString.Trim(MyString.Substring(columnDetails, typeSeparatorIndex + 1, columnDetails.Length - 1));


                    string columnTypeStr;
                    string defaultValue = null;
                    int defaultIndex = MyString.IndexOf(typeAndDefault, "default");
                    if (defaultIndex != -1)
                    {
                        columnTypeStr = MyString.Trim(MyString.Substring(typeAndDefault, 0, defaultIndex - 1));
                        defaultValue = MyString.Trim(MyString.Substring(typeAndDefault, defaultIndex + "default".Length, typeAndDefault.Length - 1));
                    }
                    else
                    {
                        columnTypeStr = typeAndDefault;
                    }

                    ColumnType columnType = MyString.ConvertStringToEnum(columnTypeStr);


                    columns.Add(new Column(columnName, columnType, defaultValue)
                    {
                        Name = columnName,
                        Type = columnType,
                        DefaultValue = defaultValue
                    });
                }
            return (tableName, columns);
        }
    }
}

