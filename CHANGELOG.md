# Changelog

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
