# FlatFile modernization plan

This repository is a mature .NET Framework codebase with broad usage. The safest modernization strategy is **incremental** and **compatibility-first**.

## Changes applied in this update

1. Added build-target aliases for all maintained .NET Framework lines used by enterprise consumers (`NET451`, `NET452`, `NET46`, `NET461`, `NET462`, `NET47`, `NET471`, `NET472`, `NET48`) in addition to legacy aliases already present (`NET35`, `NET40`, `NET45`).
2. Extended the psake build pipeline to compile every declared framework alias.
3. Added a GitHub Actions CI matrix that restores once and builds the solution for each framework alias on `windows-latest`.
4. Added SDK-style modern library projects (`*.Modern.csproj`) targeting `netstandard2.0` and `net8.0` to provide .NET Core / modern .NET support.

## Recommended next modernization phases

### Phase 1: Packaging and CI hygiene (low risk)

- Move from external `.nuspec` packing to SDK-style `dotnet pack` metadata in project files.
- Add SourceLink and deterministic builds.
- Add test execution in CI (`xUnit`) and fail-fast gates.
- Rotate package publishing credentials and use GitHub/ADO secret stores.

### Phase 2: Project-system migration (medium risk)

- Gradually migrate legacy csproj files to SDK-style projects after validating consumer migration from the new `*.Modern` projects.
- Keep compatibility by multitargeting:
  - `net48` (existing consumers)
  - `netstandard2.0` (broad modern compatibility)
- Replace `packages.config` with `PackageReference`.

### Phase 3: Runtime/API modernization (medium-high risk)

- Add nullable reference types in new API surfaces first, then expand internally.
- Add `Span<char>`/`ReadOnlySpan<char>` overloads for high-throughput parsing paths.
- Introduce async stream APIs where useful (`IAsyncEnumerable<T>` for line reading).

### Phase 4: Ecosystem and maintenance

- Add Roslyn analyzers (`Microsoft.CodeAnalysis.NetAnalyzers`) with warning baseline.
- Define semantic versioning and deprecation policy per target framework.
- Consider `net8.0` target for optimized modern runtime behavior.

## Why this approach

For a package family with millions of downloads, preserving binary compatibility while introducing modern targets in stages avoids consumer breakage and keeps release risk manageable.
