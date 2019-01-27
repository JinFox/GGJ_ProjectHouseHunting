using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinningScreen : MonoBehaviour
{
    public TextMeshProUGUI peopleHousedText;
    public TextMeshProUGUI reviewRatingText;
    public Button _restartButton;
    // Start is called before the first frame update
    void Start()
    {
        _restartButton.onClick.AddListener(OnRestartClicked);
        ScoreSaving.Load();
        peopleHousedText.text = ScoreSaving.nbHoused + "     Highest " + ScoreSaving.maxNbHoused;
        reviewRatingText.text = ScoreSaving.rating.ToString("N1") + "  Highest " + ScoreSaving.maxRating;
    }
    
    public void OnRestartClicked()
    {
        SceneManager.LoadScene("Menu");
    }
}
