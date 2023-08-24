﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DifficultyPresets
{
    private static Dictionary<Difficulty, string> DifficultyIdentifiers = new Dictionary<Difficulty, string>
    {
        { Difficulty.Easy, "Easy" },
        { Difficulty.Normal, "Normal" },
        { Difficulty.Hard, "Hard" },
    };


    public static string RetrieveIdentifier(Difficulty Difficulty) {
        return ((DifficultyIdentifiers.ContainsKey(Difficulty) ? DifficultyIdentifiers[Difficulty] : "Unknown"));
    }



    public enum Difficulty
    {
        Easy = 0,
        Normal = 1,
        Hard = 2,
    }
}
