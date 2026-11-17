using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using NUnit.Framework;
public class EnemyBrain : MonoBehaviour
{
    
    [SerializeField] private LayerMask Walls;
    private EnemyBrain sphereFollowTarget;
    #region Enemy Stats
    [Header ("EnemyStats")]
    [SerializeField] private int maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float attackRange = 10f;
    public GameObject player;
    private int currentHealth;
    #endregion
    [SerializeField] private string enemyType;
    #region Damage Numbers
    [Header ("Damage Numbers")]
    //here is where I can set the damage for each enemyType
    [SerializeField] private int sphereDamage = 1;
    #endregion
    [Header ("Cooldown Timers")]
    #region Cooldown Timers
    //here is where I can set the cooldown for each enemyType
    [SerializeField] private float sphereAttackCooldown = 2f;
    private float nextSphereAttackTime;
    #endregion

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindWithTag("Player");

        //Sphere Enemies follow sphere enemies
            EnemyBrain[] allEnemies = FindObjectsByType<EnemyBrain>();
            List<EnemyBrain> candidates = new List<EnemyBrain>();
            foreach (EnemyBrain enemy in allEnemies)
            {
                bool isSphere = enemy.enemyType == "BlueSphere" || enemy.enemyType == "OrangeSphere";

                if (enemy != this && isSphere)
                {
                    candidates.Add(enemy);
                }
            }

            if (candidates.Count > 0)
            {
                sphereFollowTarget = candidates[Random.Range(0, candidates.Count)];
            }

    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //sphereEnemies
        #region Sphere Enemies

        if (!collision.gameObject.CompareTag("Player")
        ||
        (enemyType != "BlueSphere" && enemyType != "OrangeSphere")
        ||
        Time.time < nextSphereAttackTime)
        {return;}


    
        //if player touches enemy, do damage
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
         if (playerHealth == null)
            {return;}


        playerHealth.TakeDamage(sphereDamage);
        nextSphereAttackTime = Time.time + sphereAttackCooldown;

        //[next]enemies
        //remember to keep the same pattern as what is above
        #endregion
    }

    private void Update()
    {
        if (player == null)
        {return;}
        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);

        //sphereEnemies
        #region SphereModeConfig
        if (enemyType == "BlueSphere" || enemyType == "OrangeSphere")
        {
            if (currentHealth > 0)
            {
                if (distToPlayer <= attackRange)
                {
                    AttackModeSphere();
                }
                else
                {
                    IdleModeSphere();
                }
            }
            
        }
        #endregion

        //[next]enemies
        //remember to keep the same pattern, also remember to make new funcs.

    }

    #region Attack Modes
    private void AttackModeSphere()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        if (Physics.SphereCast(
        transform.position, 0.5f, direction, out RaycastHit hit, 1f, Walls))
        {
            direction = hit.normal;
            transform.rotation = Quaternion.LookRotation(direction);
        }
        transform.position += speed  * direction * Time.deltaTime;
    }
    #endregion

    #region Idle Modes
    private void IdleModeSphere()
    {
        if (sphereFollowTarget == null)
        {return;}
        Vector3 direction = (sphereFollowTarget.transform.position - transform.position).normalized;
        if (Physics.SphereCast(
            transform.position, 1f, direction, out RaycastHit hit, 1f, Walls))
        {
            direction = hit.normal;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        transform.position += speed * direction * Time.deltaTime;

    }
    #endregion

}
