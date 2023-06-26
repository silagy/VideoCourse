using Carter;
using VideoCourse.Api.Middlewares;
using VideoCourse.Application;
using VideoCourse.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

// Add Carter
builder.Services.AddCarter();
builder.Services.AddCors();

builder.Services.AddTransient<IProblemDetailsWriter, CustomProblemDetailsWriter>();

//builder.Services.AddTransient<GlobalExceptionMiddleware>();

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyBuilder =>
{
    policyBuilder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler("/error");
app.MapCarter();
//app.CustomProblemDetails();
//app.UseMiddleware<GlobalExceptionMiddleware>();
//app.MapControllers();

app.Run();
