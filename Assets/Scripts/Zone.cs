using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public List<PreferenceType> _attachedPreference;
    public int zoneSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        
        Vector3 anchor = transform.position;
        Vector3 up = Vector3.up * 2f;
        Vector3 right = Vector3.right * zoneSize;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(anchor, anchor + right);
        Gizmos.DrawLine(anchor, anchor + up);
        Gizmos.DrawLine(anchor + up, anchor + up + right);
        Gizmos.DrawLine(anchor + right, anchor + right + up);
        Gizmos.DrawLine(anchor, anchor + up + right);
    }
}
