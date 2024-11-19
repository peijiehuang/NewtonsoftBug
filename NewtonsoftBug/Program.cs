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

            //还没序列化前打印出来的数据类型还是正确的
            foreach (var item in data1)
            {
                Console.WriteLine("data1:" + item.Value.GetType());
            }
            Console.WriteLine();
            Console.WriteLine();

            //最多只能推断到数据类型是object，而不是继续推断下去，把底层的数据类型记录起来
            var jsonStr = JsonConvert.SerializeObject(data1, SerializerSettings);

            Console.WriteLine(jsonStr);
            Console.WriteLine();
            Console.WriteLine();

            //反序列化
            var data2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr, SerializerSettings);

            //反序列化后数据类型已经提升了，int16 > int32   、int128更过分直接成了字符串
            foreach (var item in data2)
            {
                Console.WriteLine("data2:" + item.Value.GetType());
            }

        }
    }
}
