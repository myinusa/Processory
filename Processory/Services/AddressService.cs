using Processory.Utilities;

namespace Processory.Services;

public struct AddressWithOffset {
    public ulong Address { get; set; }
    public List<int> Offsets { get; set; }
}
public class AddressService {
    public ProcessoryClient ProcessoryClient { get; set; }

    /// <summary>
    /// Represents the base address in a 64-bit address space.
    /// </summary>
    private const ulong BaseAddress = 0x140000000;

    public AddressService(ProcessoryClient processoryClient) {
        ProcessoryClient = processoryClient;
    }

    /// <summary>
    /// Gets the absolute address for a given key.
    /// </summary>
    /// <param name="key">The key to look up the address for.</param>
    /// <returns>The absolute address if found and valid; otherwise, 0.</returns>
    public ulong GetAbsoluteAddress(string key) {
        Row foundRow = CSVDataOffsetManager.GetRowByStringName(key);
        if (foundRow.Category != "address") {
            return 0;
        }
        return BaseAddress + (ulong)foundRow.Offsets[0];
    }
}
