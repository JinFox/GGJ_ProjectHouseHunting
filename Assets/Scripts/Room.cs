using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public List<Transform>  zoneSlot;
    int currentZoneIndex; // define witch zone to attach to
    public SpriteRenderer   wallpaper;

    public List<Sprite> possibleWallPaper;
    List<Zone> _attached;

    public int totalSlot
    {
        get
        {
            return zoneSlot.Count;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        wallpaper.color = UnityEngine.Random.ColorHSV(0f, .8f, 0f, .5f, 0.5f, 1f);
        wallpaper.sprite = possibleWallPaper[UnityEngine.Random.Range(0, possibleWallPaper.Count)];
    }


    public void AttachZone(Zone zone)
    {
        if (_attached == null)
        {
            _attached = new List<Zone>();
            currentZoneIndex = 0;
        }
        if (currentZoneIndex >= zoneSlot.Count)
        {
            Debug.LogWarning("TRYING TO INSERT TOO MANY ZONE");
            return;
        }
        _attached.Add(zone);
        Transform zoneAnchor = zoneSlot[currentZoneIndex];

        zone.transform.SetParent(zoneAnchor);
        zone.transform.localPosition = Vector3.zero;

        currentZoneIndex += zone.zoneSize;
    }

    public List<Item> GetAttachedItems()
    {
        List<Item> items = new List<Item>();
        foreach (var z in _attached)
        {
            items.AddRange(z.ItemList);
        }
        return items;
    }

    internal void DestroyRoom()
    {
        GameObject.Destroy(gameObject);
    }
}
