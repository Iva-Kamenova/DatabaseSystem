using DatabaseProject.MyUtils;

namespace DatabaseProject.Queries
{
    public static class GetQuery
    {
        public static string GetRow(string command)
        {
            string res = "";
            var tokens = MyString.Split(command, '|');
            string tableName = tokens[4];

            string metaFile = Database.GetTableMetaFilePath(tableName);
            string dataFile = Database.GetTableDataFilePath(tableName);

                if (!File.Exists(metaFile) && !File.Exists(dataFile))
                {
                    throw new Exception($"The table '{tableName}' is not found.");
                }

                var rowIndices = MyString.Split(tokens[2], ',');

                int rowCount = Database.GetRecordsCount(dataFile);

                for (int i = 0; i < rowIndices.Length; i++)
                {
                    string trimmedIndex = MyString.Trim(rowIndices[i]);
                    int rowIndex = 0;
                    bool isValidIndex = true;


                    for (int j = 0; j < trimmedIndex.Length; j++)
                    {
                        if (trimmedIndex[j] >= '0' && trimmedIndex[j] <= '9')
                        {
                            rowIndex = rowIndex * 10 + (trimmedIndex[j] - '0');
                        }
                        else
                        {
                            isValidIndex = false;
                            break;
                        }
                    }


                    if (!isValidIndex || rowIndex <= 0 || rowIndex > rowCount)
                    {
                        throw new Exception($"Invalid row index: {rowIndices[i]}");
                    }
                }

                var columnNames = Database.GetColumnsFromMetaFile(metaFile);
                res += MyString.Join(columnNames.ToArray(), '\t')+"\n";

                using (var dataReader = new StreamReader(dataFile))
                {
                    int currentRow = 0;
                    while (!dataReader.EndOfStream)
                    {
                        string? line = dataReader.ReadLine();
                        currentRow++;

                        foreach (var index in rowIndices)
                        {
                            if (currentRow == MyString.ConvertToInteger(MyString.Trim(index)))
                            {
                                var values = MyString.Split(line, '\t');
                                res += MyString.Join(values, '\t')+"\n";
                                break;
                            }
                        }
                    }
                }
                return res;
        }
    }
}
