namespace VendingMachine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// ������ � ������� ������ ����������� ����� ����������� ����������� �����. 
    /// </summary>
    public class Wallet : IWallet
    {
        readonly IDictionary<Money, List<Coin>> _coinsByValue = new SortedDictionary<Money, List<Coin>>();

        public Wallet()
        {
            Total = Money.Zero;
        }

        public Coin[] GetMoney(Money amount)
        {
            IDictionary<Money, int> ret;
            var leftAmount = amount.Clone();
            if(amount > Total)
                throw new NotAnoughMoneyException();

            //�������� ����� ������ �������� ��������, ������� ����
            //��������� �� ��, ��� �������� �������� ������������ ��� ���� ����� ����� ����������� 
            //����������� �����. ���� �� ��� ��� �������� ����� �����.
            //��-�������� ������ ���� ���������� ��������, �� ����c���� �� ���������. 
            var coinsToGiveAway = this._coinsByValue.Keys.Reverse().ToDictionary(
                k => k,
                k => {
                         int coinsNum = Math.Min(leftAmount / k, this._coinsByValue[k].Count);
                         leftAmount   = leftAmount - k *  coinsNum;
                         return coinsNum;
                });


            if (leftAmount != Money.Zero) throw new NotAnoughCoinsException();

            var result = new List<Coin>();
            foreach (var pair in coinsToGiveAway)
            {
                var coins = this._coinsByValue[pair.Key];
                result.AddRange(coins.Take(pair.Value));
                coins.RemoveRange(0, pair.Value);
            }
            CalcTotal();
            return result.ToArray();
        }


        public void Put(params Coin[] coins)
        {
            if(coins == null) throw new ArgumentNullException("coins");
            foreach (var coin in coins)
            {
                List<Coin> list;
                if (!this._coinsByValue.TryGetValue(coin.Value, out list))
                {
                    list = new List<Coin>();
                    this._coinsByValue[coin.Value] = list;
                }
                list.Add(coin);
            }

            CalcTotal();
        }

        private void CalcTotal()
        {
            Total = _coinsByValue.Values.SelectMany(v => v).Aggregate(Money.Zero,(a,coin) => a + coin.Value);
        }

        public Money Total { get; private set; }
    }
}