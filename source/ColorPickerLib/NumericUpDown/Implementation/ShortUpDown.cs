namespace ColorPickerLib
{
    using System;

    public class ShortUpDown : CommonNumericUpDown<short>
    {
        #region Constructors

        static ShortUpDown()
        {
            UpdateMetadata(typeof(ShortUpDown), (short)1, short.MinValue, short.MaxValue);
        }

        public ShortUpDown()
            : base(Int16.Parse, Decimal.ToInt16, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override short IncrementValue(short value, short increment)
        {
            return (short)(value + increment);
        }

        protected override short DecrementValue(short value, short increment)
        {
            return (short)(value - increment);
        }

        #endregion //Base Class Overrides
    }
}
