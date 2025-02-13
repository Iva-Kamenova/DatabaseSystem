using DatabaseProject.MyUtils;

namespace DatabaseProject.Queries
{
    public static class DeleteQuery
    {
        public static string DeleteFrom(string command)
        {
                var tokens = MyString.Split(command, '|');
                string tableName = tokens[2];

                string metaFile = Database.GetTableMetaFilePath(tableName);
                string dataFile = Database.GetTableDataFilePath(tableName);

                if (!File.Exists(metaFile) && !File.Exists(dataFile))
                {
                    throw new Exception($"The table '{tableName}' is not found.");
                }

                var rowIndices = MyString.Split(tokens[4], ',');
                int rowCount = Database.GetRecordsCount(dataFile);

                var rowsList = new MyList<string>();
                using (var dataReader = new StreamReader(dataFile))
                {
                    string? line;
                    while ((line=dataReader.ReadLine()) != null)
                    {
                        var values = MyString.Split(line, '\t');
                        var resValues = MyString.Join(values, '\t');
                        rowsList.Add(resValues);
                    }
                }
                var rows = rowsList.ToArray();

                int[] rowsToDelete = new int[rowIndices.Length];
                int deleteCount = 0;

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

                    if (isValidIndex && rowIndex > 0 && rowIndex <= rowCount)
                    {
                        rowsToDelete[deleteCount] = rowIndex - 1;
                        deleteCount++;
                    }
                    else
                    {
                        throw new Exception($"Invalid row index: {rowIndices[i]}");
                    }
                }

                using (var writer = new StreamWriter(dataFile))
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        bool isToDelete = false;

                        for (int j = 0; j < deleteCount; j++)
                        {
                            if (i == rowsToDelete[j])
                            {
                                isToDelete = true;
                                break;
                            }
                        }

                        if (!isToDelete)
                        {
                            writer.WriteLine(rows[i]);
                        }
                    }
                }
                return "The records are deleted successfully.\n";
        }
    }
}
