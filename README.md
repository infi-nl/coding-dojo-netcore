# Infi '.NET Core ❤ OSX/Linux' Dojo

Welcome to this self-paced Coding Dojo about cross-platform .NET Core development.

## About the Dojo

At Infi, a "Coding Dojo" is an event that typically holds the middle ground between:

- an instructor-led workshop
- cooperative self-study (for lack of a better term)

The material in this repository is a guide through the topic.
But _you_ are the explorer, and you are _meant_ to go off the beaten path, time permitting.

Supposing you're new to .NET and C#, expect to spend around 60 to 120 minutes for the guided part.
At the end there are several suggestions for topics to spend some extra time on.

### Target Audience

There are many good tutorials and online courses around .NET Core.
The tutorials are often of the "Getting Started" type, aimed at beginners.
Online courses are aimed at folks aiming to spend a lot of time with the platform.

This Dojo tries to be different by assuming you have _some_ experience with (web) development, in any kind of tech stack.
It should guide you along the various features of the technology stack.
The fun part will be to compare it to the tech stack(s) you might already know.
To boast how much better "your" tech stack is at something, but perhaps also to learn a few tricks from "that other" tech stack.

Depending on how skilled you are, you will have time to stray further off the beaten path.

### What this Dojo is not

The Dojo was created not by teachers or full-time course-material writers, but instead by real developers.
As such, this material:

- Is not "top-notch" course-material, but a rough guide through the topic instead;
- Is not in-depth or extensive, but focused on showing a variety of interesting parts instead;

But luckily there's plenty of those other kinds of resources around on the internet.

Also important to note: this codebase is _not_ exemplary for production applications.
We normally value things like sensible test coverage, domain-driven design, CQRS, the SOLID principles, while avoiding things like over-engineering and "not-invented-here syndrome".
This codebase willingly violates several of those best practices, so that we can focus on guiding you along the platform features.

### About the Code

This codebase is structured as follows:

- The `master` branch, containing merely the basics for the repo, including **the guide** (but not any code).
- The `solution` branch, containing **all the spoilers!**: the finished codebase (your code should resemble this at the end of the guide).
- A set of `step-##-start` tags, pointing to commits that show the codebase at the _start_ of a step.

Cloning the repository will be part of one of the early steps.
Note that the code in this repo is [MIT licensed](LICENSE.txt), and that we welcome improvements and fixes!

### Prerequisites

