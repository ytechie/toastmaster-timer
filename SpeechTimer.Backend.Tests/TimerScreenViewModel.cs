using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace SpeechTimer.Backend
{
	[TestClass]
	public class TimerScreenViewModel_Tests
	{
		private TimerScreenViewModel _viewModel;
		private ITimer _mockTimer;
		private INow _mockNow;

		[TestInitialize]
		public void Init()
		{
			_mockTimer = MockRepository.GenerateMock<ITimer>();
			_mockNow = MockRepository.GenerateMock<INow>();

			_viewModel = new TimerScreenViewModel(_mockTimer, _mockNow);
		}

		[TestMethod]
		public void TimerStartsAtZero()
		{
			Assert.AreEqual(TimeSpan.Zero, _viewModel.Elapsed);
		}

		[TestMethod]
		public void FormattedTimerStartsAtZero()
		{
			Assert.AreEqual("00:00", _viewModel.ElapsedFormatted);
		}

		[TestMethod]
		public void TimerResetsToZero()
		{
			_mockNow.Stub(x => x.Now).Return(DateTime.Parse("2013-01-01 1:00:00am")).Repeat.Once();
			_mockNow.Stub(x => x.Now).Return(DateTime.Parse("2013-01-01 1:00:01am")).Repeat.Once();

			_viewModel.Start();
			_viewModel.Stop();
			_viewModel.Reset();

			Assert.AreEqual(TimeSpan.Zero, _viewModel.Elapsed);
		}

		[TestMethod]
		public void ElapsedUpdatesCorrectlyAfterStarting()
		{
			_mockNow.Stub(x => x.Now).Return(DateTime.Parse("2013-01-01 1:00:00am")).Repeat.Once();
			_mockNow.Stub(x => x.Now).Return(DateTime.Parse("2013-01-01 1:00:01am")).Repeat.Once();

			_viewModel.Start();
			Assert.AreEqual(TimeSpan.FromSeconds(1), _viewModel.Elapsed);
		}

		[TestMethod]
		public void TimerSetCorrectlyWhenStarting()
		{
			_mockTimer.Expect(x => x.Interval = TimeSpan.FromSeconds(1));

			_viewModel.Start();

			_mockTimer.VerifyAllExpectations();
		}

		[TestMethod]
		public void ElapsedPropertyChangedRaised()
		{
			var propertyChanged = new List<string>();

			_viewModel.PropertyChanged += (sender, args) => propertyChanged.Add(args.PropertyName);
			_mockTimer.Raise(x => x.Tick += null, null, null);
			
			Assert.AreEqual(2, propertyChanged.Count);
			Assert.AreEqual(true, propertyChanged.Contains("Elapsed"));
			Assert.AreEqual(true, propertyChanged.Contains("ElapsedFormatted"));

			_mockTimer.VerifyAllExpectations();
		}

		[TestMethod]
		public void StopTimerWhenStopped()
		{
			_mockTimer.Expect(x => x.Stop());

			_viewModel.Stop();

			_mockTimer.VerifyAllExpectations();
		}

	}
}
