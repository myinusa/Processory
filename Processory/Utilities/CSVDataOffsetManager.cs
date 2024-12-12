using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using Processory.Exceptions;
using static Processory.Errors.ErrorMessages;

namespace Processory.Utilities {
    public class CSVDataOffsetManager {
        private readonly string nameOfCSVFile;
        private readonly string csvPath;
        private readonly ILogger logger;

        public CSVDataOffsetManager(ILoggerFactory loggerFactory, string nameOfCSVFile, string csvPath) {
            logger = loggerFactory.CreateLogger<CSVDataOffsetManager>();
            this.nameOfCSVFile = nameOfCSVFile;
            this.csvPath = csvPath;
            if (nameOfCSVFile.Any()) {
                LoadRowsFromCsv();
            }
        }

        public static List<Row> LoadedRows { get; private set; } = [];

        public string ClientPath => GetClientPath();
        public string AddressPath => Path.Combine(ClientPath, nameOfCSVFile);

        /// <summary>
        /// Loads rows from the CSV file if not already loaded.
        /// </summary>
        public void LoadRowsFromCsv() {
            logger.LogDebug("Loading CSV data from: {Path}", AddressPath);
            if (LoadedRows.Any()) return;

            try {
                using var reader = new StreamReader(AddressPath);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) {
                    PrepareHeaderForMatch = args => args.Header.Trim(),
                    BadDataFound = null // Ignore bad data
                });
                csv.Context.RegisterClassMap<RowMap>();
                LoadedRows = csv.GetRecords<Row>().Where(record => record.Name != null).ToList();
                logger.LogDebug("Successfully loaded {RowCount} rows from the CSV file.", LoadedRows.Count);
            }
            catch (Exception ex) {
                logger.LogError(ex, "Error loading CSV file: {Message}", ex.Message);
                throw new ApplicationException("Failed to load the CSV data.", ex);
            }
        }

        /// <summary>
        /// Retrieves a row by its name.
        /// </summary>
        /// <param name="name">The name of the row.</param>
        /// <returns>The row with the specified name.</returns>
        /// <exception cref="ArgumentException">Thrown when the provided name is null or whitespace.</exception>
        /// <exception cref="ApplicationException">Thrown when the row cannot be retrieved.</exception>
        public static Row GetRowByStringName(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The provided name is null or whitespace.", nameof(name));

            var result = LoadedRows.Find(row => row.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return result ?? throw new RowNotFoundException(RowNotFoundErrorMessage);
        }

        /// <summary>
        /// Retrieves offsets by the row name.
        /// </summary>
        /// <param name="rowName">The name of the row.</param>
        /// <returns>A list of offsets.</returns>
        public static List<int> GetOffsetsByRowName(string rowName) {
            var foundRow = GetRowByStringName(rowName);
            return foundRow?.Offsets ?? [];
        }

        /// <summary>
        /// Gets the client path.
        /// </summary>
        /// <returns>The client path.</returns>
        private string GetClientPath() {
            var defaultPath = Directory.GetParent(Environment.CurrentDirectory)?.FullName ?? Environment.CurrentDirectory;
            string path = csvPath ?? defaultPath;
            var filePath = Path.Combine(path, nameOfCSVFile);
            if (!File.Exists(filePath)) {
                logger.LogError("Failed at: {Path}", GetLastThreeParts(filePath));
                path = Environment.CurrentDirectory;
            }
            // logger.LogDebug("Found at: {Path}", GetLastThreeParts(filePath));
            return path;
        }

        /// <summary>
        /// Gets the last three parts of the file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The last three parts of the file path.</returns>
        private static string GetLastThreeParts(string filePath) {
            var parts = filePath.Split(Path.DirectorySeparatorChar);
            return string.Join(Path.DirectorySeparatorChar.ToString(), parts.Skip(Math.Max(0, parts.Length - 4)));
        }

        /// <summary>
        /// Retrieves all unique keys from the loaded CSV data.
        /// </summary>
        /// <returns>A HashSet of unique keys.</returns>
        public static HashSet<string> GetAllKeys() {
            return LoadedRows.Select(row => row.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        }
    }
}