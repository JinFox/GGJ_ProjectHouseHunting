﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Text;
using System;

// REPRESENT A person searching a home (can be 1 or 2 person visually)
public class HomeSeeker
{
    public List<Preference> preferences;
    // Start is called before the first frame update
    public HomeSeeker()
    {
        preferences = new List<Preference>();
        DrawPreferences();

    }

    public string GetPreferencesFormatted ()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var pref in preferences)
        {
            sb.AppendLine(pref.GetPreferenceText("I"));
        }
        return sb.ToString();
    }
    void DrawPreferences()
    {

        preferences.Clear();
        List<PreferenceType> toAvoid = new List<PreferenceType>();
        int overallcursor = 0;
        int nbToDraw = UnityEngine.Random.Range(2, 4);
        for (int i = 0; i < nbToDraw; i++)
        {
            Preference pref = new Preference();
            if (pref.nbPrefType <= toAvoid.Count)
                break;

            pref.Randomize(toAvoid, -overallcursor); // keeps overall neutral characters
            toAvoid.Add(pref.Type);
            overallcursor += pref.Cursor; 
           

            preferences.Add(pref);
        }
        
    }

    public int CalculateOverallScore(Room room, Action callback)
    {
        int score = 0;
        List<Tweener> goodTween = new List<Tweener>();
        List<Tweener> badTween = new List<Tweener>();
        Sequence mySequence = DOTween.Sequence();

        foreach (Item item in room.GetAttachedItems())
        {
            foreach (var p in preferences)
            {
                int localScore = p.GetScore(item.preference);

                // POP TEXTS TO SEE WHAT IS GOOD OR BAD
                if (localScore != 0)
                {

                    Tweener t = PoppingTextManager.Instance.PopText(
                            item.preference.ToString(),
                            localScore > 0 ? Utils.Instance.goodGreen : Utils.Instance.badRed,
                            item.item.position,
                            localScore > 0 ? .5f : -.5f,
                            .7f);

                    if (localScore > 0)
                    {
                        goodTween.Add(t);
                    }
                    else
                    {
                        badTween.Add(t);
                    }


                    score += localScore;
                }
            }
        }

        AddToSequence(goodTween, mySequence, () => AudioManager.Instance.Play("GoodRoom"));
        AddToSequence(badTween, mySequence, () => AudioManager.Instance.Play("BadRoom"));

        if (callback != null)
            mySequence.OnComplete(() => callback());

        Debug.Log("Score = " + score);
        return score;
    }

    private void AddToSequence(List<Tweener> tweenList, Sequence mySequence, Action OnSequenceStart)
    {
        for (int i = 0; i < tweenList.Count; i++)
        {
            if (i == 0)
            {
                mySequence.AppendCallback(() => OnSequenceStart());
                mySequence.Append(tweenList[i]);
            }
            else
                mySequence.Join(tweenList[i]);
        }
    }
    
  
}
