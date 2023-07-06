using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Konsolen Input/Output
        Console.WriteLine("Geben Sie den Pfad zum Verzeichnis ein:");
        string directoryPath = Console.ReadLine();

        Console.ForegroundColor= ConsoleColor.Blue;
        Console.WriteLine("Geben Sie das neue/alte Präfix ein:");
        Console.ForegroundColor = ConsoleColor.White;
        string newPrefix = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Geben Sie die neue/alte Dateierweiterung ein:");
        Console.ForegroundColor = ConsoleColor.White;
        string newExtension = Console.ReadLine();

        // Optionsauswahl 
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Wählen Sie eine Option:");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("1. Präfix ändern");
        Console.WriteLine("2. Präfix entfernen");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("3. Dateierweiterung ändern");
        Console.WriteLine("4. Dateierweiterung entfernen");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("5. Nummerierung hinzufügen");
        Console.ForegroundColor = ConsoleColor.White;
        string option = Console.ReadLine();

        // Exceptionhandling mit Try/Catch, im bad case wird die exception message angezeigt
        try
        {
            RenameFiles(directoryPath, newPrefix, newExtension, option);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dateinamen wurden erfolgreich geändert!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ein Fehler ist aufgetreten: " + ex.Message);
        }

        Console.ReadLine();
    }

    // Umbennenugsmethode 
    static void RenameFiles(string directoryPath, string newPrefix, string newExtension, string option)
    {
        // Array wird deklariert um Dateien zu speichern und später abzurufen
        DirectoryInfo directory = new DirectoryInfo(directoryPath);
        FileInfo[] files = directory.GetFiles();

        int num = 1;
        
        // Die Schleife geht über jedes Element, dass im Array gespeichert ist
        foreach (FileInfo file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file.Name);
            string extension = file.Extension;

            if (option == "1")
            {
                fileName = newPrefix + fileName;
            }
            else if (option == "2")
            {
                fileName = fileName.Remove(0, newPrefix.Length);
            }
            else if (option == "3")
            {
                extension = newExtension.StartsWith(".") ? newExtension : "." + newExtension;
            }
            else if (option == "4")
            {
                fileName = Path.GetFileNameWithoutExtension(fileName);
                extension = string.Empty;
            }
            else if (option == "5")
            {
                fileName = num.ToString().PadLeft(4, '0');
                num++;
            }

            // Neuer Pfad, mit neuen Dateinamen
            string newFilePath = Path.Combine(directory.FullName, fileName + extension);
            file.MoveTo(newFilePath);
        }

        // Array mit Subdirectories
        DirectoryInfo[] subdirectories = directory.GetDirectories();

        // Schleife geht über jedes Element des Arrays
        foreach (DirectoryInfo subdirectory in subdirectories)
        {
            RenameFiles(subdirectory.FullName, newPrefix, newExtension, option);
        }
    }
}
