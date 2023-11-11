using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    const float wayPointGizmoRadius = 0.3f;

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.name = "Point " + (i+1).ToString("00");
            int j = GetNextIndex(i);
            Gizmos.color = new Color(0, 0, 1, 0.5f);
            Gizmos.DrawSphere(GetWayPoint(i), wayPointGizmoRadius);
            Gizmos.color = new Color(0.2f, 0.2f, 1);
            Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));
        }
    }
    public int GetNextIndex(int i)
    {
        if (i + 1 == transform.childCount)
        {
            return 0;
        }
        return i + 1;
    }
    public Vector3 GetWayPoint(int i)
    {
        return transform.GetChild(i).position;
    }
}
