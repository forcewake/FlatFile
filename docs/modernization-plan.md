# FlatFile modernization plan

## Current direction (v2)

This repository now follows a **modern-only** strategy:

1. Legacy .NET Framework build matrix was removed from CI.
2. SDK-style projects under `*.Modern` are the active build path.
3. Active target is `net8.0` with version `2.0.0` (breaking major release).

## Why

The previous mixed strategy (legacy + modern) produced unstable CI and unnecessary maintenance overhead.
A major-version reset enables simpler tooling, faster builds, and a clear support policy.

## Next steps

- Publish v2 packages from modern projects (`dotnet pack`).
- Add analyzers and nullable annotations incrementally.
- Add dedicated test projects targeting `net8.0`.


## CI/CD publishing

- `.github/workflows/publish-nuget.yml` publishes NuGet packages on pushes to `master`.
- Configure repository secret `NUGET_API_KEY` before enabling release merges.
- Package versions are generated as `2.0.<run_number>` in CI.


## Implemented performance updates

- Replaced reflection activation lock+`DynamicInvoke` path with concurrent cached compiled factories.
- Updated conversion pipeline to use converter caching and invariant-culture conversion semantics.
- Applied small parser allocation improvements (`TrimStart/TrimEnd(char)`, span-based quote prefix checks).


## Modern tests

- Added `tests/FlatFile.Modern.Tests` (xUnit, net8.0).
- CI now runs `dotnet test` for the modern test suite.


## Span/Memory guidelines used

- Use `ReadOnlySpan<char>` for scanning/tokenization (delimiter/quote detection) where data remains in-memory and does not need ownership transfer.
- Use `Memory<T>` only when data must survive async boundaries; prefer `Span<T>`/`ReadOnlySpan<T>` in synchronous hot paths.
- Avoid premature `Substring`/`string.Format` allocations in line build/parse loops.
- Keep API compatibility: introduce span optimizations internally first, then expose span APIs in a dedicated v2+ surface when needed.
