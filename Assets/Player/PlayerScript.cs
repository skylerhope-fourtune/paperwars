using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Maybe this should be called TurnScript
public class PlayerScript : MonoBehaviour
{

    public float moveSpeed = 5f; // This public variable with a definition will generate a field in Unity in the Inspector under the player gamobject, and that field will be autopopulated with the definition. In unity, you can change the default value and test other values.

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
        StartCoroutine(MovePlayer(targetPosition));
    }

    private IEnumerator MovePlayer(Vector3 targetPosition)
    {
        // While the player is not at the target position, move towards it
        while ((transform.position - targetPosition).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
