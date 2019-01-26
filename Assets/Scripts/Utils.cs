using UnityEngine;
using System;
using System.Collections.Generic;
class Utils
{
    public static Color goodGreen = new Color(.5f,.95f, .45f);
    public static Color badRed = new Color(.95f, .67f, .45f);
    public static T RandomEnumValue<T> (List<T> toAvoid = null)
        {
            List<T> list = new List<T>();
        
            foreach (T value in (T[])Enum.GetValues(typeof(T)))
            {
                list.Add(value);
            }
            foreach (T avoid in toAvoid)
            {
                list.Remove(avoid);
            }
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
}