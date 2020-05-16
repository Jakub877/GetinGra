using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

class Garaz
{
    public List<Pojazd> ListaPojazdow { get; set; }
    public List<Pojazd> ListaKupionychPojazdow { get; set; }

    public Garaz()
    {
        Pojazd skladak = new Pojazd("Zdezelowany składak Damiana", 0, 20, -100, -5, 1);
        Pojazd osiolek = new Pojazd("Osiołek Janusza", 20, 70, 10, 5, 1);
        Pojazd maluch = new Pojazd("Stuningowany maluch", 200, 90, 50, 15, 3);
        Pojazd karetka_podst = new Pojazd("Karetka podstawowa", 4000, 80, 10, 20, 4);
        Pojazd karetka_spec = new Pojazd("Karetka specjalistyczna", 20000, 90, 10, 25, 5);
        Pojazd helikopter = new Pojazd("Helikopter ratunkowy", 1000000, 99, 95, 30, 10);
        Pojazd samolot = new Pojazd("Airbus A380", 2147483647, 100, 100, 100, 525);
        List<Pojazd> listaPojazdow = new List<Pojazd>();
        listaPojazdow.Add(skladak);
        listaPojazdow.Add(osiolek);
        listaPojazdow.Add(maluch);
        listaPojazdow.Add(karetka_podst);
        listaPojazdow.Add(karetka_spec);
        listaPojazdow.Add(helikopter);
        listaPojazdow.Add(samolot);
        ListaPojazdow = listaPojazdow;
        ListaKupionychPojazdow = new List<Pojazd>();
    }

    public void PokazListePojazdow()
    {

        int k = 1;
        Console.WriteLine("Lista pojazdów na allegetin:");
        foreach (Pojazd pojazd in ListaPojazdow)
        {
            Console.WriteLine($"{k}. {pojazd.Nazwa} [koszt: {pojazd.Koszt} BZ] ");
            k++;

        }
        


        //Console.WriteLine(Nazwa + "   poziom: " + PoziomSzpitala);
        //Console.WriteLine("Budżet " + Budzet + " bananowych złotych");
        //Console.WriteLine("Lekarze: " + IloscDostepnychLekarzy + "/" + MaxIloscLekarzy);
        //Console.WriteLine("Pacjenci: " + IloscPacjentow + "/" + IloscLozek);
        //Console.WriteLine("Planowany przychód do budżetu następnego dnia to (" + Przychod() + ") bananowych złotych.");

    }

    public void KupPojazd(Pojazd pojazd, Szpital szpital)
    {
        if (ListaKupionychPojazdow.Contains(pojazd))
        {
            Console.WriteLine($"Posiadasz już: {pojazd.Nazwa}!");
        }
        else
        {
            if (szpital.PobierzZBudzetu(pojazd.Koszt))
            {
                ListaKupionychPojazdow.Add(pojazd);
                Console.WriteLine($"Pomyślnie zakupiono pojazd: {pojazd.Nazwa}!");
            }
            else
            {
                Console.WriteLine($"Jesteś spłukany! Nie stać Cię na pojazd: {pojazd.Nazwa}!");
            }
        }

        //if (ListaKupionychPojazdow?.Any() != true)

    }

    public void PokazListeKupionychPojazdow()
    {

        int k = 1;
        Console.WriteLine("Lista kupionych pojazdów:");
        if (ListaKupionychPojazdow?.Any() != true)
        {
            Console.WriteLine("Nie posiadasz pojazdów! Twoi lekarze muszą zasuwać z buta!");
        }
        else
        {
            foreach (Pojazd pojazd in ListaKupionychPojazdow)
            {
                Console.WriteLine($"{k}. {pojazd.Nazwa}");
                k++;
            }
        }


    }


}

