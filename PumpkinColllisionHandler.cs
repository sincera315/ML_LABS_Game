using UnityEngine;

public class PumpkinCollisionHandler : MonoBehaviour
{
    public int health = 1; // Set the initial health value

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RedBox"))
        {
            // Decrease health by 1
            health--;

            // Destroy the pumpkin GameObject
            Destroy(gameObject);
        }
    }
}
