using UnityEngine;
using UnityEngine.AI;

/// <summary>
///     Basescript for agents to determine, initialize and update decisionmaking and needs.
/// </summary>
public class Character : MonoBehaviour
{
    public NavMeshAgent agent;
    public ThinkGoal compositeGoal;

    public float energyLevel = 100;
    public Vector3 target;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        compositeGoal = new ThinkGoal(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        // Check, that energylevel does not get lower during resting
        energyLevel -= 8 * Time.deltaTime;

        compositeGoal.Process();
    }
}