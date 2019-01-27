using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreSaving
{
    public static int           nbHoused = 0;
    public static float         rating = 0.0f;
    public static int    maxNbHoused = 0;
    public static float  maxRating = 0.0f;


    public static void SetScore(int nbHoused, float rating)
    {
        ScoreSaving.nbHoused = nbHoused;
        ScoreSaving.rating = rating;
        Save();
    }

    static void Save()
    {
        PlayerPrefs.SetInt("maxNbHoused",nbHoused);
        PlayerPrefs.SetFloat("maxRating",rating);
        
        PlayerPrefs.Save();
    }

    public static void Load()
    {

        maxNbHoused = PlayerPrefs.GetInt("maxNbHoused", nbHoused);
        maxRating = PlayerPrefs.GetFloat("maxRating", rating);
    }
}
