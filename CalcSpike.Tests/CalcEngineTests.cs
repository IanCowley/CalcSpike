using CalcSpike.Core;
using Machine.Specifications;

namespace CalcSpike.Tests
{
	[Subject(typeof(CalcEngine))]
    public class CalcEngineTests
	{
	    Establish context = () =>
	    {
	        _calcEngine = new CalcEngine();
	    };

	    static CalcEngine _calcEngine;

	    public class when_adding_two_numbers_together
	    {
	        Because of = () => _result = _calcEngine.Add(1, 2);

	        It should_have_calculated_result = () => _result.ShouldEqual(3);

            static int _result;
	    }
    }
}
