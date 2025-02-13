using DatabaseProject.MyUtils;

namespace DatabaseProject.Queries
{
    public static class InsertQuery
    {
        public static string InsertInto(string command)
        {
                var tokens = MyString.Split(command, '|');
                string tableName = tokens[2];

                string metaFile = Database.GetTableMetaFilePath(tableName);
                string dataFile = Database.GetTableDataFilePath(tableName);

                if (!File.Exists(metaFile) && !File.Exists(dataFile))
                {
                    throw new Exception($"The table '{tableName}' is not found.");
                }

                var tableColumns = Database.LoadTableColumns(tableName);

                int columnsStartIndex = MyString.IndexOf(tokens[3], "(");
                int columnsEndIndex = MyString.IndexOf(tokens[3], ")");

                if (columnsStartIndex == -1 || columnsEndIndex == -1 || columnsEndIndex < columnsStartIndex)
                {
                    throw new Exception("Invalid command. Make sure that the columns are properly defined with brackets.");
                }

                int valuesStartIndex = MyString.IndexOf(tokens[5], "(");
                int valuesEndIndex = MyString.IndexOf(tokens[5], ")");

                if (valuesStartIndex == -1 || valuesEndIndex == -1 || valuesEndIndex < valuesStartIndex)
                {
                    throw new Exception("Invalid command. Make sure that the values are properly defined with brackets.");
                }

                var columns = MyString.Split(MyString.Substring(tokens[3], columnsStartIndex + 1, columnsEndIndex - 1), ',');
                var values = MyString.Split(MyString.Substring(tokens[5], valuesStartIndex + 1, valuesEndIndex - 1), ',');

                MyList<string> fullRowValues = new MyList<string>();

                foreach (var column in tableColumns)
                {
                    int colIndex = Array.FindIndex(columns, c => MyString.Trim(c) == column.Name);
                    if (colIndex != -1)
                    {
                        fullRowValues.Add(MyString.Trim(values[colIndex]));
                    }
                    else
                    {
                        fullRowValues.Add((string)(column.DefaultValue ?? "null"));
                    }
                }

                if (MyValidation.ValidateData(tableName, columns, values))
                {
                    InsertRowIntoDataFile(tableName, fullRowValues);
                }
                return "The records are added successfully.\n";
        }
        private static void InsertRowIntoDataFile(string tableName, MyList<string> values)
        {
            string dataFile = Database.GetTableDataFilePath(tableName);

            using (var writer = new StreamWriter(dataFile, true))
            {
                string lineToWrite = MyString.Join(values.ToArray(), '\t');
                writer.WriteLine(lineToWrite);
            }
        }

    }

}
