using API;
using API.Middlewares;
using API.Schedulers;
using Coravel;
using Microsoft.AspNetCore.Authentication.OAuth;
using Serilog;
const string _policy = "CorsPolicy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ServicesRegistry(builder.Configuration);
builder.Services.AddScheduler();
builder.Services.AddTransient<CreateBatchScheduler>();
builder.Services.AddTransient<CheckStatusScheduler>();
builder.Services.AddTransient<MorningScheduler>();

builder.Services.AddHttpClient();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: _policy, builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

Log.Logger = new LoggerConfiguration()
    //.MinimumLevel.Warning()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(_policy);
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();

//app.Services.UseScheduler(scheduler =>
//{
//    scheduler.Schedule<CreateBatchScheduler>()
//        //.Cron(builder.Configuration.GetSection("CronExpressions:CreateBatch").Value); 
//        .EveryMinute();
//});

//app.Services.UseScheduler(scheduler =>
//{
//    scheduler.Schedule<MorningScheduler>()
//        //.Cron(builder.Configuration.GetSection("CronExpressions:CreateBatch").Value); 
//        .EveryMinute();
//});

//app.Services.UseScheduler(scheduler =>
//{
//    scheduler.Schedule<EveningScheduler>()
//        //.Cron(builder.Configuration.GetSection("CronExpressions:CreateBatch").Value); 
//        .EveryMinute();
//});

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<CheckStatusScheduler>()
    //.Cron(builder.Configuration.GetSection("CronExpressions:CheckBatchStatus").Value);
    .EveryFiveMinutes();
});

app.MapControllers();

app.Run();
