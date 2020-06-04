using System;
using System.Linq;
using System.Collections.Generic;

class MainClass
{

    public class Globals
    {
        public static int dzien = 1;
        public static bool pokazKoszty = false;
        public static bool pokazStan = false;
        public static bool czyWykorzystanoKasynoWDanymDniu = false;
    }
    static void Main()
    {

        Console.Write("Nazwa Twojego szpitala: ");
        string nazwaSzpitala = Console.ReadLine();

        List<PrzedmiotWartosciowy> coMoznaZdobyc = new List<PrzedmiotWartosciowy>();

        coMoznaZdobyc.Add(new PrzedmiotWartosciowy
        {
            Nazwa = "Śmieci",
            Cena = 10,
            SzansaTrafienia = 70
        });

        coMoznaZdobyc.Add(new PrzedmiotWartosciowy
        {
            Nazwa = "Kopytko",
            Cena = 40,
            SzansaTrafienia = 40
        });

        coMoznaZdobyc.Add(new PrzedmiotWartosciowy
        {
            Nazwa = "Kredyt konsolidacyjny",
            Cena = 100,
            SzansaTrafienia = 20
        });

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
                    MenuWyprawy(mojSzpital, garaz, coMoznaZdobyc);
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
                else if (opcjaMenu == OpcjaMenu.PokazSkrytke)
                {
                    PokazSkrytke(mojSzpital);
                }
                else if (opcjaMenu == OpcjaMenu.Kasyno)
                {
                    Kasyno(mojSzpital, coMoznaZdobyc);
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


    private static void MenuWyprawy(Szpital mojSzpital,Garaz garaz,List<PrzedmiotWartosciowy> coMoznaZdobyc)
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
        if (wyprawa.WyruszNaWyprawe(poziom, coMoznaZdobyc))
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
        Console.WriteLine("6. Ustawienia");
        Console.WriteLine("7. Pokaż zawartość skrytki");
        Console.WriteLine("8. Kasyno");
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

    private static void PokazSkrytke(Szpital mojSzpital)
    {
        foreach (Przedmiot rzecz in mojSzpital.Przedmioty)
        {
            Console.WriteLine(rzecz.PrzedstawSie());
        }
    }

    private static void Kasyno(Szpital mojSzpital, List<PrzedmiotWartosciowy> coMoznaZdobyc)
    {
        int stawka = 10;
        Random losowanie = new Random();
        if (Globals.czyWykorzystanoKasynoWDanymDniu == true)
        {
            Console.WriteLine($"Nie masz czego już dzisiaj szukać w kasynie! Wróć kolejnego dnia.");
        }
        else
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine($"Dostępny budżet: {mojSzpital.Budzet} BZ");
                Console.WriteLine($"1. Zagraj za {stawka} BZ");
                Console.WriteLine($"2. Podbij stawkę dwukrotnie");
                Console.WriteLine($"3. Zmniejsz stawkę dwukrotnie");
                Console.WriteLine($"4. Pokaż aktualne szanse wygrania");
                Console.WriteLine($"5. Zagadaj hostessę");
                
                Console.WriteLine($"9. Powrót");

                string OpcjaKasyna = Console.ReadLine();
                if (OpcjaKasyna == "1")
                {
                    if (mojSzpital.PobierzZBudzetu(stawka))
                    {
                        int szansaPrzedmiot = losowanie.Next(0, 100);
                        Przedmiot wylosowany = coMoznaZdobyc.OrderBy(p => p.SzansaTrafienia).FirstOrDefault(p => p.SzansaTrafienia <= szansaPrzedmiot + ((stawka / 10) - 1));
                        if (wylosowany != null)
                        {
                            mojSzpital.Przedmioty.Add(wylosowany);
                            Console.WriteLine($"Zdobyłeś {wylosowany.Nazwa}, w poczuciu triumfu opuszczasz kasyno. Możesz spróbować ponownie kolejnego dnia!");
                        }
                        else
                        {
                            Console.WriteLine($"Tym razem nic nie wygrałeś, biednemu człowiekowi zawsze wiatr w oczy. Możesz spróbować ponownie kolejnego dnia!");
                        }
                        Globals.czyWykorzystanoKasynoWDanymDniu = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Nie masz wystarczająco środków! Pragniesz powrócić do stawki bazowej 10 BZ?(T/N)");
                        string wybor = Console.ReadLine();
                        if (wybor.ToUpper()=="T")//wybor.ToUpper().Equals('T') )
                        {
                            stawka = 10;
                        }
                        
                    }
                }
                else if (OpcjaKasyna == "2")
                {
                    if(stawka<3200)
                    {
                        stawka = stawka * 2;
                    }
                    else
                    {
                        Console.WriteLine($"Osiągnąłeś już maksymalną stawkę w tym kasynie!  Wciśnij dowolny klawisz aby kontynuować.");
                        Console.ReadLine();
                    }
                }
                else if (OpcjaKasyna == "3")
                {
                    if (stawka > 10)
                    {
                        stawka = stawka / 2;
                    }
                    else
                    {
                        Console.WriteLine($"Osiągnąłeś już minimalną stawkę w tym kasynie! Wciśnij dowolny klawisz aby kontynuować.");
                        Console.ReadLine();
                    }
                }
                else if (OpcjaKasyna == "4")
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Nic: {decimal.Round((100 *Math.Max(0,(Convert.ToDecimal(20- ((stawka / 10) - 1)))/ Convert.ToDecimal( Math.Max((100-(stawka / 10) - 1),1) ))),2, MidpointRounding.AwayFromZero)}%");
                    Console.WriteLine($"{coMoznaZdobyc.ElementAt(0).Nazwa}: {decimal.Round((100 *Math.Max(0, (Convert.ToDecimal(20 - Math.Max(0,-20+ (stawka / 10) - 1) )) / Convert.ToDecimal(Math.Max((100 - (stawka / 10) - 1), 1)))), 2, MidpointRounding.AwayFromZero)}% ");
                    Console.WriteLine($"{coMoznaZdobyc.ElementAt(1).Nazwa}: {decimal.Round((100 *Math.Max(0, (Convert.ToDecimal(30 - Math.Max(0, -40 + (stawka / 10) - 1) )) / Convert.ToDecimal(Math.Max((100 - (stawka / 10) - 1), 1)))), 2, MidpointRounding.AwayFromZero)}% ");
                    Console.WriteLine($"{coMoznaZdobyc.ElementAt(2).Nazwa}: {decimal.Round((100 *(Convert.ToDecimal(30)) / Convert.ToDecimal(Math.Max((100 - (stawka / 10) - 1),30))), 2, MidpointRounding.AwayFromZero)}% ");
                    //foreach (PrzedmiotWartosciowy rzecz in coMoznaZdobyc)
                    //{
                    //    Console.WriteLine($"{rzecz.Nazwa}: {rzecz.SzansaTrafienia + (stawka / 10) - 1}%");
                    //    //20{20-39/20%},40{40-69/30%},70{70-99/30%}
                    //   //po usunieciu nic dziedzina zmienia się na 20-99{80}, więc 20/80,30/80, 30/80 z 0-99
                    //    //   /(100-(stawka / 10) - 1)

                    //}

                    Console.WriteLine($"Wciśnij dowolny klawisz aby kontynuować.");
                    Console.ReadLine();
                }
                else if (OpcjaKasyna == "5")
                {
                    if( losowanie.Next(0, 100)<74)
                    {
                        Console.WriteLine("Hostessa zauważyła twoje skarpetki w sandałach i wezwała ochronę, która wyrzuciła cię zanim zdążyłeś zagadać. Lepiej się już dzisiaj nie pokazuj w kasynie!");
                    }
                    else
                    {
                        Console.WriteLine("Hostessa przyjrzała się twojej twarzy i stwierdziła, że może przełamie wstręt za miliard bananowych złotych. Potem standardowo wezwała ochronę, która cię tym razem nie dogoniła. Podczas ucieczki zdążyłeś pokazać im jeszcze pewien gest na odchodne co zostało uwiecznione na kamerze ochrony i zrobiło furorę na YT. Lepiej się już dzisiaj nie pokazuje w kasynie!");
                    }
                    
                    Console.ReadLine();
                    Globals.czyWykorzystanoKasynoWDanymDniu = true;
                    break;
                }
                else if (OpcjaKasyna == "9")
                {
                    break;
                }
                else 
                {
                    Console.WriteLine("Nieprawidłowa komenda! Uderz głową w klawiaturę i wykonaj salto, aby kontynuować!");
                    Console.ReadLine();
                }
            }
        }

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
        Globals.czyWykorzystanoKasynoWDanymDniu = false;

        Console.WriteLine("Za wyleczenie pacjentów do budżetu skapnęły bananowe złote w wysokości (" + mojSzpital.Przychod() + ")." );
    }
}

