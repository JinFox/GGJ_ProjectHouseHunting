using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RoomGenerator))]
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


    RoomGenerator _roomGen;

    List<Room> _roomList;
    public Transform roomContainer;
    [Range(1, 10)]
    public int SizeOfFlatCatalog = 5;


    HomeSeeker _currentHomeSeeker;
    int _currentRoomIndex;
    Room _currentRoomDisplayed;

    void Awake()
    {
        _instance = this;
        DOTween.Init();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _roomGen = GetComponent<RoomGenerator>();
        _roomList = new List<Room>();

        RefillRoom();
        _currentRoomDisplayed = _roomList[_currentRoomIndex];
        _currentRoomDisplayed.gameObject.SetActive(true);
        GetNewHomeSeeker();
    }

    void    GetNewHomeSeeker()
    {
         _currentHomeSeeker = new HomeSeeker();

    }

    void RefillRoom()
    {
        while (_roomList.Count < this.SizeOfFlatCatalog)
        {
            Room r = _roomGen.GenerateNewRoom();
            _roomList.Add(r);
            r.transform.SetParent(roomContainer);
            r.gameObject.SetActive(false);
        }
    }
    
    void SwitchToNextRoom()
    {
        int nextIndex = (_currentRoomIndex + 1) % _roomList.Count;
        _currentRoomDisplayed.gameObject.SetActive(false);
        _currentRoomDisplayed = _roomList[nextIndex];
        _currentRoomDisplayed.gameObject.SetActive(true);
        _currentRoomIndex = nextIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentHomeSeeker.CalculateOverallScore(_currentRoomDisplayed);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SwitchToNextRoom();
        }
    }
}
