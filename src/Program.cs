WebApplication.CreateBuilder(args)
              .AddInstallers()
              .Build()
              .MapEndpoints()
              .Run();

