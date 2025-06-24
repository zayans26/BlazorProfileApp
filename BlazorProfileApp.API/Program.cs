using BlazorProfileApp.API.Models;  // Import the PersonProfile model
using Microsoft.AspNetCore.Http;    // For working with HTTP results and status codes

// Create a web application builder (initializes app configuration, DI, logging, etc.)
var builder = WebApplication.CreateBuilder(args);

// Register a singleton service to hold the in-memory list of PersonProfiles
// This list persists in memory for the lifetime of the app
builder.Services.AddSingleton<List<PersonProfile>>();

// Build the app pipeline
var app = builder.Build();

/// <summary>
/// POST: Create a new profile
/// URL: /api/profile
/// Accepts a PersonProfile object and adds it to the in-memory list
/// </summary>
app.MapPost("/api/profile", (PersonProfile profile, List<PersonProfile> profiles) =>
{
    try
    {
        // Return 400 if profile is missing
        if (profile == null)
            return Results.BadRequest("Profile data is missing.");

        // Validate required fields
        if (string.IsNullOrWhiteSpace(profile.FirstName) || string.IsNullOrWhiteSpace(profile.LastName))
            return Results.BadRequest("First name and last name are required.");

        // Assign a unique ID by incrementing the highest existing ID
        profile.Id = profiles.Any() ? profiles.Max(p => p.Id) + 1 : 1;

        // Add the new profile to the list
        profiles.Add(profile);

        // Return 200 OK with the created profile
        return Results.Ok(profile);
    }
    catch (Exception ex)
    {
        // Return 500 with the error message
        return Results.Problem($"An error occurred while adding the profile: {ex.Message}");
    }
});

/// <summary>
/// PUT: Update an existing profile by ID
/// URL: /api/profile/{id}
/// Updates the profile data in the in-memory list
/// </summary>
app.MapPut("/api/profile/{id}", (int id, PersonProfile updated, List<PersonProfile> profiles) =>
{
    try
    {
        // Find the profile with the given ID
        var existing = profiles.FirstOrDefault(p => p.Id == id);

        // Return 404 if not found
        if (existing == null)
            return Results.NotFound($"Profile with ID {id} not found.");

        // Update fields
        existing.FirstName = updated.FirstName;
        existing.LastName = updated.LastName;
        existing.Email = updated.Email;
        existing.PhoneNumber = updated.PhoneNumber;

        // Return 200 OK with the updated profile
        return Results.Ok(existing);
    }
    catch (Exception ex)
    {
        // Return 500 error
        return Results.Problem($"An error occurred while updating the profile: {ex.Message}");
    }
});

/// <summary>
/// GET: Get a profile by ID
/// URL: /api/profile/{id}
/// Returns the profile if it exists
/// </summary>
app.MapGet("/api/profile/{id}", (int id, List<PersonProfile> profiles) =>
{
    try
    {
        // Look for the profile
        var profile = profiles.FirstOrDefault(p => p.Id == id);

        // Return it if found, else 404
        return profile is not null
            ? Results.Ok(profile)
            : Results.NotFound($"Profile with ID {id} not found.");
    }
    catch (Exception ex)
    {
        // Return 500 error
        return Results.Problem($"An error occurred while retrieving the profile: {ex.Message}");
    }
});

/// <summary>
/// GET: Get all profiles
/// URL: /api/profile
/// Returns the entire list of profiles
/// </summary>
app.MapGet("/api/profile", (List<PersonProfile> profiles) =>
{
    try
    {
        // Return all profiles
        return Results.Ok(profiles);
    }
    catch (Exception ex)
    {
        // Return 500 error
        return Results.Problem($"An error occurred while retrieving all profiles: {ex.Message}");
    }
});

// Start the web application
app.Run();
