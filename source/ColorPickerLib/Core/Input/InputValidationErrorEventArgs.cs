﻿namespace ColorPickerLib.Core.Input
{
    using System;

    public delegate void InputValidationErrorEventHandler(object sender, InputValidationErrorEventArgs e);

    public class InputValidationErrorEventArgs : EventArgs
    {
        #region Constructors

        public InputValidationErrorEventArgs(Exception e)
        {
            Exception = e;
        }

        #endregion

        #region Exception Property

        public Exception Exception
        {
            get
            {
                return exception;
            }
            private set
            {
                exception = value;
            }
        }

        private Exception exception;

        #endregion

        #region ThrowException Property

        public bool ThrowException
        {
            get
            {
                return _throwException;
            }
            set
            {
                _throwException = value;
            }
        }

        private bool _throwException;

        #endregion
    }
}
