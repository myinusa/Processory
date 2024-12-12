using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Processory.Utilities;
public class OffsetsConverter : DefaultTypeConverter {
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) {
        if (string.IsNullOrEmpty(text))
            return new List<int>();

        var offsets = text.Split(',');
        return offsets.Select(o => int.Parse(o.Replace("0x", string.Empty), NumberStyles.HexNumber)).ToList();
    }
}