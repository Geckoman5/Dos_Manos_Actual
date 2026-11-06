using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float maxHealth;
    private float currentHealth;
    [SerializeField] private TMP_Text healthText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
     //nothing here now
    }

//yes, below was taken from EnemyBrain, horray for effiency i guess
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        healthText.text = $"Health: {currentHealth:0}/{maxHealth:0}";
        //set health text to currentHealth/maxHealth
    }

    private void Die()
    {
        Debug.Log("Player is dead.");
        //Note find a way to display debug logs in game
        //also Put here death screen + whatever later
    }
}
