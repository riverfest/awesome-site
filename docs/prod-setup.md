# Production setup

*See ADR002 for why this was mode, and what it must contain.*

## Requirements

The following must be installed prior to hosting the project:

- NPM
- Gulp v4
- Latest .NET Core SDK

## Production pipeline

To get up and running, these are the only steps that ar needed:

- Get sources from repo
- Run `gulp prod-deploy`
- Restart dotnet