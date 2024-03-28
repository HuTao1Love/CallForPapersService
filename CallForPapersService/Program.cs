using System.Text.Json.Serialization;
using Application.Extensions;
using Database.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("Properties/appsettings.json", false);

builder.Services
    .LoadApplication()
    .LoadDatabase(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{nameof(CallForPapersService)} API V1");
        c.RoutePrefix = string.Empty;
    });

    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();