using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(KinematicArrive))]
public class ArriveEditor : Editor
{
    private void OnSceneGUI()
    {
        KinematicArrive ka = (KinematicArrive)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(ka.transform.position, Vector3.up, Vector3.forward, 360, ka.ViewRadius);
        Vector3 viewAngleA = ka.DirFromAngle(-ka.viewAngle / 2, false);
        Vector3 viewAngleB = ka.DirFromAngle(ka.viewAngle / 2, false);

        Handles.DrawLine(ka.transform.position, ka.transform.position + viewAngleA * ka.ViewRadius);
        Handles.DrawLine(ka.transform.position, ka.transform.position + viewAngleB * ka.ViewRadius);


        Handles.color = Color.green;
        foreach(Transform visibleTarget in ka.visibleTargets)
        {
            Handles.DrawLine(ka.transform.position, visibleTarget.position);
        }
    }

}