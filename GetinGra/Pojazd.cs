using System;
using System.Collections.Generic;
using System.Text;


    public class Pojazd
    {
    public string Nazwa { get; set; }
    public int Koszt { get; set; }
    public int Niezawodnosc { get; set; }
    public int Wyglad { get; set; }
    public int ModyfikatorTrudnosci { get; set; }
    public int LiczbaMiejscNaPacjentow { get; set; }

    public Pojazd(string nazwa, int koszt, int niezawodnosc, int wyglad, int modyfikatorTrudnosci, int liczbaMiejscNaPacjentow)
    {
        this.Nazwa = nazwa;
        this.Koszt = koszt;
        this.Niezawodnosc = niezawodnosc;
        this.Wyglad = wyglad;
        this.ModyfikatorTrudnosci = modyfikatorTrudnosci;
        this.LiczbaMiejscNaPacjentow = liczbaMiejscNaPacjentow;
    }


    }
