# Probability Calculator

React + .NET 6 Web API

## Overview

This is a simple Probability Calculator application built using:

-   ASP.NET Core Web API (.NET 6) for backend
-   React (Vite) for frontend
-   xUnit for unit testing
-   Text file logging (no database used)

The application calculates probability using two formulas:

1.  CombinedWith → P(A) × P(B)
2.  Either → P(A) + P(B) − P(A)P(B)

Valid input range for A and B is between 0 and 1 (inclusive).

------------------------------------------------------------------------

## Backend Structure

-   Controller → Handles HTTP requests
-   Service → Contains calculation and validation logic
-   Logging → Writes successful calculations to a text file
-   Models → Request and response objects

The backend returns: - 200 for success - 400 for validation errors

------------------------------------------------------------------------

## How to Run Backend

1.  Navigate to:
    backend/ProbabilityCalculator.Api/ProbabilityCalculator.Api

2.  Run: dotnet run

3.  Open Swagger: https://localhost:{port}/swagger

------------------------------------------------------------------------

## How to Run Frontend

1.  Navigate to: frontend/probability-calculator-ui

2.  Install dependencies: npm install

3.  Start app: npm run dev

4.  Open: http://localhost:5173

------------------------------------------------------------------------

## How to Run Tests

From the backend folder:

dotnet test

Unit tests cover: - CombinedWith calculation - Either calculation -
Invalid input handling - Logging behavior

------------------------------------------------------------------------

## Logging

All successful calculations are saved in:

backend/ProbabilityCalculator.Api/logs/calculations.log

Each log entry includes: - UTC date and time - Calculation type - Input
values - Result

Logging is thread-safe.

------------------------------------------------------------------------

## Design Notes

-   Clean separation of concerns
-   Simple and maintainable structure
-   No overengineering
-   Production-aware validation and CORS handling
