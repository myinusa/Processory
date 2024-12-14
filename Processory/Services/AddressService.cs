using Processory.Utilities;

namespace Processory.Services;

public class AddressService(ProcessoryClient processoryClient) {
    public ProcessoryClient ProcessoryClient { get; set; } = processoryClient;

    /// <summary>
    /// Represents the base address in a 64-bit address space.
    /// </summary>
    private const ulong BaseAddress = 0x140000000;

    /// <summary>
    /// Gets the absolute address for a given key.
    /// </summary>
    /// <param name="key">The key to look up the address for.</param>
    /// <returns>The absolute address if found and valid; otherwise, 0.</returns>
    public static ulong GetAbsoluteAddress(string key) {
        Row foundRow = CSVDataOffsetManager.GetRowByStringName(key);
        if (foundRow.DataType != "address") {
            return 0;
        }
        return BaseAddress + (ulong)foundRow.Offsets[0];
    }
}
