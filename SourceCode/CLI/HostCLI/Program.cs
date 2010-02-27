using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostCLI
{
    class Program : See3PO.UI
    {


        static void Main(string[] args)
        {
            Host host = new Host();
            Console.WriteLine("command:");
            Console.Write(">");
            String input;
            host.connect();
            while ((input = (Console.ReadLine().ToLower())) != null)
            {
                if (input == "listen")
                {
                    host.connect();
                }
                else if (input == "stop")
                {
                    host.connect();
                }
                else if (input == "disconnect")
                {
                    host.connect();
                }
                else if (input[0] == '-' || char.IsNumber(input[0]))
                {
                    host.move(input);
                }
                else if (input == "location")
                {
                    Console.WriteLine(host.getLocation());
                }
                else if (input == "facing")
                {
                    Console.WriteLine(host.getLocation());
                }
                else if (input == "exit")
                {
                    return;
                }
                Console.Write(">");
            }
        }
    }
}
