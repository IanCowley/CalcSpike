using CalcSpike.Core;
using Machine.Specifications;

namespace CalcSpike.Tests
{
	[Subject(typeof(CalcEngine))]
    public class CalcEngineIntegrationTests
	{
	    Establish context = () =>
	    {
	        _calcAccountName = "TestAccountName1";

	        var storageAccountName = "I would replace this with the real thing";
	        var storageAccountKey = "I would replace this with the real thing";

            _resultPersistence = new AzureResultPersistence(
                new AzureResultTableStore(storageAccountName, storageAccountKey), 
                new AzureResultPublsher(new AzureResultQueueStore(storageAccountName, storageAccountKey)));

            _calcCache = new CalcCache(_resultPersistence, new BasicLogger());
	        _calcEngine = new CalcEngine(_calcCache, new BasicLogger());
	    };

	    public class when_adding_two_numbers_together
	    {
	        Because of = () => _result = _calcEngine.AddAsync(_calcAccountName, 1, 2);

	        It should_have_calculated_result = () => _result.ShouldEqual(3);

	        It should_have_persisted_result = () => _resultPersistence.Exists(_calcAccountName, 1, 2).ShouldEqual(true);

            static int _result;
	    }
		
	    static CalcEngine _calcEngine;
	    static CalcCache _calcCache;
	    static ICalcResultPersistence _resultPersistence;
	    static string _calcAccountName;
	}
}
