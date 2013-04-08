using System;
using SpeechTimer.Backend;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SpeechTimer
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		private TimerScreenViewModel _viewModel;

		public MainPage()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.  The Parameter
		/// property is typically used to configure the page.</param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			_viewModel = new TimerScreenViewModel(new DefaultTimer(), new NowDefault());
			DataContext = _viewModel;
		}

		private void StartButton_OnClick(object sender, RoutedEventArgs e)
		{
			_viewModel.Start();
		}

		private void ResetButton_OnClick(object sender, RoutedEventArgs e)
		{
			_viewModel.Reset();
		}

		private void ActivateLight(Ellipse ellipse, Color color, bool blink = false)
		{
			var storyboard = new Storyboard();
			var animation = new ColorAnimationUsingKeyFrames();
			storyboard.Children.Add(animation);

			animation.KeyFrames.Add(new EasingColorKeyFrame()
				{
					Value = color,
					KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.0))
				});

			if (blink)
			{
				for (var i = 0; i < 3; i++)
				{
					var keyFrame = new EasingColorKeyFrame();
					animation.KeyFrames.Add(keyFrame);

					//Alternate between grey and the supplied color
					keyFrame.Value = i%2 == 0 ? Colors.Gray : color;
					keyFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.0 + (i*0.5)));
				}
			}

			Storyboard.SetTarget(animation, ellipse);
			Storyboard.SetTargetProperty(animation, "(Shape.Fill).(SolidColorBrush.Color)");

			storyboard.Begin();
		}

	}
}