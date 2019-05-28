using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSimulation.Models
{
    public class Message
    {
        public string Source;
        public string Destination;
        public string text;

        public Message(string source, string Destination, string text)
        {
            this.Source = source;
            this.Destination = Destination;
            this.text = text;
        }
    }
}
