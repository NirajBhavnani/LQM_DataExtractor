using CsvHelper;
using Models;
using System.Globalization;

namespace DataAccess
{
    public class CsvFileReader : ICsvFileReader
    {
        public List<InputData> ReadCsv(string filePath)
        {
            List<InputData> records = [];

            using (StreamReader reader = new(filePath))
            using (CsvReader csv = new(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<InputData>().ToList();
            }

            return records;
        }
    }
}
