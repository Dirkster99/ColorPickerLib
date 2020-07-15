﻿namespace ColorPickerLib.Controls
{
	using ColorPickerLib.Core.Utilities;
	using ColorPickerLib.Primitives;
	using System;
	using System.Collections.Generic;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using System.Windows.Shapes;

	[TemplatePart(Name = PART_SpectrumDisplay, Type = typeof(Rectangle))]
	public class ColorSpectrumSlider : Slider
	{
		private const string PART_SpectrumDisplay = "PART_SpectrumDisplay";

		#region Private Members

		private Rectangle _Part_SpectrumDisplay;
		private LinearGradientBrush _pickerBrush;

		#endregion Private Members

		#region Constructors

		/// <summary>
		/// Class constructor
		/// </summary>
		static ColorSpectrumSlider()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorSpectrumSlider), new FrameworkPropertyMetadata(typeof(ColorSpectrumSlider)));
		}

		#endregion Constructors

		#region Dependency Properties

		public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorSpectrumSlider), new PropertyMetadata(System.Windows.Media.Colors.Transparent));

		public Color SelectedColor
		{
			get
			{
				return (Color)GetValue(SelectedColorProperty);
			}
			set
			{
				SetValue(SelectedColorProperty, value);
			}
		}

		#endregion Dependency Properties

		#region Base Class Overrides

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_Part_SpectrumDisplay = (Rectangle)GetTemplateChild(PART_SpectrumDisplay);

			if (_Part_SpectrumDisplay == null)
				return;

			CreateSpectrum();
			OnValueChanged(Double.NaN, Value);
		}

		protected override void OnValueChanged(double oldValue, double newValue)
		{
			base.OnValueChanged(oldValue, newValue);

			Color color = HsvColor.RGBFromHSV(new HsvColor(359 - newValue, 1, 1));
			SelectedColor = color;
		}

		#endregion Base Class Overrides

		#region Methods

		private void CreateSpectrum()
		{
			_pickerBrush = new LinearGradientBrush();
			_pickerBrush.StartPoint = new Point(0.5, 0);
			_pickerBrush.EndPoint = new Point(0.5, 1);
			_pickerBrush.ColorInterpolationMode = ColorInterpolationMode.SRgbLinearInterpolation;

			List<Color> colorsList = ColorUtilities.GenerateHsvSpectrum();

			double stopIncrement = (double)1 / colorsList.Count;

			int i;
			for (i = 0; i < colorsList.Count; i++)
			{
				_pickerBrush.GradientStops.Add(new GradientStop(colorsList[i], i * stopIncrement));
			}

			_pickerBrush.GradientStops[i - 1].Offset = 1.0;
			_Part_SpectrumDisplay.Fill = _pickerBrush;
		}

		#endregion Methods
	}
}