using System;

public class Szpital
{
    public string Nazwa { get; }
    public int Budzet { get; private set; }
    public int IloscDostepnychLekarzy { get; set; }
    public int IloscPacjentow { get; set; }
    public int PoziomSzpitala { get; set; }
    public int IloscLozek { get; set; }
    public int MaxIloscLekarzy { get; set; }

    public Szpital(string nazwa)
    {
        Nazwa = nazwa;
        Budzet = 100; //100
        IloscDostepnychLekarzy = 3; //3
        MaxIloscLekarzy = 10;
        IloscPacjentow = 0;
        IloscLozek = 30;
        PoziomSzpitala = 1;
    }

    public void PokazParametrySzpitala()
    {
        int wyleczeniPacjenci = IloscDostepnychLekarzy / 2;

        if (wyleczeniPacjenci > IloscPacjentow)
            wyleczeniPacjenci = IloscPacjentow;

        Console.WriteLine(Nazwa + "   poziom: " + PoziomSzpitala);
        Console.WriteLine("Budżet " + Budzet + " bananowych złotych");
        Console.WriteLine("Lekarze: " + IloscDostepnychLekarzy + "/" + MaxIloscLekarzy);
        Console.WriteLine("Pacjenci: " + IloscPacjentow + "/" + IloscLozek);
        Console.WriteLine("Planowany przychód do budżetu następnego dnia to (" + wyleczeniPacjenci*10 + ") bananowych złotych.");
    }

    internal void NastepnyDzien()
    {
        throw new NotImplementedException();
    }

    public void WyruszNaWyprawe()
    {   
        //zabezpieczenie się przed niewystarczającym budżetem lub lekarzami
        if (IloscDostepnychLekarzy == 0 || Budzet < 50)
        {
            if(IloscDostepnychLekarzy == 0)
            {
                Console.WriteLine("Liczba dostępnych Lekarzy to (0). Liczba potrzebnych Lekarzy do wyruszenia na wyprawę to (1).");
            }

            if (Budzet < 50)
            {
                Console.WriteLine("Dostępny budżet to (" + Budzet + ") bananowych złotych . Liczba potrzebnych bananowych złotych do wyruszenia na wyprawę to (50).");
            }

            return;

        }
        //throw new NotImplementedException();


        //zdobywamy kilku pacjentów
        Random zmiennaLosowa = new Random();
        int iloscZdobytychPacjentow;
        iloscZdobytychPacjentow = zmiennaLosowa.Next(2, 10);

        if (IloscLozek >= IloscPacjentow + iloscZdobytychPacjentow)
            IloscPacjentow += iloscZdobytychPacjentow;
        else
        {
            Console.WriteLine("Liczba zdobytych pacjentów przekroczyła liczbę dostępnych łóżek. Nie wszyscy mogli zostać zabrani do szpitala");
            IloscPacjentow = IloscLozek;
            
        }


        //tracimy jednego lekarza


        IloscDostepnychLekarzy -= 1;

        //koszt wyprawy = 50bzł

        Budzet -= 50;

        Console.WriteLine("Wyprawa powiodła się. Liczba znalezionych pacjentów to (" + iloscZdobytychPacjentow + "), a łączna liczba pacjentów to (" + IloscPacjentow + ").");
        Console.WriteLine("Budżet " + Budzet + " bananowych złotych");
        Console.WriteLine("Lekarze: " + IloscDostepnychLekarzy + "/" + MaxIloscLekarzy);

    }

    public void WyleczPacjentow()
    {
        int wyleczeniPacjenci = IloscDostepnychLekarzy / 2;

        if (wyleczeniPacjenci > IloscPacjentow)
            wyleczeniPacjenci = IloscPacjentow;

        IloscPacjentow -= wyleczeniPacjenci;
        Budzet += wyleczeniPacjenci * 10;
    }

    

}