using DatabaseProject.MyUtils;

namespace DatabaseProject.Conditions
{
    public static class WhereClause
    {
        public static MyList<MyList<string>> Where(string condition, string table)
        {
            string dataFilePath = Database.GetTableDataFilePath(table);
            string metaFilePath = Database.GetTableMetaFilePath(table);

            var columns = Database.GetColumnsFromMetaFile(metaFilePath);
            MyList<MyList<string>> records = Database.LoadDataFromFile(dataFilePath);

            ICondition rootCondition = ParseCondition(condition, columns);

            var filteredRecords = new MyList<MyList<string>>();
            foreach (var record in records)
            {
                if (rootCondition.Evaluate(record, columns))
                {
                    filteredRecords.Add(record);
                }
            }

            return filteredRecords;
        }

        private static ICondition ParseCondition(string condition, MyList<string> columns)
        {
            
            condition = MyString.Trim(condition);

            
            if (MyString.IndexOf(condition,"AND") != -1)
            {
                var parts = MyString.Split(condition, "AND");
                ICondition result = ParseCondition(MyString.Trim(parts[0]), columns);

                for (int i = 1; i < parts.Length; i++)
                {
                    result = new AndCondition(result, ParseCondition(MyString.Trim(parts[i]), columns));
                }

                return result;
            }

            
            if (MyString.IndexOf(condition, "OR") != -1)
            {
                var parts = MyString.Split(condition, "OR");
                ICondition result = ParseCondition(MyString.Trim(parts[0]), columns);

                for (int i = 1; i < parts.Length; i++)
                {
                    result = new OrCondition(result, ParseCondition(MyString.Trim(parts[i]), columns));
                }

                return result;
            }

           
            if (MyString.IndexOf(condition, "NOT ") == 0)
            {
                return new NotCondition(ParseCondition(MyString.Trim(MyString.SubstringNew(condition,4)), columns));
            }

            
            var tokens = MyString.Split(condition, ';');
            if (tokens.Length != 3)
            {
                throw new Exception("Invalid WHERE clause.");
            }

            return new SimpleCondition(
                MyString.Trim(tokens[0]),
                MyString.Trim(tokens[1]),
                MyString.Trim(tokens[2])
            );
        }

    }
}

