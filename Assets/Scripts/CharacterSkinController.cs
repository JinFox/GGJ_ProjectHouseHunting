using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

[System.Serializable]
public class Character
{
    public string name;
    public GameObject prefab;
    [HideInInspector]
    public GameObject instance;
    [HideInInspector]
    public Animator theAnimator;
};
public class CharacterSkinController : MonoBehaviour
{
    static CharacterSkinController _instance;
    public static CharacterSkinController Instance
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
    public Transform characterAnchor;
  
    public TextMeshPro bubbleText;
    public TextMeshPro characterName;
    //public SpriteRenderer characterSkin;
    public SpriteRenderer bubble;
    public List<Character> characters;

    Character current;
    private bool initialized = false;

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        ResolveComponents();
    }

    private void ResolveComponents()
    {
        if (initialized)
            return;

        for (int i = 0; i < characters.Count; i++)
        {
            Character charac = characters[i];
            GameObject gb = GameObject.Instantiate(charac.prefab);
            charac.instance = gb;
            gb.transform.SetParent(characterAnchor);
            gb.transform.localPosition = Vector3.zero;

            charac.instance.gameObject.SetActive(false);

        }
        bubble.gameObject.SetActive(false);
        initialized = true;
    }

    public void GenerateNewCharacter(string preferences, bool animate = true, Action onComplete = null)
    {
        if (!initialized)
            ResolveComponents();
        if (current != null)
        {
            current.instance.SetActive(false);
        }
        current = characters[UnityEngine.Random.Range(0, characters.Count)];
        
        bubbleText.text = preferences;
        characterName.text = current.name;
        current.instance.SetActive(true);
        if (animate)
        {
            characterAnchor.localPosition = new Vector3(-2.5f, 0f, 0f);
            bubble.gameObject.SetActive(false);
            characterAnchor.DOLocalMoveX(0f, 0.6f).SetEase(Ease.OutQuad).OnComplete(() => MoveDone(onComplete));
        }
        else
        {
            bubble.gameObject.SetActive(true);
            current.instance.SetActive(true);
        }
      
       
    }

    private void MoveDone(Action onComplete)
    {
        bubble.gameObject.SetActive(true);
        if (onComplete != null)
            onComplete();
    }
}
