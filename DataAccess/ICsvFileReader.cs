using Models;

namespace DataAccess
{
    public interface ICsvFileReader
    {
        List<InputData> ReadCsv(string filePath);
    }
}
