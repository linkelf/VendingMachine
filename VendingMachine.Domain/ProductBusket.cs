﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VendingMachine .Domain
{
    using System;
    using System.Collections.Generic;

    using VendingMachine.Domain.Products;

    public class ProductBusket : IShowcaseItem
    {

        private Stack<IProduct> _product = new Stack<IProduct>(); 
        public void Add(IProduct product)
        {
            this._product.Push(product);
        }

        public IProduct Dispence()
        {
            try
            {

                return this._product.Pop();
            }
            catch (InvalidOperationException)
            {
                throw new BusketIsEmptyException();
            }
        }

        public int Amount { get
        {
            return this._product.Count;
        } }

        public ProductBusket(int number, Money cost)
        {
            this.Number = number;
            this.Cost = cost;
        }

        public int Number { get; private set; }

        public IProduct Product
        {
            get
            {
                return this._product.Count > 0 ? this._product.Peek() : null;
            }
        }

        public Money Cost { get; private set; }


        public static ProductBusket Of<T>(int number, Money cost, int amount) where T : IProduct, new()
        {
            var buskt = new ProductBusket(number, cost);
            for(int n=0; n < amount; n++) buskt.Add(new T());
            return buskt;
        }
    }
}