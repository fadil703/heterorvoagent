using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;
using RVO;
using Vector2 = RVO.Vector2;

public class LeaderAgent : MonoBehaviour {
    [HideInInspector]
    public int sid = -1;
    public Vector2 goalVector;
    private Random m_random = new Random();
    NavMeshAgent agent;

    //field of view
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngles;

    public LayerMask followerMask;
    public LayerMask obstacleMask;
    public LayerMask leaderMask;
    public LayerMask crowdsMask;

    [HideInInspector]
    public List<Transform> visibleFollower = new List<Transform>();
    [HideInInspector]
    public Vector2 followerPos = new Vector2(0, 0);
    [HideInInspector]
    Vector3 goal = new Vector3(0, 0, 0);
    [HideInInspector]
    bool haveFollower = false;
    [HideInInspector]
    float distanceFol, disToFinish;
    [HideInInspector]
    float speed;
    
    

    private LineRenderer lineRenderer;


    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine("FindTargetWithDelay", 0.2f);
        speed = Simulator.Instance.getAgentMaxSpeed(sid);
        //NavMeshPath path = new NavMeshPath();
        //agent.CalculatePath(AgentManager.Instance.finish.position, path);
    }
    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleFollower();

        }
    }
    void FindVisibleFollower()
    {
        visibleFollower.Clear();
        Collider[] followerInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, followerMask);
        for (int i = 0;i< followerInViewRadius.Length; i++)
        {
            Transform follower = followerInViewRadius[i].transform;
            Vector3 dirToFollower = (follower.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToFollower) < viewAngles / 2)
            {
                float dstToFollower = Vector3.Distance(transform.position, follower.position);
                if(!Physics.Raycast(transform.position, dirToFollower, dstToFollower, obstacleMask))
                {
                    visibleFollower.Add(follower);
                }
            }
        }
        if (followerInViewRadius.Length > 0)
        {
            Transform follower1 = followerInViewRadius[0].transform;
            distanceFol = Vector3.Distance(transform.position, follower1.position);
            haveFollower = true;
            //followerPos.x_ = follower1.position.x;
            //followerPos.y_ = follower1.position.z;
        }
        

    }
    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }
    void OnCollisionEnter(Collision collision)
    {
        if (Time.realtimeSinceStartup >= 9.0f)
        {
            Debug.Log("Leader collision detection");
        }
        
    }
    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    // Update is called once per frame
    void Update () {
        if (sid >= 0)
        {
            Vector2 pos = Simulator.Instance.getAgentPosition(sid);
            Vector2 vel = Simulator.Instance.getAgentPrefVelocity(sid);
            transform.position = new Vector3(pos.x(), transform.position.y, pos.y());
            if (Math.Abs(vel.x())>0.01f && Math.Abs(vel.y()) > 0.01f)
            {
                transform.forward = new Vector3(vel.x(), 0, vel.y()).normalized;
            }
        }
        
        if (haveFollower == true && distanceFol > 20.0f)
        {
            goal = new Vector3(followerPos.x(), 0, followerPos.y());
        }
        else if (haveFollower == true && distanceFol <= 20.0f && distanceFol > 10.0f)
        {
            Simulator.Instance.setAgentPrefVelocity(sid, new Vector2(0, 0));
            return;
        }
        else if (haveFollower == true && distanceFol <= 10.0f && distanceFol >2.0f){
            goal = new Vector3(AgentManager.Instance.finishPosition.x(), 0, AgentManager.Instance.finishPosition.y());
            //set speed value similiar with follower 

            Simulator.Instance.setAgentMaxSpeed(sid, speed / (Mathf.Exp((distanceFol - 2.0f) / viewRadius)));
        }
        else
        {
            goal = new Vector3(AgentManager.Instance.finishPosition.x(), 0, AgentManager.Instance.finishPosition.y());
            Simulator.Instance.setAgentMaxSpeed(sid, speed);
        }
        
        agent.SetDestination(goal);
        Vector3 dest = agent.destination;
        goalVector.x_ = dest.x;
        goalVector.y_ = dest.z;
        goalVector = goalVector - Simulator.Instance.getAgentPosition(sid);
        
        
        disToFinish = Vector3.Distance(dest, transform.position);
        while (disToFinish <= 3)
        {
            Simulator.Instance.setAgentPrefVelocity(sid, new Vector2(0, 0));
            return;
        }
        
        if (RVOMath.absSq(goalVector) > 1.0f)
        {
            goalVector = RVOMath.normalize(goalVector);
        }
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
