using DatabaseProject.Conditions;
using DatabaseProject.MyUtils;

namespace DatabaseProject.Queries
{
    public static class SelectQuery
    {
        public static string SelectFrom(string command)
        {
            string res = "";
            var tokens = MyString.Split(command, '|');

            string columnsPart = tokens[1];
            string tableName = tokens[3];

            string dataFilePath = Database.GetTableDataFilePath(tableName);
            string metaFilePath = Database.GetTableMetaFilePath(tableName);

                if (!File.Exists(metaFilePath) && !File.Exists(dataFilePath))
                {
                    throw new Exception($"The table '{tableName}' is not found.");
                }

                var columns = Database.GetColumnsFromMetaFile(metaFilePath);
            
                if (columnsPart == "*")
                {
                    columnsPart = MyString.Join(columns.ToArray(), ',');
                }

                var requestedColumns = MyString.Split(columnsPart, ',');


                foreach (var column in requestedColumns)
                {
                    if (!columns.Contains(MyString.Trim(column)))
                    {
                        throw new Exception($"The column '{column}' doesn't exist in table '{tableName}'.");
                    }
                }

                MyList<MyList<string>> records = Database.LoadDataFromFile(dataFilePath);

                string whereCondition = null;
                if (tokens.Length > 4 && tokens[4] == "WHERE")
                {
                    whereCondition = tokens[5];
                }

                if (whereCondition != null)
                {
                    records = WhereClause.Where(whereCondition, tableName);
                }

                string orderByColumn = null;
                bool isDescending = false;
                string orderByToken = "";

            if (tokens[tokens.Length-2] == "ORDER BY" || tokens[tokens.Length-3] == "ORDER BY")
            {
                if(tokens[tokens.Length - 2] == "ORDER BY")
                {
                    orderByToken = tokens[tokens.Length-1]; 
                }
                else if(tokens[tokens.Length - 3] == "ORDER BY")
                {
                    orderByToken = tokens[tokens.Length-2];
                }
                
                if (!MyValidation.IsNullOrEmpty(orderByToken))
                {
                    var orderParts = MyString.Split(orderByToken, ' ');
                    orderByColumn = MyString.Trim(orderParts[0]);

                    if (orderParts.Length > 1 && orderParts[1] == "DESC")
                    {
                        isDescending = true;
                    }
                    if (orderParts.Length > 1 && orderParts[1] != "DESC" && orderParts[1] != "ASC")
                    {
                        throw new Exception("Invalid ORDER BY clause.");
                    }

                    if (!columns.Contains(orderByColumn))
                    {
                        throw new Exception($"The column '{orderByColumn}' doesn't exist in table '{tableName}'.");
                    }
                }
            }

            
            if (orderByColumn != null)
            {
                int orderByIndex = columns.IndexOf(orderByColumn);
                records = CustomSort(records, orderByIndex, isDescending);
            }

            bool isDistinct = false;
            if (tokens[tokens.Length-1] == "DISTINCT")
            {
                isDistinct = true;
            }

            if (isDistinct)
            {
                records = ApplyDistinct(records, requestedColumns, columns);
            }

            foreach (var record in records)
            {
                foreach (var column in requestedColumns)
                {
                    int columnIndex = columns.IndexOf(MyString.Trim(column));
                    res += record[columnIndex] + "\t";
                }
                res += "\n";
            }
            return res;
        } 
        private static MyList<MyList<string>> CustomSort(MyList<MyList<string>> records, int columnIndex, bool isDescending)
        {
        for (int i = 0; i < records.Count - 1; i++)
        {
            for (int j = 0; j < records.Count - i - 1; j++)
            {
                bool shouldSwap = isDescending
                    ? MyString.Compare(records[j][columnIndex], records[j + 1][columnIndex]) < 0
                    : MyString.Compare(records[j][columnIndex], records[j + 1][columnIndex]) > 0;

                if (shouldSwap)
                {
                    var temp = records[j];
                    records[j] = records[j + 1];
                    records[j + 1] = temp;
                }
            }
        }
        return records;
    }

        private static MyList<MyList<string>> ApplyDistinct(MyList<MyList<string>> records, string[] requestedColumns, MyList<string> columns)
        {
            var distinctRecords = new MyList<MyList<string>>();
            var seenRecords = new MyList<string>();

            foreach (var record in records)
            {
                string key = "";
                foreach (var column in requestedColumns)
                {
                    int columnIndex = columns.IndexOf(MyString.Trim(column));
                    key += record[columnIndex] + "|";
                }

                if (!seenRecords.Contains(key))
                {
                    seenRecords.Add(key);
                    distinctRecords.Add(record);
                }
            }
            return distinctRecords;
        }
    }
}

