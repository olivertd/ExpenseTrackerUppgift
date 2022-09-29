

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ExpenseTrackerUppgift
{
    
    public class Expense
    {
        public string ExpenseNamn { get; set; }
        public decimal ExpensePris { get; set; } 
        public string ExpenseKategori { get; set; }
        
    }

    //create decimal variables that can be used in SumExpenses and SumExpensesCategory
    //IF VAT CHANGES, CHANGE THE VALUES HERE
    public static class DefineradMoms
    {
        public static decimal UtbildningMoms = 0.0m;
        public static decimal LivsemedelMoms = 0.16m;
        public static decimal UnderhållningMoms = 0.12m;
        public static decimal ÖvrigtMoms = 0.25m;
        
    }
    
    public class Program
    {
        
        public static List<Expense> expenses = new List<Expense>();

        //FUNKTION FÖR ATT LÄGGA TILL NY EXPENSE
        public static void AddExpense(string expenseNamn, decimal expensePris, string expenseKategori) 
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

            Console.Write("Vad har din utgift för pris Kr (inkl moms)?: ");
            decimal utgiftPris = Convert.ToDecimal(Console.ReadLine()); 

            string utgiftKategori = "";

            bool running = true;

            while (running)
            {
                int selectedOption = ShowMenu("VILKEN KATEGORI TILLHÖR DIN UTGIFT?", new[]
                {
                    "Utbildning",
                    "Livsemedel",
                    "Underhållning",
                    "Övrigt"
                });

                if (selectedOption == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Du har valt Utbildning");
                    utgiftKategori = "Utbildning";
                    break;
                }
                else if (selectedOption == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Du har valt Livsmedel");
                    utgiftKategori = "Livsmedel";
                    break;
                }
                else if (selectedOption == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Du har valt Underhållning");
                    utgiftKategori = "Underhållning";
                    break;
                }
                else if (selectedOption == 3)
                {
                    Console.Clear();
                    Console.WriteLine("Du har valt Övrigt");
                    utgiftKategori = "Övrigt";
                    break;
                }
                else
                {
                    Console.WriteLine("Du har valt fel");
                }

            }

            AddExpense(utgiftNamn, utgiftPris, utgiftKategori);

        }


        public static decimal SumExpenses(List<Expense> expenses, bool includeVAT)
        {
            decimal sum = 0;
            foreach (Expense expense in expenses)
            {
                if (includeVAT == false)
                {
                    if (expense.ExpenseKategori == "Utbildning")
                    {
                        sum += expense.ExpensePris - (expense.ExpensePris * DefineradMoms.UtbildningMoms); //doesn't reflect realistic VAT value
                    }
                    else if (expense.ExpenseKategori == "Livsmedel")
                    {
                        sum += expense.ExpensePris - (expense.ExpensePris * DefineradMoms.LivsemedelMoms); //doesn't reflect realistic VAT value
                    }
                    else if (expense.ExpenseKategori == "Underhållning")
                    {
                        sum += expense.ExpensePris - (expense.ExpensePris * DefineradMoms.UnderhållningMoms); //doesn't reflect realistic VAT value
                    }
                    else
                    {
                        sum += expense.ExpensePris - (expense.ExpensePris * DefineradMoms.ÖvrigtMoms); //doesn't reflect realistic VAT value
                    }
                }
                else
                {
                    sum += expense.ExpensePris;
                }
            }
            return sum;
        }
        public static void SumExpensesCategory()
        {
            bool running = true;
            while (running)
            {
                MakeSpaceForUserMenu();
                int selectedOptionChooseCat = ShowMenu("VILKEN KATEGORI TILLHÖR DIN UTGIFT?", new[]
                {
                    "Utbildning",
                    "Livsemedel",
                    "Underhållning",
                    "Övrigt",
                    "Avbryt visa summa per kategori"
                });
                if (selectedOptionChooseCat == 0)
                {
                    var sum = expenses.Where(x => x.ExpenseKategori == "Utbildning").Sum(x => x.ExpensePris);
                    Console.Clear();
                    Console.WriteLine($"Summan av dina utbildningsutgifter (inkl moms) är {sum}kr");
                    Console.WriteLine($"Summan av dina utbildningsutgifter (EX moms) är {sum * DefineradMoms.UtbildningMoms}kr");
                }
                else if (selectedOptionChooseCat == 1)
                {
                    var sum = expenses.Where(x => x.ExpenseKategori == "Livsmedel").Sum(x => x.ExpensePris);
                    Console.Clear();
                    Console.WriteLine($"Summan av dina livsmedelsutgifter (inkl moms) är {sum}kr");
                    Console.WriteLine($"Summan av dina livsmedelsutgifter (EX moms) är {sum - sum * DefineradMoms.LivsemedelMoms }kr"); 
                }
                else if (selectedOptionChooseCat == 2)
                {
                    var sum = expenses.Where(x => x.ExpenseKategori == "Underhållning").Sum(x => x.ExpensePris);
                    Console.Clear();
                    Console.WriteLine($"Summan av dina underhållningsutgifter (inkl moms) är {sum}kr");
                    Console.WriteLine($"Summan av dina underhållningsutgifter (EX moms) är {sum - sum * DefineradMoms.UnderhållningMoms}kr"); 
                }
                else if (selectedOptionChooseCat == 3)
                {
                    var sum = expenses.Where(x => x.ExpenseKategori == "Övrigt").Sum(x => x.ExpensePris);
                    Console.Clear();
                    Console.WriteLine($"Summan av dina övriga utgifter är (inkl moms) {sum}kr");
                    Console.WriteLine($"Summan av dina övriga (EX moms) är {sum - sum * DefineradMoms.ÖvrigtMoms}kr");
                }
                else if (selectedOptionChooseCat == 4)
                {
                    Console.Clear();
                    Console.WriteLine("Du har valt att avbryta att visa summa per kategori");
                    running = false;
                }
            }

        }

        //FUNKTION FÖR ATT GÖRA SPACE FÖR USERSELECTIONMENY
        static void MakeSpaceForUserMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
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
                MakeSpaceForUserMenu();
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
                    Console.WriteLine("UTGIFTEN HAR LÄGGTS TILL");


                }
                if (selectedOption == 1)
                {

                    Console.WriteLine("DU HAR VALT ATT VISA ALLA UTGIFTER");
                    // print the list of expenses
                    Console.WriteLine("UTGIFTERNA ÄR:");

                    foreach (var expense in expenses)
                    {
                        Console.WriteLine(
                            $"Namn: {expense.ExpenseNamn},  Pris inkl moms: {expense.ExpensePris}Kr,  Kategori: {expense.ExpenseKategori}");
                    }
                    int addedByUser = expenses.Count;
                    Console.WriteLine();
                    Console.WriteLine("ANTAL UPPGIFTER: " + addedByUser);
                    Console.WriteLine($"Summa:  {Math.Round(SumExpenses(expenses, true), 2)} KR  ({Math.Round(SumExpenses(expenses, false), 2)} KR exkl. moms)");
                }

                if (selectedOption == 2)
                {
                    Console.WriteLine("DU HAR VALT ATT VISA SUMMA PER KATEGORI");
                    //CALLA PRINT SUMMA PER KATEGORI FUNKTION
                    SumExpensesCategory();
                }
                
                if (selectedOption == 3)
                {
                    bool runningDelete = true;
                    while (runningDelete)
                    {
                        int selectedOptionDelete = ShowMenu("ÄR DU SÄKER PÅ ATT DU VILL TA BORT ALLA UPPGIFTER? ", new[]
                        {
                            "JA",
                            "NEJ",
                        });
                        if (selectedOptionDelete == 0)
                        {
                            Console.WriteLine("DU HAR VALT ATT TA BORT SAMTLIGA UTFIGTER");
                            expenses.Clear();
                            runningDelete = false;
                        }
                        else break;
                    }
                }

            }

            //AVSLUTA PROGRAMMET
            Console.WriteLine("███████████████████████████████████████████████████████████████████████");
            Console.WriteLine("█▄─▀─▄█▄─▄▄─█▄─▄▄─█▄─▀█▄─▄█─▄▄▄▄█▄─▄▄─███─▄─▄─█▄─▄▄▀█─▄▄▄─█▄─█─▄█▄─▄▄▀█");
            Console.WriteLine("██▀─▀███─▄▄▄██─▄█▀██─█▄▀─██▄▄▄▄─██─▄█▀█████─████─▄─▄█─███▀██─▄▀███─▄─▄█");
            Console.WriteLine("▀▄▄█▄▄▀▄▄▄▀▀▀▄▄▄▄▄▀▄▄▄▀▀▄▄▀▄▄▄▄▄▀▄▄▄▄▄▀▀▀▀▄▄▄▀▀▄▄▀▄▄▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▀▄▄▀");
            Console.WriteLine("TACK FÖR ATT DU ANVÄNT XPENSE TRCKR, HA EN FIN DAG");

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

    }
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void WithVAT()
        {
            List<Expense> expenses = new List<Expense>();
            expenses.Add(new Expense
            {
                ExpenseNamn = "Mat",
                ExpensePris = 200,
                ExpenseKategori = "Livsmedel"
            });
            expenses.Add(new Expense
            {
                ExpenseNamn = "Spel",
                ExpensePris = 600,
                ExpenseKategori = "Underhållning"
            });
            expenses.Add(new Expense
            {
                ExpenseNamn = "Skola",
                ExpensePris = 5000,
                ExpenseKategori = "Utbildning"
            });
            expenses.Add(new Expense
            {
                ExpenseNamn = "Annat",
                ExpensePris = 400,
                ExpenseKategori = "Övrigt"
            });
            decimal sum = Program.SumExpenses(expenses, true);
            Assert.AreEqual(6200, sum);
        }

        [TestMethod]
        public void WithoutVAT()
        {
            List<Expense> expenses = new List<Expense>();
            expenses.Add(new Expense
            {
                ExpenseNamn = "Mat",
                ExpensePris = 200,
                ExpenseKategori = "Livsmedel"
            });
            expenses.Add(new Expense
            {
                ExpenseNamn = "Spel",
                ExpensePris = 600,
                ExpenseKategori = "Underhållning"
            });
            expenses.Add(new Expense
            {
                ExpenseNamn = "Skola",
                ExpensePris = 5000,
                ExpenseKategori = "Utbildning"
            });
            expenses.Add(new Expense
            {
                ExpenseNamn = "Annat",
                ExpensePris = 400,
                ExpenseKategori = "Övrigt"
            });
            decimal sum = Program.SumExpenses(expenses, false);
            Assert.AreEqual(6016, sum);
        }

        [TestMethod]
        public void NoVATonlyEduc()
        {
            List<Expense> expenses = new List<Expense>();
            expenses.Add(new Expense
            {
                ExpenseNamn = "Skola",
                ExpensePris = 5000,
                ExpenseKategori = "Utbildning"
            });
            expenses.Add(new Expense
            {
                ExpenseNamn = "Läsmaterial",
                ExpensePris = 600,
                ExpenseKategori = "Utbildning"
            });
            expenses.Add(new Expense
            {
                ExpenseNamn = "Kvällskurs",
                ExpensePris = 1000,
                ExpenseKategori = "Utbildning"
            });
            
            decimal sum = Program.SumExpenses(expenses, false);
            Assert.AreEqual(6600, sum);
        }
    }
}