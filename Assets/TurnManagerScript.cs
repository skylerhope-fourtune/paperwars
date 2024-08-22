using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Important when using UI stuff:
using UnityEngine.UI;

public class TurnManagerScript : MonoBehaviour
{

    public GameObject player; // Reference to the player GameObject. It's public, so it will generate a field in the Inspector in Unity under the TurnManager GameObject for me to drag and drop the player's gameobject into the field
    private PlayerScript playerScript; // Reference to the player's script on the players gameobject
    private Button beginTurnButton; // Tells the document that there's a button that will be identified elsewhere, but we can reference that button with the name beginTurnButton

    private bool isTurnActive = false; 

    // Start is called before the first frame update
    void Start()
    {
        // In Unity, the begin turn button has a tag "BeginTurn". We tell the script to look for a button with that tag.
        GameObject buttonObject = GameObject.FindWithTag("BeginTurn");

        // First it looks for the button in the UI using the tag, then assigns it to buttonObject. If its null, it throws a log error. That it way it won't crash stuff.
        if (buttonObject != null)
        {
            // Get the right Button component attached to the GameObject if its not null
            beginTurnButton = buttonObject.GetComponent<Button>();

            // A listener to the button's onClick event that results in running the OnBeginTurnButtonClicked method
            beginTurnButton.onClick.AddListener(OnBeginTurnButtonClicked);
        }
        else
        {
            Debug.LogError("No GameObject found with the tag 'BeginTurn'");
        }

        // Get the PlayerScript component from the player GameObject
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerScript>(); // We define the public variable player in unity. If we have forgotten to link them in unity with a drag and drop,
        }
        else
        {
            Debug.LogError("Player GameObject is not assigned"); // we'll get this error in the log.
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurnActive && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Player clicked");

            // Raycast from the mouse position to the game world in 2D
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                // Move the player to the clicked position
                playerScript.MoveToPosition(hit.point);
                Debug.Log("Should be moving");
            }
            else
            {
                Debug.Log("No collider hit");
            }
            isTurnActive = false;

            // As long as no second move command is given before the player is finished moving, this works well. It will get confused and sad and not wanna play anymore
            // if it gets simultaneous move commands. The finished product should not give an opportunity for interruptions to its movement, so we shall be chillin. 
        }
    }

    // This method will be called when the Begin Turn button is clicked
    void OnBeginTurnButtonClicked()
    {
        Debug.Log("Begin Turn button clicked!");
        isTurnActive = true;
    }
}
