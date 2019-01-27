using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    int _nbPeopleHoused = 0;
    float _reviewRating = 2f;

    #endregion

    #region Character
    CharacterSkinController character;
    
    #endregion

    #region UIElements
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI nbPeopleHousedText;
    // temporary
    public TextMeshProUGUI reviewRatingText;
    public TextMeshProUGUI catalogIndex;
    public Button nextButton;
    public Button moveInButton;
    #endregion


    bool _enableInteractions = false;

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
            moveInButton.onClick.AddListener(MakeSeekerMoveIn);
            nextButton.onClick.AddListener(SwitchToNextRoom);
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
        GetNewHomeSeeker();
        RefillRooms();

        _currentRoomDisplayed = _roomList[_currentRoomIndex];
        _currentRoomDisplayed.gameObject.SetActive(true);

        // set score 
        _nbPeopleHoused = 0;
        _timer = startingTimer;
        _reviewRating = 2f;
        UpdateScorePanel();
    }

    void    GetNewHomeSeeker()
    {
        _enableInteractions = false;
        _currentHomeSeeker = new HomeSeeker();

        string s = _currentHomeSeeker.GetPreferencesFormatted();
        CharacterSkinController.Instance.GenerateNewCharacter(s, true, OnCharacterGenerated);
        
    }

    public void OnCharacterGenerated()
    {
        _enableInteractions = true;
    }

    private void GetRidOfCurrentRoom()
    {
        _currentRoomDisplayed.gameObject.SetActive(false);
        _currentRoomDisplayed.DestroyRoom();
        _roomList.Remove(_currentRoomDisplayed);
        _currentRoomIndex = 0;
        _currentRoomDisplayed = _roomList[_currentRoomIndex];
        RefillRooms();
        _currentRoomDisplayed.gameObject.SetActive(true);

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
        if (!_enableInteractions)
        {
            return ;
        }
        AudioManager.Instance.Play("Click");
        int nextIndex = (_currentRoomIndex + 1) % _roomList.Count;
        _currentRoomDisplayed.gameObject.SetActive(false);
        _currentRoomDisplayed = _roomList[nextIndex];
        _currentRoomDisplayed.gameObject.SetActive(true);
        _currentRoomIndex = nextIndex;
    }

    private void MakeSeekerMoveIn()
    {
        if (!_enableInteractions)
        {
            return;
        }
        _enableInteractions = false;
        AudioManager.Instance.Play("Click");
        float score = _currentHomeSeeker.CalculateOverallScore(_currentRoomDisplayed, OnScoringSequenceFinished);
        _reviewRating = Mathf.Clamp(_reviewRating + (score / 10f), 0f, 5f);
        
    }

    public void OnScoringSequenceFinished()
    {
        GetNewHomeSeeker();
        GetRidOfCurrentRoom();
        _nbPeopleHoused++;
    }

   

    void AlternativeInput() // call to enable keyboard input
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakeSeekerMoveIn();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            SwitchToNextRoom();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            GetNewHomeSeeker();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer < 0f) // Times Up!
        {
            EndGame();
        }

        //if (_enableInteractions)
        //{
        //    AlternativeInput();
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _enableInteractions = false;
            Application.Quit();
        }


        _timer -= Time.deltaTime;

        UpdateScorePanel();
        
    }

    private void EndGame()
    {
        _enableInteractions = false;
        ScoreSaving.SetScore(_nbPeopleHoused, _reviewRating);
        SceneManager.LoadScene("WinScreen");
    }

    private void UpdateScorePanel()
    {
        timerText.text = _timer.ToString("N1");
        nbPeopleHousedText.text = _nbPeopleHoused.ToString();
        reviewRatingText.text = _reviewRating.ToString("N2") + " / 5";
        catalogIndex.text = (_currentRoomIndex + 1) + " / " + SizeOfFlatCatalog; 
    }
}
