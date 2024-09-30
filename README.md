# Processory

## Description

Processory is a `.NET` library designed to facilitate interaction with processes and memory management on Windows systems. It provides functionalities for reading process memory, managing CSV data offsets, and interacting with the system through pointer chains and memory reads. This library aims to simplify the process of accessing information from running processes by providing a set of tools that can be easily integrated into applications.

## Usage/Getting Started

To get started with Processory, follow these steps:

1. **Install the Library**: You can install the Processory package via [NuGet](https://www.nuget.org/packages/Processory) Package Manager Console using the following command:

   ```Powershell
   Install-Package Processory
   ```

   or by using the following command in the`.NET` CLI:

   ```Powershell
   dotnet add package Processory
   ```

2. **Create a ProcessoryClient Instance**: Initialize the `ProcessoryClient` class with the name of the process you want to interact with. For example:

   ```csharp
   var processory = new ProcessoryClient("process_name");
   ```

3. **Read Memory**: Use the `MemoryReader` class to read memory from the target process. Here's a basic example of reading an integer value at a specific offset:

   ```csharp
   var baseAddress = processory.ProcessService.ProcessHandle?.MainModule?.BaseAddress;
   if (baseAddress == null) {
       Console.WriteLine("Could not find process");
       return;
   }
   var screenClient = (ulong)(baseAddress + 0x4C7FF08);
   var deferAdd = processory.PointerChainFollower.DereferencePointer(screenClient);
   var screenClientValue = processory.MemoryReader.Read<uint>(deferAdd);
   Console.WriteLine("Screen client: {0:X}", screenClient);
   Console.WriteLine("Screen client value: {0}", screenClientValue);
   ```

## FAQ

### Q1: How do I handle exceptions in Processory?

A1: Exceptions can be handled using standard C# try-catch blocks. For example:

```csharp
try {
    var processory = new ProcessoryClient("process_name");
} catch (Exception ex) {
    Console.WriteLine($"An error occurred: {ex.Message}");
}
```

### Q2: Can I read memory from a 64-bit application?

A2: Yes, Processory supports reading memory from both `32-bit` and 64-bit applications. The library dynamically adjusts its operations based on the architecture of the target process.

## Resources

For additional resources and support, you can visit the following links:

- **GitHub Repository**: [Processory GitHub](https://github.com/myinusa/processory)
- **Documentation**: Comprehensive documentation is available in the repository's Wiki section.

## Configuration

The library does not require extensive configuration. However, ensure that you have the necessary permissions to read memory from the target process. You may need to run your application with elevated privileges if access is restricted.

## Features

- **Memory Reading**: Read raw bytes and structured data from a running process's memory.
- **Pointer Chaining**: Follow complex pointer chains to retrieve values at specific addresses.
- **CSV Data Management**: Manage `csv` files for storing and retrieving offsets used in pointer chaining.
- **Process Interaction**: Open, read, and close processes safely using Windows API functions.

## Topics

- **Memory Security**: Be mindful of the security implications when accessing memory from other processes.
- **Performance Considerations**: Reading large amounts of data may impact performance; consider optimizing your access patterns.

## Considerations

- **Legal and Ethical Use**: Ensure that you have permission to use Processory on any target machine, especially if it is not your own. Unauthorized monitoring can be illegal.
- **Error Handling**: Implement robust error handling in your application to manage unexpected issues gracefully.
