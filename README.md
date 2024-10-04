# LQM_DataExtractor
## Project Overview
LQM_DataExtractor is a .NET 8.0-based command-line application designed to process CSV files from multiple banks with different schemas. The project extracts specific fields such as `ISIN`, `CFICode`, `Venue`, and the complex `PriceMultiplier` field from the `AlgoParams` column, and outputs a standardized CSV file.  

This project follows a layered architecture with SOLID principles to ensure maintainability, scalability, and ease of extension.

## Project Architecture
```
LQM_DataExtractor/
│
├── DataExtractor.ConsoleApp/       # Console app entry point
│   └── Program.cs                  # Entry point for the application
│
├── DataExtractor.BusinessLogic/    # Core business logic
│   └── CsvProcessor.cs             # Processes CSV records and extracts relevant fields
│
├── DataExtractor.DataAccess/       # File reading and writing logic
│   └── ICsvFileReader.cs           # Interface for reading input CSV files
│   └── CsvFileReader.cs            # Reads input CSV files
│   └── ICsvFileWriter.cs           # Interface for writing output CSV files
│   └── CsvFileWriter.cs            # Writes output CSV files
│
└── DataExtractor.Models/           # Data models representing input and output CSVs
    └── InputData.cs                # Model for input CSV records
    └── OutputData.cs               # Model for output CSV records

```

## Prerequisites:
- .NET 8.0 SDK
- CSVHelper library (installed via NuGet)
