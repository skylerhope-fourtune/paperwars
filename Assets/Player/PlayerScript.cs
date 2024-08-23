using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Maybe this should be called TurnScript
public class PlayerScript : MonoBehaviour
{

    public float moveSpeed = 5f; // This public variable with a definition will generate a field in Unity in the Inspector under the player gamobject, and that field will be autopopulated with the definition. In unity, you can change the default value and test other values.
    public float moveDistance = 10f; // Creates a field  in Unity for maximum movement the player has per turn

    private Vector3 startPosition; //unread right now but may be useful in the future

    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to move the player to a specific position
    public void MoveToPosition(Vector3 targetPosition)
    {
        // Calculate the direction and distance to the target position
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        // Calculate the final position to move to, limited by moveDistance
        float moveDistanceClamped = Mathf.Min(distanceToTarget, moveDistance);
        Vector3 finalPosition = transform.position + direction * moveDistanceClamped;

        startPosition = transform.position; // Save the starting position. Unread right now but may be useful in the future
        StartCoroutine(MovePlayer(finalPosition));
    }

    private IEnumerator MovePlayer(Vector3 targetPosition)
    {
        while ((transform.position - targetPosition).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Stop the player movement by setting the velocity to zero (if using Rigidbody2D)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
