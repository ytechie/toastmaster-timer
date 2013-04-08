using System;

namespace SpeechTimer.Backend
{
	public interface INow
	{
		DateTime Now { get; }
	}

	public class NowDefault : INow
	{
		public DateTime Now { get { return DateTime.Now; }}
	}
}
