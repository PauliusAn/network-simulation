using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSimulation.Models
{
    public class Router
    {
        public string Id { get; set; }
        public List<Router> neighbours;
        public Dictionary<String, Path> pathes;
        public Boolean isWorking = false;

        public Router(string id)
        {
            neighbours = new List<Router>();
            pathes = new Dictionary<string, Path>();
            this.Id = id;
        }

        public void AddEdge(Router newNeighbour)
        {
            if(newNeighbour == this)
            {
                return;
            }
            neighbours.Add(newNeighbour);
            try
            {
                if (!newNeighbour.neighbours.Contains(this))
                    {
                        newNeighbour.AddEdge(this);
                    }
                Path a = new Path(newNeighbour.Id, newNeighbour.Id, 1);
                if (!pathes.ContainsKey(a.destination))
                {
                    pathes.Add(a.destination, a);
                }
                else
                {
                    pathes[newNeighbour.Id].nextRouter = newNeighbour.Id;
                    pathes[newNeighbour.Id].hopsAmount = 1;
                }
            }
            catch (Exception)
            {
                return;
            }
            
            //TO DO: PERSKAICIUOTI SHORTEST PATHES
        }

        public void RefreshPathes(Dictionary<String, Path> pathes, string tableSource)
        {
            foreach(var a in pathes)
            {
                if (!this.pathes.ContainsKey(a.Key) && a.Key != this.Id && MainNetwork.GetRouter(a.Key) != null && MainNetwork.GetRouter(a.Value.nextRouter) != null && a.Value.nextRouter != this.Id && a.Value.hopsAmount + 1 < 16)
                {
                    this.pathes.Add(a.Key, new Path(a.Key, tableSource, a.Value.hopsAmount + 1));
                }
                else
                {
                    if(this.pathes.ContainsKey(a.Key) && this.pathes[a.Key].hopsAmount > a.Value.hopsAmount + 1 && a.Value.hopsAmount + 1 < 16)
                    {
                        this.pathes[a.Key] = new Path(a.Key, tableSource, a.Value.hopsAmount + 1);
                    }
                    if (MainNetwork.GetRouter(a.Value.destination) == null || MainNetwork.GetRouter(a.Value.nextRouter) == null)
                    {
                        this.pathes.Remove(a.Key);
                    }
                }
            }

            foreach(var a in this.pathes.ToList())
            {
                //Patikriname ar kaimynai nera isjungti
                if(MainNetwork.GetRouter(a.Key) == null || MainNetwork.GetRouter(a.Value.nextRouter) == null)
                {
                    this.pathes.Remove(a.Key);
                }
            }
        }

        public void SendTable()
        {
            foreach(var a in neighbours)
            {
                a.RefreshPathes(this.pathes, this.Id);
            }
        }

        public void SendMessage(Message message)
        {
            if(message.Destination == this.Id && message.text != "message received")
            {
                System.Console.WriteLine(this.Id + " got the message from " + message.Source + ": " + message.text);
                foreach(var a in neighbours)
                {
                    if(a.Id == pathes[message.Source].nextRouter)
                    {
                        a.SendMessage(new Message(this.Id, message.Source, "message received"));
                    }
                }
            }

            else if(message.Destination != this.Id)
            {
                try
                {
                    foreach(var a in neighbours)
                    {
                        if(a.Id == pathes[message.Destination].nextRouter)
                        {
                            if(message.text != "message received")
                            {
                                System.Console.WriteLine(this.Id + " forwards message to " + a.Id);
                            }
                            a.SendMessage(message);
                        }
                    }
                }
                catch(Exception)
                {
                    return;
                }
            }
        }
    }
}
