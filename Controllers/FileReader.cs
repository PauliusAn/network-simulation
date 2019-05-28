using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkSimulation.Models;

namespace NetworkSimulation.Controllers
{
    class FileReader
    {
        private readonly string file = @"C:\Users\Paulius\source\repos\NetworkSimulation\DefaultNetwork.txt";
        private List<Router> routerList;

        public List<Router> ReadRouters()
        {
            string text = System.IO.File.ReadAllText(file);
            string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            string[] routerData;
            routerList = new List<Router>();

            foreach(var a in lines)
            {
                routerData = null;
                routerData = a.Split(' ');
                routerList.Add(new Router(routerData[0]));
            }
            int i = 0;
            foreach(var a in lines)
            {
                routerData = null;
                routerData = a.Split(' ');
                foreach(var b in routerData)
                {
                    routerList[i].AddEdge(GetByID(b));
                }
                i++;
            }
            return routerList;
        }

        private Router GetByID(string id)
        {
            foreach(var a in routerList)
            {
                if(a.Id == id)
                {
                    return a;
                }
            }
            return null;
        }
    }
}
