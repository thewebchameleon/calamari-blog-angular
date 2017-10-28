## *this project is still in early development ##
![VSTS](https://adrianbrink.visualstudio.com/_apis/public/build/definitions/d997519b-4e10-4936-b403-69ea97908441/10/badge)
# Calamari Blog
A light-weight blogging client written in ASP.NET Core that reads from a headless CMS system called Squidex.


- Based off the the ASP.NET Core template for Angular included in Visual Studio 2017
- Serilog is used for logging events to [any location](https://github.com/serilog/serilog/wiki/Provided-Sinks) 
- Responses are gzipped
- Uses the [Milligram](http://milligram.io/) CSS framework

Caching
-------

The client will query the Squidex API for content when needed and cache it to a CacheProvider. Cached items are cleared via a Webhook configured in Squidex for all content publish events

Demo
----

http://calamari-blog-demo.azurewebsites.net
