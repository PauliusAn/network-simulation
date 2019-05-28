using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkSimulation.Models;

namespace NetworkSimulation.Views
{
    class MainConsole
    {
        public MainConsole()
        {

        }

        public int PrintMenu()
        {
            Console.WriteLine("1. Add router");
            Console.WriteLine("2. Add edge");
            Console.WriteLine("3. Print all the routers");
            Console.WriteLine("4. View router's routing table");
            Console.WriteLine("5. Remove router");
            Console.WriteLine("6. Send message");
            Console.WriteLine();
            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }

        public string ChooseRouter()
        {
            int i = 0;
            Dictionary<int, string> routersDict = new Dictionary<int, string>();
            foreach(var a in MainNetwork.routers)
            {
                System.Console.WriteLine(i + ". " + a.Id);
                routersDict.Add(i, a.Id);
                i++;
            }
            System.Console.Write("Choose the number of the router: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            return routersDict[choice];
        }

        public void PrintAllRouters()
        {
            int i = 0;
            foreach(var a in MainNetwork.routers)
            {
                Console.WriteLine(i +". " + a.Id);
                i++;
            }
            System.Console.WriteLine();
        }

        public string GetNewRouterName()
        {
            Console.WriteLine("Name of the new router: ");
            return Console.ReadLine();
        }
    }
}
