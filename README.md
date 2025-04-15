# Lab Management App

## Description

This is a specialized application designed for managing the testing and quality control processes of Nuclear Magnetic Resonance (NMR) probes. It allows users to record test results for various probe performance metrics, track calibration records, and manage essential information related to each probe. The application consists of a backend ASP.NET Core API and a frontend built with Blazor Server and MudBlazor. An WPF desktop client is also included to access the same backend API.

## Tech Stack

- **Backend:** ASP.NET Core Web API (.NET 8.0 recommended)
- **Frontend:** Blazor Server (.NET 8.0 recommended)
- **UI Components:** MudBlazor
- **Data Access:**
  - Option 1: In-memory repository (for simple setups or testing)
  - Option 2: EF Core with SQLite (for persistent data)
- **Optional Desktop Client:** WPF (.NET 8.0 recommended)
- **API Documentation:** Swagger

## Getting Started

1.  **Clone the Repository:**

    ```bash
    git clone [repository_url]
    cd LabManagementApp
    ```

    _(Replace `[repository_url]` with the actual URL of your repository)_

2.  **Navigate to the Solution Directory:**

    ```bash
    cd LabManagementApp.sln
    ```

3.  **Build the Solution:**

    ```bash
    dotnet build
    ```

4.  **Run the Backend API:**

    - Navigate to the `LabManagementApp.API` directory:
      ```bash
      cd LabManagementApp.API
      ```
    - Run the API:
      ```bash
      dotnet run
      ```
    - The API will typically start on `https://localhost:[port]` (check the console output).
    - You can access the Swagger UI at `https://localhost:[port]/swagger` to explore the API endpoints.

5.  **Run the Blazor Frontend:**

    - Navigate to the `LabManagementApp.UI.Blazor` directory:
      ```bash
      cd LabManagementApp.UI.Blazor
      ```
    - Run the Blazor app:
      ```bash
      dotnet run
      ```
    - The Blazor app will typically start on `https://localhost:[another_port]` (check the console output).

6.  **Run the WPF Desktop Client:**
    - Navigate to the `LabManagementApp.UI.WPF` directory:
      ```bash
      cd LabManagementApp.UI.WPF
      ```
    - You can run the WPF application directly from your IDE

## Features

<!-- List the main features of the solution here -->

## Repository Structure

The repository contains the following projects:

- **`LabManagementApp.API`:** This project contains the ASP.NET Core Web API backend. It handles the business logic and data access (either in-memory or via EF Core). It exposes RESTful endpoints for managing lab samples.
- **`LabManagementApp.UI.Blazor`:** This project is the Blazor Server frontend. It provides a user interface using MudBlazor components for interacting with the backend API to manage lab samples.
- **`LabManagementApp.Shared`:** This class library contains Data Transfer Objects (DTOs) that are shared between the backend and frontend for consistent data exchange.
- **`LabManagementApp.Infrastructure`:** This project houses the infrastructure-related code, specifically the Entity Framework Core context and the repository implementation for data access when using EF Core.
- **`LabManagementApp.UI.WPF`:** This project is a WPF desktop application that provides an alternative user interface to interact with the same backend API. It follows the MVVM pattern and includes basic functionality for viewing and managing samples.
