using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawbridgeSimulator.Models
{
    internal class Ponte
    {
        public Ponte()
        {
        }

        // Scrive la prima riga (quella superiore) del ponte
        public string CreaPonteSopra(int nrCar)
        {
            // nrCar indica quante macchine possono passare contemporanemante nel ponte

            StringBuilder sb = new StringBuilder();

            
            for(int i = 0; i < 30; i++)
            {
                sb.Append("-");   
            }

            // Crea lo spazio nel ponte per far passare le macchine
            for (int j = 0; j < nrCar; j++) 
            {
                sb.AppendLine("\n");
            }

            return sb.ToString();
        }

        public string CreaPonteSotto(int nrCar) 
        {
            StringBuilder sb = new StringBuilder();

            // Scrive la seconda riga (quella inferiore) del ponte
            for (int k = 0; k < 30; k++)
            {
                sb.Append("-");
            }
            return sb.ToString();
        }
    }
}
