using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FollowerAgent))]
public class NewBehaviourScript : Editor
{

    private void OnSceneGUI()
    {
        FollowerAgent fow = (FollowerAgent)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngles / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngles / 2, false);

        //Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        //Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        foreach (Transform visibleTarget in fow.visibleLeader)
        {
            Handles.color = Color.blue;
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }
        foreach (Transform visibleCrowds in fow.visibleCrowds)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fow.transform.position, visibleCrowds.position);
        }
    }
}
