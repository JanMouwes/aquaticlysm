using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Basescript for agents to determine, initialize and update decisionmaking and needs.
/// </summary>
public class PlayerScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3      target;
    public ThinkGoal compositeGoal;

    public float        energyLevel = 100;
    
    // Start is called before the first frame update
    private void Start()
    {
        agent     = GetComponent<NavMeshAgent>();
        compositeGoal = GetComponent<ThinkGoal>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Check, that energylevel does not get lower during resting
        if (gameObject.GetComponent<RestGoal>() == null)
            energyLevel -= 10 * Time.deltaTime;

        compositeGoal.Process();
    }
}