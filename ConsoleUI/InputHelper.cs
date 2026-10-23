using System;
using System.Globalization;
using WestcoastEducation.Enums;
using WestcoastEducation.InputModels;
using WestcoastEducation.Models;

namespace WestcoastEducation.ConsoleUI;

public static class InputHelper
{
    public static PersonalInfo ReadPersonalInfo()
    {
        string firstName = ReadString("Förnamn");
        string lastName = ReadString("Efternamn");
        string phoneNumber = ReadString("Telefonnummer");
        string socialSecurityNumber = ReadString("Personnummer");
        string streetName = ReadString("Gatuaddress");
        string zipCode = ReadString("Postnummer");
        string city = ReadString("Ort");

        Address address = new(streetName, zipCode, city);

        PersonalInfo personalInfo = new(firstName, lastName, phoneNumber, socialSecurityNumber, address);

        return personalInfo;
    }
    public static string ReadFieldOfKnowledge()
    {
        string fieldOfKnowledge = ReadString("Kunskapområde");
        return fieldOfKnowledge;
    }

    public static string ReadString(string prompt)
    {
        while (true)
        {
            Console.WriteLine(" ");
            Console.Write($"{prompt}: ");
            var input = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            Console.WriteLine(" ");
            Console.WriteLine($"{prompt} kan inte vara tomt. Försök igen.");
            Console.WriteLine(" ");
        }
    }
    public static int ReadNumber(string prompt)
    {
        while (true)
        {
            Console.WriteLine(" ");
            Console.Write($"{prompt}: ");
            var input = Console.ReadLine();

            if (Int32.TryParse(input, out int result))
            {
                return result;
            }

            Console.WriteLine(" ");
            Console.WriteLine("Måste vara ett giltigt heltal. Försök igen.");
            Console.WriteLine(" ");
        }
    }
    public static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            Console.WriteLine(" ");
            Console.Write($"{prompt} (ÅÅÅÅ-MM-DD): ");
            var input = Console.ReadLine();

            if (DateTime.TryParseExact(
                input,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime date
            ))
            {
                return date;
            }

            Console.WriteLine(" ");
            Console.WriteLine("Ogiltigt datum. Försök igen.");
            Console.WriteLine(" ");
        }
    }

}
