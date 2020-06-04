using System;
using System.Collections.Generic;
using System.Linq;
public class Wyprawa
{
    private Szpital mojSzpital;

    public Wyprawa(Szpital szpital)
    {
        mojSzpital = szpital;
    }

    public bool WyruszNaWyprawe(int poziomTrudnosci, List<PrzedmiotWartosciowy> coMoznaZdobyc)  
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

        Random losowanie = new Random();
        int szansaPrzedmiot = losowanie.Next(0, 100);

        //OrderBy - sortowanie po tym co w nawiasie
        //FirtstOrDefault - znajduje pierwszy element spełniający warunek,
        //jeśli nic nie znajdzie, to zwraca null
        Przedmiot wylosowany = coMoznaZdobyc.OrderBy(p => p.SzansaTrafienia).FirstOrDefault(p => p.SzansaTrafienia <= szansaPrzedmiot);

        // foreach(Przedmiot p in coMoznaZdobyc)
        // {
        //     if(p.SzansaTrafienia <= szansaPrzedmiot)
        //     {
        //         wylosowany = p;
        //         break;
        //     }
        // }

        if (wylosowany != null)
        {
            mojSzpital.Przedmioty.Add(wylosowany);
            Console.WriteLine($"Podczas wyprawy twój lekarz zdobył {wylosowany.Nazwa}. Podjarał się tym jak lampion w roraty lub tester w dzień wypłaty!");
        }
            

        if (mojSzpital.IloscDostepnychLekarzy > 0 && mojSzpital.PobierzZBudzetu(50))
        {
            //Random losowanie = new Random();
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

        //    mojSzpital.IloscPacjentow += nowiPacjenci;
        //    if (mojSzpital.IloscPacjentow > mojSzpital.IloscLozek)
        //    {
        //        mojSzpital.IloscPacjentow = mojSzpital.IloscLozek;
        //        Console.WriteLine("Liczba znalezionych pacjentów przekroczyła liczbę dostępnych łóżek. Nie wszyscy mogli zostać zabrani do szpitala.");
        //    }

    }
}