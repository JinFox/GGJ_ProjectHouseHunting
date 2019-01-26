using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    public List<Zone> zonePool; // Prefab



    Zone        DrawCorrectZoneFromPool(List<Zone> pool, int maxZoneSize)
    {
        // COuld use some variation
        List<Zone> viable = new List<Zone>();
        foreach (var z in pool)
        {
            if (z.zoneSize <= maxZoneSize)
            {
                viable.Add(z);
            }
        }
        if (viable.Count != 0)
        {
            return viable[UnityEngine.Random.Range(0, viable.Count)];
        }
        else // no more viable in the POOL, emergency, just draw from main pool
        {
            return DrawCorrectZoneFromPool(zonePool, maxZoneSize);
        }
    }

    public Room GenerateNewRoom()
    {
        Room r = GameObject.Instantiate(roomPrefab).GetComponent<Room>();
        List<Zone> tmpPool = new List<Zone>(zonePool);

        int remainingSlot = r.totalSlot;
        while (remainingSlot > 0)
        {
            Zone zPrefab = DrawCorrectZoneFromPool(tmpPool, remainingSlot);
            
            Zone newZone = GameObject.Instantiate(zPrefab).GetComponent<Zone>();
            r.AttachZone(newZone);

            remainingSlot -= newZone.zoneSize;
        }

        return r; 
    }
}
