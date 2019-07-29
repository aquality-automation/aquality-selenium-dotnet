using System;

namespace Aquality.Selenium.Configurations
{
    public class TimeoutConfiguration : ITimeoutConfiguration
    {
        public TimeSpan Implicit => throw new NotImplementedException();

        public TimeSpan Script => throw new NotImplementedException();

        public TimeSpan PageLoad => throw new NotImplementedException();

        public TimeSpan Condition => throw new NotImplementedException();

        public TimeSpan PollingInterval => throw new NotImplementedException();
    }
}
