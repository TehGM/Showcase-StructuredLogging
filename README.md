# Structured Logging Showcase
This is code prepared for Dev Club presentation at work. Shows benefits of Structured Logging over older logging approaches.

## Project structure
Most of actual code happens in [Runner.cs](Showcase.StructuredLogging/Runner.cs). Other files used mainly for configuration.

### Branches
Master branch serves only as base case. All actual changes are in ordered `example/<number>-<title>` branches to present an incremental upgrade from base case.

### Secrets
[appsecrets.json](Showcase.StructuredLogging/appsecrets.json) is designed to hold secrets but is NOT git ignored - beware of that if forking the repository.

You'll need to add your own secrets, as the ones in [appsecrets.json](Showcase.StructuredLogging/appsecrets.json) are invalidated within DataDog and MongoDB for security.

### Notes
Most of examples have `EXAMPLE-NOTES.md` file, with some personal notes to make presenting easier. They're quickly put together as a personal reminder so they might be far from perfect, but they could contain some useful info!

## License
Copyright (c) 2021 TehGM 

Licensed under [MIT License](LICENSE).