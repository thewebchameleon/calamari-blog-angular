## *this project is still in early development ##
# Calamari Blog
A light-weight blogging client written in ASP.NET Core that reads from a headless CMS system called Squidex.


- Based off the the ASP.NET Core template for Angular included in Visual Studio 2017
- Serilog is used for logging events to [any location](https://github.com/serilog/serilog/wiki/Provided-Sinks) 

Caching
-------

The client will query the Squidex API for content when needed and cache it to a CacheProvider. Cached items are cleared via a Webhook configured in Squidex for all content publish events
