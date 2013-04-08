using System;
using SpeechTimer.Backend;
using Windows.UI.Xaml;

namespace SpeechTimer
{
	public class DefaultTimer : ITimer
	{
		private readonly DispatcherTimer _timer;
		public event EventHandler<EventArgs> Tick;

		public DefaultTimer()
		{
			_timer = new DispatcherTimer();
			_timer.Tick += (sender, o) =>
				{
					var t = Tick;
					if (t != null)
						t(sender, EventArgs.Empty);
				};
		}

		public TimeSpan Interval
		{
			get { return _timer.Interval; }
			set { _timer.Interval = value; }
		}

		public void Start()
		{
			_timer.Start();
		}

		public void Stop()
		{
			_timer.Stop();
		}
	}
}
