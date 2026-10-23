using System;
using WestcoastEducation.Enums;
using WestcoastEducation.Models;

namespace WestcoastEducation.ConsoleUI;

public class CourseMenu(School school)
{
    public void Run()
    {
        OpenCourseMenu();
    }

    private void OpenCourseMenu()
    {
        Console.Clear();
        Console.WriteLine(" ");
        Console.WriteLine("KURSHANTERING");
        Console.WriteLine("----------------------------");
        Console.WriteLine("1. Visa alla kurser");
        Console.WriteLine("2. Lägg till kurs");
        Console.WriteLine("3. Ta bort kurs");
        Console.WriteLine(" ");
        Console.WriteLine("0. Tillbaka");
        Console.WriteLine("----------------------------");
        Console.WriteLine(" ");
        while (true)
        {
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    ListCourses();
                    return;
                case ConsoleKey.D2:
                    AddCourse();
                    return;
                case ConsoleKey.D3:
                    RemoveCourse();
                    return;
                case ConsoleKey.D0:
                    return;
                default:
                    Console.WriteLine(" ");
                    Console.WriteLine("Måste välja ett av alternativen. Försök igen.");
                    Console.WriteLine(" ");
                    break;
            }
        }
    }
    private void ListCourses()
    {
        IReadOnlyList<Course> courses = school.GetCourses();
        foreach (var c in courses)
        {
            Console.WriteLine(c);
        }
    }
    private void AddCourse()
    {
        string courseNumber = InputHelper.ReadString("Kursnummer");
        string title = InputHelper.ReadString("Namn");
        int duration = InputHelper.ReadNumber("Längd (veckor)");
        DateTime startDate = InputHelper.ReadDate("Startdatum");
        DateTime endDate = InputHelper.ReadDate("Slutdatum");
        CourseLocation location = ReadCourseLocation();


        Course course = new(courseNumber, title, duration, startDate, endDate, location);

        bool added = school.AddCourse(course);

        if (added)
        {
            Console.WriteLine(" ");
            Console.WriteLine($"{title} har lagts till som kurs");
            Console.WriteLine(" ");
        }
        else
        {
            Console.WriteLine(" ");
            Console.WriteLine($"En kurs med kursnummer {courseNumber} finns redan.");
            Console.WriteLine(" ");
        }

    }
    private void RemoveCourse()
    {
        Console.WriteLine(" ");
        Console.WriteLine("TA BORT KURS");
        Console.WriteLine("----------------------------");
        IReadOnlyList<Course> courses = school.GetCourses();
        int i = 1;
        foreach (var c in courses)
        {
            Console.WriteLine($"{i}. [{c.CourseNumber}] {c.Title} - {c.Location}");
            i++;
        }
        Console.WriteLine(" ");
        Console.WriteLine("0. Avbryt");
        Console.WriteLine(" ");

        var key = Console.ReadKey(true);

        if (key.Key == ConsoleKey.D0)
        {
            return;
        }

        if (Int32.TryParse(key.KeyChar.ToString(), out int result))
        {
            while (true)
            {
                Course course = courses[result - 1];
                Console.WriteLine(" ");
                Console.WriteLine("Du har valt:");
                Console.WriteLine($"[{course.CourseNumber}] {course.Title} - {course.Location}");
                Console.WriteLine(" ");

                Console.WriteLine("Är du säker på att du vill ta bort kursen? (j/n):");
                var keyConfirm = Console.ReadKey(true);

                switch (keyConfirm.Key)
                {
                    case ConsoleKey.J:
                        school.DeleteCourse(course);
                        Console.WriteLine("Kursen är borttagen.");
                        return;
                    case ConsoleKey.N:
                        return;
                    default:
                        Console.WriteLine(" ");
                        Console.WriteLine("Måste välja ett av alternativen. Försök igen.");
                        Console.WriteLine(" ");
                        break;
                }

            }
        }
    }


    private static CourseLocation ReadCourseLocation()
    {
        Console.WriteLine(" ");
        Console.WriteLine("Undervisningsform:");
        Console.WriteLine("1. Distans");
        Console.WriteLine("2. Klassrum");
        Console.WriteLine(" ");
        while (true)
        {
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    return CourseLocation.Distans;
                case ConsoleKey.D2:
                    return CourseLocation.Klassrum;
                default:
                    Console.WriteLine(" ");
                    Console.WriteLine("Måste välja ett av alternativen. Försök igen.");
                    Console.WriteLine(" ");
                    break;
            }
        }
    }

}
