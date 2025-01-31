using Microsoft.Extensions.Logging;
using Processory.Exceptions;
using Processory.Utilities;
using static Processory.Errors.ErrorMessages;

namespace Processory.Internal;
public class KeyValueReader(ProcessoryClient processoryClient, ILoggerFactory loggerFactory) {
    private readonly ProcessoryClient processoryClient = processoryClient ?? throw new ArgumentNullException(nameof(processoryClient));
    private readonly ILogger<KeyValueReader> logger = loggerFactory.CreateLogger<KeyValueReader>();
    private readonly HashSet<string> validKeys = CSVDataOffsetManager.GetAllKeys();

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

    public nuint GetAddress(string key, HashSet<string>? validKeys = null) {
        validKeys ??= this.validKeys;

        if (!validKeys.Contains(key)) {
            throw new RowNotFoundException(string.Format(RowNotFoundErrorMessage, key));
        }

        var foundRow = CSVDataOffsetManager.GetRowByStringName(key);
        if (!string.IsNullOrEmpty(foundRow.Parent)) {
            return ResolveRowWithParent(foundRow);
        }

        return 0;
    }

    private T ReadFromParent<T>(Row foundRow)
        where T : unmanaged {
        var parentAddress = Services.AddressService.GetAbsoluteAddress(foundRow.Parent);
        // logger.LogDebug("Parent | Key: {Name} | address {Parent:X}", foundRow.Parent, parentAddress);
        var deferAddress = processoryClient.MemoryPointer.Dereference(parentAddress);

        var resolvedAddress = processoryClient.MemoryPointer.FollowPointerChain(deferAddress, foundRow.Offsets);
        // logger.LogDebug("Resolved | {Name} | address {Resolved:X}", foundRow.Name, resolvedAddress);
        return processoryClient.MemoryReader.Read<T>(resolvedAddress);
    }

    private nuint ResolveRowWithParent(Row foundRow) {
        var parentAddress = Services.AddressService.GetAbsoluteAddress(foundRow.Parent);
        var deferAddress = processoryClient.MemoryPointer.Dereference(parentAddress);
        return processoryClient.MemoryPointer.FollowPointerChain(deferAddress, foundRow.Offsets);
    }

    private T ReadFromKey<T>(string key)
        where T : unmanaged {
        var address = Services.AddressService.GetAbsoluteAddress(key);
        return processoryClient.MemoryReader.Read<T>(address);
    }

    // private nuint ResolveAddressFromKey<T>(string key) {
    //     var address = Services.AddressService.GetAbsoluteAddress(key);
    //     return processoryClient.MemoryReader.Read(address);
    // }
}
