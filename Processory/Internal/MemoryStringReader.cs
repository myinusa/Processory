using System.Text;
using Microsoft.Extensions.Logging;

namespace Processory.Internal;
public class MemoryStringReader(ProcessoryClient processoryClient, ILoggerFactory loggerFactory) {
    private readonly ProcessoryClient processoryClient = processoryClient ?? throw new ArgumentNullException(nameof(processoryClient));
    private readonly ILogger logger = loggerFactory.CreateLogger<MemoryStringReader>();

    public string ResolveStringPointerE(UIntPtr initialAddress, List<int> offsets) {
        UIntPtr address = processoryClient.PointerChainFollower.FollowPointerChain(initialAddress, offsets);
        var lengthOffset = new List<int>(offsets);
        lengthOffset[lengthOffset.Count - 1] = 0x0;
        var length = processoryClient.MemoryReader.Read<int>(initialAddress, lengthOffset);

        Span<byte> buffer = stackalloc byte[length];
        processoryClient.MemoryReader.ReadR(address, buffer);
        return Encoding.UTF8.GetString(buffer);
    }

    public List<string> ResolveStringPointerList(nuint initialAddress, List<int> offsets) {
        nuint address = processoryClient.PointerChainFollower.FollowPointerChain(initialAddress, offsets);
        List<int> list = new List<int>(offsets);
        list[list.Count - 1] = 0;
        Span<byte> span = stackalloc byte[processoryClient.MemoryReader.Read<int>(initialAddress, list)];
        processoryClient.MemoryReader.ReadR(address, span);

        // List to hold the found strings
        List<string> strings = new List<string>();
        // Console.WriteLine(span.Length);

        // Iterate through the span to find segments separated by null terminators
        int startIndex = 0;
        while (startIndex < span.Length) {
            try {
                // Find the index of the next null terminator (either 0 or 10)
                int nullIndex = span.Slice(startIndex).IndexOfAny((byte)0, (byte)10);

                // Console.WriteLine($"nullIndex: {nullIndex}");
                // If no more null terminator is found, take the rest of the span
                if (nullIndex == -1) {
                    // if (nullIndex < -1) {
                    nullIndex = span.Length - startIndex;
                    Span<byte> segment1 = span.Slice(startIndex, nullIndex);
                    strings.Add(Encoding.UTF8.GetString(segment1));
                    // Console.WriteLine($"segment: {Encoding.UTF8.GetString(segment1)}");
                    break;
                }
                else {
                    nullIndex += startIndex; // Adjust index to the original span
                }

                // Console.WriteLine($"startIndex: {startIndex}, span.Length: {span.Length}");
                // Create a segment from startIndex to nullIndex
                Span<byte> segment = span.Slice(startIndex, nullIndex - startIndex);
                // Console.WriteLine($"segment: {Encoding.UTF8.GetString(segment)}");
                strings.Add(Encoding.UTF8.GetString(segment));

                // Move the startIndex past the null terminator
                startIndex = nullIndex + 1; // +1 to skip the null terminator
            }
            catch (ArgumentOutOfRangeException ex) {
                logger.LogError(ex, "An error occurred while resolving string pointer list.");
                break; // Exit the loop if an error occurs
            }
        }

        return strings; // Return the list of strings
    }

}
