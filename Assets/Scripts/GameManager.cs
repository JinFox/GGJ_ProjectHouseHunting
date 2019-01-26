using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance {
        get {
            return _instance;
        }
        private set {
            _instance = value;
        }
    }

  
    void Awake()
    {
        _instance = this;
        DOTween.Init();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        RoomGenerator r = GetComponent<RoomGenerator>();
        r.GenerateNewRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
