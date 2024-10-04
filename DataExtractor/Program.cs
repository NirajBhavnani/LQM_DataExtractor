using BusinessLogic;
using DataAccess;

Console.WriteLine("Please enter the full path of the input CSV file:");
string inputFilePath = Console.ReadLine();

while (string.IsNullOrEmpty(inputFilePath) || !File.Exists(inputFilePath))
{
    Console.WriteLine("Invalid input file path. Please enter a valid path:");
    inputFilePath = Console.ReadLine();
}

// Ask the user for the output file path
Console.WriteLine("Please enter the full path where the output CSV should be saved:");
string outputFilePath = Console.ReadLine();

try
{
    ICsvFileReader fileReader = new CsvFileReader();
    ICsvFileWriter fileWriter = new CsvFileWriter();

    CsvProcessor csvProcessor = new(fileReader, fileWriter);
    Console.WriteLine("Processing the CSV file...");

    // Read the input CSV file
    csvProcessor.ProcessCsv(inputFilePath, outputFilePath);

    Console.WriteLine($"Processing completed. Output written to: {outputFilePath}");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
