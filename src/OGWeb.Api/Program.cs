using OGWeb.Core;
using OGWeb.Core.Extensions;
using OGWeb.Core.Settings;
using OGWeb.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCore();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.Configure<DocumentSetting>(builder.Configuration.GetSection(nameof(DocumentSetting)));
builder.Services.AddCors(opt =>
                            opt.AddDefaultPolicy(builder =>
                            builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
