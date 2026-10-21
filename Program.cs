using System;

namespace Lista0
{
    class Program
    {
        private static void TestInt(int wartosc, string opis)
        {
            LiczbaCalkowita l = new LiczbaCalkowita(wartosc);
            Console.Write($"[int] {opis} ({wartosc}) -> ");
            l.Wypisz();
            Console.WriteLine();
        }

        private static void TestString(string napis, string opis)
        {
            try
            {
                LiczbaCalkowita l = new LiczbaCalkowita(napis);
                Console.Write($"[str] {opis} (\"{napis}\") -> ");
                l.Wypisz();
                Console.WriteLine();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"[str] {opis} (\"{napis}\") -> WYJĄTEK: {e.Message}");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== TESTY KONSTRUKTORA OD INT ===");
            TestInt(0, "Zero");
            TestInt(7, "Pojedyncza cyfra");
            TestInt(10000, "Dokładnie podstawa (granica bloku)");
            TestInt(-9999, "Granica jednego bloku ujemna");
            TestInt(123456789, "Standardowa liczba dodatnia");
            TestInt(-100000005, "Liczba z zerami w środku bloku");
            TestInt(int.MaxValue, "int.MaxValue");
            TestInt(int.MinValue, "int.MinValue");

            Console.WriteLine("\n=== TESTY KONSTRUKTORA OD NAPISU ===");
            TestString("0", "Zero z napisu");
            TestString("-0", "Minus zero (powinno znormalizować do 0)");
            TestString("-00000000", "Wiele zer z minusem");
            TestString("00000123456", "Zera wiodące dodatnie");
            TestString("-0001000000000000000000000005", "Duża liczba z zerami w środku i na początku");
            TestString("999999999999999999999999999999999999999999", "Bardzo duża liczba (42 dziewiątki)");
            TestString("+12345678901234567890", "Jawny znak plus");

            Console.WriteLine("\n=== TESTY BŁĘDNYCH DANYCH ===");
            TestString("", "Pusty napis");
            TestString("-", "Sam znak minus");
            TestString("1234a5678", "Litera w środku");
            TestString("12 34", "Spacja w środku");
        }
    }
}
