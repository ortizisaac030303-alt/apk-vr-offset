# Contributing Guidelines

## Code Style

- Use C# naming conventions (PascalCase for classes, camelCase for variables)
- Add XML documentation comments to public methods
- Keep methods focused and under 50 lines when possible

## Adding New Features

1. Create feature branch: `git checkout -b feature/your-feature-name`
2. Implement feature with appropriate scripts in `Assets/Scripts/`
3. Test thoroughly in VR environment
4. Submit pull request with description of changes

## Testing

- Test with actual VR controllers when possible
- Verify offset application in all three axes
- Check UI responsiveness on various devices
- Profile performance impact of changes

## Reporting Issues

Include:
- Device model and Android version
- Steps to reproduce
- Expected vs actual behavior
- Relevant logs or error messages

## Pull Request Process

1. Update documentation for any changes
2. Test APK build completes successfully
3. Include link to test build or testing instructions
4. Request review from project maintainers

## Project Structure

```
Assets/
├── Scenes/          # Unity scenes
├── Scripts/         # C# MonoBehaviours and utilities
├── Prefabs/         # Reusable GameObject prefabs
├── Materials/       # Material and shader files
└── Resources/       # Runtime loadable assets
```

## Development Workflow

1. Clone repository: `git clone <repo-url>`
2. Open in Unity 2022 LTS
3. Open VROffsetScene.unity
4. Make changes and test in Play mode
5. Build and test APK on device
6. Commit and push changes
7. Create pull request

## Documentation

- Update README.md for major changes
- Add inline code comments for complex logic
- Update BUILD_CONFIGURATION.md if build process changes
- Include setup instructions for new features
