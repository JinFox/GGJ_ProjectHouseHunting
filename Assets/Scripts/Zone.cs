using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Item
{
    public PreferenceType preference;
    public Transform item;
};

public class Zone : MonoBehaviour
{
    public List<Item> _attachedPreference;
    [Range(1,6)]
    public int zoneSize = 1;
    public List<Item> ItemList {
        get
        {
            return _attachedPreference;
        }
    }


    private void OnDrawGizmos()
    {
        
        Vector3 anchor = transform.position;
        Vector3 up = Vector3.up * 2.5f;
        Vector3 right = Vector3.right * zoneSize;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(anchor, anchor + right);
        Gizmos.DrawLine(anchor, anchor + up);
        Gizmos.DrawLine(anchor + up, anchor + up + right);
        Gizmos.DrawLine(anchor + right, anchor + right + up);
        Gizmos.DrawLine(anchor, anchor + up + right);
    }
}
