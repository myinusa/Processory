using Microsoft.Extensions.Logging;
using Processory.Exceptions;
using Processory.Utilities;
using static Processory.Errors.ErrorMessages;

namespace Processory.Internal;
public class KeyValueReader {
    private readonly ProcessoryClient processoryClient;
    private readonly ILogger<KeyValueReader> logger;
    private readonly HashSet<string> validKeys;

    public KeyValueReader(ProcessoryClient processoryClient, ILoggerFactory loggerFactory) {
        this.processoryClient = processoryClient ?? throw new ArgumentNullException(nameof(processoryClient));
        logger = loggerFactory.CreateLogger<KeyValueReader>();
        validKeys = CSVDataOffsetManager.GetAllKeys();
    }

    public T ReadAbsolute<T>(string key, HashSet<string>? validKeys = null)
        where T : unmanaged {
        validKeys ??= this.validKeys;

        if (!validKeys.Contains(key)) {
            throw new RowNotFoundException(string.Format(RowNotFoundErrorMessage, key));
        }

        var foundRow = CSVDataOffsetManager.GetRowByStringName(key);
        if (!string.IsNullOrEmpty(foundRow.Parent)) {
            return ReadFromParent<T>(foundRow);
        }

        return ReadFromKey<T>(key);
    }

    private T ReadFromParent<T>(Row foundRow)
        where T : unmanaged {
        var parentAddress = processoryClient.AddressService.GetAbsoluteAddress(foundRow.Parent);
        logger.LogDebug("Parent address {Parent:X}", parentAddress);
        var deferAddress = processoryClient.PointerChainFollower.DereferencePointer(parentAddress);

        var resolvedAddress = processoryClient.PointerChainFollower.FollowPointerChain(deferAddress, foundRow.Offsets);
        logger.LogDebug("Resolved address {Resolved:X}", resolvedAddress);
        return processoryClient.MemoryReader.Read<T>(resolvedAddress);
    }

    private T ReadFromKey<T>(string key)
        where T : unmanaged {
        var address = processoryClient.AddressService.GetAbsoluteAddress(key);
        return processoryClient.MemoryReader.Read<T>(address);
    }
}
