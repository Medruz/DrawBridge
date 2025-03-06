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
        private const int NrAutoSulPonte = 4;
        private SemaphoreSlim sSlim;
        private readonly int width = Console.WindowWidth;
        public GestioneTraffico()
        {
            VeicoliDx = new List<Veicolo>();
            VeicoliSx = new List<Veicolo>();
            Bridge = new Ponte();
            sSlim = new SemaphoreSlim(NrAutoSulPonte);
        }

        
        public void Gestione()
        {
            //Thread tDx = new Thread(AttraversaDxSx);
            ConsoleKeyInfo lettera;
            bool fine = false;
            //int height = Console.WindowHeight;
            do
            {
                // Console di controllo 
                Console.SetCursorPosition(width/2, 1);
                Console.WriteLine("Controllo dell'attraversamento ponte");

                Acqua(4);

                // Stampo il ponte
                Console.SetCursorPosition(width / 2, 12);
                Console.WriteLine(Bridge.CreaPonteSopra(4));
                Console.SetCursorPosition(width / 2, 16);
                Console.WriteLine(Bridge.CreaPonteSotto(4));

                Acqua(17);

                // Scrivo le auto a sx
                for (int i = 0; i < VeicoliSx?.Count; i++)
                {
                    Console.SetCursorPosition(5,i+4);
                    Console.WriteLine(VeicoliSx[i].ToString());
                }

                // Scrivo le auto a dx
                for (int i = 0; i < VeicoliDx?.Count; i++)
                {
                    Console.SetCursorPosition(width-9, i + 4);
                    Console.WriteLine(VeicoliDx[i].ToString());
                }

                // Legge la pressione del tasto (senza dover premere Enter)
                //ConsoleKeyInfo tasto = Console.ReadKey(true); 'true' per non mostrare il tasto premuto


                // Prendo in ingresso la lettera di comando
                Console.SetCursorPosition(5, 25);
                lettera = Console.ReadKey(true);
                
                switch (lettera.Key)
                {
                    // Aggiunge una macchina alla lista sinsitra
                    case ConsoleKey.L:
                        VeicoliSx?.Add(new Veicolo(VeicoliSx.Count));
                        break;

                    // Aggiunge una macchina alla lista sinsitra
                    case ConsoleKey.R:
                        VeicoliDx?.Add(new Veicolo(VeicoliDx.Count));
                        break;

                    // Avvia il movimento delle macchine
                    case ConsoleKey.P:
                        break;

                    // Killa il programma
                    case ConsoleKey.E:
                        fine = true;
                        break;

                    // Se la lettera selezionata non ha nessun comando, non fa niente
                    default:
                        break;
                }
            } while (fine == false);  
        }

        //public string AttraversaDxSx()
        //{
        //    // SetCursorPosition per rappresentare l'attraversamento
        //    for (int i = 0; i < VeicoliDx.Count; i++)
        //    {
                
        //    }
        //}
        
        public void Acqua(int startRow)
        {
            for (int k = 0; k < 9; k++)
            {
                Console.SetCursorPosition(width / 2, k + startRow);
                for (int j = 0; j < 30; j++)
                {
                    Console.Write("s");
                }
            }
        }
    }
}
