using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

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
    public SpriteRenderer characterSkin;
    public SpriteRenderer bubble;
    public List<Character> characters;

    Character current;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
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
    }

    public void GenerateNewCharacter(string preferences)
    {
        if (current != null)
        {
            current.instance.SetActive(false);
        }
        current = characters[UnityEngine.Random.Range(0, characters.Count)];
        bubbleText.text = preferences;
        characterName.text = current.name;
        bubble.gameObject.SetActive(true);
        current.instance.SetActive(true);
    }
  
}
