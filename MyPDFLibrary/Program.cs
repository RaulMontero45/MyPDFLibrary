using System;
using System.Diagnostics;
using System.IO;

namespace MyPDFLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            string booksDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Books");

            // Display available options
            Console.WriteLine("Welcome to My PDF Book Library");
            Console.WriteLine("1. View Books");
            Console.WriteLine("2. Add a Book");
            Console.WriteLine("3. Exit");

            // Handle user input
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ViewBooks(booksDirectory);
                    break;
                case 2:
                    AddBook(booksDirectory);
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void ViewBooks(string directory)
        {
            string[] books = Directory.GetFiles(directory);
            Console.WriteLine("Available Books:");
            for (int i = 0; i < books.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(books[i])}");
            }

            Console.WriteLine($"{books.Length + 1}. Go back");

            Console.Write("Enter the number of the book to view or 'Go back': ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int selection))
            {
                if (selection >= 1 && selection <= books.Length)
                {
                    string selectedBookPath = books[selection - 1];
                    Console.WriteLine($"Opening {Path.GetFileName(selectedBookPath)}...");

                    // Provide the full path to chrome.exe
                    string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

                    // Use Chrome to open the PDF file
                    Process.Start(chromePath, $"--new-window \"{selectedBookPath}\"");
                }
                else if (selection == books.Length + 1)
                {
                    // Go back
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void AddBook(string directory)
        {
            Console.Write("Enter the name of the book file (with extension): ");
            string fileName = Console.ReadLine();
            string filePath = Path.Combine(directory, fileName);

            if (File.Exists(filePath))
            {
                Console.WriteLine("Book already exists.");
                return;
            }

            Console.Write("Upload the book file: ");
            string uploadedFilePath = Console.ReadLine();

            File.Move(uploadedFilePath, filePath);

            Console.WriteLine("Book added successfully.");
        }
    }
}
