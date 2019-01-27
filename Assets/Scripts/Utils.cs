using UnityEngine;
using System;
using System.Collections.Generic;
class Utils : MonoBehaviour
{
    static Utils _instance;
    public static Utils Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    public Color goodGreen = new Color(.5f,.95f, .45f);
    public Color badRed = new Color(.95f, .67f, .45f);
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