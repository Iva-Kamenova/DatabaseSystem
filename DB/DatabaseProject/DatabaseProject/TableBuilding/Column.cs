namespace DatabaseProject.TableBuilding
{
    public class Column
    {
        public string Name { get; set; }
        public ColumnType Type { get; set; }
        public object? DefaultValue { get; set; }

        public Column(string name, ColumnType type, object? defaultValue = null)
        {
            Name = name;
            Type = type;
            DefaultValue = defaultValue;
        }
    }
}
