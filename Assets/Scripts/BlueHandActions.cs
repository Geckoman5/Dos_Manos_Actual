using UnityEngine;
using UnityEngine.InputSystem;

public class BlueHandActions : MonoBehaviour
{
   private Vector3 originalPos;
   private Vector3 handPos;
   private float t;

   [Header ("Punch Settings")]
   public float maxPunchDistance;
   public float punchSpeed;
   public float punchDuration = 1f;
   public float punchCooldown = 0.5f;

   private float cooldownTimer;

   [Header ("Punch Bools")]
   public bool currentlyPunching = false;
   public bool currentlyReturning = false;
   private bool isSelected;

   [SerializeField] private Hitbox hitbox;
   
   void Start()
    {
        originalPos = transform.localPosition;
        cooldownTimer = punchCooldown - punchCooldown;
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Keyboard.current != null)
        {
            if (isSelected && Keyboard.current.spaceKey.wasPressedThisFrame && cooldownTimer <= 0)
            {
                Vector3 punchDirection = Vector3.forward;

                originalPos = transform.localPosition;
                t = 0f;
                currentlyPunching = true;

                hitbox.EnableHitbox();
                cooldownTimer = punchCooldown;

                handPos = originalPos + punchDirection * maxPunchDistance;
            }
        }

        if (currentlyPunching)
        {
            Vector3 currentPos = Vector3.Lerp(originalPos, handPos, t);

            transform.localPosition = currentPos;

            t += punchSpeed * Time.deltaTime;
        }

        if (t>= punchDuration && currentlyPunching)
        {
            currentlyPunching = false;
            currentlyReturning = true;
            t = 0f;

            hitbox.DisableHitbox();
        }

        if (currentlyReturning)
        {
            Vector3 currentPos = Vector3.Lerp(handPos, originalPos, t);

            transform.localPosition = currentPos;

            t += punchSpeed * Time.deltaTime;

            if (t >= punchDuration)
            {
                currentlyReturning = false;
            }
        }
    }




}
