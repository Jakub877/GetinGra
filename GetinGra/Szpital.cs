using System;
using System.Collections.Generic;
public class Szpital
{
    public string Nazwa { get; }
    public int Budzet { get; private set; }
    public int IloscDostepnychLekarzy { get; set; }
    public int IloscPacjentow { get; set; }
    public int PoziomSzpitala { get; set; }
    public int IloscLozek { get; set; }
    public int MaxIloscLekarzy { get; set; }
    public Pojazd WybranyPojazd { get; set; }
    public List<Przedmiot> Przedmioty { get; set; }

    public Szpital(string nazwa)
    {
        Nazwa = nazwa;
        Budzet = 100; 
        IloscDostepnychLekarzy = 3; //3
        MaxIloscLekarzy = 10;
        IloscPacjentow = 0;
        IloscLozek = 30;
        PoziomSzpitala = 1;
        Przedmioty = new List<Przedmiot>();

        Bron nowaBron = new Bron();
        nowaBron.Nazwa = "Kije samobije";
        nowaBron.Cena = 20;
        nowaBron.Obrazenia = 3;
        Przedmioty.Add(nowaBron);

        Wzmacniacz nowyWzmacniacz = new Wzmacniacz();
        nowyWzmacniacz.Nazwa = "Riki tiki narkotyki";
        nowyWzmacniacz.Atak = 2;
        nowyWzmacniacz.Obrona = -1;
        Przedmioty.Add(nowyWzmacniacz);
    }

    public void PokazParametrySzpitala()
    {

        Console.WriteLine(Nazwa + "   poziom: " + PoziomSzpitala);
        Console.WriteLine("Budżet " + Budzet + " bananowych złotych");
        Console.WriteLine("Lekarze: " + IloscDostepnychLekarzy + "/" + MaxIloscLekarzy);
        Console.WriteLine("Pacjenci: " + IloscPacjentow + "/" + IloscLozek);
        Console.WriteLine("Planowany przychód do budżetu następnego dnia to (" + Przychod() + ") bananowych złotych.");
        if(WybranyPojazd==null)
        {
            Console.WriteLine("Aktywny pojazd: brak");
        }
        else
        {
            Console.WriteLine($"Aktywny pojazd: {WybranyPojazd.Nazwa}");
        }
        
    }

    public int Przychod()
    {
        int wyleczeniPacjenci = (IloscDostepnychLekarzy / 2)+1;

        if (wyleczeniPacjenci > IloscPacjentow)
            wyleczeniPacjenci = IloscPacjentow;
        return wyleczeniPacjenci * (9 + PoziomSzpitala);
    }

    public void UlepszSzpital()
    {
        if(Budzet < (50 * (Math.Pow(PoziomSzpitala + 1, 2))) )
        {
            Console.WriteLine($"Dostępny budżet to ({Budzet}) bananowych złotych. Liczba potrzebnych bananowych złotych do ulepszenia to ({50 * (Math.Pow(PoziomSzpitala + 1, 2))}).");
            return;
        }   
        IloscLozek += 10;
        MaxIloscLekarzy += 5;
        Budzet -= 50 * Convert.ToInt32(Math.Pow(PoziomSzpitala + 1, 2));
        PoziomSzpitala++;
        Console.WriteLine($"Szpital pomyślnie ulepszono na poziom {PoziomSzpitala}.");
    }

    public void PokazPodstawoweParametrySzpitala()
    {
        Console.WriteLine("[" + Nazwa + "] [poziom: " + PoziomSzpitala + "] [Dzień "+ MainClass.Globals.dzien + "] [Budżet: " + Budzet + " bananowych złotych" + "] [Lekarze: " + IloscDostepnychLekarzy + "/" + MaxIloscLekarzy + "] [Pacjenci: " + IloscPacjentow + "/" + IloscLozek+"]");
    }

    public void ZatrudnijLekarzy()
    {
        if ( IloscDostepnychLekarzy == MaxIloscLekarzy )
        {
            Console.WriteLine($"Nie możesz zatrudnić więcej Lekarzy. Posiadani Lekarze: {IloscDostepnychLekarzy} / {MaxIloscLekarzy}.");
            return;
        }
        
        if (Budzet < 30)
        {
            Console.WriteLine("Dostępny budżet to (" + Budzet + ") bananowych złotych . Liczba potrzebnych bananowych złotych do zatrudnienia Lekarza to (30).");
            return;
        }
            
        else
        {
            Budzet -= 30;
            IloscDostepnychLekarzy += 1;
            Console.WriteLine("Lekarz został zatrudniony. Aktualna liczba Lekarzy to (" + IloscDostepnychLekarzy + ").");
            Console.WriteLine("Aktualny budżet to (" + Budzet + ") bananowych złotych.");
        }
            
    }

    public void WyleczPacjentow()
    {
        int wyleczeniPacjenci = (IloscDostepnychLekarzy / 2) + 1;

        if (wyleczeniPacjenci > IloscPacjentow)
            wyleczeniPacjenci = IloscPacjentow;

        IloscPacjentow -= wyleczeniPacjenci;
        Budzet += wyleczeniPacjenci * 10;
    }

    public bool PobierzZBudzetu(int kwota)
    {
        if (Budzet < kwota) return false;

        Budzet -= kwota;
        return true;
    }

    public void WplywDoBudzetu(int kwota)
    {
        Budzet += kwota;
    }

    public void AktywujPojazd(Pojazd pojazd)
    {
        if (pojazd == null)
        {
            Console.WriteLine("Nie można aktywować pojazdu!");
        }
        else
        {
            if(WybranyPojazd==null)
            {
                WybranyPojazd = pojazd;
                Console.WriteLine($"Pojazd {WybranyPojazd.Nazwa}  jest aktywny!");
            }
            else
            {          
                Console.WriteLine($"Pojazd {WybranyPojazd.Nazwa}  już nie jest aktywny!");
                WybranyPojazd = pojazd;
                Console.WriteLine($"Pojazd {WybranyPojazd.Nazwa}  jest aktywny!");
                
            }
        }
    }

    public void DezaktywujPojazd()
    {
        if(WybranyPojazd==null)
        {
            Console.WriteLine("Nie posiadasz aktywnego pojazdu!");
        }
        else
        {           
            Console.WriteLine($"Pojazd {WybranyPojazd.Nazwa} już nie jest aktywny!");
            WybranyPojazd = null;
        }
    }

}