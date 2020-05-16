using System;
using System.Linq;

class MainClass
{

    public static class Globals
    {
        public static int dzien = 1;
    }
    static void Main()
    {

        Console.Write("Nazwa Twojego szpitala: ");
        string nazwaSzpitala = Console.ReadLine();

        Szpital mojSzpital = new Szpital(nazwaSzpitala);
        bool pokazKoszty = false;
        bool pokazStan = false;
        while (true)
        {
            
            string opcja = Menu(mojSzpital, pokazKoszty, pokazStan);
            

            if (opcja == "1")
            {
                mojSzpital.PokazParametrySzpitala();
            }
            else if (opcja == "2")
            {

                string wybranaOpcja = WyborTrudnosciWyprawy();

                Wyprawa wyprawa = new Wyprawa(mojSzpital);

                while (true)
                {
                    if (wybranaOpcja == "1")
                    {
                        wyprawa.WyruszNaWyprawe("latwy");
                        break;

                    }
                    else if (wybranaOpcja == "2")
                    {
                        wyprawa.WyruszNaWyprawe("normalny");
                        break;
                    }
                    else if (wybranaOpcja == "3")
                    {
                        wyprawa.WyruszNaWyprawe("trudny");
                        break;
                    }
                    else if (wybranaOpcja == "9")
                    {
                        break;
                    }
                        
                }
            }
            else if (opcja == "3")
            {
                mojSzpital.ZatrudnijLekarzy();

            }

            else if (opcja == "4")
            {
                mojSzpital.UlepszSzpital();
            }
            else if (opcja == "5")
            {           
                Garaz garaz = new Garaz();
                string opcjaGarazu;
                string opcjaDodatkowaGarazu;
                string[] values = { "1", "2","3","4","5","6","7" };

                //if (values.Contains(interSubDir))

                                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1. Pokaż oferty pojazdów");
                    Console.WriteLine("2. Pokaż dostępne pojazdy");
                    Console.WriteLine("9. Powrót do menu");

                opcjaGarazu =Console.ReadLine();
                    
                    if (opcjaGarazu == "1")
                    {
                        while(true)
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
                                Console.WriteLine("");
                                Console.WriteLine("Wpisz numer dodatkowej opcji:");
                                Console.WriteLine("1. Kup Pojazd");
                                garaz.KupPojazd(garaz.ListaPojazdow.ElementAt(int.Parse(opcjaGarazu)-1),mojSzpital);
                                Console.WriteLine("Wduś dowolny klawisz, żeby kontynuować...");
                                Console.ReadLine();
                            }
                            else if (opcjaGarazu =="9")
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
                        garaz.PokazListeKupionychPojazdow();
                        Console.WriteLine("");
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

                //Console.WriteLine

                //garaz.PokazListePojazdow();
                //garaz.PokazListeKupionychPojazdow();

            }

            else if (opcja == "9")
            {
                //przechodzimy do następnego dnia
                NextDay(mojSzpital);

            }
            else if (opcja == "7")
            {
                while (true)
                {
                    string OpcjaUstawien = Ustawienia(pokazStan, pokazKoszty);
                    if (OpcjaUstawien == "1")
                    {
                        pokazStan = !pokazStan;
                    }
                    else if (OpcjaUstawien == "2")
                    {
                        pokazKoszty = !pokazKoszty;
                    }
                    else if (OpcjaUstawien == "9")
                    {
                        break;
                    }
                    
                }

            }

            else if (opcja == "0")
            {
                Console.WriteLine("Koniec gry");
                break;
            }
        }
    }

    private static string Menu(Szpital mojSzpital, bool pokazKoszty, bool pokazStan)
    {
        Console.WriteLine("Wduś Enter, żeby kontynuować...");
        Console.ReadLine();
        Console.Clear();

        if(pokazStan == true)
        mojSzpital.PokazPodstawoweParametrySzpitala();

        Console.WriteLine("1. Sprawdź parametry szpitala");
        if(pokazKoszty == true)
        {
            Console.WriteLine("2. Wyrusz na wyprawę [koszt: 50 bananowych złotych, 1 Lekarz] ");
            //[3. zatrudnij lekarza] 
            Console.WriteLine("3. Zatrudnij Lekarza [koszt: 30 bananowych złotych]");
            Console.WriteLine($"4. Ulepsz Szpital [koszt: {50 * (Math.Pow(mojSzpital.PoziomSzpitala+1, 2))} bananowych złotych]");
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

    private static string Ustawienia(bool pokazStan, bool pokazKoszty)
    {

            Console.Clear();
            if (pokazStan == false)
                Console.WriteLine("1. Pokaż pasek stanu nad menu");
            else
                Console.WriteLine("1. Ukryj pasek stanu nad menu");

            if (pokazKoszty == false)
                Console.WriteLine("2. Pokaż koszty opcji w menu");
            else
                Console.WriteLine("2. Ukryj koszty opcji w menu");
            Console.WriteLine("9. Powrót do menu");


            return Console.ReadLine();
         
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

