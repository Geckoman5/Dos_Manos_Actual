using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private string targetTag;
    private Collider hitboxCollider;

    void Start()
    {
        hitboxCollider = GetComponent<Collider>();
        gameObject.GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.Log("Valid Detection from Hitbox");
            Destroy(other.gameObject); //maybe put here then the damage thing?
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
