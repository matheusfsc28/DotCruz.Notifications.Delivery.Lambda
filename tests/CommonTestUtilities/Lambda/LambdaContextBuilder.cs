using Moq;
using Amazon.Lambda.Core;

namespace CommonTestUtilities.Lambda;

public class LambdaContextBuilder
{
    private readonly Mock<ILambdaContext> _context;
    private readonly Mock<ILambdaLogger> _logger;

    public LambdaContextBuilder()
    {
        _context = new Mock<ILambdaContext>();
        _logger = new Mock<ILambdaLogger>();
        _context.Setup(c => c.Logger).Returns(_logger.Object);
    }

    public ILambdaContext Build() => _context.Object;
    public Mock<ILambdaContext> GetMock() => _context;
    public Mock<ILambdaLogger> GetLoggerMock() => _logger;
}
