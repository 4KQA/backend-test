using Nerd.Api.Models;

namespace Nerd.Api.Contracts.Requests
{
    public record SurvivorsResponse(
        int PercentageSurvived,
        List<Person> People
    );
}