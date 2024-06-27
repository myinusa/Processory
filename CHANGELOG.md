# Changelog

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
