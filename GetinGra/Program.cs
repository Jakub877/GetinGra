using System;

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

                //przeniesione do metody WyruszNaWyprawe wewnątrz klasy Szpital ze względu na private set parametru Budzet

                mojSzpital.WyruszNaWyprawe();


            }
            else if (opcja == "3")
            {
                mojSzpital.ZatrudnijLekarzy();

            }

            else if (opcja == "9")
            {
                //przechodzimy do następnego dnia
                mojSzpital.WyleczPacjentow();
                mojSzpital.NastepnyDzien();
                Globals.dzien++ ;
                Console.WriteLine("Zaczyna się dzień " + Globals.dzien);

            }
            else if (opcja == "8")
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
                    
                    break;
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
        }
        else
        {
            Console.WriteLine("2. Wyrusz na wyprawę");
            Console.WriteLine("3. Zatrudnij Lekarza");
        }
        Console.WriteLine("8. Ustawienia");
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
}

