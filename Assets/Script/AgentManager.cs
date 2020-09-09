using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using RVO;
using Lean;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Experimental.UIElements;
using Random = UnityEngine.Random;
using Vector2 = RVO.Vector2;

public class AgentManager : SingletonBehaviour<AgentManager> {
    public GameObject agentIndependent;
    public GameObject agentLeader;
    public GameObject agentFollower;
    public Transform finish;
    public Transform tempFinish;
    NavMeshAgent agent;
    int totalFrames = 0;

    [HideInInspector]
    public Vector2 finishPosition;
    [HideInInspector]
    public Vector2 tempFinishPosition;
    [HideInInspector]
    public Vector2 startPosition;

    //agent dictionary or list
    private Dictionary<int, IndependentAgent> indAgentMap = new Dictionary<int, IndependentAgent>();
    private Dictionary<int, FollowerAgent> folAgentMap = new Dictionary<int, FollowerAgent>();
    private Dictionary<int, LeaderAgent> leadAgentMap = new Dictionary<int, LeaderAgent>();

    // Use this for initialization
    void Start() {
        agent = this.GetComponent<NavMeshAgent>();
        Simulator.Instance.setTimeStep(0.22f);
        Simulator.Instance.setAgentDefaults(1.5f, 7, 5.0f, 2.0f, 2.0f, 0.50f, new Vector2(0.0f, 0.0f));

        // add in awake
        Simulator.Instance.processObstacles();
        //CreateLeaderAgent(62.0f, 64.0f, 8.0f, 10.0f);
        //CreateLeaderAgent(62.0f, 64.0f, 8.0f, 10.0f);
        //CreateIndependentAgent(66.0f, 68.0f, 15.0f, 17.0f, 0.3f);
        //kiri bawah

        //use this for homogeneous agents
        /*CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.28f);*/
        
        //use this for heterogeneous agents
        //CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);//case if there is no leader
        
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateLeaderAgent(62.0f, 64.0f, 11.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 10.0f, 11.0f, 0.28f);


        //kanan bawah

        //use this for homogeneous agents
        /*CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);*/


        //use this for heterogeneous agents
        //CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f); //case if there is no leader

        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateLeaderAgent(106.0f, 108.0f, 11.0f, 11.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 8.0f, 10.0f, 0.28f);


        //tengah

        //use this for homogeneous agents
        /*CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);*/

        //use this for heterogeneous agents
        //CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f); //case if there is no leader
        
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateLeaderAgent(84.0f, 86.0f, 37.0f, 37.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.3f);
        CreateIndependentAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.25f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);
        CreateFollowerAgent(84.0f, 86.0f, 34.0f, 36.0f, 0.28f);

        //kiri atas

        //use this for homogeneous agents
        /*CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);*/

