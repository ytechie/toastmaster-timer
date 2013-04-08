using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpeechTimer.Backend
{
	[TestClass]
	public class TimerConfigurationTests
	{
		private TimerConfiguration _t;

		[TestInitialize]
		public void TestInit()
		{
			_t = new TimerConfiguration(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(2));
		}

		[TestMethod]
		public void GetSetGreenTime()
		{
			_t.GreenTime = TimeSpan.FromMinutes(5);
			Assert.AreEqual(TimeSpan.FromMinutes(5), _t.GreenTime);
		}

		[TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void SetGreenTimeZeroThrowsException()
		{
			_t.GreenTime = TimeSpan.FromMinutes(0);
		}

		[TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void SetGreenTimeNegativeThrowsException()
		{
			_t.GreenTime = TimeSpan.FromMinutes(-5);
		}

		[TestMethod]
		public void GetSetRedTime()
		{
			_t.RedTime = TimeSpan.FromMinutes(5);
			Assert.AreEqual(TimeSpan.FromMinutes(5), _t.RedTime);
		}

		[TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void SetRedTimetZeroThrowsException()
		{
			_t.RedTime = TimeSpan.FromMinutes(0);
		}

		[TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void SetRedTimetNegativeThrowsException()
		{
			_t.RedTime = TimeSpan.FromMinutes(-5);
		}

		[TestMethod]
		public void EndIs30SecondsAfterRedTime()
		{
			_t.RedTime = TimeSpan.FromMinutes(2);

			Assert.AreEqual(TimeSpan.FromSeconds(150), _t.DoneTime);
		}

		[TestMethod]
		public void YellowTimerHalfwayBetweenGreenAndRed()
		{
			_t.GreenTime = TimeSpan.FromMinutes(1);
			_t.RedTime = TimeSpan.FromMinutes(2);

			Assert.AreEqual(TimeSpan.FromSeconds(90), _t.YellowTime);
		}
	}
}
