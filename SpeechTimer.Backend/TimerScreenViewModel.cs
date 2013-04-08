using System;
using System.ComponentModel;

namespace SpeechTimer.Backend
{
	public class TimerScreenViewModel : INotifyPropertyChanged
	{
		private bool _running;
		private DateTime _start;
		private DateTime _end;

		private readonly ITimer _timer;
		private readonly INow _now;

		public event PropertyChangedEventHandler PropertyChanged;

		public TimerScreenViewModel(ITimer timer, INow now)
		{
			_timer = timer;
			_now = now;

			_timer.Tick += (sender, o) => RefreshElapsed();
		}

		public TimeSpan Elapsed
		{
			get
			{
				if (_running)
					return _now.Now.Subtract(_start);

				return _end.Subtract(_start);
			}
		}

		public string ElapsedFormatted
		{
			get { return string.Format("{0:mm\\:ss}", Elapsed); }
		}

		public void Start()
		{
			_start = _now.Now;
			_running = true;

			_timer.Interval = TimeSpan.FromSeconds(1);
			_timer.Start();
		}

		public void Stop()
		{
			_end = _now.Now;
			_running = false;

			_timer.Stop();
			RefreshElapsed();
		}

		public void Reset()
		{
			Stop();

			_start = _now.Now;
			_end = _now.Now;

			RefreshElapsed();
		}

		private void RefreshElapsed()
		{
			var pc = PropertyChanged;
			if (pc != null)
			{
				pc(this, new PropertyChangedEventArgs("Elapsed"));
				pc(this, new PropertyChangedEventArgs("ElapsedFormatted"));
			}
		}
	}
}
