using System;
using System.Collections.Generic;
using System.Text;

    public class Przedmiot
    {
        public string Nazwa { get; set; }
        public int Cena { get; set; }

        public virtual string PrzedstawSie()
        {
            return $"{Nazwa}   Cena: {Cena}";
        }

        public int KwotaSprzedazy()
        {
            return Cena / 5;
        }
    }

