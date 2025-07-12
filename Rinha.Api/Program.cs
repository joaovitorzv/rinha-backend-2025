var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var apiUrl = Environment.GetEnvironmentVariable("PAYMENT_PROCESSOR_URL_DEFAULT");
var apiUrlFallback = Environment.GetEnvironmentVariable("PAYMENT_PROCESSOR_URL_FALLBACK");

var httpClient = new HttpClient();

app.MapGet("/health", async () =>
{
    var response = await httpClient.GetAsync($"{apiUrl}/payments/service-health");
    return response.Content.ReadAsStringAsync();
});

// TODO: process payments
app.MapPost("/payments", () => "Not Implemented!");

// TODO: payments summary
app.MapGet("/payments-summary", () => "Not Implemented!");

app.Run();
