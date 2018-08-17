namespace ColorPickerLib
{
    using System;

    internal class UIntegerUpDown : CommonNumericUpDown<uint>
    {
        #region Constructors

        static UIntegerUpDown()
        {
            UpdateMetadata(typeof(UIntegerUpDown), (uint)1, uint.MinValue, uint.MaxValue);
        }

        public UIntegerUpDown()
            : base(uint.Parse, Decimal.ToUInt32, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override uint IncrementValue(uint value, uint increment)
        {
            return (uint)(value + increment);
        }

        protected override uint DecrementValue(uint value, uint increment)
        {
            return (uint)(value - increment);
        }

        #endregion //Base Class Overrides
    }
}
