using Microsoft.Extensions.Logging;
using Processory.Exceptions;
using Processory.Utilities;
using static Processory.Errors.ErrorMessages;

namespace Processory.Internal;
public class KeyValueReader(ProcessoryClient processoryClient, ILoggerFactory loggerFactory) {
    private readonly ProcessoryClient processoryClient = processoryClient ?? throw new ArgumentNullException(nameof(processoryClient));
    private readonly ILogger logger = loggerFactory.CreateLogger<KeyValueReader>();
    private readonly HashSet<string> validKeys = CSVDataOffsetManager.GetAllKeys();

    public T ReadAbsolute<T>(string key, HashSet<string> validKeys = null)
        where T : unmanaged {
        validKeys ??= this.validKeys;

        if (!validKeys.Contains(key)) {
            throw new RowNotFoundException(string.Format(RowNotFoundErrorMessage, key));
        }

        Row foundRow = CSVDataOffsetManager.GetRowByStringName(key);
        if (foundRow.Parent != string.Empty) {
            var parentAddress = processoryClient.AddressService.GetAbsoluteAddress(foundRow.Parent);
            logger.LogDebug("Parent address {Parent:X}", parentAddress);
            UIntPtr deferAddress = processoryClient.PointerChainFollower.DereferencePointer(parentAddress);

            UIntPtr resolvedAddress = processoryClient.PointerChainFollower.FollowPointerChain(deferAddress, foundRow.Offsets);
            logger.LogDebug("Resolved address {Resolved:X}", resolvedAddress);
            return processoryClient.MemoryReader.Read<T>(resolvedAddress);
        }

        var address = processoryClient.AddressService.GetAbsoluteAddress(key);
        return processoryClient.MemoryReader.Read<T>(address);
    }
}