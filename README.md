# C# Paint Application

A desktop painting application created with **C#**, **Windows Forms**, and **System.Drawing (GDI+)**.

## Features

- Freehand drawing and eraser tools
- Lines, rectangles, ellipses, and filled shapes
- Bézier curves, polygons, and curves
- Text placement and colour selection
- Gradient-filled rectangles
- Image loading and saving (JPEG, BMP, PNG options)

## Technology

- C# / Windows Forms
- .NET 5.0 (original project target; **out of support**)
- System.Drawing / GDI+

## Open the project

1. Use **Windows** and Visual Studio with the **.NET desktop development** workload.
2. Open `Paint_/Paint_.csproj` in Visual Studio.
3. Restore any required .NET 5 targeting/runtime components if prompted. .NET 5 is no longer supported; consider using an isolated development environment for older projects.
4. Build and run the application.

> **Note:** This repository preserves the original project source and target framework without modification. The code has not been build-tested as part of preparing this repository. Some features may have existing limitations or bugs.

## Repository layout

- `Paint_/Form1.cs` — painting logic and UI events
- `Paint_/Form1.Designer.cs` — Windows Forms layout
- `Paint_/Program.cs` — application entry point
- `Paint_/Resources/` — image assets
- `Paint_/Paint_.csproj` — project configuration

## About

A personal C# desktop application project shared as part of my programming portfolio.