We assume that you have an operating system that supports .NET Core 2.x (tested with 2.0).
Windows, macOS, and most mainstream Linux flavors are all supported.
[**The first thing you need is to download the .NET Core SDK**](https://www.microsoft.com/net/download/).

**The second thing you need is an editor.**

Although technically you can use Vim or Notepad or whatever to write code, we recommend using an IDE.
Some of us love and use [Jetbrains Rider](https://www.jetbrains.com/rider/), and we certainly recommend trying it.
It works on all platforms, but has a price tag (there's a trial available though).

Another great (though more minimal and slightly tougher) option is [VS Code](https://code.visualstudio.com/).
It's gratis, cross-platform, and lightweight.

Finally, you could use [Visual Studio 2017](https://www.visualstudio.com/vs/community/).
On Windows, that's definitely a great option still, and the Community Edition is gratis and works just fine.
It's a pretty hefty download though.

PS. Note that the "Visual Studio for Mac" IDE is ultimately a clone of Xamarin Studio (as opposed to a port of VS itself).
Although it should theoretically work, it's untested.

## The Guide

### Step 00 - Exploring the dotnet CLI

Before, there was ".NET", a.k.a. "The 'Full' .NET Framework".
This was a Windows-only runtime for .NET applications.
There was also an API-compatible clone called Mono allowing you to build the same code to make it run on other platforms (Linux, or iOS/Android via Xamarin).

Now there are _two_ new things:

- **.NET Standard (2.x currently)**: an "API specification" for .NET runtimes, similar to the Android API "Level";
- **.NET Core (2.x currently)**: an implementation of .NET Standard, available builds cross-platform, and sources available under the MIT license;

To _create_ .NET Core applications, you need to download the SDK.
Check the Prerequisites above for instructions, you should be able to run this from your favorite terminal:

```bash
dotnet --version
# Example output: 2.1.200

dotnet --help
# Shows CLI commands
```

Confirm this is working properly before continuing.

Before we move to the code from this guide, let's first try our setup.
Create a clean, empty folder, go there with your terminal, and execute:

```bash
dotnet new --help
```

This shows a lot of stuff, and at the bottom it shows all the pre-installed project templates.
Let's skip the boring old Console Application, and dive straight into things:

```bash
# Execute in your fresh "test" folder:
dotnet new webapi
```

Follow this by:

```bash
dotnet run
# Outputs "Now listening on: http://localhost:5000" or similar
```

The root of that address will be empty.
But on [http://localhost:5000/api/values](http://localhost:5000/api/values) you will find that the API responds to your `GET` just fine.

Congratulations, you're now a .NET Developer!

In fact, you're an _ASP.NET Core_ developer.
ASP.NET Core is the cross-platform web application framework that runs on top of .NET Core.
You can create server-side websites with client-side components with it (ASP.NET Core "MVC").
In this Dojo however, we will focus on ASP.NET Core "Web API" instead, and use a simple handcrafted SPA to talk to it.

**Recommended bonus**: if things are going smooth, we recommend spending about 5 minutes to explore the created project in your IDE.
For example, open the `test.csproj` file in Jetbrains or Visual Studio, or the project folder in VSCode.
See what the CLI generated for you.

### Step 01 - Running the Dojo Code

We're ready to dive in. Start with these steps:

1. Clone this repository (or your own fork, if you like);
1. Check out the `step-01-start` tag;

From here on out there are two ways to follow the Dojo.
You can mix and match approaches, which you take is a matter of preference.
You can either:

1. Do work on detached heads (the tag commit), discard after each step, and start the next step fresh from the repo's code.
1. Start with `git checkout -b my-dojo-attempt` or some such, and commit your own results as you go.

In any case, now that you're set up, open up the `InfiCoreDojo.sln` (VS2017, Jetbrains), or the root folder itself (VSCode).
Note that you might need to mark the `InfiCoreDojo.Api` as the Startup Project in your IDE.
Try to "Build" the solution, and then "Debug" or "Start with Debugger" option.
Typically this is either the F5 key or some main menu option.

Open `http://localhost:5000` if it hasn't opened automatically, and you should see the application's welcome screen.
It's a light-purle site with "WELCOME" in big friendly letters.

Congratulations, you're up and running!

**Recommended bonus**: check out the application's current state, click through the GUI, check out the app.
Also, check out **the "SWAGGER" button top left** and give the API endpoints a go.
Finally, find `PlayerController.cs` and place a debugger breakpoint at the first line of the `Choose(...)` method.
Click a bit in the App and see that your IDE hits the break point.

### Step 02 - Codebase Layout

The starting point for this step is the same as for step 1.
If you want to be completely fresh, reset your working copy and check out `step-02-start`.

Let's investigate the codebase layout a bit.
Here's the basic structure of our application:

- The `InfiCoreDojo.Api` project contains the main project, and is also the "composition root". It contains:
  - `Program.cs` for running/hosting the application (the ".NET Core" part)
  - `Startup.cs` for bootstrapping the API bits (the "ASP.NET Core" part)
  - Several `Controllers` corresponding to API endpoints
  - A `wwwroot` containing the SPA as part of the API project (this could've also been hosted separately, but then we'd have CORS stuff to handle)
- The `InfiCoreDojo.Api.Tests` project. Typically each .NET project has a sibling `.Tests` project for unit and/or integration tests.
- The `InfiCoreDojo.DataAccess` project. For demo purposes we'll have different data stores in one project.
- The `InfiCoreDojo.Domain` project with "entities".

The architecture of this application isn't great.
But remember: the point is to go on a Tour, not to design the perfect N-Tier Microservices Containerized Strangler Application Monolyth™.
It showcases how you can have multiple projects that reference eachother.

Now, to your exercise, for this step, try to:

- Use your IDE's filter features to find all classes with "Player" in them;
- Start by navigating to the `PlayerController` class, find the _current_ controller
- Use your IDE's "go to definition" feature to drill down to the `IPlayerDal` interface
- Use your IDE's "find implementations" feature to navigate to the (only current) implementation of that interface
- Investigate that Player DAL, drill down to the `Player` class and check that one out too

This step is complete when you've got a feeling you're comfortable navigating the solution.
Being able to find stuff will be instrumental for the next steps.

Congratulations, you're now an _expert_ with your IDE!

**Recommended bonus**: if you have some spare time (because you are _that_ awesome!) then consider investigating the `wwwroot` contents, skimming through the JavaScript a bit.
In addition, you could check out the properties of projects, most notably the API project.

### Step 03 - Managing NuGet Packages

TODO

### Step 04 - Changing the Controller

TODO

### Step 05 - Dependency Injection

TODO

### Step 06 - Unit Tests

TODO

### Step 07 - Wrapping Up

TODO

Of course, this is not the end of things.
It's merely the beginning!
Check out the next section for suggestions for next steps.

## Exercises for the Reader

So, you've come this far, and you want more?
Here's a recommended set of things to follow up with.

- [EF Core on ASP.NET Core](https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/) is about using Microsoft's ORM "Entity Framework" to access your RDBMs (tip: consider using the [mssql-server-linux](https://hub.docker.com/r/microsoft/mssql-server-linux/) Docker images);
- [ASP.NET Core MVC](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/) rush through the tutorial to create an MVC application;
- [Dockerization](https://docs.microsoft.com/en-us/dotnet/core/docker/intro-net-docker) running .NET Core applications in Docker containers;
- [C# Language Tour](https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/index) if you want to see a bit more about the main .NET language;
- [SignalR](https://docs.microsoft.com/en-us/aspnet/core/signalr/get-started) for WebSockets communication between a browser client and ASP.NET Core application;
- [Hosting and Deployment](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy) for trying out hosting options for ASP.NET Core;
- **Console Application**: try to create a console application that reads from stdin and writes to a file, and run it on your own OS as well as another (e.g. in a VM or interactive container)

Perhaps all those options seem boring.
Or you've already done all of them!?
That probably means it's time to start building your first actual application with .NET.
Or send some mean tweets or write an angry blog post about how stupid .NET Core is.
All good to us, we just hope you learned a thing or two today ❤

## License

This code is provided under the [MIT license](LICENSE.txt).
