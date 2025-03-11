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
        private const int NrAutoSulPonte = 3;
        private SemaphoreSlim sSlim;
        private readonly int width = Console.WindowWidth;
        private Task[] tasks = new Task[2];
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
                Console.WriteLine(Bridge.CreaPonteSotto());

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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(VeicoliDx[i].ToString());
                    Console.ForegroundColor = ConsoleColor.White;
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
                        AttraversaDxSx();
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

        public void Passaggio()
        {
            List<string> autos = new List<string>();
            int count = 0;
            do
            {
                autos.Add(VeicoliDx[count].ToString());
                count++;
            } while (autos.Count < NrAutoSulPonte || autos.Count != VeicoliDx.Count);
            for(int i = 0; i < 32; i++)
            {

            }
        }

        public async Task AttraversaDxSx()
        {
            //Prima parte in un metodo secondario(passaggio) dove poi chiamo per il numero di auto il task con questo metodo
            // SetCursorPosition per rappresentare l'attraversamento
            if (VeicoliDx != null)
            {
                List<string> autos = new List<string>();
                int count = 0; 
                do
                {
                    autos.Add(VeicoliDx[count].ToString());
                    count++;
                } while (autos.Count < NrAutoSulPonte || autos.Count != VeicoliDx.Count);
                int pos = 27;
                //seconda parte
                for (int j = 0; j < 32; j++)
                {
                    //questo primo for non ci va
                    for (int i = 0; i < NrAutoSulPonte; i++)
                    {
                        if (j == 0)
                        {
                            //da mettere nel metodo passaggio
                            Console.SetCursorPosition(width - pos, i + 13);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(VeicoliDx?[i].ToString());
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(width - pos - 5, i + 13);
                            Console.WriteLine(" ");
                            Console.SetCursorPosition(width - pos, i + 13);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(VeicoliDx?[i].ToString());
                        }
                        else
                        {
                           
                            Console.SetCursorPosition(width - pos, i + 13);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(VeicoliDx?[i].ToString());
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                    }
                    pos+=5;
                }
            }
        }

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
