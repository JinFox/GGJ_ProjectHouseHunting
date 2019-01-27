using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DumbShake : MonoBehaviour
{
    List<Transform> children;
    // Start is called before the first frame update
    void Start()
    {
        children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
            child.DOShakePosition(3f, .5f, 2).SetLoops(-1, LoopType.Yoyo);
            child.DOShakeRotation(5f, .2f, 1).SetLoops(-1, LoopType.Yoyo);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
