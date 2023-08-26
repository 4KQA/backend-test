using Nerd.Api.Models;

namespace Nerd.Api.Contracts.Responses;

public record PersonResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Gender,
    int Age,
    bool IsAlive,
    GeoLocation Location
);