using System;
using System.Collections.Generic;
using System.Text;

    public class Wzmacniacz : Przedmiot
    {
    public int Atak { get; set; }
    public int Obrona { get; set; }
    public int SzansaUratowaniaLekarza { get; set; }

    public override string PrzedstawSie()
    {
        string parametr = "";
        if (Atak != 0)
        {
            parametr += " Atak: " + Atak;
        }
        if (Obrona != 0)
        {
            parametr += " Obrona: " + Obrona;
            //parametr = parametr + " Obrona: " + Obrona;
        }
        if (SzansaUratowaniaLekarza != 0)
        {
            parametr += " Szansa uratowania lekarza: " + SzansaUratowaniaLekarza;
        }

        return $"{base.PrzedstawSie()} {parametr}";
    }
}

