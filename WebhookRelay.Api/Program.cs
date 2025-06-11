using Hangfire;
using Hangfire.SqlServer;
using Serilog;
using WebhookRelay.Application.Services;
using WebhookRelay.Domain.Interfaces;
using WebhookRelay.Infrastructure.Interfaces;
using WebhookRelay.Infrastructure.Jobs;
using WebhookRelay.Infrastructure.Services;
using WebhookRelay.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddHangfire(configuration =>
    configuration.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

builder.Services.AddHangfireServer();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IWebhookHandler, WebhookHandler>();
builder.Services.AddScoped<IDLQService, DLQService>();
builder.Services.AddScoped<WebhookDLQJob>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCustomExceptionMiddleware();

app.MapControllers();
app.UseHangfireDashboard();

app.Run();