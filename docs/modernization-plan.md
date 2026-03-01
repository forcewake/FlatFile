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
