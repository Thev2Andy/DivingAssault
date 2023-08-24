using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PowerEX;
using UnityEngine;

public static class Evaluator
{
    public static string Evaluate(string Expression, IContext Context){
        return Evaluator.EvaluateBooleanExpressions(Evaluator.EvaluateMathematicalExpressions(Expression, Context).ToString());
    }


    public static string EvaluateMathematicalExpressions(string Expression, IContext Context) {
        return Regex.Replace(Expression, @"\^(.+?)\^", (Match Match) => {
            return Parser.Parse(Match.Groups[1].Value, Context).ToString();
        });
    }

    public static string EvaluateBooleanExpressions(string Expression) {
        return Regex.Replace(Expression, @"\$(.*?)\$", (Match Match) => {
            return Converter.AsIntegerStringFromBoolean(BooleanEvaluator.Evaluate(Match.Groups[1].Value));
        });
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
