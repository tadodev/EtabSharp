# Publishing EtabSharp to NuGet

This guide explains how to publish the EtabSharp NuGet package and the CI/CD setup.

## Prerequisites

- GitHub repository with admin access
- NuGet.org account (create at https://www.nuget.org/users/account/LogOn)

## Step 1: Create NuGet API Key

1. Go to https://www.nuget.org/account/apikeys
2. Click **Create** to generate a new API key
3. Give it a name like "EtabSharp-CI-CD" and set expiration (optional)
4. Copy the generated key (you won't see it again)

## Step 2: Add GitHub Secret

1. Go to your GitHub repository: https://github.com/tadodev/EtabSharp
2. Navigate to **Settings** ? **Secrets and variables** ? **Actions**
3. Click **New repository secret**
4. Name: `NUGET_API_KEY`
5. Value: Paste the API key from Step 1
6. Click **Add secret**

## Step 3: Create a Release

The CI/CD pipeline is triggered when you create a Git tag in the format `v*` (e.g., `v0.1.0-beta`, `v1.0.0`).

### Option A: Using Git Command Line

For the first beta release:
```bash
git tag v0.1.0-beta
git push origin v0.1.0-beta
```

For future releases:
```bash
git tag v1.0.0
git push origin v1.0.0
```

### Option B: Using GitHub Web Interface

1. Go to your repository's **Releases** page
2. Click **Draft a new release**
3. Tag version: Enter `v0.1.0-beta` (for first release) or `v1.0.0` for stable versions
4. Release title: Enter `Release v0.1.0-beta`
5. Check "Set as a pre-release" for beta/rc versions
6. Add release notes describing changes
7. Click **Publish release**

## What Happens Automatically

When you create a tag matching `v*`:

1. ? GitHub Actions checks out your code
2. ? Builds the project in Release mode
3. ? Runs all tests
4. ? Extracts version from tag
5. ? Updates the project version in `.csproj`
6. ? Packs the NuGet package
7. ? Publishes to NuGet.org
8. ? Creates a GitHub Release (marked as prerelease for beta versions)

## Monitoring the Build

1. Go to your repository's **Actions** tab
2. Select the **Publish NuGet Package** workflow
3. Monitor the build progress in real-time
4. View detailed logs if any step fails

## Versioning Strategy

### Semantic Versioning with Prerelease Indicators

- `v0.1.0-beta` - Beta release (current)
- `v0.1.0-rc.1` - Release candidate
- `v1.0.0` - Major version 1.0 (stable)
- `v1.0.1` - Patch version (bug fixes)
- `v1.1.0` - Minor version (new features)
- `v2.0.0` - Major version (breaking changes)

The workflow automatically detects prerelease versions (containing `-`) and marks the GitHub release appropriately.

### Updating Release Notes

Update your package release notes in `src/EtabSharp/EtabSharp.csproj` before each release:

```xml
<PackageReleaseNotes>
  v0.1.0-beta - Initial beta release
  - Added feature X
  - Fixed bug Y
</PackageReleaseNotes>
```

## Continuous Integration (Tests)

The `Tests` workflow automatically runs on:
- Every push to `master`, `main`, or `develop` branches
- Every pull request to these branches

This ensures code quality before merging or publishing.

## Manual Local Testing Before Publishing

```bash
# Build the project
dotnet build -c Release

# Run tests
dotnet test -c Release

# Pack the NuGet package
dotnet pack src/EtabSharp/EtabSharp.csproj -c Release -o ./nuget

# Verify the package (optional)
dotnet nuget locals all --list
```

## Troubleshooting

### Workflow Failed?

1. Check the **Actions** tab for error logs
2. Common issues:
   - API key expired ? Generate a new one and update the secret
   - Tests failed ? Fix the issues and push new code
   - ETABSv1.dll not found ? This is expected in CI (mock it in tests)

### Package Not on NuGet?

- Check https://www.nuget.org/packages/EtabSharp/
- It may take a few minutes to appear after publishing
- Beta versions will show under "Pre-release versions"
- Verify via: `dotnet package search EtabSharp --prerelease`

## Using Your Published Package

Once published, install the beta version:

```bash
dotnet add package EtabSharp --version 0.1.0-beta
```

Or in `.csproj`:

```xml
<PackageReference Include="EtabSharp" Version="0.1.0-beta" />
```

For future stable releases:
```bash
dotnet add package EtabSharp --version 1.0.0
```

## Additional Resources

- [NuGet.org Policies](https://learn.microsoft.com/en-us/nuget/policies/policy-faq)
- [Semantic Versioning](https://semver.org/)
- [GitHub Actions Documentation](https://docs.github.com/actions)
- [NuGet Package Best Practices](https://learn.microsoft.com/en-us/nuget/create-packages/package-authoring-best-practices)
