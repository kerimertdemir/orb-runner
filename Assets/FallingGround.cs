using UnityEngine;

public class FallingGround : MonoBehaviour
{
    public float customGravity = 0.5f;

    public void ActivateFall()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = customGravity;
            rb.linearVelocity = Vector2.zero;
            rb.WakeUp();
        }
    }
}
