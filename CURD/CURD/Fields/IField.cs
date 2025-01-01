namespace CURD.Fields
{
    internal interface IField<T>
    {
        public void SetLabel(string label);
        public T GetValue();
        public void SetValue(T value);
    }
}
