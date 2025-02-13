using DatabaseProject.MyUtils;

namespace DatabaseProject.Conditions
{
    public class OrCondition : ICondition
    {
        private readonly ICondition _left;
        private readonly ICondition _right;

        public OrCondition(ICondition left, ICondition right)
        {
            _left = left;
            _right = right;
        }

        public bool Evaluate(MyList<string> record, MyList<string> columns)
        {
            return _left.Evaluate(record, columns) || _right.Evaluate(record, columns);
        }
    }
}

