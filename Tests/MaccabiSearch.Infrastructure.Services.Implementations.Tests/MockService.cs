namespace MaccabiSearch.Infrastructure.Services.Implementations.Tests
{
    public class MockService : IMockService
    {
        public string Test()
        {
            return Constants.ItsWorking;
        }
    }
}