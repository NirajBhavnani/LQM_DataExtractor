using BusinessLogic;
using DataAccess;

Console.WriteLine("Please enter the bank name (e.g., Barclays, HSBC):");
string bankName = Console.ReadLine();

while (string.IsNullOrEmpty(bankName))
{
    Console.WriteLine("Bank name cannot be empty. Please enter a valid bank name:");
    bankName = Console.ReadLine();
}

Console.WriteLine("Please enter the full path of the input CSV file:");
string inputFilePath = Console.ReadLine();

while (string.IsNullOrEmpty(inputFilePath) || !File.Exists(inputFilePath))
{
    Console.WriteLine("Invalid input file path. Please enter a valid path:");
    inputFilePath = Console.ReadLine();
}

Console.WriteLine("Please enter the full path where the output CSV should be saved:");
string outputFilePath = Console.ReadLine();

try
{
    ICsvFileReader fileReader = new CsvFileReader();
    ICsvFileWriter fileWriter = new CsvFileWriter();

    // Initialize the bank processor factory
    BankProcessorFactory processorFactory = new(fileReader, fileWriter);

    // Get the appropriate bank processor based on the bank name
    IBankProcessor bankProcessor = processorFactory.GetProcessor(bankName);

    // Read the input CSV file and create output CSV using the selected bank processor
    bankProcessor.ProcessCsv(inputFilePath, outputFilePath);

    Console.WriteLine($"Processing completed. Output written to: {outputFilePath}");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
