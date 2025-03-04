using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawbridgeSimulator.Models
{
    internal class Veicolo
    {
        // quando si clicca il tasto le macchine si muovono
        private int Id { get; set; }
        public Veicolo(int id)
        {
            Id = id;
        }
        public override string ToString()
        {
            return $"Veicolo-{Id}";
        }
    }
}
