﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Coin
{
    public readonly Money Value;

    public readonly CoinGrade Grade;

    private static Dictionary<CoinGrade, Money> GradeValues = new Dictionary<CoinGrade, Money>();

    static Coin()
    {
        GradeValues[CoinGrade.One]  = new Money(Currency.Rub, 100);    
        GradeValues[CoinGrade.Two]  = new Money(Currency.Rub, 200);    
        GradeValues[CoinGrade.Five] = new Money(Currency.Rub, 500);    
        GradeValues[CoinGrade.Ten]  = new Money(Currency.Rub, 1000);    
    }

    private Coin(Money money, CoinGrade grade)
    {
        Value = money;
        Grade = grade;
    }

    public static Coin Create(CoinGrade grade)
    {

        Money value;     
        if(GradeValues.TryGetValue(grade, out value))
            return new Coin(value, grade);
        throw new CantFindGradeValueException(grade);
	}

    public override bool Equals(object obj)
    {
        var coin = obj as Coin;
        if (coin == null) return false;

        return coin.Grade.Equals(this.Grade) && coin.Value.Equals(this.Value);
    }

    public static Coin One() { return Coin.Create(CoinGrade.One); }
    public static Coin Two() { return Coin.Create(CoinGrade.Two); }
    public static Coin Five() { return Coin.Create(CoinGrade.Five); }
    public static Coin Ten() { return Coin.Create(CoinGrade.Ten); }
}