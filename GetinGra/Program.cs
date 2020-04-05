using System;

class MainClass
{
    static void Main()
    {
        Console.Write("Nazwa Twojego szpitala: ");
        string nazwaSzpitala = Console.ReadLine();

        Szpital mojSzpital = new Szpital(nazwaSzpitala);


        while (true)
        {
            string opcja = Menu();

            if (opcja == "1")
            {
                mojSzpital.PokazParametrySzpitala();
            }
            else if (opcja == "2")
            {

                //przeniesione do metody WyruszNaWyprawe wewnątrz klasy Szpital ze względu na private set parametru Budzet

                mojSzpital.WyruszNaWyprawe();


            }
            else if (opcja == "9")
            {
                //przechodzimy do następnego dnia
                mojSzpital.WyleczPacjentow();
                mojSzpital.NastepnyDzien();
            }
            else if (opcja == "0")
            {
                Console.WriteLine("Koniec gry");
                break;
            }
        }
    }

    private static string Menu()
    {
        Console.WriteLine("Wduś Enter, żeby kontynuować...");
        Console.ReadLine();
        Console.Clear();

        Console.WriteLine("1. Sprawdź parametry szpitala");
        Console.WriteLine("2. Wyrusz na wyprawę");
        //3. zatrudnij lekarza za 30bzł do zrobienia
        Console.WriteLine("9. Następny dzień");
        Console.WriteLine("0. Koniec");

        return Console.ReadLine();
    }
}