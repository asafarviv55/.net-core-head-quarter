# Bank HeadQuarter

Windows Forms desktop application for bank headquarters management.

## Prerequisites

- .NET 6.0 SDK
- Windows OS

## Build & Run

```bash
cd Bank-HeadQuarter
dotnet restore
dotnet run
```

## Build Release

```bash
dotnet publish -c Release -r win-x64 --self-contained
```

## Project Structure

```
Bank-HeadQuarter/
├── Form1.cs           # Main form
├── Form1.Designer.cs  # Form designer code
├── Program.cs         # Application entry point
└── Bank-HeadQuarter.csproj
```
