using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DrawbridgeSimulator.Models
{
    internal class GestioneTraffico
    {
        private List<Veicolo>? VeicoliDx { get; set; }
        private List<Veicolo>? VeicoliSx { get; set; }
        private Ponte Bridge { get; set; }
        public GestioneTraffico()
        {
            VeicoliDx = new List<Veicolo>();
            VeicoliSx = new List<Veicolo>();
            Bridge = new Ponte();
        }

        public void Gestione()
        {
            string lettera = "";
            bool fine = false;
            int width = Console.WindowWidth;
            //int height = Console.WindowHeight;
            do
            {
                // Console di controllo 
                Console.SetCursorPosition(width/2, 1);
                Console.WriteLine("Controllo dell'attraversamento ponte");

                // Stampo il ponte
                Console.SetCursorPosition(width / 2, 3);
                Console.WriteLine(Bridge.CreaPonte(4));

                // Prendo in ingresso la lettera di comando
                lettera = Console.ReadLine();

                switch (lettera?.ToUpper())
                {
                    // Aggiunge una macchina alla lista sinsitra
                    case "L":
                        VeicoliSx?.Add(new Veicolo(VeicoliSx.Count));
                        break;

                    // Aggiunge una macchina alla lista sinsitra
                    case "R":
                        VeicoliDx?.Add(new Veicolo(VeicoliDx.Count));
                        break;

                    // Avvia il movimento delle macchine
                    case "P":
                        break;

                    // Killa il programma
                    case "E":
                        fine = true;
                        break;

                    // Se la lettera selezionata non ha nessun comando, non fa niente
                    default:
                        break;
                }




            } while (fine == false);  
        }
        //public string Attraversa()
        //{
        //    // SetCursorPosition per rappresentare l'attraversamento
        //    for(int i = 0; i < VeicoliDx.Count; i++)
        //    {

        //    }
        //}
    }
}
