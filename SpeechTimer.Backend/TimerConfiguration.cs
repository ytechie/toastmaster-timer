using System;

namespace SpeechTimer.Backend
{
	public class TimerConfiguration
	{
		private TimeSpan _greenTime;
		private TimeSpan _redTime;

		public TimerConfiguration(TimeSpan greenTime, TimeSpan redTime)
		{
			GreenTime = greenTime;
			RedTime = redTime;
		}

		public TimeSpan GreenTime
		{
			get { return _greenTime; }
			set
			{
				if (value.TotalSeconds <= 0)
					throw new ArgumentOutOfRangeException("GreenTime", "GreenTime must be >= 0");

				_greenTime = value;
			}
		}

		public TimeSpan RedTime
		{
			get { return _redTime; }
			set
			{
				if (value.TotalSeconds <= 0)
					throw new ArgumentOutOfRangeException("RedTime", "RedTime must be >= 0");

				_redTime = value;
			}
		}

		public TimeSpan YellowTime
		{
			get
			{
				if(RedTime == TimeSpan.Zero || GreenTime == TimeSpan.Zero)
					throw new Exception("Cannot calculate the 'Yellow' time when the 'green' or 'red' time is not yet");

				var redGreenDuration = RedTime - GreenTime;
				return TimeSpan.FromSeconds(GreenTime.TotalSeconds + (redGreenDuration.TotalSeconds/2));
			}
		}

		public TimeSpan DoneTime
		{
			get
			{
				if (RedTime == TimeSpan.Zero)
					throw new Exception("Cannot calculate the 'done' time when the 'red' time is not set");

				return RedTime + TimeSpan.FromSeconds(30);
			}
		}
	}
}
