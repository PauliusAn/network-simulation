using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkSimulation.Controllers;

namespace NetworkSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            MainController mainController = new MainController();
            mainController.StartSimulation();
        }
    }
}
