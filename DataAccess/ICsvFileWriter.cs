using Models;

namespace DataAccess
{
    public interface ICsvFileWriter
    {
        void WriteCsv(string filePath, List<OutputData> records);
    }
}
