using System.Windows.Media;

namespace ColorPickerDemo.ViewModels
{
    public class AppViewModel : Base.ViewModelBase
    {
        private Color _SelectedAccentColor;

        public AppViewModel()
        {
            _SelectedAccentColor = Color.FromRgb(180,0,0);
        }

        public Color SelectedAccentColor
        {
            get { return this._SelectedAccentColor; }
            set
            {
                if (this._SelectedAccentColor != value)
                {
                    this._SelectedAccentColor = value;
                    RaisePropertyChanged(() => SelectedAccentColor);
                }
            }
        }

        public string Description
        {
            get
            {
                return "The ColorCanvas and ColorPicker controls allow the user to input a color. Not shown in the sample are the ColorPicker's Available, Standard and Recent color lists, which are entirely customizable.";
            }
        }
    }
}
