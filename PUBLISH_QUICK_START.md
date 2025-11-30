# Quick Start: Publishing Your Package

## One-Time Setup (5 minutes)

1. **Create NuGet API key**: https://www.nuget.org/account/apikeys
2. **Add GitHub Secret**:
   - Go to: Settings ? Secrets and variables ? Actions
   - Add secret `NUGET_API_KEY` with your NuGet API key

## First Release: v0.1.0-beta (30 seconds)

### Using GitHub Web UI
1. Go to Releases ? Draft a new release
2. Tag: `v0.1.0-beta`
3. Title: `Release v0.1.0-beta`
4. Check "Set as a pre-release"
5. Click "Publish release"

### Using Command Line
```bash
git tag v0.1.0-beta
git push origin v0.1.0-beta
```

That's it! The CI/CD pipeline will:
- ? Build your project
- ? Run tests
- ? Publish to NuGet.org automatically
- ? Mark as prerelease on GitHub

## Future Releases

After beta testing, increment the version:
- `v1.0.0` for stable release
- `v1.0.1` for patch fixes
- `v1.1.0` for new features

Update the version in `src/EtabSharp/EtabSharp.csproj` before creating the tag.

## Check Status
- Actions tab ? Publish NuGet Package workflow ? View logs

---

**Full Guide**: See `PUBLISH_GUIDE.md` for detailed instructions
