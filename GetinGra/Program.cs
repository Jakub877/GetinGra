using System;
using System.Linq;

class MainClass
{

    public class Globals
    {
        public static int dzien = 1;
        public static bool pokazKoszty = false;
        public static bool pokazStan = false;
    }
    static void Main()
    {

        Console.Write("Nazwa Twojego szpitala: ");
        string nazwaSzpitala = Console.ReadLine();
        Garaz garaz = new Garaz();
        Szpital mojSzpital = new Szpital(nazwaSzpitala);

        while (true)
        {

            string opcja = Menu(mojSzpital);
            int indeksOpcji;
            if(int.TryParse(opcja, out indeksOpcji))
            {
                OpcjaMenu opcjaMenu = (OpcjaMenu)indeksOpcji; 
                if (opcjaMenu == OpcjaMenu.PokazParametrySzpitala)
                {
                    mojSzpital.PokazParametrySzpitala();
                }
                else if (opcjaMenu == OpcjaMenu.MenuWyprawy)
                {
                    MenuWyprawy(mojSzpital, garaz);
                }
                else if (opcjaMenu == OpcjaMenu.ZatrudnijLekarzy)
                {
                    mojSzpital.ZatrudnijLekarzy();
                }

                else if (opcjaMenu == OpcjaMenu.UlepszSzpital)
                {
                    mojSzpital.UlepszSzpital();
                }
                else if (opcjaMenu == OpcjaMenu.MenuGarazu)
                {
                    MenuGarazu(mojSzpital, garaz);
                }

                else if (opcjaMenu == OpcjaMenu.NextDay)
                {
                    //przechodzimy do następnego dnia
                    NextDay(mojSzpital);

                }
                else if (opcjaMenu == OpcjaMenu.MenuUstawien)
                {
                    MenuUstawien();
                }

                else if (opcjaMenu == OpcjaMenu.Koniec)
                {
                    Console.WriteLine("Koniec gry");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowa komenda!");
            }
           
        }
    }


    private static void MenuWyprawy(Szpital mojSzpital,Garaz garaz)
    {
        Console.Clear();
        for (int numer = 0; numer < 4; numer++)
        {
            Console.WriteLine($"{numer + 1}. {((PoziomTrudnosci)numer).ToString()}");
        }
        string opcja = Console.ReadLine();
        int poziom = int.Parse(opcja);

        if (mojSzpital.WybranyPojazd != null)
        {
            garaz.ListaKupionychPojazdow.Remove(mojSzpital.WybranyPojazd);
        }

        Wyprawa wyprawa = new Wyprawa(mojSzpital);
        if (wyprawa.WyruszNaWyprawe(poziom))
        {
            mojSzpital.WybranyPojazd = null;
        }

        if (mojSzpital.WybranyPojazd != null)
        {
            garaz.ListaKupionychPojazdow.Add(mojSzpital.WybranyPojazd);
        }
}

    private static void MenuGarazu(Szpital mojSzpital, Garaz garaz)
    {
        string opcjaGarazu;
        string opcjaDodatkowaGarazu;
        string[] values = { "1", "2", "3", "4", "5", "6", "7" };

        //if (values.Contains(interSubDir))

        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Pokaż menu ofert pojazdów");
            Console.WriteLine("2. Pokaż menu dostępnych pojazdów");
            Console.WriteLine("9. Powrót do głównego menu");

            opcjaGarazu = Console.ReadLine();

            if (opcjaGarazu == "1")
            {
                while (true)
                {
                    Console.Clear();

                    garaz.PokazListePojazdow();
                    Console.WriteLine("9. Powrót");
                    Console.WriteLine("");
                    Console.WriteLine("Wpisz numer opcji:");
                    opcjaGarazu = Console.ReadLine();
                    Console.WriteLine("");
                    if (values.Contains(opcjaGarazu))
                    {
                        while (true)
                        {
                            Console.Clear();
                            //Console.WriteLine("");
                            Console.WriteLine($"Wpisz numer dodatkowej opcji dla pojazdu {garaz.ListaPojazdow.ElementAt(int.Parse(opcjaGarazu) - 1).Nazwa}:");
                            Console.WriteLine("1. Pokaż parametry pojazdu");
                            Console.WriteLine("2. Kup pojazd");
                            Console.WriteLine("9. Anuluj wybór");
                            Console.WriteLine("");
                            opcjaDodatkowaGarazu = Console.ReadLine();

                            if (opcjaDodatkowaGarazu == "1")
                            {
                                Console.Clear();
                                garaz.ListaPojazdow.ElementAt(int.Parse(opcjaGarazu) - 1).PokazParametry();
                                Console.WriteLine("");
                                Console.WriteLine("Wduś dowolny klawisz, żeby kontynuować...");
                                Console.ReadLine();
                            }
                            else if (opcjaDodatkowaGarazu == "2")
                            {
                                garaz.KupPojazd(garaz.ListaPojazdow.ElementAt(int.Parse(opcjaGarazu) - 1), mojSzpital);
                                Console.WriteLine("");
                                Console.WriteLine("Wduś dowolny klawisz, żeby kontynuować...");
                                Console.ReadLine();
                            }
                            else if (opcjaDodatkowaGarazu == "9")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Nieprawidłowa komenda! Wybór wycofany, wduś dowolny klawisz, żeby kontynuować...");
                                Console.ReadLine();
                            }
                        }
                        Console.WriteLine("Wduś dowolny klawisz, żeby kontynuować...");
                        Console.ReadLine();
                    }
                    else if (opcjaGarazu == "9")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowa komenda! Wduś dowolny klawisz, żeby kontynuować...");
                        Console.ReadLine();
                    }
                }

            }
            else if (opcjaGarazu == "2")
            {
                while (true)
                {
                    Console.Clear();

                    if (garaz.PokazListeKupionychPojazdow())
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Wpisz numer opcji:");
                        opcjaGarazu = Console.ReadLine();
                        int indeks = 0;
                        if (int.TryParse(opcjaGarazu, out indeks) == true && indeks <= garaz.ListaKupionychPojazdow.Count() + 1 && indeks >= 1)
                        {
                            while (true)
                            {
                                Console.Clear();
                                //Console.WriteLine("");
                                Console.WriteLine($"Wpisz numer dodatkowej opcji dla pojazdu {garaz.ListaKupionychPojazdow.ElementAt(int.Parse(opcjaGarazu) - 1).Nazwa}:");
                                Console.WriteLine("1. Pokaż parametry pojazdu");
                                if (mojSzpital.WybranyPojazd == null || garaz.ListaKupionychPojazdow.ElementAt(int.Parse(opcjaGarazu) - 1).Nazwa != mojSzpital.WybranyPojazd.Nazwa) //(mojSzpital.WybranyPojazd?.Nazwa ?? "")
                                {
                                    Console.WriteLine("2. Wybierz pojazd");
                                }
                                else
                                {
                                    Console.WriteLine("2. Zdezaktywuj pojazd");
                                }


                                Console.WriteLine("9. Anuluj wybór");
                                Console.WriteLine("");
                                opcjaDodatkowaGarazu = Console.ReadLine();

                                if (opcjaDodatkowaGarazu == "1")
                                {
                                    Console.Clear();
                                    garaz.ListaKupionychPojazdow.ElementAt(int.Parse(opcjaGarazu) - 1).PokazParametry();
                                    Console.WriteLine("");
                                    Console.WriteLine("Wduś dowolny klawisz, żeby kontynuować...");
                                    Console.ReadLine();
                                }
                                else if (opcjaDodatkowaGarazu == "2")
                                {
                                    if (mojSzpital.WybranyPojazd == null || garaz.ListaKupionychPojazdow.ElementAt(int.Parse(opcjaGarazu) - 1).Nazwa != mojSzpital.WybranyPojazd.Nazwa)
                                    {
                                        mojSzpital.AktywujPojazd(garaz.ListaKupionychPojazdow.ElementAt(int.Parse(opcjaGarazu) - 1));
                                    }
                                    else
                                    {
                                        mojSzpital.DezaktywujPojazd();
                                    }
                                    Console.WriteLine("");
                                    Console.WriteLine("Wduś dowolny klawisz, żeby kontynuować...");
                                    Console.ReadLine();
                                }
                                else if (opcjaDodatkowaGarazu == "9")
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Nieprawidłowa komenda! Wybór wycofany, wduś dowolny klawisz, żeby kontynuować...");
                                    Console.ReadLine();
                                }
                            }
                        }
                        else if (opcjaGarazu == "9")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowa komenda! Wybór wycofany, wduś dowolny klawisz, żeby kontynuować...");
                            Console.ReadLine();
                        }

