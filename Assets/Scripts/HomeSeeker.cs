using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// REPRESENT A person searching a home (can be 1 or 2 person visually)
public class HomeSeeker : MonoBehaviour
{
    List<Preference> preferences;
    // Start is called before the first frame update
    void Start()
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

        int overallcursor = 0;
        for (int i = 0; i < UnityEngine.Random.Range(2,4); i++)
        {
            Preference pref = new Preference();
            pref.Randomize();
            overallcursor += pref.Cursor; // may help to draw a less random not to have 3 "I LIKE" or 3 I dont like
            preferences.Add(pref);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
