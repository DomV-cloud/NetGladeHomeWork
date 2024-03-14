using NetGlade.Infrastructure.DependencyInjection;
using NetGlade.Application.DependencyInjection;
using NetGlade.Application.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using NetGlade.Api.Middleware;
using NetGlade.Api.Filters;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();

    builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddDbContext<NetGladeContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