                        //break;
                    }
                    else
                    {
                        Console.WriteLine("Wduś dowolny klawisz, żeby kontynuować...");
                        Console.ReadLine();
                        break;
                    }



                }

            }
            else if (opcjaGarazu == "9")
            {
                break;
            }
            else
            {
                Console.WriteLine("Nieprawidłowa komenda! Wduś dowolny klawisz, żeby kontynuować...");
                Console.ReadLine();
            }
        }
    }

    private static string Menu(Szpital mojSzpital)
    {
        Console.WriteLine("Wduś Enter, żeby kontynuować...");
        Console.ReadLine();
        Console.Clear();

        if (Globals.pokazStan == true)
            mojSzpital.PokazPodstawoweParametrySzpitala();

        Console.WriteLine("1. Sprawdź parametry szpitala");
        if (Globals.pokazKoszty == true)
        {
            Console.WriteLine("2. Wyrusz na wyprawę [koszt: 50 bananowych złotych, 1 Lekarz] ");
            //[3. zatrudnij lekarza] 
            Console.WriteLine("3. Zatrudnij Lekarza [koszt: 30 bananowych złotych]");
            Console.WriteLine($"4. Ulepsz Szpital [koszt: {50 * (Math.Pow(mojSzpital.PoziomSzpitala + 1, 2))} bananowych złotych]");
        }
        else
        {
            Console.WriteLine("2. Wyrusz na wyprawę");
            Console.WriteLine("3. Zatrudnij Lekarza");
            Console.WriteLine("4. Ulepsz Szpital");
        }
        Console.WriteLine("5. Garaż");
        //3 rodzaje wypraw do wyboru
        //opcja 3. zatrudnij lekarza za 30bzł
        //opcja 8. ulepsz szpital
        //  -podnieść poziom szpitala
        //  -zwiększenie ilości łóżek
        //  -zwiększenie ilości możliwych dostępnych lekarzy
        //  -ulepszenie powinno być drogie :)

        Console.WriteLine("7. Ustawienia");
        Console.WriteLine("9. Następny dzień");
        Console.WriteLine("0. Koniec");

        return Console.ReadLine();
    }

    private static void MenuUstawien()
    {
               while (true)
                {
                    Console.Clear();
                    if (Globals.pokazStan == false)
                        Console.WriteLine("1. Pokaż pasek stanu nad menu");
                    else
                        Console.WriteLine("1. Ukryj pasek stanu nad menu");

                    if (Globals.pokazKoszty == false)
                        Console.WriteLine("2. Pokaż koszty opcji w menu");
                    else
                        Console.WriteLine("2. Ukryj koszty opcji w menu");
                    Console.WriteLine("9. Powrót do menu");

                    string OpcjaUstawien = Console.ReadLine();

                    if (OpcjaUstawien == "1")
                    {
                        Globals.pokazStan = !Globals.pokazStan;
                    }
                    else if (OpcjaUstawien == "2")
                    {
                        Globals.pokazKoszty = !Globals.pokazKoszty;
                    }
                    else if (OpcjaUstawien == "9")
                    {
                        break;
                    }
                    
                }
    }




    private static string WyborTrudnosciWyprawy()
    {

        Console.Clear();
        Console.WriteLine("1. Łatwa wyprawa");
        Console.WriteLine("2. Normalna wyprawa");
        Console.WriteLine("3. Trudna wyprawa");
        Console.WriteLine("9. Powrót do menu");

        return Console.ReadLine();
    }


    private static void NextDay(Szpital mojSzpital)
    {
        Globals.dzien++;
        Console.WriteLine("Zaczyna się dzień " + Globals.dzien);

        mojSzpital.WyleczPacjentow();
        Random losowanie = new Random();
        if (losowanie.Next(0, 5) == 3)
        {
            int mnoznik = mojSzpital.PoziomSzpitala * 10;
            int kasa = losowanie.Next(mnoznik, mnoznik * 3);
            mojSzpital.WplywDoBudzetu(kasa);
            Console.WriteLine("Do budżetu skapnęły ochłapy z NFZ w wysokości (" + kasa +").");
        }


        Console.WriteLine("Za wyleczenie pacjentów do budżetu skapnęły bananowe złote w wysokości (" + mojSzpital.Przychod() + ")." );
    }
}

