using TechBox.API;

try
{
    var builder = WebApplication.CreateBuilder(args);


    var app = builder
         .ConfigureServices()
         .ConfigurePipeline();

    app.Run();
}
catch (Exception ex) { throw ex; }
