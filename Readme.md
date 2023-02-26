
# Summary

Setup for workshop focusing on observability and testing

## includes

* InfluxDB
* Grafana
* Prometheus
* Jeager
* MS SQL Server
* Dotnet 7 container (for dev)

# Getting started

## Option 1: Docker-compose

Start API (from inside `api` folder):

* Run docker-compose from folder `.devcontainer`
* Run database migrations: `dotnet ef database update`
* start API

Run Tests (from inside `k6` folder):

* Run task `npm run dev` to start webpack
* Run task `k6 intro` to run test

## Option 2: Devcontainer

Start API:

* Run docker-compose from folder `.devcontainer`
* Run task `init DB` to run migrations
* Run task `watch` to start API

Run Tests:

* Run task `npm run dev` to start webpack
* Run task `k6 intro` to run test

Links:

* Jeager: <http://localhost:16686/>
* Prometheus: <http://localhost:9090/>
* Grafana Dashboards: <http://localhost:3000/dashboards>
* API: <http://localhost:5065/>
