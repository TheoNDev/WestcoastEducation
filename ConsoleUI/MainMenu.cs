using System;
using WestcoastEducation.Models;

namespace WestcoastEducation.ConsoleUI;

public class MainMenu(School school)
{
    public void Run()
    {
        OpenMainMenu();
    }
    private void OpenMainMenu()
    {
        Console.WriteLine(" ");
        Console.WriteLine("WESTCOAST EDUCATION");
        Console.WriteLine("----------------------------");
        Console.WriteLine("Huvudmeny");
        Console.WriteLine(" ");
        Console.WriteLine("1. Kurser");
        Console.WriteLine("2. Studenter");
        Console.WriteLine("3. Personal");
        Console.WriteLine(" ");
        Console.WriteLine("0. Avsluta");
        Console.WriteLine("----------------------------");
        Console.WriteLine(" ");
        var key = Console.ReadKey(true);

        switch (key.Key)
        {
            case ConsoleKey.D1:
                CourseMenu courseMenu = new(school);
                courseMenu.Run();
                break;
            case ConsoleKey.D2:
                StudentMenu studentMenu = new(school);
                studentMenu.Run();
                break;
            case ConsoleKey.D3:
                StaffMenu staffMenu = new(school);
                staffMenu.Run();
                break;
            case ConsoleKey.D0:
                break;
            default:
                break;
        }
    }

}
