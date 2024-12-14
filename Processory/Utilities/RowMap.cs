using CsvHelper.Configuration;

namespace Processory.Utilities {
    public class RowMap : ClassMap<Row> {
        public RowMap() {
            Map(m => m.Name).Name("name");
            Map(m => m.DataType).Name("data_type");
            Map(m => m.Parent).Name("parent");
            Map(m => m.Offsets).Name("offsets").TypeConverter<OffsetsConverter>();
        }
    }
}