# Air Pollution Prediction API

**Air Pollution Prediction API** — a multi-component project for predicting air pollution using machine learning models, data collection tasks, and a web API to serve predictions.

This project combines Python and .NET components to collect data, run prediction models, and expose the results via a backend API. It includes worker clients, a Python model, and a .NET Web API server.

## Overview

Air pollution forecasting is solved by training regression models that estimate particulate matter (e.g., PM2.5) levels based on historical environmental data. This project structures the workflow into:

- **Client workers** that schedule and execute pipeline tasks.
- **Python model code** to build, train, and serve prediction models.
- **Server (Web API)** that exposes prediction endpoints used by clients or frontend applications.

## Features

- Background task execution with clients which are executed by Windows Task Scheduler
- Machine learning models for air pollution prediction
- REST API to serve predictions
- Cross-language components (C#, Python) collaborating
- Automated data fetch and preprocessing

## Project Structure

```text
.
├── ClientDelete/   # Background job responsible for permanently removing outdated records (older than 2 days)
├── ClientPost/     # Background job that fetches open-source data from public APIs and stores it in the local database
├── Model/          # Machine learning model XGBoost and training code writed in python
├── Server/         # .NET Web API for serving predictions
└── README.md
```

## Technologies Used

- **.NET (C#)** – Web API and client workers
- **Python** – Data science tasks, ML models, data preprocessing
- **scikit-learn / other ML libraries** – model training and evaluation
- **REST API** – for prediction serving


## What I Learned
- **Multi-language Integration**
  - Working with diffrent languages and integrating them.<br>
- **Background Task Scheduling**
  - Background Script that schedule periodic data fetch. <br> 
- **Machine Learning Workflow**
  - Creating own datasets for training and tesing model accuracy.
  - Preparing datasets with methods such as standarization, feature selection, oversampling.
  - Comparing different machine learning algorithms to evaluate the best model for the problem.
 
## Challenges 
- Maintaining data consistency in the database to always provide correct, time-windowed data for the model.
- Collecting data from public API's and fetching them into models. Few features, that were used in model training, were available on public API's historical data, but were not included at real-time providing 

## Future Improvements
- Add frontend UI to visualize predicted pollution levels
- Implement auth with JWT Token bearer
- Dockerize each component for easier deployment
- CI/CD pipeline to automate training and deployment







