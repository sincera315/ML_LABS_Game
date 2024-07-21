using UnityEngine;

public class BoxHealthManager : MonoBehaviour
{
    public int BoxHealth = 10; // Initial health for the box
    private void Start()
    {
        Debug.Log($"Initial Box health: {BoxHealth}");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BoxHealth--;
        }
    }

    private void update() 
    {
        

        if (BoxHealth == 0)
        {
            // Game over logic (e.g., end the game)
            Debug.Log("Game over! Box health depleted.");
        }
    }
}
