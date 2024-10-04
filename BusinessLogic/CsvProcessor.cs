using DataAccess;
using Models;

namespace BusinessLogic
{
    public class CsvProcessor(ICsvFileReader fileReader, ICsvFileWriter fileWriter)
    {
        private readonly ICsvFileReader _fileReader = fileReader;
        private readonly ICsvFileWriter _fileWriter = fileWriter;

        public void ProcessCsv(string inputFilePath, string outputFilePath)
        {
            // Read input CSV
            List<InputData> inputRecords = _fileReader.ReadCsv(inputFilePath);

            // Prepare list for output records
            List<OutputData> outputRecords = [];

            foreach (InputData record in inputRecords)
            {
                // Parse AlgoParams field and extract PriceMultiplier
                decimal contractSize = ParseAlgoParams(record.AlgoParams);

                OutputData outputRecord = new()
                {
                    ISIN = record.ISIN,
                    CFICode = record.CFICode,
                    Venue = record.Venue,
                    ContractSize = contractSize // Use extracted value
                };

                outputRecords.Add(outputRecord);
            }

            // Write output records to output CSV
            _fileWriter.WriteCsv(outputFilePath, outputRecords);
        }

        private static decimal ParseAlgoParams(string algoParams)
        {
            // Split AlgoParams into key-value pairs separated by "|;"
            string[] keyValuePairs = algoParams.Split('|', ';');
            for (int i = 0; i < keyValuePairs.Length; i++)
            {
                // Find the key-value pair for PriceMultiplier
                if (keyValuePairs[i].Contains("PriceMultiplier"))
                {
                    string[] keyValue = keyValuePairs[i].Split(':');
                    if (keyValue.Length == 2 && keyValue[0] == "PriceMultiplier")
                    {
                        // Return the parsed value as decimal
                        if (decimal.TryParse(keyValue[1], out decimal priceMultiplier))
                        {
                            return priceMultiplier;
                        }
                    }
                }
            }

            // Default value if PriceMultiplier not found
            return 0m;
        }
    }
}
