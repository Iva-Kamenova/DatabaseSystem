using DatabaseProject.MyUtils;

namespace DatabaseProject.Queries
{
    public static class DropQuery
    {
        public static string DropTable(string command)
        {
                var tokens = MyString.Split(command, '|');
                string tableName = tokens[2];

                string metaFile = Database.GetTableMetaFilePath(tableName);
                string dataFile = Database.GetTableDataFilePath(tableName);


                if (!File.Exists(metaFile) && !File.Exists(dataFile))
                {
                    throw new Exception($"The table '{tableName}' is not found.");
                }
                else 
                { 
                     File.Delete(metaFile);
                     File.Delete(dataFile);
                }
                return $"The table '{tableName}' is deleted successfully.\n";
        }
    }
}
