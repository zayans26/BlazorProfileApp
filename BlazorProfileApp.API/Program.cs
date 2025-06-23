using BlazorProfileApp.API.Models;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Register a singleton list of profiles (in-memory store)
builder.Services.AddSingleton<List<PersonProfile>>();

var app = builder.Build();

/// <summary>
/// POST: Add a new profile
/// </summary>
app.MapPost("/api/profile", (PersonProfile profile, List<PersonProfile> profiles) =>
{
    try
    {
        if (profile == null)
            return Results.BadRequest("Profile data is missing.");

        if (string.IsNullOrWhiteSpace(profile.FirstName) || string.IsNullOrWhiteSpace(profile.LastName))
            return Results.BadRequest("First name and last name are required.");

        // Auto-increment ID
        profile.Id = profiles.Any() ? profiles.Max(p => p.Id) + 1 : 1;
        profiles.Add(profile);

        return Results.Ok(profile);
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while adding the profile: {ex.Message}");
    }
});

/// <summary>
/// PUT: Update an existing profile
/// </summary>
app.MapPut("/api/profile/{id}", (int id, PersonProfile updated, List<PersonProfile> profiles) =>
{
    try
    {
        var existing = profiles.FirstOrDefault(p => p.Id == id);
        if (existing == null)
            return Results.NotFound($"Profile with ID {id} not found.");

        existing.FirstName = updated.FirstName;
        existing.LastName = updated.LastName;
        existing.Email = updated.Email;
        existing.PhoneNumber = updated.PhoneNumber;

        return Results.Ok(existing);
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while updating the profile: {ex.Message}");
    }
});

/// <summary>
/// GET: Get a profile by ID
/// </summary>
app.MapGet("/api/profile/{id}", (int id, List<PersonProfile> profiles) =>
{
    try
    {
        var profile = profiles.FirstOrDefault(p => p.Id == id);
        return profile is not null
            ? Results.Ok(profile)
            : Results.NotFound($"Profile with ID {id} not found.");
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while retrieving the profile: {ex.Message}");
    }
});

/// <summary>
/// GET: Get all profiles (Optional)
/// </summary>
app.MapGet("/api/profile", (List<PersonProfile> profiles) =>
{
    try
    {
        return Results.Ok(profiles);
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while retrieving all profiles: {ex.Message}");
    }
});

app.Run();
