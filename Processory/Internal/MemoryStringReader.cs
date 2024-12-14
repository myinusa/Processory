using System.Text;

namespace Processory.Internal;
public class MemoryStringReader(ProcessoryClient processoryClient) {
    private readonly ProcessoryClient processoryClient = processoryClient ?? throw new ArgumentNullException(nameof(processoryClient));

    public string ResolveStringPointerE(UIntPtr initialAddress, List<int> offsets) {
        UIntPtr address = processoryClient.PointerChainFollower.FollowPointerChain(initialAddress, offsets);
        var lengthOffset = new List<int>(offsets);
        lengthOffset[lengthOffset.Count - 1] = 0x0;
        var length = processoryClient.MemoryReader.Read<int>(initialAddress, lengthOffset);

        Span<byte> buffer = stackalloc byte[length];
        processoryClient.MemoryReader.ReadR(address, buffer);
        return Encoding.UTF8.GetString(buffer);
    }
}
