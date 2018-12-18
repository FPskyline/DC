using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Supermarket.Models.InitModels;
using MySQL.Data.EntityFrameworkCore.Extensions;
using Supermarket.Data;
using System.IO;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.FileProviders;


namespace Supermarket
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      services.AddDbContext<SupermarketDbContext>(options => options.UseMySQL(connectionString));
      services.AddTransient<DbInitializer>();
      // Add framework services.
      services.AddMvc().AddWebApiConventions();

      //services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
      services.AddSession();
      // Add framework services.
      services.AddMvc().AddWebApiConventions();

      services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
      #region CORS
      services.AddCors(options =>
     {
       options.AddPolicy("AllowSpecificOrigin",
                       builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
     });

      #endregion
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "Supermarket API", Version = "v1" });
        var basePath = PlatformServices.Default.Application.ApplicationBasePath;
        var xmlPath = Path.Combine(basePath, "Supermarket.xml");
        c.IncludeXmlComments(xmlPath);
      });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DbInitializer dbInitializer)
    {
      app.UseSession();
      app.Use(async (context, next) =>
      {
        await next();
        if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) && !context.Request.Path.Value.StartsWith("/api/"))
        {
          context.Request.Path = "/index.html";
          await next();
        }
      });
      app.Use(async (context, next) =>
      {
        if (context.Request.Path == "/ws")
        {
          if (context.WebSockets.IsWebSocketRequest)
          {
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await Echo(context, webSocket);
          }
          else
          {
            context.Response.StatusCode = 400;
          }
        }
        else
        {
          await next();
        }
      });


      app.UseCors("AllowSpecificOrigin");
      //app.UseStaticFiles(new StaticFileOptions()
      //{
      //  FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files")),
      //  RequestPath = new PathString("/src")
      //});

      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseMvcWithDefaultRoute();

      app.UseSwagger();

      app.UseSwaggerUI(c =>
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Supermarket API")
            );

      // Add  Data
      await dbInitializer.InitializeDataAsync();

      var webSocketOptions = new WebSocketOptions()
      {
        KeepAliveInterval = TimeSpan.FromSeconds(120),
        ReceiveBufferSize = 4 * 1024
      };
      app.UseWebSockets(webSocketOptions);


    }

    private async Task Echo(HttpContext context, WebSocket webSocket)
    {
      var buffer = new byte[1024 * 4];
      WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
      while (!result.CloseStatus.HasValue)
      {
        await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
      }
      await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }
  }
}