        //use this for heterogeneous agents
        //CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f); //case if there is no leader
        
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateLeaderAgent(62.0f, 64.0f, 51.0f, 51.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(62.0f, 64.0f, 48.0f, 50.0f, 0.28f);


        //kanan atas

        //use this for homogeneous agents
        /*CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);*/

        //use this for heterogeneous agents
        //CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f); //case if there is no leader
        
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateLeaderAgent(106.0f, 108.0f, 51.0f, 51.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.3f);
        CreateIndependentAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.25f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);
        CreateFollowerAgent(106.0f, 108.0f, 48.0f, 50.0f, 0.28f);

    }

    private void UpdateFinishPosition()
    {
        Vector3 position = new Vector3(finish.position.x, finish.position.y, finish.position.z);
        finishPosition.x_ = finish.position.x;
        finishPosition.y_ = finish.position.z;
    }

    private void UpdateTempFinishPosition()
    {
        Vector3 position = new Vector3(tempFinish.position.x, tempFinish.position.y, tempFinish.position.z);
        tempFinishPosition.x_ = tempFinish.position.x;
        tempFinishPosition.y_ = tempFinish.position.z;
    }

    void CreateIndependentAgent(float x1, float x2, float z1, float z2, float v)
    {
        Vector3 position = new Vector3(Random.Range(x1, x2), 0, Random.Range(z1,z2));
        startPosition.x_ = position.x;
        startPosition.y_ = position.z;
        int sid = Simulator.Instance.addAgent(startPosition);
        Simulator.Instance.setAgentMaxSpeed(sid, v);
        if (sid >= 0)
        {
            GameObject go = LeanPool.Spawn(agentIndependent, new Vector3(startPosition.x(), 0, startPosition.y()), Quaternion.identity);
            IndependentAgent ga = go.GetComponent<IndependentAgent>();
            Assert.IsNotNull(ga);
            ga.sid = sid;
            indAgentMap.Add(sid, ga);
        }
    }

    void CreateLeaderAgent(float x1, float x2, float z1, float z2, float v)
    {
        Vector3 position = new Vector3(Random.Range(x1, x2), 0, Random.Range(z1, z2));
        startPosition.x_ = position.x;
        startPosition.y_ = position.z;
        int sid = Simulator.Instance.addAgent(startPosition);
        Simulator.Instance.setAgentMaxSpeed(sid, v);
        if (sid >= 0)
        {
            GameObject go = LeanPool.Spawn(agentLeader, new Vector3(startPosition.x(), 0, startPosition.y()), Quaternion.identity);
            LeaderAgent ga = go.GetComponent<LeaderAgent>();
            Assert.IsNotNull(ga);
            ga.sid = sid;
            leadAgentMap.Add(sid, ga);
        }
    }

    void CreateFollowerAgent(float x1, float x2, float z1, float z2, float v)
    {
        Vector3 position = new Vector3(Random.Range(x1, x2), 0, Random.Range(z1, z2));
        startPosition.x_ = position.x;
        startPosition.y_ = position.z;
        int sid = Simulator.Instance.addAgent(startPosition);
        Simulator.Instance.setAgentMaxSpeed(sid, v);
        if (sid >= 0)
        {
            GameObject go = LeanPool.Spawn(agentFollower, new Vector3(startPosition.x(), 0, startPosition.y()), Quaternion.identity);
            FollowerAgent ga = go.GetComponent<FollowerAgent>();
            Assert.IsNotNull(ga);
            ga.sid = sid;
            folAgentMap.Add(sid, ga);
        }
    }
    void DeleteAgent()
    {
     
        //float rangeSq = float.MaxValue;
        int agentNo = Simulator.Instance.queryNearAgent(finishPosition, 1.5f);
        if (agentNo == -1 || !indAgentMap.ContainsKey(agentNo))
            return;

        Simulator.Instance.delAgent(agentNo);
        LeanPool.Despawn(indAgentMap[agentNo].gameObject);
        indAgentMap.Remove(agentNo);
    }
    void DeleteAgent1()
    {

        //float rangeSq = float.MaxValue;
        int agentNo = Simulator.Instance.queryNearAgent(finishPosition, 1.5f);
        if (agentNo == -1 || !folAgentMap.ContainsKey(agentNo))
            return;

        Simulator.Instance.delAgent(agentNo);
        LeanPool.Despawn(folAgentMap[agentNo].gameObject);
        folAgentMap.Remove(agentNo);
    }
    /*
    void DeleteAgent2() // deleate agent leader
    {

        //float rangeSq = float.MaxValue;
        int agentNo = Simulator.Instance.queryNearAgent(finishPosition, 4.5f);
        if (agentNo == -1 || !leadAgentMap.ContainsKey(agentNo))
            return;

        Simulator.Instance.delAgent(agentNo);
        LeanPool.Despawn(leadAgentMap[agentNo].gameObject);
        leadAgentMap.Remove(agentNo);
    }*/
    // Update is called once per frame
    void Update () {
        UpdateFinishPosition();
        UpdateTempFinishPosition();
        int numAgent = Simulator.Instance.getNumAgents();
        Simulator.Instance.doStep();
        totalFrames += 1;
        DeleteAgent();
        DeleteAgent1();
        //DeleteAgent2();
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Waktu " + Time.realtimeSinceStartup + "   " + totalFrames);

        }
        //Debug.Log("Waktu " + Time.realtimeSinceStartup + "   " + totalFrames);

    }
}
