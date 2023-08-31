using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PowerEX;
using UnityEngine;

public static class Evaluator
{
    public static string Evaluate(string Expression, IContext Context){
        return Parser.Parse(Expression, Context).ToString();
    }



    public static class Converter
    {
        public static bool AsBooleanFromIntegerString(string Expression) {
            return Convert.ToBoolean(Convert.ToInt32(Expression));
        }

        public static string AsIntegerStringFromBoolean(bool Boolean) {
            return ((Boolean) ? "1" : "0");
        }
    }
}
