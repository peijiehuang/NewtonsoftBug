using Newtonsoft.Json;

namespace NewtonsoftBug
{
    internal class Program
    {
        private static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
        };

        static void Main(string[] args)
        {
            var data1 = new Dictionary<string, object>
            {
                { "TEST", (Int16)123 },
                { "TEST2", (Int128)123 },
            };

            foreach (var item in data1)
            {
                Console.WriteLine("data1:" + item.Value.GetType());
            }
            Console.WriteLine();
            Console.WriteLine();

            var jsonStr = JsonConvert.SerializeObject(data1, SerializerSettings);

            Console.WriteLine(jsonStr);
            Console.WriteLine();
            Console.WriteLine();

            var data2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr, SerializerSettings);

            foreach (var item in data2)
            {
                Console.WriteLine("data2:" + item.Value.GetType());
            }

        }
    }
}
