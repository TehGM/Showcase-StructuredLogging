# 1 - Adding a proper logger
First example replaces `Console.WriteLine` with an `ILogger` injected into the service.

`ILogger` is supported out of the box by ASP.NET Core applications, but other .NET Core Applications (such as this one) can make use of it by installing a few packages:
- [Microsoft.Extensions.Logging](https://www.nuget.org/packages/Microsoft.Extensions.Logging/)
- [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection)

I also use [Serilog](https://serilog.net/) which is the most popular logging library for .NET - but others should work just fine!