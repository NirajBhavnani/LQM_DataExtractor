using DataAccess;
using Models;

namespace BusinessLogic
{
    public class BarclaysProcessor(ICsvFileReader fileReader, ICsvFileWriter fileWriter) : IBankProcessor
    {
        private readonly ICsvFileReader _fileReader = fileReader;
        private readonly ICsvFileWriter _fileWriter = fileWriter;

        public void ProcessCsv(string inputFilePath, string outputFilePath)
        {
            List<InputData> inputRecords = _fileReader.ReadCsv(inputFilePath);
            List<OutputData> outputRecords = [];

            foreach (InputData record in inputRecords)
            {
                // Barclays-specific parsing logic (e.g., extract PriceMultiplier or other complex fields)
                decimal contractSize = ParseAlgoParams();

                OutputData outputRecord = new()
                {
                    ISIN = record.ISIN,
                    CFICode = record.CFICode,
                    ContractSize = contractSize
                };

                outputRecords.Add(outputRecord);
            }

            _fileWriter.WriteCsv(outputFilePath, outputRecords);
        }

        private static decimal ParseAlgoParams()
        {
            // Barclays-specific logic for extracting PriceMultiplier from AlgoParams
            return 25m; // Example for demonstration
        }
    }
}
