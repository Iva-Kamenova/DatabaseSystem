using DatabaseProject.TableBuilding;

namespace DatabaseProject.MyUtils
{
    public static class MyValidation
    {
        public static bool IsNullOrEmpty(string value)
        {
            return (value == null || value.Length == 0);
        }
        private static bool IsInteger(string value)
        {
            if (IsNullOrEmpty(value))
            {
                return false;
            }

            int startIndex = 0;


            if (value[0] == '-')
            {
                startIndex = 1;
            }


            for (int i = startIndex; i < value.Length; i++)
            {
                if (value[i] < '0' || value[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsDate(string value)
        {
            if (IsNullOrEmpty(value))
            {
                return false;
            }


            int day = 0, month = 0, year = 0;
            int dotCount = 0;
            string currentPart = "";


            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '.')
                {
                    dotCount++;
                    if (dotCount > 2)
                    {
                        return false;
                    }


                    if (!IsInteger(currentPart))
                    {
                        return false;
                    }


                    if (dotCount == 1) day = MyString.ConvertToInteger(currentPart);
                    else if (dotCount == 2) month = MyString.ConvertToInteger(currentPart);

                    currentPart = "";
                }
                else
                {
                    currentPart += value[i];
                }
            }


            if (!IsInteger(currentPart))
            {
                return false;
            }
            year = MyString.ConvertToInteger(currentPart);


            if (month < 1 || month > 12)
            {
                return false;
            }


            if (day < 1 || day > DaysInMonth(month, year))
            {
                return false;
            }

            return true;
        }

        private static int DaysInMonth(int month, int year)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                case 2:
                    if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
                    {
                        return 29;
                    }
                    return 28;
                default:
                    return 0;
            }
        }

        private static bool ValidateValueType(ColumnType columnType1, string value)
        {
            string columnType = MyString.ConvertEnumToString(columnType1);

            if (MyValidation.IsNullOrEmpty(value))
            {
                return false;
            }

            switch (columnType)
            {
                case "Int":
                    return MyValidation.IsInteger(value);
                case "String":
                    return true;
                case "Date":
                    return MyValidation.IsDate(value);
                default:
                    return false;
            }
        }
        public static bool ValidateData(string tableName, string[] columns, string[] values)
        {
                string metaFile = Database.GetTableMetaFilePath(tableName);
                var columnMetaData = Database.LoadTableColumns(tableName);

                for (int i = 0; i < columns.Length; i++)
                {
                    int columnIndex = -1;

                    for (int j = 0; j < columnMetaData.Count; j++)
                    {
                        if (columnMetaData[j].Name == MyString.Trim(columns[i]))
                        {
                            columnIndex = j;
                            break;
                        }
                    }

                    if (columnIndex == -1)
                    {
                        throw new Exception($"The column '{columns[i]}' doesn't exist in '{tableName}'.");
                    }

                    if (!ValidateValueType(columnMetaData[columnIndex].Type, MyString.Trim(values[i])))
                    {
                        throw new Exception($"The value '{values[i]}' is invalid for column '{columns[i]}' with type {columnMetaData[columnIndex].Type}.");
                    }
            }
            return true;
        }
    }
}
