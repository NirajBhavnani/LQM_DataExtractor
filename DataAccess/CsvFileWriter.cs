using CsvHelper;
using Models;
using System.Globalization;

namespace DataAccess
{
    public class CsvFileWriter : ICsvFileWriter
    {
        public void WriteCsv(string filePath, List<OutputData> records)
        {
            using StreamWriter writer = new(filePath);
            using CsvWriter csv = new(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(records);
        }
    }
}
