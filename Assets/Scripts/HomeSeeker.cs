using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// REPRESENT A person searching a home (can be 1 or 2 person visually)
public class HomeSeeker
{
    public List<Preference> preferences;
    // Start is called before the first frame update
    public HomeSeeker()
    {
        preferences = new List<Preference>();
        DrawPreferences();

        //TODO : REMOVE DEBUG
        Debug.Log("I am Bob here is my preference : ");
        foreach (var pref in preferences)
        {
            Debug.Log(pref.GetPreferenceText("I"));
        }
    }

    
    void DrawPreferences()
    {
        preferences.Clear();
        List<PreferenceType> toAvoid = new List<PreferenceType>();
        int overallcursor = 0;
        int nbToDraw = UnityEngine.Random.Range(2, 5);
        for (int i = 0; i < nbToDraw; i++)
        {
            Preference pref = new Preference();
            if (pref.nbPrefType <= toAvoid.Count)
                break;

            pref.Randomize(toAvoid);
            toAvoid.Add(pref.Type);
            overallcursor += pref.Cursor; // may help to draw a less random not to have 3 "I LIKE" or 3 I dont like
            preferences.Add(pref);
        }
        
    }

    public int CalculateOverallScore(Room room)
    {
        int score = 0;

        //Sequence mySequence = DOTween.Sequence();
        foreach (Item item in room.GetAttachedItems())
        {
            foreach(var p in preferences)
            {
                int localScore = p.GetScore(item.preference);

                // POP TEXTS TO SEE WHAT IS GOOD OR BAD
                if (localScore != 0)
                {
                    Tweener t = PoppingTextManager.Instance.PopText(
                            item.preference.ToString(),
                            localScore > 0 ? Utils.goodGreen:Utils.badRed,
                            item.item.position, .7f);
                    //mySequence.Append(t);
                    score += localScore;
                }
            }
        }
        Debug.Log("Score = " + score);
        return score;
    }
}
