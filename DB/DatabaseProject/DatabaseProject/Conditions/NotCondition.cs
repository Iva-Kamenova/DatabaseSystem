using DatabaseProject.MyUtils;

namespace DatabaseProject.Conditions
{
    public class NotCondition : ICondition
    {
        private readonly ICondition _condition;

        public NotCondition(ICondition condition)
        {
            _condition = condition;
        }

        public bool Evaluate(MyList<string> record, MyList<string> columns)
        {
            return !_condition.Evaluate(record, columns);
        }
    }
}
