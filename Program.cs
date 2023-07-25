using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentDataSystem
{
    public class Student
    {
        public string Name { get; set; }
        public string Class { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "student data.txt";

            // Read student data from the text file and store it in a list
            List<Student> students = ReadStudentDataFromFile(filePath);

            // Display the unsorted student data
            Console.WriteLine("===== Unsorted Student Data =====");
            DisplayStudentData(students);

            // Sort the student data by name
            students = SortStudentsByName(students);

            // Display the sorted student data
            Console.WriteLine("\n===== Sorted Student Data =====");
            DisplayStudentData(students);

            // Search for a student by name
            Console.Write("\nEnter the name of the student to search: ");
            string searchName = Console.ReadLine();
            List<Student> searchResults = SearchStudentByName(students, searchName);

            if (searchResults.Count > 0)
            {
                Console.WriteLine("\n===== Search Results =====");
                DisplayStudentData(searchResults);
            }
            else
            {
                Console.WriteLine("\nNo students found with the given name.");
            }

            Console.ReadLine();
        }

        static List<Student> ReadStudentDataFromFile(string filename)
        {
            List<Student> students = new List<Student>();
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        if (data.Length == 2)
                        {
                            students.Add(new Student
                            {
                                Name = data[0].Trim(),
                                Class = data[1].Trim()
                            });
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found: " + filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return students;
        }

        static List<Student> SortStudentsByName(List<Student> students)
        {
            return students.OrderBy(s => s.Name).ToList();
        }

        static List<Student> SearchStudentByName(List<Student> students, string searchName)
        {
            return students.FindAll(s => s.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));
        }

        static void DisplayStudentData(List<Student> students)
        {
            Console.WriteLine("Name\t\tClass");
            foreach (Student student in students)
            {
                Console.WriteLine($"{student.Name}\t\t{student.Class}");
            }
        }
    }
}

