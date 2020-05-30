namespace NetBDI.STRIPS
{
    public class ValueParameter : IParameter
    {
        public object Value { get; }

        public ValueParameter(object obj)
        {
            Value = obj;
        }

        public override string ToString() => Value.ToString();
    }
}