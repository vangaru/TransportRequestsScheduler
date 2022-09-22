using System.Collections;
using BSTU.RequestsScheduler.Interactor.Tests.Utils;

namespace BSTU.RequestsScheduler.Interactor.Tests.TestData
{
    public class BusStopNamesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (string name in ConfigurationMock.BusStopNames)
            {
                yield return new object[] { name };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}