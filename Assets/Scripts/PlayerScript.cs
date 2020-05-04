using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3      target;
    public float        energyLevel = 100;
    public ThinkGoal    thinkGoal;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        agent     = GetComponent<NavMeshAgent>();
        thinkGoal = GetComponent<ThinkGoal>();
        thinkGoal.Activate();
    }

    // Update is called once per frame
    private void Update()
    {
        energyLevel -= 10 * Time.deltaTime;
        thinkGoal.Process();
    }
}