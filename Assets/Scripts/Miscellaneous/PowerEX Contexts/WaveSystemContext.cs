using System;
using System.Collections;
using System.Collections.Generic;
using PowerEX;
using UnityEngine;

public class WaveSystemContext : IContext
{
    public int Wave { get; private set; }


    public decimal Call(string Identifier, Decimal[] Arguments)
    {
        if (Identifier.ToUpper() == "SIN") {
            return (Decimal)Math.Sin((double)Arguments[0]);
        }

        if (Identifier.ToUpper() == "COS") {
            return (Decimal)Math.Cos((double)Arguments[0]);
        }

        if (Identifier.ToUpper() == "TAN") {
            return (Decimal)Math.Tan((double)Arguments[0]);
        }

        if (Identifier.ToUpper() == "TANH") {
            return (Decimal)Math.Tanh((double)Arguments[0]);
        }

        if (Identifier.ToUpper() == "SQRT") {
            return (Decimal)Math.Sqrt((double)Arguments[0]);
        }

        if (Identifier.ToUpper() == "POW") {
            return (Decimal)Math.Pow((double)Arguments[0], (double)Arguments[1]);
        }

        if (Identifier.ToUpper() == "ABS") {
            return (Decimal)Math.Abs((double)Arguments[0]);
        }

        if (Identifier.ToUpper() == "MAX") {
            return (Decimal)Math.Max((double)Arguments[0], (double)Arguments[1]);
        }

        if (Identifier.ToUpper() == "MIN") {
            return (Decimal)Math.Min((double)Arguments[0], (double)Arguments[1]);
        }

        if (Identifier.ToUpper() == "EXP") {
            return (Decimal)Math.Exp((double)Arguments[0]);
        }

        if (Identifier.ToUpper() == "LOG") {
            return (Decimal)Math.Log((double)Arguments[0]);
        }

        if (Identifier.ToUpper() == "FLOOR") {
            return (Decimal)Math.Floor((double)Arguments[0]);
        }

        if (Identifier.ToUpper() == "CEIL") {
            return (Decimal)Math.Ceiling((double)Arguments[0]);
        }

        if (Identifier.ToUpper() == "ROUND") {
            return (Decimal)Math.Round((double)Arguments[0]);
        }

        if (Identifier.ToUpper() == "TRUNCATE") {
            return (Decimal)Math.Truncate((double)Arguments[0]);
        }

        if (Identifier.ToUpper() == "RAND"){
            return (Decimal)UnityEngine.Random.Range((float)Arguments[0], (float)Arguments[1]);
        }

        throw new Exception("Unknown function name `" + Identifier + "`.");
    }

    public decimal Resolve(string Identifier)
    {
        if (Identifier.ToUpper() == "PI") {
            return (Decimal)Math.PI;
        }

        if (Identifier.ToUpper() == "E") {
            return (Decimal)Math.E;
        }

        if (Identifier.ToUpper() == "WAVE") {
            return (Decimal)Wave;
        }

        if (Identifier.ToUpper() == "TIME") {
            return (Decimal)Time.timeSinceLevelLoad;
        }

        throw new Exception("Unknown variable name `" + Identifier + "`.");
    }



    public WaveSystemContext(int Wave) {
        this.Wave = Wave;
    }
}
