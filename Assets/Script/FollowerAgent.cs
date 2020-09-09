using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;
using RVO;
using Vector2 = RVO.Vector2;

public class FollowerAgent : MonoBehaviour {
    [HideInInspector]
    public int sid = -1;
    public Vector2 goalVector;
    private Random m_random = new Random();
    NavMeshAgent agent;

    //field of view
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngles;
    public LayerMask obstacleMask;
    public LayerMask leaderMask;
    public LayerMask crowdsMask;
    public LayerMask followerMask;

    [HideInInspector]
    public List<Transform> visibleLeader = new List<Transform>();
    [HideInInspector]
    public List<Transform> visibleCrowds = new List<Transform>();
    [HideInInspector]
    public int numOfCrowds, numOfLeader;
    [HideInInspector]
    bool haveLeader, haveCrowds = false;
    [HideInInspector]
    Vector3 goal;
    [HideInInspector]
    Vector2 leaderPos, crowdPos = new Vector2(0, 0);
    
    [HideInInspector]
    float dstToTarget;
    [HideInInspector]
    float agentSpeedDefault;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agentSpeedDefault = Simulator.Instance.getAgentMaxSpeed(sid);
        StartCoroutine("FindTargetsWithDelay", 0.2f);
	}

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleLeader();
            FindVisibleCrowds();
        }
    }
    void FindVisibleLeader()
    {
        visibleLeader.Clear();
        Collider[] LeaderInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, leaderMask);
        for(int i = 0; i < LeaderInViewRadius.Length; i++)
        {
            Transform leader = LeaderInViewRadius[i].transform;
            Vector3 dirToLeader = (leader.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToLeader) < viewAngles / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, leader.position);
                if (!Physics.Raycast(transform.position,dirToLeader, dstToTarget, obstacleMask))
                {
                    visibleLeader.Add(leader);
                    numOfLeader = visibleLeader.Count;
                }
            }
        }
        if (LeaderInViewRadius.Length > 0)
        {
            Transform leader = LeaderInViewRadius[0].transform;
            leaderPos.x_ = leader.position.x;
            leaderPos.y_ = leader.position.z;
            haveLeader = true;
        }
        else
        {
            haveLeader = false;
        }
    }

    void FindVisibleCrowds()
    {
        visibleCrowds.Clear();
        Collider[] CrowdsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, crowdsMask);
        for(int i = 0; i < CrowdsInViewRadius.Length; i++)
        {
            Transform Crowds = CrowdsInViewRadius[i].transform;
            Vector3 dirToCrowds = (Crowds.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToCrowds)< viewAngles / 2)
            {
                float dstToCrowds = Vector3.Distance(transform.position, Crowds.position);
                if(!Physics.Raycast(transform.position,dirToCrowds, dstToCrowds, obstacleMask))
                {
                    visibleCrowds.Add(Crowds);
                    numOfCrowds = visibleCrowds.Count;
                }
            }
        }
        if (CrowdsInViewRadius.Length > 0)
        {
            Transform crowd = CrowdsInViewRadius[0].transform;
            crowdPos.x_ = crowd.position.x;
            crowdPos.y_ = crowd.position.z;
            haveCrowds = true;
        }
    }

    ViewCastInfo viewCast(float globalAngle)
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
            Debug.Log("Follower collision detection");
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
    void Update() {
        if (sid >= 0)
        {
            Vector2 pos = Simulator.Instance.getAgentPosition(sid);
            Vector2 vel = Simulator.Instance.getAgentPrefVelocity(sid);
            transform.position = new Vector3(pos.x(), transform.position.y, pos.y());

            if (Math.Abs(vel.x()) > 0.01f && Math.Abs(vel.y()) > 0.01f)
            {
                transform.forward = new Vector3(vel.x(), 0, vel.y());
            }
        }
        //agentSpeedDefault = Simulator.Instance.getAgentMaxSpeed(sid);
        if (haveLeader == true)
        {
            goal = new Vector3(leaderPos.x()-2.0f, 0, leaderPos.y());

        }
        else if (haveLeader == false && haveCrowds == true)
        {
            goal = new Vector3(crowdPos.x()-2.0f, 0, crowdPos.y());
        }
        else {
            Simulator.Instance.setAgentPrefVelocity(sid, new Vector2(0, 0));
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("strayed");
            }
            //Debug.Log(sid + "strayed");
            return;
        }

        agent.SetDestination(goal);
        Vector3 dest = agent.destination;
        goalVector.x_ = dest.x;
        goalVector.y_ = dest.z;
        goalVector = goalVector - Simulator.Instance.getAgentPosition(sid);

        if (RVOMath.absSq(goalVector) > 1.0f)
        {
            goalVector = RVOMath.normalize(goalVector);
        }
        dstToTarget = Vector3.Distance(dest, transform.position);
      
        if (dstToTarget >=1.0f && dstToTarget < 10.0f)
        {
            //Simulator.Instance.setAgentMaxSpeed(sid, agentSpeedDefault *0.1f* Vector3.Distance(dest, transform.position));
            Simulator.Instance.setAgentMaxSpeed(sid, agentSpeedDefault * Mathf.Exp((Vector3.Distance(dest, transform.position) - 3.5f) / viewRadius));
        }
        else
        {
            Simulator.Instance.setAgentMaxSpeed(sid, agentSpeedDefault );
        }
        

        Simulator.Instance.setAgentPrefVelocity(sid, goalVector);
        //Debug.Log(Simulator.Instance.getAgentMaxSpeed(sid));
	}
}
