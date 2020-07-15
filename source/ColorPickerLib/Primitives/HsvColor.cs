namespace ColorPickerLib.Primitives
{
	using System;
	using System.Windows.Media;

	/// <summary>
	/// Class models a color using the HSV (Hue, Saturation, Value) color space
	/// instead of RGB values generally used for the RGB (Red, Green, Blue) color space.
	/// </summary>
	internal class HsvColor
	{
		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="hue"></param>
		/// <param name="saturation"></param>
		/// <param name="value"></param>
		public HsvColor(double hue,
						double saturation,
						double value)
		{
			if (hue < 0)
				throw new ArgumentOutOfRangeException(string.Format("Hue: {0}", hue));

			if (hue > 360)
			{
				hue = ((int)hue) % 360;

				if (hue > 359)
					hue = 359;
			}

			if (saturation < 0 || saturation > 1.0)
				throw new ArgumentOutOfRangeException(string.Format("Saturation: {0}", saturation));

			if (value < 0 || value > 1.0)
				throw new ArgumentOutOfRangeException(string.Format("Value: {0}", value));

			this.Hue = hue;
			this.Saturation = saturation;
			this.Value = value;
		}

		/// <summary>
		/// Gets/sets the Hue (H) component of the HSV color space object.
		/// </summary>
		public double Hue { get; protected set; }

		/// <summary>
		/// Gets/sets the Saturation (S) component of the HSV color space object.
		/// </summary>
		public double Saturation { get; protected set; }

		/// <summary>
		/// Gets/sets the Value (V) component of the HSV color space object.
		/// </summary>
		public double Value { get; protected set; }

		/// <summary>
		/// Converts the given RGB color values into a HSV color.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static HsvColor RGBToHSV(Color? color)
		{
			if (color is null)
				return new HsvColor(0, 0, 0);

			System.Drawing.Color convColor = System.Drawing.Color.FromArgb(color.Value.A,
																		   color.Value.R,
																		   color.Value.G,
																		   color.Value.B);

			int max = Math.Max(convColor.R, Math.Max(convColor.G, convColor.B));
			int min = Math.Min(convColor.R, Math.Min(convColor.G, convColor.B));

			double hue = convColor.GetHue();
			if (hue > 359)
				hue = 359;

			double saturation = (max == 0) ? 0 : 1d - (1d * min / max);
			double value = max / 255d;

			return new HsvColor(hue, saturation, value);
		}

		/// <summary>
		/// Converts the given HSV color values into a RGB color.
		/// </summary>
		/// <param name="hsvColor"></param>
		/// <returns></returns>
		public static Color RGBFromHSV(HsvColor hsvColor)
		{
			int hi = Convert.ToInt32(Math.Floor(hsvColor.Hue / 60)) % 6;
			double f = hsvColor.Hue / 60 - Math.Floor(hsvColor.Hue / 60);

			double value = hsvColor.Value * 255;
			byte v = Convert.ToByte(value);
			byte p = Convert.ToByte(value * (1 - hsvColor.Saturation));
			byte q = Convert.ToByte(value * (1 - f * hsvColor.Saturation));
			byte t = Convert.ToByte(value * (1 - (1 - f) * hsvColor.Saturation));

			if (hi == 0)
				return Color.FromArgb(255, v, t, p);
			else if (hi == 1)
				return Color.FromArgb(255, q, v, p);
			else if (hi == 2)
				return Color.FromArgb(255, p, v, t);
			else if (hi == 3)
				return Color.FromArgb(255, p, q, v);
			else if (hi == 4)
				return Color.FromArgb(255, t, p, v);
			else
				return Color.FromArgb(255, v, p, q);
		}
	}
}