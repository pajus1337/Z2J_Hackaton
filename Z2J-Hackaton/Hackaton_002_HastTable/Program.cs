using System.Text;

namespace Hackaton_002_HastTable
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public class DummyObject
    {
        public string Key { get; private set; }
        public string Value { get; set; }

        public DummyObject()
        {

        }
    }

    public class DummyObjectKeyGenerator
    {
        public string EmailDomainName { get; init; }
        public int MyProperty { get; set; }
        Random random = new Random();

        public DummyObjectKeyGenerator()
        {

        }

        public string GenerateKey()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < 5; i++)
            {
                stringBuilder.Append((char)random.Next(97, 123));
            }

            return stringBuilder.ToString() + EmailDomainName;
        }
    }
}


