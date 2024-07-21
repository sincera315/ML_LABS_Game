using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class automove : MonoBehaviour
{
    public float speed;
    public Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Assuming the player is tagged as "Player"
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            targetPosition = player.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
