namespace ColorPickerDemo.ViewModels
{
	using System.Windows.Media;

	internal class AppViewModel : Base.ViewModelBase
	{
		#region fields

		private Color _SelectedAccentColor;

		#endregion fields

		#region ctors

		/// <summary>
		/// Class constructor
		/// </summary>
		public AppViewModel()
		{
			// Explicit use of opacity is recommended here to initialize all components as expected
			_SelectedAccentColor = Color.FromArgb(128, 180, 0, 0);
		}

		#endregion ctors

		#region properties

		/// <summary>
		/// Gets/sets the currently selected color.
		/// </summary>
		public Color SelectedAccentColor
		{
			get { return _SelectedAccentColor; }
			set
			{
				if (_SelectedAccentColor != value)
				{
					_SelectedAccentColor = value;
					RaisePropertyChanged(() => SelectedAccentColor);
				}
			}
		}

		/// <summary>
		/// Gets/sets a description for the currently selected color.
		/// </summary>
		public string Description
		{
			get
			{
				return "The ColorCanvas and ColorPicker controls allow the user to input a color. Not shown in the sample are the ColorPicker's Available, Standard and Recent color lists, which are entirely customizable.";
			}
		}

		#endregion properties
	}
}