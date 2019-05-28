using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSimulation.Models
{
    public class Path
    {
        public string destination;
        public string nextRouter;
        public int hopsAmount;

        public Path(string destination, string nextRouter, int hopsAmount)
        {
            this.destination = destination;
            this.nextRouter = nextRouter;
            this.hopsAmount = hopsAmount;
        }
    }
}
