using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lista0
{
    public class LiczbaCalkowita
    {
        protected const int Podstawa = 10000;
        protected const int DlugoscBloku = 4;

        protected readonly List<int> cyfry;
        protected bool ujemna;

        public LiczbaCalkowita(int n = 0)
        {
            cyfry = new List<int>();
            ujemna = n < 0;

            if (n == 0)
            {
                cyfry.Add(0);
                return;
            }

            while (n != 0)
            {
                cyfry.Add(Math.Abs(n % Podstawa));
                n /= Podstawa;
            }
        }
        public LiczbaCalkowita(string napis)
        {
            if (string.IsNullOrEmpty(napis))
            {
                throw new ArgumentException("Napis nie może być pusty ani null.", nameof(napis));
            }

            cyfry = new List<int>();
            ujemna = false;

            int start = 0;
            if (napis[0] == '-')
            {
                ujemna = true;
                start = 1;
            }
            else if (napis[0] == '+')
            {
                start = 1;
            }

            if (start == napis.Length)
            {
                throw new ArgumentException("Napis zawiera sam znak bez cyfr.", nameof(napis));
            }

            for (int i = start; i < napis.Length; i++)
            {
                if (!char.IsDigit(napis[i]))
                {
                    throw new ArgumentException($"Napis zawiera niedozwolony znak: '{napis[i]}'.", nameof(napis));
                }
            }

            for (int i = napis.Length; i > start; i -= DlugoscBloku)
            {
                int poczatekBloku = Math.Max(start, i - DlugoscBloku);
                int dlugosc = i - poczatekBloku;
                int wartoscBloku = int.Parse(napis.Substring(poczatekBloku, dlugosc));
                cyfry.Add(wartoscBloku);
            }

            Normalizuj();
        }

        protected void Normalizuj()
        {
            while (cyfry.Count > 1 && cyfry[cyfry.Count - 1] == 0)
            {
                cyfry.RemoveAt(cyfry.Count - 1);
            }

            if (cyfry.Count == 1 && cyfry[0] == 0)
            {
                ujemna = false;
            }
        }
        public void Wypisz(TextWriter? wyjscie = null)
        {
            wyjscie ??= Console.Out;
            wyjscie.Write(this.ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (ujemna)
            {
                sb.Append('-');
            }

            sb.Append(cyfry[cyfry.Count - 1]);

            for (int i = cyfry.Count - 2; i >= 0; i--)
            {
                sb.Append(cyfry[i].ToString($"D{DlugoscBloku}"));
            }

            return sb.ToString();
        }
    }
}
