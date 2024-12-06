using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.Extensions.Logging;

namespace Processory.Utilities {
    public class CSVDataOffsetManager {
        private readonly string nameOfCSVFile;
        private readonly ILogger logger;

        public CSVDataOffsetManager(ILoggerFactory loggerFactory, string nameOfCSVFile) {
            logger = loggerFactory.CreateLogger<CSVDataOffsetManager>();
            this.nameOfCSVFile = nameOfCSVFile;
            if (nameOfCSVFile.Any()) {
                LoadRowsFromCsv();
            }
        }

        public static List<Row> LoadedRows { get; private set; } = new List<Row>();

        public string ClientPath => GetClientPath();
        public string AddressPath => Path.Combine(ClientPath, nameOfCSVFile);

        /// <summary>
        /// Loads rows from the CSV file if not already loaded.
        /// </summary>
        public void LoadRowsFromCsv() {
            //logger.LogDebug("Loading CSV data from: {Path}", AddressPath);
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
        public Row GetRowByStringName(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The provided name is null or whitespace.", nameof(name));

            var result = LoadedRows.Find(row => row.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return result ?? throw new ApplicationException("Failed to retrieve the row from the CSV data.");
        }

        /// <summary>
        /// Retrieves offsets by the row name.
        /// </summary>
        /// <param name="rowName">The name of the row.</param>
        /// <returns>A list of offsets.</returns>
        public List<int> GetOffsetsByRowName(string rowName) {
            var foundRow = GetRowByStringName(rowName);
            return foundRow?.Offsets ?? new List<int>();
        }

        /// <summary>
        /// Gets the client path.
        /// </summary>
        /// <returns>The client path.</returns>
        private string GetClientPath() {
            // var path = Directory.GetParent(Environment.CurrentDirectory)?.FullName;
            var path = Directory.GetParent(Environment.CurrentDirectory)?.FullName ?? Environment.CurrentDirectory;
            var filePath = Path.Combine(path, nameOfCSVFile);
            if (!File.Exists(filePath)) {
                logger.LogError("Failed at: {Path}", GetLastThreeParts(filePath));
                path = Environment.CurrentDirectory;
            }
            logger.LogDebug("Found at: {Path}", GetLastThreeParts(filePath));
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
    }

    public class Row {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Parent { get; set; } = string.Empty;
        public List<int> Offsets { get; set; } = new List<int>();
    }

    public class RowMap : ClassMap<Row> {
        public RowMap() {
            Map(m => m.Name).Name("name");
            Map(m => m.Category).Name("category");
            Map(m => m.Parent).Name("parent");
            Map(m => m.Offsets).Name("offsets").TypeConverter<OffsetsConverter>();
        }
    }

    public class OffsetsConverter : DefaultTypeConverter {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) {
            if (string.IsNullOrEmpty(text))
                return new List<int>();

            var offsets = text.Split(',');
            return offsets.Select(o => int.Parse(o.Replace("0x", string.Empty), NumberStyles.HexNumber)).ToList();
        }
    }
}