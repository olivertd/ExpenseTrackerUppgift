
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace ExpenseTrackerUppgift
{
    public class Expense
    {
        public string ExpenseNamn { get; set; }
        public double ExpensePris { get; set; }
        public string ExpenseKategori { get; set; }
        
    }
    

    public class Program
    {
        public static List<Expense> expenses = new List<Expense>();
        
        //FUNKTION FÖR ATT LÄGGA TILL NY EXPENSE
        public static void AddExpense(string expenseNamn, double expensePris, string expenseKategori)
        {
            var expense = new Expense();
            expense.ExpenseNamn = expenseNamn;
            expense.ExpensePris = expensePris;
            expense.ExpenseKategori = expenseKategori;
            expenses.Add(expense);
        }

        //FUNKTION FÖR ATT FRÅGA ANVÄNDARE OM NY EXPENSE
        public static void AskUserforExpenseParamters()
        {
            Console.WriteLine("DU SKALL LÄGGA TILL EN UTGIFT");
            
            Console.Write("Vad har din utgift för namn?: ");
            string utgiftNamn = Console.ReadLine();

            Console.Write("Vad har din utgift för pris?: ");
            double utgiftPris = Convert.ToDouble(Console.ReadLine());

            Console.Write("Vad har din utgift för kategori?: ");
            string utgiftKategori = Console.ReadLine();

            AddExpense(utgiftNamn, utgiftPris, utgiftKategori);
            
        }
        
        public static void Main()
        {
            
            
            //FIN VÄLKOMST TILL PROGRAMMET OCH SE TILL ATT INGA PROBLEM MED SVENSKA KARAKTÄRER SKER
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            Console.WriteLine("███████████████████████████████████████████████████████████████████████");
            Console.WriteLine("█▄─▀─▄█▄─▄▄─█▄─▄▄─█▄─▀█▄─▄█─▄▄▄▄█▄─▄▄─███─▄─▄─█▄─▄▄▀█─▄▄▄─█▄─█─▄█▄─▄▄▀█");
            Console.WriteLine("██▀─▀███─▄▄▄██─▄█▀██─█▄▀─██▄▄▄▄─██─▄█▀█████─████─▄─▄█─███▀██─▄▀███─▄─▄█");
            Console.WriteLine("▀▄▄█▄▄▀▄▄▄▀▀▀▄▄▄▄▄▀▄▄▄▀▀▄▄▀▄▄▄▄▄▀▄▄▄▄▄▀▀▀▀▄▄▄▀▀▄▄▀▄▄▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▀▄▄▀");
            Console.WriteLine();

            //GÖR EN WHILE LOOP SOM KÖRS TILLS ANVÄNDAREN HAR KLICKAT PÅ AVSLUTA I MENYN
            bool running = true;
            while (running)  
            {
                int selectedOption = ShowMenu("Vad vill du göra?", new[]
                {
                    "Lägg till utgift",
                    "Visa alla utgifter",
                    "Visa summa per kategori",
                    "Ta bort samtliga utgifter",
                    "Avsluta"
                });
                Console.Clear();
                
                if (selectedOption == 4)
                {
                    running = false;
                }

                if (selectedOption == 0)
                {
                    
                    Console.WriteLine("DU HAR VALT ATT LÄGGA TILL EN UTGIFT");
                    //CALLA AD TO CART METOD
                    AskUserforExpenseParamters();
                    
                }
                if (selectedOption == 1)
                {
                    
                    Console.WriteLine("DU HAR VALT ATT VISA ALLA UTGIFTER");
                    //CALLA PRINT ALLA UTGIFTER METOD (LOOP)
                    Console.ReadLine();
                }

                if (selectedOption == 2)
                {
                    Console.WriteLine("DU HAR VALT ATT VISA SUMMA PER KATEGORI");
                    //CALLA PRINT SUMMA PER KATEGORI FUNKTION
                    Console.ReadLine();
                }
                if (selectedOption == 3)
                {
                    Console.WriteLine("DU HAR VALT ATT TA BORT SAMTLIGA UTFIGTER");
                    //CALLA TA BORT UGIFTER FUNKTIUON
                    Console.ReadLine();
                }
                
            }
            
            //AVSLUTA PROGRAMMET
            Console.WriteLine("███████████████████████████████████████████████████████████████████████");
            Console.WriteLine("█▄─▀─▄█▄─▄▄─█▄─▄▄─█▄─▀█▄─▄█─▄▄▄▄█▄─▄▄─███─▄─▄─█▄─▄▄▀█─▄▄▄─█▄─█─▄█▄─▄▄▀█");
            Console.WriteLine("██▀─▀███─▄▄▄██─▄█▀██─█▄▀─██▄▄▄▄─██─▄█▀█████─████─▄─▄█─███▀██─▄▀███─▄─▄█");
            Console.WriteLine("▀▄▄█▄▄▀▄▄▄▀▀▀▄▄▄▄▄▀▄▄▄▀▀▄▄▀▄▄▄▄▄▀▄▄▄▄▄▀▀▀▀▄▄▄▀▀▄▄▀▄▄▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▀▄▄▀");
            Console.WriteLine("TACK FÖR ATT DU ANVÄNT XPENSE TRCKR, HA EN FIN DAG");

            
        }

        // Return the sum of all expenses in the specified list, with or without VAT based on the second parameter.
        // This method *must* be in the program and *must* be used in both the main program and in the tests.
        public static decimal SumExpenses(List<Expense> expenses, bool includeVAT)
        {
            decimal sum = 0;
            // Implement the rest of this method here.
            return sum;
        }

        // Do not change this method.
        // For more information about ShowMenu: https://csharp.jakobkallin.com/large-exercises/
        public static int ShowMenu(string prompt, IEnumerable<string> options)
        {
            if (options == null || options.Count() == 0)
            {
                throw new ArgumentException("Cannot show a menu for an empty list of options.");
            }

            Console.WriteLine(prompt);

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            // Calculate the width of the widest option so we can make them all the same width later.
            int width = options.Max(option => option.Length);

            int selected = 0;
            int top = Console.CursorTop;
            for (int i = 0; i < options.Count(); i++)
            {
                // Start by highlighting the first option.
                if (i == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                var option = options.ElementAt(i);
                // Pad every option to make them the same width, so the highlight is equally wide everywhere.
                Console.WriteLine("- " + option.PadRight(width));

                Console.ResetColor();
            }
            Console.CursorLeft = 0;
            Console.CursorTop = top - 1;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(intercept: true).Key;

                // First restore the previously selected option so it's not highlighted anymore.
                Console.CursorTop = top + selected;
                string oldOption = options.ElementAt(selected);
                Console.Write("- " + oldOption.PadRight(width));
                Console.CursorLeft = 0;
                Console.ResetColor();

                // Then find the new selected option.
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Count() - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }

                // Finally highlight the new selected option.
                Console.CursorTop = top + selected;
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.ForegroundColor = ConsoleColor.White;
                string newOption = options.ElementAt(selected);
                Console.Write("- " + newOption.PadRight(width));
                Console.CursorLeft = 0;
                // Place the cursor one step above the new selected option so that we can scroll and also see the option above.
                Console.CursorTop = top + selected - 1;
                Console.ResetColor();
            }

            // Afterwards, place the cursor below the menu so we can see whatever comes next.
            Console.CursorTop = top + options.Count();

            // Show the cursor again and return the selected option.
            Console.CursorVisible = true;
            return selected;
        }
    }}