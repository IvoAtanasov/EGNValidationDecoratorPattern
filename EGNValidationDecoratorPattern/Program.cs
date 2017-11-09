using EGNValidation;
using EGNValidation.DecoratorInstances;
using System;


namespace EGNValidationDecoratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            string toContinue = "y";
            string egn = string.Empty;
            while (toContinue == "y")
            {
                Console.WriteLine("Enter EGN:");
                egn=Console.ReadLine();
                EgnAbstractValidation validation = new BasicEgnValidation(egn);
                validation = new JuniorDecorator(validation);
                if (validation.Validate())
                {
                    Console.WriteLine("Correct EGN ");
                }
                else
                {
                    Console.Write(string.Format("Error for EGN {0} is {1} :", egn, validation.GetMessage()));
                }
                Console.WriteLine("Type 'y' if you like to continue...");
                toContinue = Console.ReadLine().ToLower();
            }
        }
    }
}
