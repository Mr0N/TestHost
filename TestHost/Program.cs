var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();


app.MapGet("/test", (HttpContext a) =>
{
    var response = a.Request.Headers.UserAgent;
    Console.WriteLine(response);
    return new { check = true };
});
Task.Run(async () =>
{
    await Execute();
});
app.Run("http://localhost:1111");



async Task Execute()
{
    //const string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36";
    const string userAgent = "12345";
    using var client = new HttpClient();
    for (int i = 0; i < 5; i++)
    {
        client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
        string text = await client.GetStringAsync("http://localhost:1111/test");
        Console.WriteLine(text);
    }

}