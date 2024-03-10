using MaccabiSearc.Tests.Utils;

namespace MaccabiSearch.Tests.Utils
{
    public class MockService : IMockService
    {
        public string Test()
        {
            return Constants.ItsWorking;
        }
    }
}