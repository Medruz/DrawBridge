using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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
        private List<Task> tasks;
        public GestioneTraffico()
        {
            VeicoliDx = new List<Veicolo>();
            VeicoliSx = new List<Veicolo>();
            Bridge = new Ponte();
            sSlim = new SemaphoreSlim(NrAutoSulPonte);
            tasks = new List<Task>();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(VeicoliSx[i].ToString());
                    Console.ForegroundColor = ConsoleColor.White;
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
                        Passaggio();
                        Console.ForegroundColor = ConsoleColor.White;
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

        public async void Passaggio()
        {
            //<string> autos = new List<string>();
            //int count = 0;
            int pos;

            //do
            //{
            //    autos.Add(VeicoliDx[count].ToString());
            //    count++;
            //} while (autos.Count < NrAutoSulPonte && autos.Count != VeicoliDx.Count);


            //creo una lista di task per ogni macchina a cui affido il metodo di attraversamento
            //con il semaforeslim controllo quando i task agiscono
            //quando clicco il tasto P tutti i veicoli aggiunti devono attraversare il ponte, prima dx poi sx
            do
            {
                if (VeicoliDx?.Count != 0)
                {
                    pos = 27;
                    for (int i = 0; i < NrAutoSulPonte; i++)
                    {
                        tasks.Add(AttraversaDxSx(i, pos));
                    }
                    for (int i = 0; i < 9; i++)
                    {
                        for(int k = 0; k < tasks.Count; k++)
                        {
                            await sSlim.WaitAsync();
                            try
                            {
                                // Esegue il task asincrono
                                await AttraversaDxSx(k, pos);
                            }
                            finally
                            {
                                // Rilascia il semaforo per consentire ad altri task di eseguire
                                sSlim.Release();
                            }
                        }
                        
                        pos += 5;
                        Thread.Sleep(200);
                    }
                    VeicoliDx.Remove(VeicoliDx[0]);
                    VeicoliDx.Remove(VeicoliDx[0]);
                    VeicoliDx.Remove(VeicoliDx[0]);
                    for(int j = 0; j < NrAutoSulPonte; j++)
                    {
                        Console.SetCursorPosition(width - 72, j+13);
                        Console.WriteLine("              ");
                    }
                }
                if (VeicoliSx?.Count != 0)
                {
                    pos = 20;
                    for (int i = 0; i < 9; i++)
                    {
                        AttraversaSxDx(pos);
                        pos += 5;
                        Thread.Sleep(200);
                    }
                }
            } while (VeicoliDx.Count != 0 || VeicoliSx.Count != 0);
        }

        public async Task AttraversaDxSx(int i, int pos)
        {
            //Prima parte in un metodo secondario(passaggio) dove poi chiamo per il numero di auto il task con questo metodo
            // SetCursorPosition per rappresentare l'attraversamento
            
            //List<string> autos = new List<string>();
            //int count = 0; 
            //do
            //{
            //    autos.Add(VeicoliDx[count].ToString());
            //    count++;
            //} while (autos.Count < NrAutoSulPonte || autos.Count != VeicoliDx.Count);
            //int pos = 27;
            //seconda parte
            //questo primo for non ci va
            //for (int i = 0; i < NrAutoSulPonte; i++)
            //{

                //da mettere nel metodo passaggio
                Console.SetCursorPosition(width - (pos - 5), i + 13);
                Console.WriteLine("          ");
                Console.SetCursorPosition(width - pos, i + 13);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(VeicoliDx?[i].ToString());
                Console.ForegroundColor = ConsoleColor.White;
                //Console.SetCursorPosition(width - pos, i + 13);
                //Console.ForegroundColor = ConsoleColor.Green;
                //Console.WriteLine(VeicoliDx?[i].ToString());
                //Console.ForegroundColor = ConsoleColor.White;
            //}
            //await Task.CompletedTask;
        }

        public async Task AttraversaSxDx(int pos)
        {
            for (int i = 0; i < NrAutoSulPonte; i++)
            {
                Console.SetCursorPosition(width - (pos - 5), i + 13);
                Console.WriteLine("          ");
                Console.SetCursorPosition(width - pos, i + 13);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(VeicoliDx?[i].ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }
            await Task.CompletedTask;
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
