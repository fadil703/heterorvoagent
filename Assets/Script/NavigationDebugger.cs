using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class NavigationDebugger : MonoBehaviour {
    [SerializeField]
    private NavMeshAgent agentToDebug;

   

    private LineRenderer lineRenderer; 
	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        
	}
	
	// Update is called once per frame
	void Update () {
		if (agentToDebug.hasPath)
        {
            lineRenderer.positionCount = agentToDebug.path.corners.Length;
            lineRenderer.SetPositions(agentToDebug.path.corners);
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
        //Debug.Log(lineRenderer.enabled);
	}
}
