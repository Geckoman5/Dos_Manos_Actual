using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private string targetTag;
    [SerializeField] private int damageAmount = 1;
    private Collider hitboxCollider;


    void Start()
    {
        hitboxCollider = GetComponent<Collider>();
        gameObject.GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag))
        return;

        EnemyBrain enemy = other.GetComponent<EnemyBrain>();
        if (enemy != null)
        {
            enemy.TakeDamage(damageAmount);
            //Note that damageAmount might change depending on the action done by the hands
            //the hands currently only do 1 action
        }
    
    }
     public void EnableHitbox()
    {
        gameObject.GetComponent<Collider>().enabled = true;
    }

    public void DisableHitbox()
    {
        gameObject.GetComponent<Collider>().enabled = false;
    }

    
}
