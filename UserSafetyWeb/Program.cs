var builder = WebApplication.CreateBuilder (args);

builder.Services.AddRazorPages ();

// Registrera HttpClient för kommunikation med UserSafetyAPI
builder.Services.AddHttpClient ("UserSafetyAPI", client =>
{
    client.BaseAddress = new Uri ("http://localhost:5062");
});

var app = builder.Build ();

// Konfigurera HTTP request pipeline
if (!app.Environment.IsDevelopment ())
{
    app.UseExceptionHandler ("/Error");
    app.UseHsts ();
}

app.UseHttpsRedirection ();
app.UseStaticFiles ();
app.UseRouting ();
app.UseAuthorization ();
app.MapRazorPages ();
app.Run ();