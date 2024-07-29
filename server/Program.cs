var builder = WebApplication.CreateBuilder(args);
var CLIENT_URL = builder.Configuration.GetValue<string>("env:CLIENT_URL");
Console.WriteLine(CLIENT_URL);
// Add services to the container.
builder.Services.AddHttpLogging(o => { });
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Cors",
        policy =>
        {
            policy.WithOrigins(CLIENT_URL)
                .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();
app.UseHttpLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Cors");
app.UseAuthorization();

app.MapControllers();

app.Run();
