// Create a new web application builder using command-line args
var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------
// Register Services
// -------------------------------------------

// Adds Razor Pages support (needed for _Host.cshtml)
builder.Services.AddRazorPages();

// Adds support for Blazor Server
builder.Services.AddServerSideBlazor();

// Registers HttpClient for dependency injection
// BaseAddress should point to the backend API URL
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5023/")  
});

// -------------------------------------------
// Configure the HTTP request pipeline
// -------------------------------------------

var app = builder.Build();

// Configure error handling and HSTS in non-dev environments
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");  // Fallback error page
    app.UseHsts(); // Enforce HTTP Strict Transport Security for 30 days
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Serve static content from wwwroot/
app.UseStaticFiles();

// Enable routing middleware
app.UseRouting();

// Map Blazor SignalR hub (required for real-time updates)
app.MapBlazorHub();

// Fallback to the _Host.cshtml page for client-side navigation
app.MapFallbackToPage("/_Host");

// Run the web app
app.Run();
