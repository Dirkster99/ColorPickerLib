namespace ColorPickerLib
{
    using System;

    public class ByteUpDown : CommonNumericUpDown<byte>
    {
        #region Constructors

        static ByteUpDown()
        {
            UpdateMetadata(typeof(ByteUpDown), (byte)1, byte.MinValue, byte.MaxValue);
        }

        public ByteUpDown()
            : base(Byte.Parse, Decimal.ToByte, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override byte IncrementValue(byte value, byte increment)
        {
            return (byte)(value + increment);
        }

        protected override byte DecrementValue(byte value, byte increment)
        {
            return (byte)(value - increment);
        }

        #endregion //Base Class Overrides
    }
}
