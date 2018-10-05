# The production pipeline and dependencies must be documented

To ensure a reproduceable prod environment, all steps and dependencies in the prod pipeline must be documented.

## Considered Options

* Public git repo with full config: This allows full transparancy for the production environment, but also perhaps to easy a hacker target.
* Private git repo with full config: Same benefits as above, but instead of risk, it requires another shared git repo that all contributers have access to.
* General documentation in this repo: Will not be as detailed, but enough to reproduce a working prod environement.

## Decision Outcome

* Chosen option: General documentation in this repo
* At the moment we do not have another shared repo, and are not prepared to introduce a such, so we stick with the 3rd option.
* The documentation must be placed in `/docs/prod-setup.md`.
* It must contain, at the very least, steps to get a running production environment, plus a list of dependencies.