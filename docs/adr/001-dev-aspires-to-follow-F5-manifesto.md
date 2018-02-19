# The Dev environment aspires to follow "The F5 Manifesto For .NET Developers"

Bringing in new people on the project should be very simple. How do we best achieve this?

## Considered Options

* [The F5 Manifesto for .NET Developers](https://www.khalidabuhakmeh.com/the-f5-manifesto-for-net-developers) - (henceforth "The F5 Manifesto) dictactes a set of rules for exactly how accessible a project should be, and how it should be that.
* Letting dev and prod be "equal": Use the same tooling and approach, for easier testing, and avoid "but it works on my machine!".
* Documentation and scripting: Simply document the various steps needed for dev, test and prod, but also supply various scripting facilities for different platforms.

## Decision Outcome

* Chosen option: [The F5 Manifesto](https://www.khalidabuhakmeh.com/the-f5-manifesto-for-net-developers)
* We allow a current requirement of installing .NET Core, but that should eventually also be scripted.
* We disallow introducing "dev side" tooling like NPM. It can, however, be a requirement in prod, and currently is.
* This also means that MADR is not fully implemented, as the index file is missing. We believe that the folder structure is sufficient anyways. :)