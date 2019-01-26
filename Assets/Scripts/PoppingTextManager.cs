using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoppingTextManager : MonoBehaviour
{
    static PoppingTextManager _instance;
    public static PoppingTextManager Instance
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


    [SerializeField] GameObject poppingTextPrefab = null;
    int poolSize = 20;

    Queue<PoppingText> poppingPool;

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        poppingPool = new Queue<PoppingText>();

        for (int i = 0; i < poolSize; i++)
        {
            PoppingText p = GameObject.Instantiate(poppingTextPrefab).GetComponent<PoppingText>();
            p.DisableText();
            p.transform.SetParent(transform);
            p.gameObject.SetActive(false);
            poppingPool.Enqueue(p);
        }
    }
    
    public void PopText(string Content, Color color, Vector3 position, float duration = 1f)
    {
        PoppingText p = poppingPool.Dequeue();
        p.gameObject.SetActive(true);
        p.PopText(Content, color, position,duration);
        
        poppingPool.Enqueue(p);
    }
    
}
