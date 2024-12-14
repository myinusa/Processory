namespace Processory.Utilities {
    public class Row {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string DataType { get; set; } = string.Empty;
        public string Parent { get; set; } = string.Empty;
        public List<int> Offsets { get; set; } = [];
    }
}