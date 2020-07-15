namespace ColorPickerLib.Models
{
	using System.Windows.Media;

	/// <summary>
	/// Model a color with a color value and name.
	/// </summary>
	public class ColorItem
	{
		/// <summary>
		/// Class constructor
		/// </summary>
		/// <param name="color"></param>
		/// <param name="name"></param>
		public ColorItem(Color? color, string name)
		{
			Color = color;
			Name = name;
		}

		/// <summary>
		/// Gets/sets the color value of this item.
		/// </summary>
		public Color? Color
		{
			get;
			set;
		}

		/// <summary>
		/// Gets/sets the name of this item.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Standard object method to compute equality between 2 color items.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			var ci = obj as ColorItem;
			if (ci == null)
				return false;

			return (ci.Color.Equals(Color) && ci.Name.Equals(Name));
		}

		/// <summary>
		/// Serves as a hash function.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.Color.GetHashCode() ^ this.Name.GetHashCode();
		}
	}
}