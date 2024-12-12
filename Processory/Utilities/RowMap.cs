using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.Extensions.Logging;
using Processory.Exceptions;
using static Processory.Errors.ErrorMessages;

namespace Processory.Utilities {

    public class RowMap : ClassMap<Row> {
        public RowMap() {
            Map(m => m.Name).Name("name");
            Map(m => m.Category).Name("category");
            Map(m => m.Parent).Name("parent");
            Map(m => m.Offsets).Name("offsets").TypeConverter<OffsetsConverter>();
        }
    }
}