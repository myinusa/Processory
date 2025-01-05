# Changelog

## [1.24.0](https://github.com/myinusa/Processory/compare/v1.23.0...v1.24.0) (2025-01-05)


### Features

* **Internal/ProcessoryClient:** Add AddressInfoFactory class with ReadAddressInfo and ReadAddressPointer methods. ([a754341](https://github.com/myinusa/Processory/commit/a754341a41370fd62938ade66c77dfaf95f67206))
* **Internal:** Add new classes and methods to read memory and pointers ([cd15179](https://github.com/myinusa/Processory/commit/cd15179863fe104979044752bab860fb35b5e2ed))
* **MemoryReader.cs:** Add `ReadAddressInfo&lt;T&gt;` method to return an AddressInfo instance ([cd15179](https://github.com/myinusa/Processory/commit/cd15179863fe104979044752bab860fb35b5e2ed))
* **MemoryReader.cs:** Add `ReadNo&lt;T&gt;` for reading unmanaged types without a reference ([cd15179](https://github.com/myinusa/Processory/commit/cd15179863fe104979044752bab860fb35b5e2ed))
* **MemoryReader.cs:** Add `ReadNoRef&lt;T&gt;` for reading unmanaged types into a ref variable ([cd15179](https://github.com/myinusa/Processory/commit/cd15179863fe104979044752bab860fb35b5e2ed))
* **MemoryReader.cs:** Add `ReadPointerCE` method to dereference a pointer using CE techniques ([cd15179](https://github.com/myinusa/Processory/commit/cd15179863fe104979044752bab860fb35b5e2ed))
* **MemoryReader.cs:** Add `ReadPointerInfo` method to read and dereference pointers ([cd15179](https://github.com/myinusa/Processory/commit/cd15179863fe104979044752bab860fb35b5e2ed))
* **Processory.Tests:** add new test classes for common functionality ([f52ad7d](https://github.com/myinusa/Processory/commit/f52ad7d6366ee7277862f66ef2007f2fff7af382))
* **Processory.Tests:** add test for AddressPointerInfo ([2ecf487](https://github.com/myinusa/Processory/commit/2ecf4874127b0ca4380a2d69b73dbec23e3cf505))
* **Processory/Helpers:** Add IsValidAddress method to AddressHelper class ([d3b716f](https://github.com/myinusa/Processory/commit/d3b716fb2032750671603dddd56f05ee2a187cb3))
* **Processory/Internal/AddressPointer.cs:** Add AddressPointer class for reading pointers in memory ([b495b46](https://github.com/myinusa/Processory/commit/b495b46e919df1154c7e115735c09825bcecdad6))
* **Processory/Internal/InterfaceManager.cs:** Added logic to handle minimized and restored window statuses ([87d4d0a](https://github.com/myinusa/Processory/commit/87d4d0a2f1ad6c24ed691f1c694e78bedc4b9b4f))
* **Processory/Internal/MemoryReader.cs:** Replace hardcoded `MemoryReader` with `processoryClient.AddressInfoFactory.ReadAddressInfo&lt;T&gt;()` in `ReadAddressInfo<T>(ulong address)` method ([ccf84c7](https://github.com/myinusa/Processory/commit/ccf84c7fd2671a29a6bc7b05bf74c9ad1b486fff))
* **Processory/ProcessoryClient.cs:** Add AddressInfoFactory service to the container with `GetService&lt;AddressInfoFactory&gt;()` method ([ccf84c7](https://github.com/myinusa/Processory/commit/ccf84c7fd2671a29a6bc7b05bf74c9ad1b486fff))


### Bug Fixes

* Ensure that the `InterfaceManager` correctly handles various window states, including minimizing and restoring. ([0e1af71](https://github.com/myinusa/Processory/commit/0e1af713a327db0609a0d37e21a6dfa184aafcb7))
* **Processorory/Internal/WindowManager.cs:** Fixed issue with minimizing and restoring windows ([87d4d0a](https://github.com/myinusa/Processory/commit/87d4d0a2f1ad6c24ed691f1c694e78bedc4b9b4f))
* **Processorory/Properties/AssemblyInfo.cs:** Updated assembly information to match version prefix ([87d4d0a](https://github.com/myinusa/Processory/commit/87d4d0a2f1ad6c24ed691f1c694e78bedc4b9b4f))
* **Processory.Tests/UnitMemoryTest.cs:** Add protected memory reader property to unit tests ([e1391f5](https://github.com/myinusa/Processory/commit/e1391f585e73cdc6970fa5aed0d90792732547b9))
* **Processory/Helpers:** Correct memory query logic in IsValidPointer method ([d3b716f](https://github.com/myinusa/Processory/commit/d3b716fb2032750671603dddd56f05ee2a187cb3))
* **Processory/Internal/MemoryReader.cs:** Add documentation for Read&lt;T&gt; method, change ReadR to ReadSpanBytes for consistency and clarity. ([2c2229d](https://github.com/myinusa/Processory/commit/2c2229d790466bd0897e297e4290e7f9f15599ab))
* **Processory/Internal/MemoryReader.cs:** Remove unused default return statement from `ReadAddressPointer&lt;TAddressValue, TPointerValue&gt;(ulong address)` method ([ccf84c7](https://github.com/myinusa/Processory/commit/ccf84c7fd2671a29a6bc7b05bf74c9ad1b486fff))
* **Processory/Internal/RunTimeTypeInformation.cs:** Read class hierarchy descriptor pointer by offset instead of direct address ([21ea796](https://github.com/myinusa/Processory/commit/21ea79600352a199007ab953ee2b11a4abdc0ffc))
* **Processory/Processory.csproj:** Update version prefix to reflect changes made to MemoryReader class. ([2c2229d](https://github.com/myinusa/Processory/commit/2c2229d790466bd0897e297e4290e7f9f15599ab))
* **Processory/Properties/AssemblyInfo.cs:** Update assembly version and informational version accordingly. ([2c2229d](https://github.com/myinusa/Processory/commit/2c2229d790466bd0897e297e4290e7f9f15599ab))
* **UnitMemoryTest.cs:** Corrected logic for logging and validation ([ae2c6e5](https://github.com/myinusa/Processory/commit/ae2c6e5568f0c3adf902aec2892d21e45f40b982))
* **UnitMemoryTest.cs:** replace placeholders with valid values and addresses ([f52ad7d](https://github.com/myinusa/Processory/commit/f52ad7d6366ee7277862f66ef2007f2fff7af382))

## [1.23.0](https://github.com/myinusa/Processory/compare/v1.22.0...v1.23.0) (2024-12-14)


### Features

* **InterfaceManager:** Add support for logging to InterfaceManager ([f089cf8](https://github.com/myinusa/Processory/commit/f089cf89cfeb72b9d1433fb59c1de0e59a920586))
* **MemoryReader:** Enhance MemoryReader to handle string pointers efficiently ([f089cf8](https://github.com/myinusa/Processory/commit/f089cf89cfeb72b9d1433fb59c1de0e59a920586))
* **MemoryStringReader.cs:** Add MemoryStringReader class with ResolveStringPointerE method ([ec09dd3](https://github.com/myinusa/Processory/commit/ec09dd371ff1e8738650b345b5e659f9997fe307))
* **Processory/Utilities/Row.cs:** Add DataType property to Row class ([ab542cc](https://github.com/myinusa/Processory/commit/ab542cc6e610d30ee7d6313b777287e8296b8740))
* **Utilities/AddressService:** Add Category field to Row and update AddressService methods ([fa5c0c7](https://github.com/myinusa/Processory/commit/fa5c0c7e643389eef5b7018410b5043e3205c9c5))


### Bug Fixes

* **KeyValueReader:** Fix issue with KeyValueReader when resolving strings ([f089cf8](https://github.com/myinusa/Processory/commit/f089cf89cfeb72b9d1433fb59c1de0e59a920586))
* **launchSettings.json:** Remove old launch settings configuration ([6c262f3](https://github.com/myinusa/Processory/commit/6c262f380b45f93ab16c8532d112630eaac3a821))
* **MemoryReader.cs:** Remove console.WriteLine statement ([ec09dd3](https://github.com/myinusa/Processory/commit/ec09dd371ff1e8738650b345b5e659f9997fe307))
* **Processory/Utilities/RowMap.cs:** Update DataType mapping in RowMap ([ab542cc](https://github.com/myinusa/Processory/commit/ab542cc6e610d30ee7d6313b777287e8296b8740))
* **Utilities/RowMap.cs:** Update typeConverter property name from OffsetsConverter to OffsetsConverterType ([fa5c0c7](https://github.com/myinusa/Processory/commit/fa5c0c7e643389eef5b7018410b5043e3205c9c5))


### Performance Improvements

* **AssemblyInfo.cs:** Update AssemblyFileVersion and AssemblyInformationalVersion ([ec09dd3](https://github.com/myinusa/Processory/commit/ec09dd371ff1e8738650b345b5e659f9997fe307))

## [1.22.0](https://github.com/myinusa/Processory/compare/v1.21.0...v1.22.0) (2024-12-14)


### Features

* **InterfaceManager:** Update window management and mouse move logic for better performance ([5404750](https://github.com/myinusa/Processory/commit/5404750b1d575b3419902aa54da58bc6b4c6d2be))
* **Tools:** update .editorconfig to use more strict code analysis rules ([2622cb0](https://github.com/myinusa/Processory/commit/2622cb0cecad6ec603b039a16c5a278c3e6d77f8))


### Bug Fixes

* **.editorconfig:** remove unnecessary suppressions for CA1016 and S3904 ([2622cb0](https://github.com/myinusa/Processory/commit/2622cb0cecad6ec603b039a16c5a278c3e6d77f8))
* **AssemblyInfo.cs:** Update assembly version from 1.21.0 to 1.21.241214.1151 ([5404750](https://github.com/myinusa/Processory/commit/5404750b1d575b3419902aa54da58bc6b4c6d2be))
* **PointerChainFollower.cs:** ensure the pointer dereference and address following methods use correct processoryClient references ([2622cb0](https://github.com/myinusa/Processory/commit/2622cb0cecad6ec603b039a16c5a278c3e6d77f8))
* **Row.cs:** add a missing closing curly brace ([2622cb0](https://github.com/myinusa/Processory/commit/2622cb0cecad6ec603b039a16c5a278c3e6d77f8))
* **WindowManager, MonitorInfo:** Correct monitor information types to match C# conventions ([5404750](https://github.com/myinusa/Processory/commit/5404750b1d575b3419902aa54da58bc6b4c6d2be))

## [1.21.0](https://github.com/myinusa/Processory/compare/v1.20.0...v1.21.0) (2024-12-12)


### Features

* **Processory/Errors:** Add ErrorMessages.cs ([3528cf1](https://github.com/myinusa/Processory/commit/3528cf14b39e96e947fc256d713ea18d782dfb06))
* **Processory/Exceptions:** Add RowNotFoundException.cs ([3528cf1](https://github.com/myinusa/Processory/commit/3528cf14b39e96e947fc256d713ea18d782dfb06))
* **Processory/Internal/MemoryArrayReader.cs:** Add MemoryArrayReader class with ReadArray and ReadArrayRef methods ([906007c](https://github.com/myinusa/Processory/commit/906007c9988a7c67027bb70f2828907aa1ea7b2f))
* **Processory/Native/DbgHelp.cs:** UnDecorateSymbolName function now uses DbgHelpInterop class ([0d01c01](https://github.com/myinusa/Processory/commit/0d01c012f86e2c169d79a701febcd980344da498))


### Bug Fixes

* **Processory/Internal/KeyValueReader.cs:** Corrected the logic for reading absolute values, addressing potential null pointer dereferences and invalid keys. ([e84e810](https://github.com/myinusa/Processory/commit/e84e810b7375895321403577e861653245828721))
* **Processory/Internal/RunTimeTypeInformation.cs:** AddressHelper.cs and RunTimeTypeInformation.cs are now using Processory.Native instead of System.Runtime.InteropServices ([0d01c01](https://github.com/myinusa/Processory/commit/0d01c012f86e2c169d79a701febcd980344da498))
* **Processory/ProcessoryClient.cs:** replace null with an InvalidOperationException when process handle is invalid ([1eb9e76](https://github.com/myinusa/Processory/commit/1eb9e760e59289e8231ded8fe344566df699e21a))

## [1.20.0](https://github.com/myinusa/Processory/compare/v1.19.0...v1.20.0) (2024-12-10)


### Features

* **Processory/Processory.csproj:** update target framework to net8.0 ([a5ea490](https://github.com/myinusa/Processory/commit/a5ea49003477fe790ac4bf2dd3dc3085f5922d6a))


### Bug Fixes

* **Processor.Tests/Processor.Tests.csproj, Processor.Insight/Processor.Insight.csproj, Processory.csproj, Processory/Properties/AssemblyInfo.cs:** update assembly version and informational version ([a5ea490](https://github.com/myinusa/Processory/commit/a5ea49003477fe790ac4bf2dd3dc3085f5922d6a))

## [1.19.0](https://github.com/myinusa/Processory/compare/v1.18.0...v1.19.0) (2024-12-09)


### Features

* **.editorconfig:** Update IDE0002 diagnostic severity to warning ([976f88b](https://github.com/myinusa/Processory/commit/976f88b2dd7b00ffaca30483d10d2c0b0c77ff80))
* **KeyboardConstants:** Added constants for virtual-key codes and key event flags. ([502017a](https://github.com/myinusa/Processory/commit/502017a3927a6ba7f5f077fe58b71cce83109f70))
* **Processorory/Native/CursorManagement:** Add CursorManagement class with SetCursorPos method ([a171757](https://github.com/myinusa/Processory/commit/a1717575f27748cb3d4d26a715bd58a1eae4937b))
* **Processory/Internal/InterfaceManager.cs:** Add logging for invalid window handle and log debug information for window status ([a58066f](https://github.com/myinusa/Processory/commit/a58066f009fc6f6746b02eefcdfee79b7ed3bde7))
* **Processory/Native/KeyboardMouseEvents.cs:** Add new methods for keyboard and mouse events ([85a51a6](https://github.com/myinusa/Processory/commit/85a51a6854479601dcf8be7d8704dcfd6623b792))
* **Processory/Native/MonitorManagement.cs:** added new file containing MonitorManagement class with necessary DllImport methods for user32.dll. ([017369f](https://github.com/myinusa/Processory/commit/017369fbab0eccb14e4b2c498c17d07b287934f5))


### Bug Fixes

* **Processory/Internal/WindowManager.cs:** BringWindowToFront method with improved error handling and retry mechanism ([a58066f](https://github.com/myinusa/Processory/commit/a58066f009fc6f6746b02eefcdfee79b7ed3bde7))
* **Processory/Native/User32/User32.cs:** Implement GetWindowStatus method to retrieve window status ([a58066f](https://github.com/myinusa/Processory/commit/a58066f009fc6f6746b02eefcdfee79b7ed3bde7))
* **ProcessService.cs:** Remove redundant private field declaration ([976f88b](https://github.com/myinusa/Processory/commit/976f88b2dd7b00ffaca30483d10d2c0b0c77ff80))
* **User32/User32.cs:** Corrected SetForegroundWindow API call ([bdb4c8a](https://github.com/myinusa/Processory/commit/bdb4c8a4cdae3bc5013bdc963942813d92e80689))

## [1.18.0](https://github.com/myinusa/Processory/compare/v1.17.0...v1.18.0) (2024-12-07)


### Features

* add KeyboardConstants class for virtual key codes ([e31750b](https://github.com/myinusa/Processory/commit/e31750bd7fd0badb015ce6677a81794b43f0317c))
* add MouseEventConstants class for mouse event flags ([e31750b](https://github.com/myinusa/Processory/commit/e31750bd7fd0badb015ce6677a81794b43f0317c))
* add NullSafe utility class for safe dereferencing ([e31750b](https://github.com/myinusa/Processory/commit/e31750bd7fd0badb015ce6677a81794b43f0317c))
* **CSVDataOffsetManager:** add csvPath parameter to constructor ([8452c85](https://github.com/myinusa/Processory/commit/8452c856d2dd8239075fa28ece23fd91ebeac859))
* **Processory:** add csvPath parameter to ProcessoryClient constructor ([8452c85](https://github.com/myinusa/Processory/commit/8452c856d2dd8239075fa28ece23fd91ebeac859))
* **WindowManager:** add new class to handle window operations like IsValidWindow, RestoreWindow, SetWindowToForeground, SnapWindowToRightHalf, and GetMonitorInfo ([54ac980](https://github.com/myinusa/Processory/commit/54ac9807f98064695d11c29a78a0ce6fd5adf825))


### Bug Fixes

* add null check in LogProcessInfo method in ProcessService ([e31750b](https://github.com/myinusa/Processory/commit/e31750bd7fd0badb015ce6677a81794b43f0317c))
* **CSVDataOffsetManager:** enable logger for loading CSV data ([8452c85](https://github.com/myinusa/Processory/commit/8452c856d2dd8239075fa28ece23fd91ebeac859))

## [1.17.0](https://github.com/myinusa/Processory/compare/v1.16.0...v1.17.0) (2024-10-19)


### Features

* **Processory.Tests/UnitMemoryTest.cs:** Add test for process validity and memory reading functionality. ([b3cbbd0](https://github.com/myinusa/Processory/commit/b3cbbd01fb08dccf3abb97b7240a9576cb6b2636))

## [1.16.0](https://github.com/myinusa/Processory/compare/v1.15.0...v1.16.0) (2024-10-04)


### Features

* implement ResolveStringPointerList method in MemoryReader ([0ecc8d8](https://github.com/myinusa/Processory/commit/0ecc8d8c63f3392cf7901b8745720adcff43e2c8))


### Bug Fixes

* correct formatting of loggerFactory creation in Program.cs ([0ecc8d8](https://github.com/myinusa/Processory/commit/0ecc8d8c63f3392cf7901b8745720adcff43e2c8))

## [1.15.0](https://github.com/myinusa/Processory/compare/v1.14.0...v1.15.0) (2024-10-02)


### Features

* **Processory.Insight/Program.cs:** integrate Microsoft Logging and configure logger factory ([150c91f](https://github.com/myinusa/Processory/commit/150c91fd9c2f99a5d1ed3aca939910c58110962c))
* **Processory/Pointers/PointerChainFollower.cs:** add method to resolve pointer chain ([150c91f](https://github.com/myinusa/Processory/commit/150c91fd9c2f99a5d1ed3aca939910c58110962c))
* **Processory/Processory.csproj:** update version prefix ([150c91f](https://github.com/myinusa/Processory/commit/150c91fd9c2f99a5d1ed3aca939910c58110962c))
* **Processory/Services/ProcessService.cs:** initialize InputSimulator using object initializer syntax ([150c91f](https://github.com/myinusa/Processory/commit/150c91fd9c2f99a5d1ed3aca939910c58110962c))

## [1.14.0](https://github.com/myinusa/Processory/compare/v1.13.0...v1.14.0) (2024-09-30)


### Features

* **Processory:** add new properties for package generation and repository details ([e676763](https://github.com/myinusa/Processory/commit/e676763144c8e4c4f0b489d4498f0a9340ca0e46))
* **Processory:** include README.md in package files ([e676763](https://github.com/myinusa/Processory/commit/e676763144c8e4c4f0b489d4498f0a9340ca0e46))


### Bug Fixes

* **Processory.Tests:** update Microsoft.NET.Test.Sdk to 17.11.1 and xunit to 2.9.2 ([e676763](https://github.com/myinusa/Processory/commit/e676763144c8e4c4f0b489d4498f0a9340ca0e46))
* **Processory:** update assembly file version and informational version to 1.13.0 ([e676763](https://github.com/myinusa/Processory/commit/e676763144c8e4c4f0b489d4498f0a9340ca0e46))
* **Processory:** update Roslynator analyzers to 4.12.6 ([e676763](https://github.com/myinusa/Processory/commit/e676763144c8e4c4f0b489d4498f0a9340ca0e46))

## [1.13.0](https://github.com/myinusa/Processory/compare/v1.12.0...v1.13.0) (2024-09-07)


### Features

* **InterfaceManager.cs:** add functionality to snap window to the right half of the screen ([c0051d8](https://github.com/myinusa/Processory/commit/c0051d8af60655b47553e7146d9e4c1716f8d2ed))
* **InterfaceManager.cs:** implement method to move mouse to center of the right half of the screen ([c0051d8](https://github.com/myinusa/Processory/commit/c0051d8af60655b47553e7146d9e4c1716f8d2ed))
* **Processory.Tests.csproj:** add warning level suppression for Debug and Release configurations ([c0051d8](https://github.com/myinusa/Processory/commit/c0051d8af60655b47553e7146d9e4c1716f8d2ed))
* **User32.cs:** add MoveWindow method to User32 class ([c0051d8](https://github.com/myinusa/Processory/commit/c0051d8af60655b47553e7146d9e4c1716f8d2ed))

## [1.12.0](https://github.com/myinusa/Processory/compare/v1.11.0...v1.12.0) (2024-07-06)


### Features

* **CSVDataOffsetManager:** conditionally load CSV rows based on file name existence ([492affb](https://github.com/myinusa/Processory/commit/492affb3510ad7cb769bb77a9856db51194401e8))
* **Processory.csproj:** add warning level settings for Debug and Release configurations ([492affb](https://github.com/myinusa/Processory/commit/492affb3510ad7cb769bb77a9856db51194401e8))
* **ProcessService:** integrate InputSimulator for simulating key presses and mouse clicks ([492affb](https://github.com/myinusa/Processory/commit/492affb3510ad7cb769bb77a9856db51194401e8))
* **User32:** add new virtual key constants and mouse event constants ([492affb](https://github.com/myinusa/Processory/commit/492affb3510ad7cb769bb77a9856db51194401e8))

## [1.11.0](https://github.com/myinusa/Processory/compare/v1.10.0...v1.11.0) (2024-07-06)


### Features

* **memory-reader:** add ILogger to MemoryReader for logging support ([1e09edd](https://github.com/myinusa/Processory/commit/1e09eddf6f7c78d945caf851f7e47d7867f5be73))
* **memory-reader:** add ReadOffsetString method for reading offset strings ([1e09edd](https://github.com/myinusa/Processory/commit/1e09eddf6f7c78d945caf851f7e47d7867f5be73))
* **memory-reader:** implement ReadAbsolute method for reading absolute addresses with logging ([1e09edd](https://github.com/myinusa/Processory/commit/1e09eddf6f7c78d945caf851f7e47d7867f5be73))
* **memory-reader:** implement ReadUnsignedFileOffset method for reading unsigned file offsets ([1e09edd](https://github.com/myinusa/Processory/commit/1e09eddf6f7c78d945caf851f7e47d7867f5be73))

## [1.10.0](https://github.com/myinusa/Processory/compare/v1.9.0...v1.10.0) (2024-07-05)


### Features

* add AddressService class to handle address retrieval ([16a5421](https://github.com/myinusa/Processory/commit/16a5421d61a84027da59f6c1c80bc82bd3a236d2))
* add AddressService property to ProcessoryClient ([16a5421](https://github.com/myinusa/Processory/commit/16a5421d61a84027da59f6c1c80bc82bd3a236d2))
* add CSVDataOffsetManager class for managing CSV data offsets ([16a5421](https://github.com/myinusa/Processory/commit/16a5421d61a84027da59f6c1c80bc82bd3a236d2))
* add CSVDataOffsetManager to ProcessoryClient ([16a5421](https://github.com/myinusa/Processory/commit/16a5421d61a84027da59f6c1c80bc82bd3a236d2))
* update ProcessoryClient constructor to include CSVDataOffsetManager ([16a5421](https://github.com/myinusa/Processory/commit/16a5421d61a84027da59f6c1c80bc82bd3a236d2))

## [1.9.0](https://github.com/myinusa/Processory/compare/v1.8.0...v1.9.0) (2024-06-30)


### Features

* **ProcessoryClient:** integrate InterfaceManager with dependency injection ([236ecbe](https://github.com/myinusa/Processory/commit/236ecbe4aa370ddb0a492d0f537076e016d79594))
* **ProcessService:** implement SimulateF5KeyPress method ([ae41cde](https://github.com/myinusa/Processory/commit/ae41cde14bed65fce06635da22bc54fab7e4a772))
* **User32:** add keybd_event and update DLL imports for user32 functions ([ae41cde](https://github.com/myinusa/Processory/commit/ae41cde14bed65fce06635da22bc54fab7e4a772))

## [1.8.0](https://github.com/myinusa/Processory/compare/v1.7.0...v1.8.0) (2024-06-30)


### Features

* **Processory:** add new InterfaceManager and update logging in services ([82b6f4f](https://github.com/myinusa/Processory/commit/82b6f4f37e6a470a041c9f5797032bd209fa41ae))
* **Processory:** integrate Microsoft.Extensions.Logging ([82b6f4f](https://github.com/myinusa/Processory/commit/82b6f4f37e6a470a041c9f5797032bd209fa41ae))
* **User32:** add new User32 classes for system metrics and window management ([82b6f4f](https://github.com/myinusa/Processory/commit/82b6f4f37e6a470a041c9f5797032bd209fa41ae))

## [1.7.0](https://github.com/myinusa/Processory/compare/v1.6.0...v1.7.0) (2024-06-27)


### Features

* **Processory:** Add IntPtrExtension class with extension methods ([bf1f4e8](https://github.com/myinusa/Processory/commit/bf1f4e86464cdf654601159392cbeaa1d8126325))
* **Processory:** Add IsValidPointer method in AddressHelper.cs ([bf1f4e8](https://github.com/myinusa/Processory/commit/bf1f4e86464cdf654601159392cbeaa1d8126325))
* **Processory:** Add UIntPtrExtension class with extension methods ([bf1f4e8](https://github.com/myinusa/Processory/commit/bf1f4e86464cdf654601159392cbeaa1d8126325))

## [1.6.0](https://github.com/myinusa/Processory/compare/v1.5.0...v1.6.0) (2024-06-26)


### Features

* add AddressHelper class for address validation ([c4f4b1b](https://github.com/myinusa/Processory/commit/c4f4b1bbcd67951074e3f33fadf1074c92653b85))

## [1.5.0](https://github.com/myinusa/Processory/compare/v1.4.0...v1.5.0) (2024-06-24)


### Features

* **Processory.Native:** introduce DbgHelp with UnDecorateSymbolName method [#13](https://github.com/myinusa/Processory/issues/13) ([e5f318f](https://github.com/myinusa/Processory/commit/e5f318f64c481596eabae6895e6c365ce6bb8680))
* **Processory:** add RunTimeTypeInformation class for RTTI handling [#13](https://github.com/myinusa/Processory/issues/13) ([e5f318f](https://github.com/myinusa/Processory/commit/e5f318f64c481596eabae6895e6c365ce6bb8680))

## [1.4.0](https://github.com/myinusa/Processory/compare/v1.3.0...v1.4.0) (2024-06-24)


### Features

* **assembly:** add AssemblyInfo with version details [#11](https://github.com/myinusa/Processory/issues/11) ([553d299](https://github.com/myinusa/Processory/commit/553d299a33ff18309712feaa1dea37c746264e6d))
* **build:** update csproj with new build properties and pre-build event [#11](https://github.com/myinusa/Processory/issues/11) ([553d299](https://github.com/myinusa/Processory/commit/553d299a33ff18309712feaa1dea37c746264e6d))
* **scripts:** add PowerShell script for assembly version update [#11](https://github.com/myinusa/Processory/issues/11) ([553d299](https://github.com/myinusa/Processory/commit/553d299a33ff18309712feaa1dea37c746264e6d))

## [1.3.0](https://github.com/myinusa/Processory/compare/v1.2.0...v1.3.0) (2024-06-23)


### Features

* **MemoryReader:** add ReadPointer and ReadPointer&lt;T&gt; methods for enhanced pointer handling [#9](https://github.com/myinusa/Processory/issues/9) ([a8c608a](https://github.com/myinusa/Processory/commit/a8c608ad99273d0090994b21c90a0041de676436))
* **MemoryReader:** implement Read&lt;T&gt; method overload for pointer chain resolution ([a8c608a](https://github.com/myinusa/Processory/commit/a8c608ad99273d0090994b21c90a0041de676436))
* **PointerChainFollower:** introduce new class for managing pointer chains [#9](https://github.com/myinusa/Processory/issues/9) ([a8c608a](https://github.com/myinusa/Processory/commit/a8c608ad99273d0090994b21c90a0041de676436))
* **Processory.Insight:** streamline namespace declaration in Program.cs [#9](https://github.com/myinusa/Processory/issues/9) ([a8c608a](https://github.com/myinusa/Processory/commit/a8c608ad99273d0090994b21c90a0041de676436))
* **Processory:** integrate PointerChainFollower into ProcessoryClient [#9](https://github.com/myinusa/Processory/issues/9) ([a8c608a](https://github.com/myinusa/Processory/commit/a8c608ad99273d0090994b21c90a0041de676436))

## [1.2.0](https://github.com/myinusa/Processory/compare/v1.1.0...v1.2.0) (2024-06-23)


### Features

* **MemoryReader:** enhance memory reading capabilities with new methods [#7](https://github.com/myinusa/Processory/issues/7) ([2834830](https://github.com/myinusa/Processory/commit/2834830626092eb33a80d07e253956b7f12ad412))

## [1.1.0](https://github.com/myinusa/Processory/compare/v1.0.0...v1.1.0) (2024-06-23)


### Features

* **MemoryReader:** introduce new MemoryReader class for process memory access ([d6a7b86](https://github.com/myinusa/Processory/commit/d6a7b8655413cce4eb67d4392a36755cacaa92ae))
* **Processory.csproj:** enable unsafe blocks and add multiple analyzer packages ([d6a7b86](https://github.com/myinusa/Processory/commit/d6a7b8655413cce4eb67d4392a36755cacaa92ae))
* **Processory.Insight:** add memory reading and logging for screen client ([d6a7b86](https://github.com/myinusa/Processory/commit/d6a7b8655413cce4eb67d4392a36755cacaa92ae))
* **solution:** integrate .editorconfig into solution items ([d6a7b86](https://github.com/myinusa/Processory/commit/d6a7b8655413cce4eb67d4392a36755cacaa92ae))

## 1.0.0 (2024-06-23)


### Features

* **github:** add issue templates for bug reports and feature requests ([66658c1](https://github.com/myinusa/Processory/commit/66658c1ec4bb9d8215cb054ad78f46c53bac6cbf))
* **github:** introduce dependabot configuration for automatic dependency updates ([66658c1](https://github.com/myinusa/Processory/commit/66658c1ec4bb9d8215cb054ad78f46c53bac6cbf))
* **github:** setup release-please workflow for automated release management ([66658c1](https://github.com/myinusa/Processory/commit/66658c1ec4bb9d8215cb054ad78f46c53bac6cbf))
* **Processory:** add new projects, tests, and native methods ([fdf0b8f](https://github.com/myinusa/Processory/commit/fdf0b8f80b94fc267cafe551d0453b9b46cce528))
* **Processory:** Create memory structures and system info in Native.Structures ([fdf0b8f](https://github.com/myinusa/Processory/commit/fdf0b8f80b94fc267cafe551d0453b9b46cce528))
* **Processory:** Define native flags and kernel methods in Processory.Native ([fdf0b8f](https://github.com/myinusa/Processory/commit/fdf0b8f80b94fc267cafe551d0453b9b46cce528))
