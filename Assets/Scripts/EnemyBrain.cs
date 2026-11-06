using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [Header ("EnemyStats")]
    [SerializeField] private int maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float attackRange = 10f;
    public Transform player;
    private int currentHealth;
    [SerializeField] private string enemyType;
    [Header ("Damage Numbers")]
    //here is where I can set the damage for each enemyType
    [SerializeField] private int sphereDamage = 1;

    void Start()
    {
        currentHealth = maxHealth;
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
        if (collision.gameObject.CompareTag("Player") && (enemyType == "BlueSphere" || enemyType == "OrangeSphere"))
        {
                //if player touches enemy, do damage
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(sphereDamage);
                }
            
        }
    }

    private void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

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
    }

    private void AttackModeSphere()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += speed  * direction * Time.deltaTime;
    }

    private void IdleModeSphere()
    {
        Vector3 direction = transform.forward;
    }
}
