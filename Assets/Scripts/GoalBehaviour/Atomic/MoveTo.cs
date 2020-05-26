using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : IGoal
{
    private GameObject _owner;
    public GoalStatus Status { get; private set; }
    public string Name { get; private set; }
    public float NearRange { get; set; }
    private Animator _playerAnim;

    private Vector3 _previousPosition;

    // Target position.
    private readonly Vector3 _target;

    public MoveTo(GameObject owner, Vector3 position)
    {
        _owner = owner;
        Name = "MoveTo";
        _target = position;
        NearRange = 2f;
        _playerAnim = _owner.GetComponent<Animator>();
    }

    public void Activate()
    {
        // Set destination.
        _owner.GetComponent<NavMeshAgent>().destination = _target;


        NavMeshPath path = new NavMeshPath();
        _playerAnim.SetFloat("Speed", 0.5f);

        bool success = NavMesh.CalculatePath(_owner.transform.position, _target,
                                             _owner.GetComponent<NavMeshAgent>().areaMask, path);

        bool hasFailed = !success || path.status != NavMeshPathStatus.PathComplete;

        this.Status = hasFailed ? GoalStatus.Failed : GoalStatus.Active;
    }

    public GoalStatus Process()
    {
        if (Status == GoalStatus.Inactive)
            Activate();

        // Checks if the Owner arrived at the target.
        if (Vector3.Distance(_owner.transform.position, _target) < NearRange) { Terminate(); }
        
        return Status;
    }

    public void Terminate()
    {
        _playerAnim.SetFloat("Speed", 0);
        Status = GoalStatus.Completed;
    }
}