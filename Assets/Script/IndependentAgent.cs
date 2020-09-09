using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RVO;
using Lean;
using Random = UnityEngine.Random;
using Vector2 = RVO.Vector2;

public class IndependentAgent : MonoBehaviour {

    [HideInInspector]
    public int sid = -1;
    public Vector2 goalVector;

    public LayerMask obstacleMask;
    public LayerMask leaderMask;
    public LayerMask crowdsMask;
    public LayerMask followerMask;

   

    private Random m_random = new Random();
    
    NavMeshAgent agent;
    //int collide = 0;

    //float disToFinish;
    private LineRenderer lineRenderer;


    // Use this for initialization
    void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (Time.realtimeSinceStartup >= 9.0f)
        {
            Debug.Log("Independent collision detection                         !!!!!");
            //collide += 1;
        }
    }
    // Update is called once per frame
    void Update () {
		if (sid >= 0)
        {
            Vector2 pos = Simulator.Instance.getAgentPosition(sid);
            Vector2 vel = Simulator.Instance.getAgentPrefVelocity(sid);
            transform.position = new Vector3(pos.x(), transform.position.y, pos.y());

            if (Math.Abs(vel.x())> 0.01f && Math.Abs(vel.y()) > 0.01f)
            {
                transform.forward = new Vector3(vel.x(), 0, vel.y()).normalized;
            }
        }
        Vector3 goal = new Vector3(AgentManager.Instance.finishPosition.x(), 0, AgentManager.Instance.finishPosition.y());
        agent.SetDestination(goal);
        Vector3 dest = agent.destination;
        goalVector.x_ = dest.x;
        goalVector.y_ = dest.z;
        goalVector = goalVector - Simulator.Instance.getAgentPosition(sid);
        /*
        while (disToFinish <= 10.0f)
        {
            Simulator.Instance.setAgentMaxSpeed(sid, 1.0f * disToFinish * 0.1f);
        }*/

        if (RVOMath.absSq(goalVector) > 1.0f)
        {
            goalVector = RVOMath.normalize(goalVector);
        }
        float distance = Vector3.Distance(dest, transform.position);
        Simulator.Instance.setAgentPrefVelocity(sid, goalVector);


        //debug line
        /*
        if (agent.hasPath)
        {
            lineRenderer.positionCount = agent.path.corners.Length;
            lineRenderer.SetPositions(agent.path.corners);
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }*/
    }
}
