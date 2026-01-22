# Air Pollution Prediction API

**Air Pollution Prediction API** — a multi-component project for predicting air pollution using machine learning models, data collection tasks, and a web API to serve predictions.

This project combines Python and .NET components to collect data, run prediction models, and expose the results via a backend API. It includes worker clients, a Python model, and a .NET Web API server.

## 🧠 Overview

Air pollution forecasting is solved by training regression models that estimate particulate matter (e.g., PM2.5) levels based on historical environmental data. This project structures the workflow into:

- **Client workers** that schedule and execute pipeline tasks.
- **Python model code** to build, train, and serve prediction models.
- **Server (Web API)** that exposes prediction endpoints used by clients or frontend applications.

## 🚀 Features

- Background task execution with clients which are executed by Windows Task Scheduler
- Machine learning models for air pollution prediction
- REST API to serve predictions
- Cross-language components (C#, Python) collaborating
- Automated data fetch and preprocessing

## 📂 Project Structure

```text
.
├── ClientDelete/   # Background job responsible for permanently removing outdated records (older than 2 days)
├── ClientPost/     # Background job that fetches open-source data from public APIs and stores it in the local database
├── Model/          # Machine learning model XGBoost and training code writed in python
├── Server/         # .NET Web API for serving predictions
└── README.md
```
