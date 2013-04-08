using System;

namespace SpeechTimer.Backend
{
	public interface ITimer
	{
		void Start();
		void Stop();
		TimeSpan Interval { get; set; }
		event EventHandler<EventArgs> Tick;
	}
}
