using System;

public class Wyprawa
{
    private Szpital mojSzpital;

    public Wyprawa(Szpital szpital)
    {
        mojSzpital = szpital;
    }

    public bool WyruszNaWyprawe(int poziomTrudnosci)
    {
        PoziomTrudnosci poziom = (PoziomTrudnosci)poziomTrudnosci; //(PoziomTrudnosci)Enum.Parse(typeof(PoziomTrudnosci), poziomTrudnosci); 
        //SomeEnum enum = (SomeEnum)Enum.Parse(typeof(SomeEnum), "EnumValue")
        bool czyZepsutyPojazd =false;
        if(mojSzpital.WybranyPojazd?.AwariaPojazdu() ==true)
        {
            Console.WriteLine($"Ups! {mojSzpital.WybranyPojazd.Nazwa} się rozklekotał! Lekarz musi odbyć tę wyprawę bez pojazdu.");
            czyZepsutyPojazd = true;
            mojSzpital.WybranyPojazd = null; 
        }

        int modyfikatorTrudnosci = (mojSzpital.WybranyPojazd?.ModyfikatorTrudnosci ?? 0);
        int liczbaMiejscNaPacjentow = (mojSzpital.WybranyPojazd?.LiczbaMiejscNaPacjentow ?? 0);

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
            int nowiPacjenci = losowanie.Next(5, maxIloscPacjentow + liczbaMiejscNaPacjentow);
            mojSzpital.IloscPacjentow += nowiPacjenci;
            if (mojSzpital.IloscPacjentow > mojSzpital.IloscLozek)
                mojSzpital.IloscPacjentow = mojSzpital.IloscLozek;

            if (losowanie.Next(1, 101) > 100-szansaUtratyLekarza+ modyfikatorTrudnosci)
            {
                mojSzpital.IloscDostepnychLekarzy -= 1;
                Console.WriteLine($"Wyprawa udana. Niestety jeden lekarz poświęcił się dla uratowania przed straszliwym wirusem {nowiPacjenci} nowych pacjentów");
                return czyZepsutyPojazd;
            }
            else
            {
                Console.WriteLine($"Wyprawa udana, znaleziono {nowiPacjenci} nowych pacjentów. Wszyscy szczęśliwi, radośni, poobijani i z nowym kredytem wracają do domu.");
                return czyZepsutyPojazd;
            }
        }
        else if (mojSzpital.IloscDostepnychLekarzy == 0)
        {
            Console.WriteLine("Dopadła Cię choroba Polskiego NFZ - wszyscy lekarze są na Zachodzie");
            return czyZepsutyPojazd;
        }
        else
        {
            Console.WriteLine("Zgłoś się do NFZ po fundusze, bo ten wyjazd nie będzie refundowany");
            return czyZepsutyPojazd;
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