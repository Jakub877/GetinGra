using System;

public class Wyprawa
{
    private Szpital mojSzpital;

    public Wyprawa(Szpital szpital)
    {
        mojSzpital = szpital;
    }

    public void WyruszNaWyprawe(string poziomTrudnosci)
    {
        PoziomTrudnosci poziom = (PoziomTrudnosci)Enum.Parse(typeof(PoziomTrudnosci), poziomTrudnosci); //poziomTrudnosci. //PoziomTrudnosci.Parse(poziomTrudnosci);  //poziomTrudnosci;
        //SomeEnum enum = (SomeEnum)Enum.Parse(typeof(SomeEnum), "EnumValue")

        int maxIloscPacjentow = 0;
        int szansaUtratyLekarza = 20;
        if (poziom == PoziomTrudnosci.Latwy)
        {
            maxIloscPacjentow = 14;
            szansaUtratyLekarza = 20;
        }
        else if (poziom == PoziomTrudnosci.Normalny)
        {
            maxIloscPacjentow = 20;
            szansaUtratyLekarza = 30;
        }
        else if (poziom == PoziomTrudnosci.Trudny)
        {
            maxIloscPacjentow = 30;
            szansaUtratyLekarza = 60;
        }
        else if (poziom == PoziomTrudnosci.GetinHardocore)
        {
            maxIloscPacjentow = 100;
            szansaUtratyLekarza = 95;
        }


        if (mojSzpital.IloscDostepnychLekarzy > 0 && mojSzpital.PobierzZBudzetu(50))
        {
            Random losowanie = new Random();
            mojSzpital.IloscPacjentow += losowanie.Next(5, maxIloscPacjentow);
            if (mojSzpital.IloscPacjentow > mojSzpital.IloscLozek)
                mojSzpital.IloscPacjentow = mojSzpital.IloscLozek;

            if (losowanie.Next(1, 101) > 100-szansaUtratyLekarza)
            {
                mojSzpital.IloscDostepnychLekarzy -= 1;
                Console.WriteLine("Wyprawa udana. Niestety jeden lekarz poświęcił się dla uratowania przed straszliwym wirusem nowych pacjentów");
            }
            else
            {
                Console.WriteLine("Wyprawa udana. Wszyscy szczęśliwi, radośni, poobijani i z nowym kredytem wracają do domu.");
            }
        }
        else if (mojSzpital.IloscDostepnychLekarzy == 0)
        {
            Console.WriteLine("Dopadła Cię choroba Polskiego NFZ - wszyscy lekarze są na Zachodzie");
        }
        else
        {
            Console.WriteLine("Zgłoś się do NFZ po fundusze, bo ten wyjazd nie będzie refundowany");
        }

        //if (mojSzpital.IloscDostepnychLekarzy > 0 && mojSzpital.PobierzZBudzetu(50))
        //{
        //    int modyfikatorTrudnosci = 0;

        //    if (poziomTrudnosci == "latwy")
        //        {
        //            modyfikatorTrudnosci = 0;
        //        }
        //    else if (poziomTrudnosci == "normalny")
        //        {
        //            modyfikatorTrudnosci = 3;
        //        }
        //    else if (poziomTrudnosci == "trudny")
        //        {
        //            modyfikatorTrudnosci = 6;
        //        }

        //    int nowiPacjenci;
        //    Random losowanie = new Random();
        //    nowiPacjenci = losowanie.Next(5, 14 + modyfikatorTrudnosci);

        //    Random losowanieStraty = new Random();

        //    if (losowanie.Next(1, 11) > (10 - modyfikatorTrudnosci) )
        //      {
        //        mojSzpital.IloscDostepnychLekarzy -= 1;
        //        Console.WriteLine($"Wyprawa udana. Niestety jeden lekarz poświęcił się dla uratowania przed straszliwym wirusem ({nowiPacjenci}) nowych pacjentów.");
        //      }   
        //    else
        //     {
        //        Console.WriteLine($"Wyprawa udana. Lekarzowi udało się powrócić i uratować przed straszliwym wirusem ({nowiPacjenci}) nowych pacjentów.");
        //     }



        //    mojSzpital.IloscPacjentow += nowiPacjenci;
        //    if (mojSzpital.IloscPacjentow > mojSzpital.IloscLozek)
        //    {
        //        mojSzpital.IloscPacjentow = mojSzpital.IloscLozek;
        //        Console.WriteLine("Liczba znalezionych pacjentów przekroczyła liczbę dostępnych łóżek. Nie wszyscy mogli zostać zabrani do szpitala.");
        //    }




        //}
        //else if (mojSzpital.IloscDostepnychLekarzy == 0)
        //{
        //    Console.WriteLine("Dopadła Cię choroba Polskiego NFZ - wszyscy lekarze są na Zachodzie");
        //}
        //else
        //{
        //    Console.WriteLine("Zgłoś się do NFZ po fundusze, bo ten wyjazd nie będzie refundowany");
        //}
    }
}