﻿using System;

namespace VendingMachine.Domain
{
    public class InvalidProductNumberException : Exception
    {
        #region Constructors and Destructors

        public InvalidProductNumberException(int number)
            : base("Выбран неверный номер продукта: " + number)
        {
        }

        #endregion
    }
}