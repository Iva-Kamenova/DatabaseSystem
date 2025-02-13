using DatabaseProject.MyUtils;

namespace DatabaseProject.Conditions
{
    public class SimpleCondition : ICondition
    {
        private readonly string _column;
        private readonly string _operator;
        private readonly string _value;

        public SimpleCondition(string column, string op, string value)
        {
            _column = column;
            _operator = op;
            _value = value;
        }

        public bool Evaluate(MyList<string> record, MyList<string> columns)
        {
            int columnIndex = columns.IndexOf(_column);
            string cellValue = record[columnIndex];
            return EvaluateCondition(cellValue, _operator, _value);
        }

        public static bool EvaluateCondition(string cellValue, string op, string value)
        {
            if (int.TryParse(cellValue, out int intCellValue) && int.TryParse(value, out int intValue))
            {
                return op switch
                {
                    ">" => intCellValue > intValue,
                    "<" => intCellValue < intValue,
                    "=" => intCellValue == intValue,
                    "<>" => intCellValue != intValue,
                    _ => throw new Exception("Invalid numeric comparison operator.")
                };
            }

            if (DateTime.TryParse(cellValue, out DateTime dateCellValue) && DateTime.TryParse(value, out DateTime dateValue))
            {
                return op switch
                {
                    ">" => dateCellValue > dateValue,
                    "<" => dateCellValue < dateValue,
                    "=" => dateCellValue == dateValue,
                    "<>" => dateCellValue != dateValue,
                    _ => throw new Exception("Invalid date comparison operator.")
                };
            }

            return op switch
            {
                ">" => MyString.Compare(cellValue, value) > 0,
                "<" => MyString.Compare(cellValue, value) < 0,
                "=" => MyString.Compare(cellValue, value) == 0,
                "<>" => MyString.Compare(cellValue, value) != 0,
                _ => throw new Exception("Invalid text comparison operator.")
            };
        }
    }
}
