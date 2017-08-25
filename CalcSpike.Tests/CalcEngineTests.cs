using CalcSpike.Core;
using Machine.Specifications;

namespace CalcSpike.Tests
{
	[Subject(typeof(CalcEngine))]
    public class CalcEngineTests
	{
	    Establish context = () =>
	    {
	        _resultPersistence = new InMemoryResultPersistence();
            _calcCache = new CalcCache(_resultPersistence, new BasicLogger());
	        _calcEngine = new CalcEngine();
	    };

	    public class when_adding_two_numbers_together
	    {
	        Because of = () => _result = _calcEngine.Add(1, 2);

	        It should_have_calculated_result = () => _result.ShouldEqual(3);

	        It should_have_persisted_result = () => _resultPersistence.Exists(1, 2).ShouldEqual(true);

            static int _result;
	    }
		
	    static CalcEngine _calcEngine;
	    static CalcCache _calcCache;
	    static ICalcResultPersistence _resultPersistence;
	}
}
