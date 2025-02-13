using DatabaseProject.MyUtils;

namespace DatabaseProject.Conditions
{
    public interface ICondition
    {
        bool Evaluate(MyList<string> record, MyList<string> columns);
    }
}
