using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
	private Transform player;
    // private PlayerHealth playerHealth;
    // private EnemyHealth enemyHealth;
    private NavMeshAgent agent;


    void Awake() {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // playerHealth = player.GetComponent <PlayerHealth> ();
        // enemyHealth = GetComponent <EnemyHealth> ();
        agent = GetComponent<NavMeshAgent>();
    }


    void Update() {
        Vector3 velocity = Quaternion.Inverse(agent.transform.rotation) * agent.desiredVelocity;
        float angle = Mathf.Atan2(velocity.x, velocity.z) * 180.0f / Mathf.PI;
        float speed = agent.desiredVelocity.magnitude;

        // if the angle between the agent and the target is too great, rotate without translating...
        if (Mathf.Abs(angle) > 45) {
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 xzVector = new Vector3(direction.x, 0.0f, direction.z);
            Quaternion lookRotation = Quaternion.LookRotation(xzVector);

            agent.updateRotation = false;
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * 5);
        }
        // ...otherwise translate + rotate...
        else if (true/*enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0*/) {
            agent.updateRotation = true;
            agent.SetDestination(player.position);
            animator.SetBool("Walk Forward", true);
        }
        // ...otherwise idle
        else {
            agent.enabled = false;
        }
    } 
}