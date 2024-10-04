using DataAccess;

namespace BusinessLogic
{
    public class BankProcessorFactory(ICsvFileReader fileReader, ICsvFileWriter fileWriter)
    {
        private readonly ICsvFileReader _fileReader = fileReader;
        private readonly ICsvFileWriter _fileWriter = fileWriter;

        public IBankProcessor GetProcessor(string bankName)
        {
            return bankName switch
            {
                "Barclays" => new BarclaysProcessor(_fileReader, _fileWriter),
                "HSBC" => new HSBCProcessor(_fileReader, _fileWriter),
                _ => new CsvProcessor(_fileReader, _fileWriter)  // Default processor
            };
        }
    }
}
