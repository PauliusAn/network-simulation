using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetworkSimulation.Models;
using NetworkSimulation.Views;

namespace NetworkSimulation.Controllers
{
    class MainController
    {
        private MainConsole mainConsole;

        public MainController()
        {
            mainConsole = new MainConsole();
        }

        public void StartSimulation()
        {
            FileReader fileReader = new FileReader();
            MainNetwork.routers = fileReader.ReadRouters();
            //Pradedama marsrutu apsikeitimo tarp routeriu gija.
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    foreach (var a in MainNetwork.routers)
                    {
                        foreach (var b in a.neighbours)
                        {
                            a.RefreshPathes(b.pathes, b.Id);
                        }
                    }
                    Thread.Sleep(1000);
                }
            }).Start();

            int choice;
            while (true)
            {
                choice = mainConsole.PrintMenu();
                switch (choice)
                {
                    //1. Add router
                    case 1:
                        AddNewRouter(mainConsole.GetNewRouterName());
                        break;
                    //2. Add edge
                    case 2:
                        AddEdge();
                        break;
                    //3. Print all the routers
                    case 3:
                        mainConsole.PrintAllRouters();
                        break;
                    //4. View router's routing table
                    case 4:
                        PrintRoutingTable();
                        break;
                    //5. Remove router
                    case 5:
                        RemoveRouter();
                        break;
                    //6. Send message
                    case 6:
                        SendMessage();
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        mainConsole.PrintMenu();
                        break;
                }
            }
        }

        public void AddNewRouter(string name)
        {
            MainNetwork.routers.Add(new Router(name));
        }

        public void AddEdge()
        {
            string routerOne = mainConsole.ChooseRouter();
            string routerTwo = mainConsole.ChooseRouter();
            Router routOne = null, routTwo = null;

            foreach(var a in MainNetwork.routers)
            {
                if(a.Id == routerOne)
                {
                    routOne = a;
                }
                if(a.Id == routerTwo)
                {
                    routTwo = a;
                }
            }
            
            if(routOne != null && routTwo != null)
            {
                routOne.AddEdge(routTwo);
            }
        }

        public void PrintRoutingTable()
        {
            Router router = MainNetwork.GetRouter(mainConsole.ChooseRouter());
            foreach(var a in router.pathes.Values)
            {
                Console.WriteLine("Source: " + router.Id + "  Destination: " + a.destination + "  Hops Count: " + a.hopsAmount + "  Next Router: " + a.nextRouter);
            }
            Console.WriteLine();
        }

        public void RemoveRouter()
        {
            Router router = MainNetwork.GetRouter(mainConsole.ChooseRouter());
            MainNetwork.routers.Remove(router);
            router = null;
        }

        public void SendMessage()
        {
            string routerOne = mainConsole.ChooseRouter();
            string routerTwo = mainConsole.ChooseRouter();
            Console.WriteLine("Type the message you want to send: ");
            string text = Console.ReadLine();
            MainNetwork.GetRouter(routerOne).SendMessage(new Message(routerOne, routerTwo, text));
        }
    }
}
