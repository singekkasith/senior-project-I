using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    //Path Patrol
    [SerializeField] GameObject[] PatrolSpots;
    [SerializeField] bool isPathPatroling;

    [SerializeField] Vector3 stopDistance;

    private GameObject spotLocation;

    //Animataion Related
    [SerializeField] GameObject enemyModel;

    private Animator enemyAnim;
    private float enemySpeedCheck;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask groundMask, playerMask;

    //AI Type
    [Range(0,2)]
    //Note: 0 = Range, 1 = Melee, 2 = Special
    public int aiType;

    //patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Stunt

    [SerializeField]
    private float aiDefaultSpeed = 5f;

    [SerializeField]
    private float stunTime = 5f;

    [SerializeField] GameObject AIHitBox;

    
    public bool isFound = false;

    //Attacking
    public float shootForce = 32f;
    public float upwardShootForce = 8f;

    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    public GameObject projectile;
    public GameObject attackPoint;
    public GameObject meleeHitbox;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [SerializeField] private AudioSource stuntSF;

    private void Awake()
    {

        player = GameObject.Find("PlayerCapsule").transform;
        
        
        agent = GetComponent<NavMeshAgent>();

        if (enemyModel != null){
            enemyAnim = enemyModel.GetComponent<Animator>();
        }
        
    }

    private void Update()
    {
        enemySpeedCheck = agent.velocity.magnitude;
        if (enemyAnim != null){
            if (enemySpeedCheck <= 0){
                enemyAnim.SetBool("isWalking", false);
            }

            else if(enemySpeedCheck > 0){
                enemyAnim.SetBool("isWalking", true);
            }
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if(!playerInSightRange && !playerInAttackRange && !isFound) Patroling();
        if (playerInSightRange && !playerInAttackRange || isFound) Chasing();
        if (playerInAttackRange && playerInSightRange) Attacking();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalk = transform.position - walkPoint;

        if (distanceToWalk.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint(){
        if (isPathPatroling){
            spotLocation = PatrolSpots[Random.Range(0, PatrolSpots.Length-1)];
            Debug.Log(spotLocation);
            
            walkPoint = new Vector3(spotLocation.transform.position.x, transform.position.y, spotLocation.transform.position.z);
            
            if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
                walkPointSet = true;
        }

        else {
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
                walkPointSet = true;
        }
    }

    private void Chasing()
    {
        //Set "isFound" to false for enemy to stop following when out of the range
        isFound = true;
        agent.SetDestination(player.position + stopDistance);

        //Idea
        //If player.position.y - agent.position.y > (10f)
        //Go to Second Floor, Spot (Create a Spot on Second Floor)
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (attackPoint != null){
            attackPoint.transform.LookAt(player);
        }

        if (!alreadyAttacked){
            
            //Attack Code
            switch(aiType){
                case 0:
                    RangeAttack();
                    break;

                case 1:
                    MeleeAttack();
                    break;

                case 2:
                    break;
            } 
            
            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


    private void MeleeAttack(){
        //Melee Attack Anim
        if (enemyAnim != null){
            enemyAnim.SetTrigger("isAttacking");
        }

        if (meleeHitbox != null){
            meleeHitbox.SetActive(true);
            Invoke("DisableHitbox", 1f);
        }
    }

    private void RangeAttack(){
        Rigidbody rb = Instantiate(projectile, attackPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(attackPoint.transform.forward * shootForce, ForceMode.Impulse);
        rb.AddForce(attackPoint.transform.up * upwardShootForce, ForceMode.Impulse);
    }

    private void DisableHitbox(){
        meleeHitbox.SetActive(false);
    }

    private void OnTriggerEnter(Collider item){
        if (item.gameObject.CompareTag("Throwable")) {
            agent.speed = 0;
            AIHitBox.SetActive(false);
            if (stuntSF != null){
                stuntSF.Play();
            }
            Destroy(item.gameObject);
            StartCoroutine(StunTime(stunTime));
        }
        
    }

    IEnumerator StunTime(float stunTime){
        yield return new WaitForSeconds(stunTime);
        agent.speed = aiDefaultSpeed;
        AIHitBox.SetActive(true);
    }
}
