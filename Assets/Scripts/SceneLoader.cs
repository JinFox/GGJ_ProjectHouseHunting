using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class SceneLoader : MonoBehaviour
{
    public Image fadingPanel;

    public void Awake()
    {
        fadingPanel.color = new Color(1f, 1f, 1f, 0f);
        fadingPanel.gameObject.SetActive(false);
    }
    public void PlayGame()
    {
        fadingPanel.gameObject.SetActive(true);
        DOTween.To(() => fadingPanel.color, (x) => fadingPanel.color = x,
                    new Color(0f, 0f, 0f, 1.2f), .3f).OnComplete(ChangeScene);
       
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

}