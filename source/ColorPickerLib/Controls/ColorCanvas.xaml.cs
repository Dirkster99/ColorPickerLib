namespace ColorPickerLib.Controls
{
    using ColorPickerLib.Core.Utilities;
    using ColorPickerLib.Primitives;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public enum ColorSpace
    {
        RGB = 0,
        HSV = 1
    }

    [TemplatePart(Name = PART_ColorShadingCanvas, Type = typeof(Canvas))]
    [TemplatePart(Name = PART_ColorShadeSelector, Type = typeof(Canvas))]
    [TemplatePart(Name = PART_SpectrumSlider, Type = typeof(ColorSpectrumSlider))]
    [TemplatePart(Name = PART_HexadecimalTextBox, Type = typeof(TextBox))]
    public class ColorCanvas : Control
    {
        #region fields
        private const string PART_ColorShadingCanvas = "PART_ColorShadingCanvas";
        private const string PART_ColorShadeSelector = "PART_ColorShadeSelector";
        private const string PART_SpectrumSlider = "PART_SpectrumSlider";
        private const string PART_HexadecimalTextBox = "PART_HexadecimalTextBox";

        private TranslateTransform _colorShadeSelectorTransform = new TranslateTransform();
        private Canvas _colorShadingCanvas;
        private Canvas _colorShadeSelector;
        private ColorSpectrumSlider _spectrumSlider;
        private TextBox _hexadecimalTextBox;
        private Point? _currentColorPosition;
        private bool _surpressPropertyChanged;
        #endregion fields

        #region Constructors
        /// <summary>
        /// Static class constructor
        /// </summary>
        static ColorCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorCanvas), new FrameworkPropertyMetadata(typeof(ColorCanvas)));
        }

        #endregion Constructors

        #region Properties
        #region SelectedColor

        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor",
                                             typeof(Color?),
                                             typeof(ColorCanvas),
                                             new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedColorChanged));
        public Color? SelectedColor
        {
            get
            {
                return (Color?)GetValue(SelectedColorProperty);
            }
            set
            {
                SetValue(SelectedColorProperty, value);
            }
        }

        private static void OnSelectedColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnSelectedColorChanged((Color?)e.OldValue, (Color?)e.NewValue);
        }

        protected virtual void OnSelectedColorChanged(Color? oldValue, Color? newValue)
        {
            SetHexadecimalStringProperty(ColorUtilities.GetFormatedColorString(newValue, UsingAlphaChannel), false);
            UpdateRGBValues(newValue);
            UpdateColorShadeSelectorPosition(newValue);

            RoutedPropertyChangedEventArgs<Color?> args = new RoutedPropertyChangedEventArgs<Color?>(oldValue, newValue);
            args.RoutedEvent = SelectedColorChangedEvent;
            RaiseEvent(args);
        }

        #endregion //SelectedColor

        #region RGB
        #region A
        /// <summary>
        /// Dependency property for channel A (Opacity)
        /// </summary>
        public static readonly DependencyProperty AProperty =
            DependencyProperty.Register("A",
                typeof(byte), typeof(ColorCanvas), new UIPropertyMetadata((byte)255, OnAChanged));

        public byte A
        {
            get
            {
                return (byte)GetValue(AProperty);
            }
            set
            {
                SetValue(AProperty, value);
            }
        }

        private static void OnAChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnAChanged((byte)e.OldValue, (byte)e.NewValue);
        }

        protected virtual void OnAChanged(byte oldValue, byte newValue)
        {
            if (!_surpressPropertyChanged)
                UpdateSelectedColor();
        }
        #endregion A

        #region R
        /// <summary>
        /// Dependency property for channel Red (R) of RGB.
        /// </summary>
        public static readonly DependencyProperty RProperty =
            DependencyProperty.Register("R", typeof(byte),
                typeof(ColorCanvas), new UIPropertyMetadata((byte)0, OnRChanged));

        public byte R
        {
            get
            {
                return (byte)GetValue(RProperty);
            }
            set
            {
                SetValue(RProperty, value);
            }
        }

        private static void OnRChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnRChanged((byte)e.OldValue, (byte)e.NewValue);
        }

        protected virtual void OnRChanged(byte oldValue, byte newValue)
        {
            if (!_surpressPropertyChanged)
                UpdateSelectedColor();
        }
        #endregion R

        #region G
        /// <summary>
        /// Dependency property for channel Green (G) of RGB.
        /// </summary>
        public static readonly DependencyProperty GProperty = DependencyProperty.Register("G", typeof(byte), typeof(ColorCanvas), new UIPropertyMetadata((byte)0, OnGChanged));

        public byte G
        {
            get
            {
                return (byte)GetValue(GProperty);
            }
            set
            {
                SetValue(GProperty, value);
            }
        }

        private static void OnGChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnGChanged((byte)e.OldValue, (byte)e.NewValue);
        }

        protected virtual void OnGChanged(byte oldValue, byte newValue)
        {
            if (!_surpressPropertyChanged)
                UpdateSelectedColor();
        }
        #endregion G

        #region B
        /// <summary>
        /// Dependency property for channel Blue (B) of RGB.
        /// </summary>
        public static readonly DependencyProperty BProperty = DependencyProperty.Register("B", typeof(byte), typeof(ColorCanvas), new UIPropertyMetadata((byte)0, OnBChanged));

        public byte B
        {
            get
            {
                return (byte)GetValue(BProperty);
            }
            set
            {
                SetValue(BProperty, value);
            }
        }

        private static void OnBChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnBChanged((byte)e.OldValue, (byte)e.NewValue);
        }

        protected virtual void OnBChanged(byte oldValue, byte newValue)
        {
            if (!_surpressPropertyChanged)
                UpdateSelectedColor();
        }
        #endregion B

        #endregion RGB

        #region HSV
        // HSV (hue, saturation, value)
        // https://en.wikipedia.org/wiki/HSL_and_HSV
        //
        #region H
        public double H
        {
            get { return (double)GetValue(HProperty); }
            set { SetValue(HProperty, value); }
        }

        // Using a DependencyProperty as the backing store for H.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HProperty =
            DependencyProperty.Register("H", typeof(double),
                typeof(ColorCanvas), new PropertyMetadata((double)0, OnHChanged));

        private static void OnHChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = d as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnHChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual void OnHChanged(double oldValue, double newValue)
        {
            if (!_surpressPropertyChanged)
                UpdateSelectedColorFromHSV(H, S, V);
        }
        #endregion H

        #region S
        public double S
        {
            get { return (double)GetValue(SProperty); }
            set { SetValue(SProperty, value); }
        }

        // Using a DependencyProperty as the backing store for S.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SProperty =
            DependencyProperty.Register("S", typeof(double), typeof(ColorCanvas),
                new PropertyMetadata((double)0, OnSChanged));

        private static void OnSChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = d as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnSChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual void OnSChanged(double oldValue, double newValue)
        {
            if (!_surpressPropertyChanged)
                UpdateSelectedColorFromHSV(H, S, V);
        }
        #endregion S

        #region V
        public double V
        {
            get { return (double)GetValue(VProperty); }
            set { SetValue(VProperty, value); }
        }

        // Using a DependencyProperty as the backing store for V.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VProperty =
            DependencyProperty.Register("V", typeof(double),
                typeof(ColorCanvas), new PropertyMetadata((double)0, OnVChanged));

        private static void OnVChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = d as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnVChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual void OnVChanged(double oldValue, double newValue)
        {
            if (!_surpressPropertyChanged)
                UpdateSelectedColorFromHSV(H, S, V);
        }
        #endregion V
        #endregion HSV

        #region ColorSpace Display
        /// <summary>
        /// Defines the color space (RGB, HSV ...) in which values are displayed.
        /// </summary>
        public ColorSpace DisplayColorSpace
        {
            get { return (ColorSpace)GetValue(DisplayColorSpaceProperty); }
            set { SetValue(DisplayColorSpaceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayColorSpace.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayColorSpaceProperty =
            DependencyProperty.Register("DisplayColorSpace", typeof(ColorSpace),
                typeof(ColorCanvas), new PropertyMetadata(ColorSpace.RGB));
        #endregion ColorSpace Display

        #region HexadecimalString

        public static readonly DependencyProperty HexadecimalStringProperty =
            DependencyProperty.Register("HexadecimalString",
            typeof(string), typeof(ColorCanvas), new UIPropertyMetadata("", OnHexadecimalStringChanged, OnCoerceHexadecimalString));

        public string HexadecimalString
        {
            get
            {
                return (string)GetValue(HexadecimalStringProperty);
            }
            set
            {
                SetValue(HexadecimalStringProperty, value);
            }
        }

        private static void OnHexadecimalStringChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnHexadecimalStringChanged((string)e.OldValue, (string)e.NewValue);
        }

        protected virtual void OnHexadecimalStringChanged(string oldValue, string newValue)
        {
            string newColorString = GetFormatedColorString(newValue);
            string currentColorString = ColorUtilities.GetFormatedColorString(SelectedColor, UsingAlphaChannel);
            if (!currentColorString.Equals(newColorString))
            {
                Color? col = null;
                if (!string.IsNullOrEmpty(newColorString))
                {
                    col = (Color)ConvertFromString(newColorString);
                }
                UpdateSelectedColor(col);
            }

            SetHexadecimalTextBoxTextProperty(newValue);
        }

        private static object OnCoerceHexadecimalString(DependencyObject d, object basevalue)
        {
            var colorCanvas = (ColorCanvas)d;
            if (colorCanvas == null)
                return basevalue;

            return colorCanvas.OnCoerceHexadecimalString(basevalue);
        }

        private object OnCoerceHexadecimalString(object newValue)
        {
            var value = newValue as string;
            string retValue = value;

            try
            {
                if (!string.IsNullOrEmpty(retValue))
                {
                    ConvertFromString(value);
                }
            }
            catch
            {
                //When HexadecimalString is changed via Code-Behind and hexadecimal format is bad, throw.
                throw new InvalidDataException("Color provided is not in the correct format.");
            }

            return retValue;
        }

        #endregion //HexadecimalString

        #region UsingAlphaChannel

        public static readonly DependencyProperty UsingAlphaChannelProperty = DependencyProperty.Register("UsingAlphaChannel", typeof(bool), typeof(ColorCanvas), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnUsingAlphaChannelPropertyChanged)));
        public bool UsingAlphaChannel
        {
            get
            {
                return (bool)GetValue(UsingAlphaChannelProperty);
            }
            set
            {
                SetValue(UsingAlphaChannelProperty, value);
            }
        }

        private static void OnUsingAlphaChannelPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnUsingAlphaChannelChanged();
        }

        protected virtual void OnUsingAlphaChannelChanged()
        {
            SetHexadecimalStringProperty(ColorUtilities.GetFormatedColorString(SelectedColor, UsingAlphaChannel), false);
        }

        #endregion //UsingAlphaChannel
        #endregion Properties

        #region methods
        #region Base Class Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_colorShadingCanvas != null)
            {
                _colorShadingCanvas.MouseLeftButtonDown -= ColorShadingCanvas_MouseLeftButtonDown;
                _colorShadingCanvas.MouseLeftButtonUp -= ColorShadingCanvas_MouseLeftButtonUp;
                _colorShadingCanvas.MouseMove -= ColorShadingCanvas_MouseMove;
                _colorShadingCanvas.SizeChanged -= ColorShadingCanvas_SizeChanged;
                _colorShadingCanvas.Loaded -= _colorShadingCanvas_Loaded;
            }

            _colorShadingCanvas = GetTemplateChild(PART_ColorShadingCanvas) as Canvas;

            if (_colorShadingCanvas != null)
            {
                _colorShadingCanvas.MouseLeftButtonDown += ColorShadingCanvas_MouseLeftButtonDown;
                _colorShadingCanvas.MouseLeftButtonUp += ColorShadingCanvas_MouseLeftButtonUp;
                _colorShadingCanvas.MouseMove += ColorShadingCanvas_MouseMove;
                _colorShadingCanvas.SizeChanged += ColorShadingCanvas_SizeChanged;
                _colorShadingCanvas.Loaded += _colorShadingCanvas_Loaded;
            }

            _colorShadeSelector = GetTemplateChild(PART_ColorShadeSelector) as Canvas;

            if (_colorShadeSelector != null)
                _colorShadeSelector.RenderTransform = _colorShadeSelectorTransform;

            if (_spectrumSlider != null)
                _spectrumSlider.ValueChanged -= SpectrumSlider_ValueChanged;

            _spectrumSlider = GetTemplateChild(PART_SpectrumSlider) as ColorSpectrumSlider;

            if (_spectrumSlider != null)
                _spectrumSlider.ValueChanged += SpectrumSlider_ValueChanged;

            if (_hexadecimalTextBox != null)
            {
                _hexadecimalTextBox.LostFocus -= new RoutedEventHandler(HexadecimalTextBox_LostFocus);
            }

            _hexadecimalTextBox = GetTemplateChild(PART_HexadecimalTextBox) as TextBox;

            if (_hexadecimalTextBox != null)
            {
                _hexadecimalTextBox.LostFocus += new RoutedEventHandler(HexadecimalTextBox_LostFocus);
            }

            UpdateRGBValues(SelectedColor);
            UpdateColorShadeSelectorPosition(SelectedColor);

            // When changing theme, HexadecimalString needs to be set since it is not binded.
            SetHexadecimalTextBoxTextProperty(ColorUtilities.GetFormatedColorString(SelectedColor, UsingAlphaChannel));
        }

        private void _colorShadingCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            // Components of each color are not always updated when using complicated scenarious
            // like PopUp controls and partially visible elements
            // So, this hack makes sure colors are always correct when UI is loaded after construction
            if (SelectedColor != null)
            {
                var backUPColor = SelectedColor;

                SelectedColor = Color.FromArgb(00,00,00,00);
                SelectedColor = Color.FromArgb(0xff, 0xff, 0xff, 0xff);

                SelectedColor = backUPColor;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            //hitting enter on textbox will update Hexadecimal string
            if (e.Key == Key.Enter && e.OriginalSource is TextBox)
            {
                TextBox textBox = (TextBox)e.OriginalSource;
                if (textBox.Name == PART_HexadecimalTextBox)
                    SetHexadecimalStringProperty(textBox.Text, true);
            }
        }

        #endregion //Base Class Overrides

        #region Event Handlers

        void ColorShadingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(_colorShadingCanvas);
            UpdateColorShadeSelectorPositionAndCalculateColor(p, true);
            _colorShadingCanvas.CaptureMouse();
            //Prevent from closing ColorCanvas after mouseDown in ListView
            e.Handled = true;
        }

        void ColorShadingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _colorShadingCanvas.ReleaseMouseCapture();
        }

        void ColorShadingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(_colorShadingCanvas);
                UpdateColorShadeSelectorPositionAndCalculateColor(p, true);
                Mouse.Synchronize();
            }
        }

        void ColorShadingCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_currentColorPosition != null)
            {
                Point _newPoint = new Point
                {
                    X = ((Point)_currentColorPosition).X * e.NewSize.Width,
                    Y = ((Point)_currentColorPosition).Y * e.NewSize.Height
                };

                UpdateColorShadeSelectorPositionAndCalculateColor(_newPoint, false);
            }
        }

        void SpectrumSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if ((_currentColorPosition != null) && (this.SelectedColor != null))
            {
                CalculateColor((Point)_currentColorPosition);
            }
        }

        void HexadecimalTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            SetHexadecimalStringProperty(textbox.Text, true);
        }
        #endregion //Event Handlers

        #region Events

        public static readonly RoutedEvent SelectedColorChangedEvent = EventManager.RegisterRoutedEvent("SelectedColorChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color?>), typeof(ColorCanvas));
        public event RoutedPropertyChangedEventHandler<Color?> SelectedColorChanged
        {
            add
            {
                AddHandler(SelectedColorChangedEvent, value);
            }
            remove
            {
                RemoveHandler(SelectedColorChangedEvent, value);
            }
        }

        #endregion //Events

        /// <summary>
        /// Updates the currently selected color in the color canvas
        /// from the given HSV parameters.
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="value"></param>
        private void UpdateSelectedColorFromHSV(double hue,
                                                double saturation,
                                                double value)
        {
            var color = HsvColor.RGBFromHSV(new HsvColor(hue, saturation, value));

            UpdateRGBValues(Color.FromArgb(A, color.R, color.G, color.B));
            UpdateSelectedColor();
        }

        /// <summary>
        /// Updates the currently selected color of the color canvas
        /// from the A,R,G,B parameters in the color canvas .
        /// </summary>
        private void UpdateSelectedColor()
        {
            SelectedColor = Color.FromArgb(A, R, G, B);
        }

        private void UpdateSelectedColor(Color? color)
        {
            SelectedColor = ((color != null) && color.HasValue)
                            ? (Color?)Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B)
                            : null;
        }

        /// <summary>
        /// Updates all color channels in all spaces (Opacity, RGB, HSV)
        /// from the given color parameter, or does nothing if the given
        /// color parameter 'Has No Value' or is null.
        /// </summary>
        /// <param name="color"></param>
        private void UpdateRGBValues(Color? color)
        {
            if ((color == null) || !color.HasValue)
                return;

            _surpressPropertyChanged = true;

            A = color.Value.A;
            R = color.Value.R;
            G = color.Value.G;
            B = color.Value.B;

            HsvColor hsv = HsvColor.RGBToHSV(color);

            H = hsv.Hue;
            S = hsv.Saturation;
            V = hsv.Value;

            _surpressPropertyChanged = false;
        }

        private void UpdateColorShadeSelectorPositionAndCalculateColor(Point p, bool calculateColor)
        {
            if (p.Y < 0)
                p.Y = 0;

            if (p.X < 0)
                p.X = 0;

            if (p.X > _colorShadingCanvas.ActualWidth)
                p.X = _colorShadingCanvas.ActualWidth;

            if (p.Y > _colorShadingCanvas.ActualHeight)
                p.Y = _colorShadingCanvas.ActualHeight;

            _colorShadeSelectorTransform.X = p.X - (_colorShadeSelector.Width / 2);
            _colorShadeSelectorTransform.Y = p.Y - (_colorShadeSelector.Height / 2);

            p.X = p.X / _colorShadingCanvas.ActualWidth;
            p.Y = p.Y / _colorShadingCanvas.ActualHeight;

            _currentColorPosition = p;

            if (calculateColor)
                CalculateColor(p);
        }

        private void UpdateColorShadeSelectorPosition(Color? color)
        {
            if ((_spectrumSlider == null) || (_colorShadingCanvas == null) || (color == null) || !color.HasValue)
                return;

            _currentColorPosition = null;

            HsvColor hsv = HsvColor.RGBToHSV(color);
//            HsvColor hsv = ColorUtilities.ConvertRgbToHsv(color.Value.R, color.Value.G, color.Value.B);

            if (!(color.Value.R == color.Value.G && color.Value.R == color.Value.B))
                _spectrumSlider.Value = 359 - hsv.Hue;

            Point p = new Point(hsv.Saturation, 1 - hsv.Value);

            _currentColorPosition = p;

            _colorShadeSelectorTransform.X = (p.X * _colorShadingCanvas.Width) - 5;
            _colorShadeSelectorTransform.Y = (p.Y * _colorShadingCanvas.Height) - 5;
        }

        private void CalculateColor(Point p)
        {
            HsvColor hsv = new HsvColor(359 - _spectrumSlider.Value, p.X, 1 - p.Y);

            //            var currentColor = ColorUtilities.ConvertHsvToRgb(hsv.Hue, hsv.Saturation, hsv.Value);
            var currentColor = HsvColor.RGBFromHSV(hsv);
            currentColor.A = A;
            SelectedColor = currentColor;
            string newColor = ColorUtilities.GetFormatedColorString(SelectedColor, UsingAlphaChannel);

            SetHexadecimalStringProperty(newColor, false);
        }

        private string GetFormatedColorString(string stringToFormat)
        {
            return ColorUtilities.FormatColorString(stringToFormat, UsingAlphaChannel);
        }

        private void SetHexadecimalStringProperty(string newValue, bool modifyFromUI)
        {
            if (modifyFromUI)
            {
                try
                {
                    if (!string.IsNullOrEmpty(newValue))
                    {
                        ConvertFromString(newValue);
                    }
                    HexadecimalString = newValue;
                }
                catch
                {
                    //When HexadecimalString is changed via UI and hexadecimal format is bad, keep the previous HexadecimalString.
                    SetHexadecimalTextBoxTextProperty(HexadecimalString);
                }
            }
            else
            {
                //When HexadecimalString is changed via Code-Behind, hexadecimal format will be evaluated in OnCoerceHexadecimalString()
                HexadecimalString = newValue;
            }
        }

        private void SetHexadecimalTextBoxTextProperty(string newValue)
        {
            if (_hexadecimalTextBox != null)
                _hexadecimalTextBox.Text = newValue;
        }

        /// <summary>
        /// Attempts to convert a string to a System.Windows.Media.Color.
        /// </summary>
        /// <param name="colorValue">
        /// This shoudl contain an hexadecimal code, such as: FF008000
        /// without the '#' character as in normal color coding conventions.
        /// </param>
        /// <returns></returns>
        private object ConvertFromString(string colorValue)
        {
            if (colorValue == null)
                return null;

            // Enter standard values if dark channels are missing
            if (colorValue.Length < 8)
            {
                for (int i = 0; colorValue.Length < 8; i++)
                    colorValue += '0';
            }

            return ColorConverter.ConvertFromString('#' + colorValue);
        }
        #endregion methods
    }
}
