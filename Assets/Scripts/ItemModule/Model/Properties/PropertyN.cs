namespace ItemModule
{
    public class PropertyN : IPropertyN
    {
        private int _n;

        public int N => _n;
        
        public PropertyN(int n)
        {
            _n = n;
        }
    }
}