using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.Extensions.Logging;
using Processory.Exceptions;
using static Processory.Errors.ErrorMessages;

namespace Processory.Utilities {

    public class Row {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Parent { get; set; } = string.Empty;
        public List<int> Offsets { get; set; } = new List<int>();
    }
}