using DatabaseProject.MyUtils;

namespace DatabaseProject.Queries
{
    public static class InfoQuery
    {
        public static string TableInfo(string command)
        {
            string res = "";
            var tokens = MyString.Split(command, '|');
            string tableName = tokens[2];

            string metaFile = Database.GetTableMetaFilePath(tableName);
            string dataFile = Database.GetTableDataFilePath(tableName);

                if (!File.Exists(metaFile) && !File.Exists(dataFile))
                {
                    throw new Exception($"The table '{tableName}' is not found.");
                }

                res += $"Information for table '{tableName}':\n";
                res += "Table scheme:\n";

                    using (var metaReader = new StreamReader(metaFile))
                    {
                        string? line;
                        while ((line = metaReader.ReadLine()) != null)
                        {
                           res += line+"\n";
                        }
                    }
                    
                    int recordCount = Database.GetRecordsCount(dataFile);

                    if (recordCount == 0)
                    {
                       res += $"The table '{tableName}' is empty.\n";
                    }
                    else 
                    {
                       res += $"The number of records: {recordCount}\n"; 
                    }

                    long metaSize = new FileInfo(metaFile).Length;
                    long dataSize = new FileInfo(dataFile).Length;

                    res += $"Occupied space: {metaSize + dataSize} bytes\n";
                
                return res;
        }
    }
}
