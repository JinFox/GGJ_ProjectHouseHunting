using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;

public enum PreferenceType
{
    HOT,
    COLD,
    COMFORT,
    NOISE,
    FANCYNESS,
    ANIMALS
};
class Preference
{
    int[] possibleValues = {-1,1};
    public int nbPrefType;
   
    PreferenceType _type;
    int _cursor; // -1 Hate it / 0 Neutral / 1 Like it ///////// POTENTIALLY -2 BIG NONO and 2 LOVEIT!!!!!
    
    public int Cursor {get{return _cursor;}}
    public PreferenceType Type {get{return _type;}}

    public Preference()
    {
        nbPrefType = Enum.GetNames(typeof(PreferenceType)).Length;
    }
    public void Randomize(List<PreferenceType> toAvoid, int favor = 0) // in case want to influence
    {
        _type = Utils.RandomEnumValue<PreferenceType>(toAvoid);
        if (favor != 0)
        {
            _cursor = Mathf.Clamp(favor, -1, 1);
        } else
        {
            _cursor = possibleValues[UnityEngine.Random.Range(0, possibleValues.Length)];    
        }

    }
    public String GetPreferenceText(String subject, int plurality = 1) // will provide "I" or "We" or funny stuff
    {
        StringBuilder sb = new StringBuilder(subject);

        if (_cursor <= -1) 
        {
            sb.Append(" don't like");
        } else if (_cursor >= 1) {
            sb.Append(" like");
        } else {
            if (plurality > 1)
                sb.Append(" are ok");
            else if (plurality == 1)
                sb.Append(" am ok");
            else // weird case just in case
                sb.Append(" is ok");
        }

        switch (_type)
        {
            case PreferenceType.HOT:
                sb.Append(" being hot.");
            break;
            case PreferenceType.COLD:
                sb.Append(" being cold.");
            break;
            case PreferenceType.COMFORT:
                sb.Append(" being comfy.");
            break;
            case PreferenceType.NOISE:
                sb.Append(" loud noises.");
            break;
            case PreferenceType.FANCYNESS:
                sb.Append(" having a fancy interior.");
            break;
            case PreferenceType.ANIMALS:
                sb.Append(" having animals.");
            break;
        }      

        return sb.ToString();
    }

    public int GetScore(PreferenceType type)
    {
        if (_type.Equals(type))
            return _cursor;
        return 0;
    }


}