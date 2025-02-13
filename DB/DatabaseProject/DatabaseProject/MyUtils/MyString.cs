using DatabaseProject.TableBuilding;

namespace DatabaseProject.MyUtils
{
    public static class MyString
    {
        public static string DirectoryConcat(string part1, string part2)
        {
            var separator = Path.DirectorySeparatorChar;

            if (part1[^1] == separator)
            {
                return part1 + part2;
            }

            return part1 + separator + part2;
        }

        public static string Trim(string input)
        {
            int startIndex = 0;
            int endIndex = input.Length - 1;

            while (startIndex <= endIndex && (input[startIndex] == ' ' || input[startIndex] == '\t'))
            {
                startIndex++;
            }

            while (endIndex >= startIndex && (input[endIndex] == ' ' || input[endIndex] == '\t'))
            {
                endIndex--;
            }

            string result = "";
            for (int i = startIndex; i <= endIndex; i++)
            {
                result += input[i];
            }

            return result;
        }
        public static int IndexOf(string source, string toFind, int start)
        {
            for (int i = start; i <= source.Length - toFind.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < toFind.Length; j++)
                {
                    if (source[i + j] != toFind[j])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    return i;
                }
            }

            return -1;
        }
        public static int IndexOf(string source, string toFind)
        {
            for (int i = 0; i <= source.Length - toFind.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < toFind.Length; j++)
                {
                    if (source[i + j] != toFind[j])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    return i;
                }
            }
            return -1;
        }

        public static string Substring(string source, int startIndex, int endIndex)
        {
                string result = "";
                for (int i = startIndex; i <= endIndex; i++)
                {
                    result += source[i];
                }
                return result;
        }

        public static string SubstringNew(string source, int startIndex, int length = -1)
        {
            if (length == -1)
            {
                length = source.Length - startIndex;
            }

            var result = "";
            for (int i = startIndex; i < startIndex + length; i++)
            {
                result += source[i];
            }
            return result;
        }

        public static string[] Split(string source, char delimiter)
        {
            int partCount = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == delimiter)
                {
                    partCount++;
                }
            }


            string[] parts = new string[partCount + 1];
            int currentPartIndex = 0;
            int startIndex = 0;


            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == delimiter)
                {
                    parts[currentPartIndex] = Substring(source, startIndex, i - 1);
                    currentPartIndex++;
                    startIndex = i + 1;
                }
            }

            parts[currentPartIndex] = Substring(source, startIndex, source.Length - 1);

            return parts;
        }

        public static string[] Split(string input, string separator)
        {
            
            var result = new MyList<string>();
            int start = 0;

            while (start < input.Length)
            {
                int index = IndexOf(input, separator, start);

                if (index == -1)
                {
                    result.Add(SubstringNew(input, start));
                    break;
                }

                result.Add(SubstringNew(input, start, index - start));
                start = index + separator.Length;
            }

            return result.ToArray();
        }


        public static string Join(string[] values, char separator)
        {
            string result = values[0];


            for (int i = 1; i < values.Length; i++)
            {
                result += separator + values[i];
            }

            return result;
        }

        public static int ConvertToInteger(string value)
        {
            int result = 0;
            bool isNegative = false;
            int startIndex = 0;

            if (value[0] == '-')
            {
                isNegative = true;
                startIndex = 1;
            }


            for (int i = startIndex; i < value.Length; i++)
            {
                result = result * 10 + (value[i] - '0');
            }

            return isNegative ? -result : result;
        }

        public static ColumnType ConvertStringToEnum(string value)
        {
            if (value == "Int")
            {
                return ColumnType.Int;
            }
            else if (value == "Date")
            {
                return ColumnType.Date;
            }
            else if (value == "String")
            {
                return ColumnType.String;
            }
            return ColumnType.Unknown;
        }

        public static string ConvertEnumToString(ColumnType columnType)
        {
            switch (columnType)
            {
                case ColumnType.Int:
                    return "Int";
                case ColumnType.Date:
                    return "Date";
                case ColumnType.String:
                    return "String";
                case ColumnType.Unknown:
                    return "Unknown";
                default:
                    return "Unknown";
            }
        }
        public static int Compare(string str1, string str2)
        {
            if (str1 == null && str2 == null)
                return 0;
            if (str1 == null)
                return -1;
            if (str2 == null)
                return 1;

            int length = (str1.Length < str2.Length) ? str1.Length : str2.Length;

            for (int i = 0; i < length; i++)
            {
                if (str1[i] != str2[i])
                    return str1[i] - str2[i];
            }

            return str1.Length - str2.Length;
        }
    }
}
