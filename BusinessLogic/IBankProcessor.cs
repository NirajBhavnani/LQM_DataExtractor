namespace BusinessLogic
{
    public interface IBankProcessor
    {
        void ProcessCsv(string inputFilePath, string outputFilePath);
    }
}
