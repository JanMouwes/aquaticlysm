using UnityEngine;
using UnityEngine.AI;

/// <summary>
///     Basescript for agents to determine, initialize and update decisionmaking and needs.
/// </summary>
public class Character : MonoBehaviour
{
    public NavMeshAgent agent;
    public ThinkGoal thinkGoal;

    public float energyLevel = 100;
    public Vector3 target;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        thinkGoal = new ThinkGoal(this);
    }

    // Update is called once per frame
    private void Update()
    {
        // Check, that energylevel does not get lower during resting
        energyLevel -= 8 * Time.deltaTime;

        thinkGoal.Process();
    }
}