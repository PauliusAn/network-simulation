using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSimulation.Models
{
    static public class MainNetwork
    {
        static public List<Router> routers = new List<Router>();
        
        static public void UpdatePathes()
        {
            Router defaultRaut = routers[0];
            for(int i = 0; i < routers.Count - 1; i++)
            {

            }
        }

        static public Router GetRouter(string routerId)
        {
            foreach(var a in routers)
            {
                if(a.Id == routerId)
                {
                    return a;
                }
            }
            return null;
        }
    }
}
