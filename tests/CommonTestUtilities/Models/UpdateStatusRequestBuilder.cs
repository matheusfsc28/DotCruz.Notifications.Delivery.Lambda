using Bogus;
using DotCruz.Notifications.Delivery.Lambda.Models;

namespace CommonTestUtilities.Models;

public class UpdateStatusRequestBuilder
{
    private bool _success;
    private string? _errorMessage;

    public UpdateStatusRequestBuilder()
    {
        var faker = new Faker();
        _success = faker.Random.Bool();
        _errorMessage = _success ? null : faker.Lorem.Sentence();
    }

    public UpdateStatusRequestBuilder WithSuccess(bool success)
    {
        _success = success;
        return this;
    }

    public UpdateStatusRequestBuilder WithErrorMessage(string? errorMessage)
    {
        _errorMessage = errorMessage;
        return this;
    }

    public UpdateStatusRequest Build()
    {
        return new UpdateStatusRequest
        {
            Success = _success,
            ErrorMessage = _errorMessage
        };
    }
}
