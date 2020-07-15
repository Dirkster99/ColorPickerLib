namespace ColorPickerLib.Behaviours
{
	using System.Text.RegularExpressions;
	using System.Windows;
	using System.Windows.Controls;

	/// <summary>
	/// Class implements a textbox behaviour that can help filter textbox characters
	/// for valid characters only. Filtering for hex input is, for example, realized
	/// by attaching this behaviour to a textbox and setting the RegularExpression
	/// property to: "^[0-9|a-f|A-F]+$"
	/// </summary>
	public class AllowableCharactersTextBoxBehavior
	{
		#region fields

		// Using a DependencyProperty as the backing store for RegularExpressionProperty.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty RegularExpressionPropertyProperty =
			DependencyProperty.RegisterAttached("RegularExpressionProperty",
						   typeof(string), typeof(AllowableCharactersTextBoxBehavior),
						   new PropertyMetadata(".*", OnRegularExpressionPropertyChanged));

		#endregion fields

		#region methods

		/// <summary>
		/// Implements the getter method of the regular expression dependency property.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string GetRegularExpressionProperty(DependencyObject obj)
		{
			return (string)obj.GetValue(RegularExpressionPropertyProperty);
		}

		/// <summary>
		/// Implements the setter method of the regular expression dependency property.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="value"></param>
		public static void SetRegularExpressionProperty(DependencyObject obj, string value)
		{
			obj.SetValue(RegularExpressionPropertyProperty, value);
		}

		/// <summary>
		/// Attach detach behaviour when regular expression property has changed.
		/// </summary>
		/// <param name="d"></param>
		/// <param name="e"></param>
		private static void OnRegularExpressionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var uiElement = d as TextBox;                   // Remove the handler if it exist to avoid memory leaks
			uiElement.PreviewTextInput -= uiElement_PreviewTextInput;

			var value = e.NewValue as string;
			if (value != null)
			{
				// the property is attached so we attach the Drop event handler
				uiElement.PreviewTextInput += uiElement_PreviewTextInput;

				DataObject.AddPastingHandler(uiElement, OnPaste);
			}
		}

		/// <summary>
		/// Method checks if pasted string contains input that is invalid and cancels
		/// past command if string is not conforming to regular expression.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void OnPaste(object sender, DataObjectPastingEventArgs e)
		{
			if (e.DataObject.GetDataPresent(DataFormats.Text))
			{
				var RegularExpression = AllowableCharactersTextBoxBehavior.GetRegularExpressionProperty(sender as TextBox);

				string text = System.Convert.ToString(e.DataObject.GetData(DataFormats.Text));

				if (!IsValid(text, true, RegularExpression))
				{
					e.CancelCommand();
				}
			}
			else
			{
				e.CancelCommand();
			}
		}

		/// <summary>
		/// Determines whether string conforms to regurlar expression or not.
		/// </summary>
		/// <param name="newText"></param>
		/// <param name="paste"></param>
		/// <param name="regularExpression"></param>
		/// <returns></returns>
		private static bool IsValid(string newText, bool paste, string regularExpression)
		{
			return Regex.IsMatch(newText, regularExpression);
		}

		/// <summary>
		/// Previews a text change and cancels the change if text appears to be invalid.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void uiElement_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
			var RegularExpression = AllowableCharactersTextBoxBehavior.GetRegularExpressionProperty(sender as TextBox);

			e.Handled = !IsValid(e.Text, false, RegularExpression);
		}

		#endregion methods
	}
}