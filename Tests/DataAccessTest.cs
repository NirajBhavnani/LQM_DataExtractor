using CsvHelper;
using DataAccess;
using Models;
using System.Globalization;

namespace Tests
{
    [TestFixture]
    public class DataAccessTest
    {
        private CsvFileReader _csvFileReader;
        private CsvFileWriter _csvFileWriter;

        [SetUp]
        public void Setup()
        {
            _csvFileReader = new CsvFileReader();
            _csvFileWriter = new CsvFileWriter();
        }

        [Test]
        public void ReadCsvShouldReturnListOfInputDataWhenValidCsvProvided()
        {
            string csvData = "ISIN,CFICode,Venue,AlgoParams\n" +
                          "DE000C4SA5W8,FFICSX,XEUR,PriceMultiplier:25.0|;\n" +
                          "PL0GF0019331,FFICSX,WDER,PriceMultiplier:20.0|;";

            // Use StringReader to simulate file reading from a CSV file
            // Simulate reading the CSV file using StringReader instead of an actual file
            using StringReader reader = new(csvData);
            using CsvReader csv = new(reader, CultureInfo.InvariantCulture);

            List<InputData> expectedRecords = csv.GetRecords<InputData>().ToList();

            List<InputData> actualRecords = _csvFileReader.ReadCsv("C:\\Users\\nbhavna\\Downloads\\Temp\\LQM_TechTest_DataExtractor\\Input_Example.csv");

            Assert.That(actualRecords, Has.Count.EqualTo(expectedRecords.Count));
            Assert.That(actualRecords[0].ISIN, Is.EqualTo(expectedRecords[0].ISIN));
        }

        [Test]
        public void WriteCsvShouldWriteRecordsToFileWhenCalledWithValidData()
        {
            List<OutputData> outputRecords =
            [
                new OutputData { ISIN = "DE000C4SA5W8", CFICode = "FFICSX", Venue = "XEUR", ContractSize = 25.0m },
                new OutputData { ISIN = "PL0GF0019331", CFICode = "FFICSX", Venue = "WDER", ContractSize = 20.0m }
            ];

            // Create a temporary file path for the test
            string tempFilePath = Path.GetTempFileName();

            try
            {
                _csvFileWriter.WriteCsv(tempFilePath, outputRecords);

                string actualCsv = File.ReadAllText(tempFilePath);

                // Assert: Verify the output matches the expected CSV format
                string expectedCsv = "ISIN,CFICode,Venue,ContractSize\r\n" +
                                     "DE000C4SA5W8,FFICSX,XEUR,25.0\r\n" +
                                     "PL0GF0019331,FFICSX,WDER,20.0\r\n";

                Assert.That(actualCsv, Is.EqualTo(expectedCsv));
            }
            finally
            {
                // Cleanup: Delete the temporary file after the test
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }
    }
}
