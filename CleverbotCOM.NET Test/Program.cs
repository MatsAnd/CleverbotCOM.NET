using CleverbotCOM.NET;
using System;

namespace CleverbotCOM.NET_Test
{
    class Program
    {
        private const string ApiKey = "YOUR_APIKEY_HERE";

        static void Main(string[] args)
        {
            var cleverbot1 = new Cleverbot(ApiKey);
            var cleverbot2 = new Cleverbot(ApiKey);

            var message = "Hello. Im sorry.";  // Our start phrase.
            var requests = 0;
            while (requests < 100)
            {
                message = cleverbot1.Ask(message);
                Console.WriteLine("Cleverbot #1:  " + message);

                message = cleverbot2.Ask(message);
                Console.WriteLine("Cleverbot #2:  " + message);

                requests = requests + 2;
            }

            Console.Write("\n\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
