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

    #region SCORING AND TIMER
    public float startingTimer = 60f; // total duration of a game
    float _timer;


    #endregion


    void Awake()
    {
        _instance = this;
        DOTween.Init();
    }
    
    void ResolveComponents()
    {
        if (_roomGen == null)
        {
            _roomGen = GetComponent<RoomGenerator>();
            _roomList = new List<Room>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartGame();   
    }

    void    StartGame()
    {
        ResolveComponents();
        RefillRooms();
        GetNewHomeSeeker();

        _currentRoomDisplayed = _roomList[_currentRoomIndex];
        _currentRoomDisplayed.gameObject.SetActive(true);
        
    }

    void    GetNewHomeSeeker()
    {
        if (_currentHomeSeeker != null)
         _currentHomeSeeker = new HomeSeeker();
        //SAMPLE _currentHomeSeeker.preferences[0].GetPreferenceText()

    }

    void RefillRooms()
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

        _timer -= Time.deltaTime;
    }
}
