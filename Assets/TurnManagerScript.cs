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
    private Button submitTurnButton;

    private bool isTurnActive = false;

    private Vector3 plannedPosition;     // The position the player plans to move to
    private LineRenderer lineRenderer;   // Line Renderer to draw the planned path


    // Start is called before the first frame update
    void Start()
    {
        // Check if player is assigned and log an error if it's not
        if (player == null)
        {
            Debug.LogError("Player GameObject is not assigned in the inspector.");
        }
        else
        {
            // Get the PlayerScript component from the player GameObject
            playerScript = player.GetComponent<PlayerScript>();
            if (playerScript == null)
            {
                Debug.LogError("PlayerScript component not found on the player GameObject.");
            }

            // Set up the LineRenderer for showing the planned path
            Debug.Log("Adding LineRenderer component to player...");
            lineRenderer = player.AddComponent<LineRenderer>();
            Debug.Log("LineRenderer component added.");

            Debug.Log("Setting startWidth to 0.1f...");
            lineRenderer.startWidth = 0.1f;
            Debug.Log("startWidth set to 0.1f.");

            Debug.Log("Setting endWidth to 0.1f...");
            lineRenderer.endWidth = 0.1f;
            Debug.Log("endWidth set to 0.1f.");

            Debug.Log("Setting positionCount to 0...");
            lineRenderer.positionCount = 0;
            Debug.Log("positionCount set to 0.");

            Debug.Log("Assigning material to LineRenderer...");
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            Debug.Log("Material assigned: " + lineRenderer.material.name);

            Debug.Log("Setting startColor to green...");
            lineRenderer.startColor = Color.green;
            Debug.Log("startColor set to green.");

            Debug.Log("Setting endColor to green...");
            lineRenderer.endColor = Color.green;
            Debug.Log("endColor set to green.");
        }

        // Set up the Begin Turn button listener
        GameObject beginTurnButtonObject = GameObject.FindWithTag("BeginTurn");
        if (beginTurnButtonObject != null)
        {
            Debug.Log("Setting Up Begin Button...");
            beginTurnButton = beginTurnButtonObject.GetComponent<Button>();
            beginTurnButton.onClick.AddListener(OnBeginTurnButtonClicked);
            Debug.Log("Begin Button Has Set Up");
        }
        else
        {
            Debug.LogError("No GameObject found with the tag 'BeginTurn'");
        }

        // Set up the Submit Turn button listener
        GameObject submitTurnButtonObject = GameObject.FindWithTag("SubmitTurn");
        if (submitTurnButtonObject != null)
        {
            Debug.Log("Setting Up Submit Button... 1/3");

            submitTurnButton = submitTurnButtonObject.GetComponent<Button>();

            Debug.Log("Setting Up Submit Button... 2/3");

            submitTurnButton.onClick.AddListener(OnSubmitTurnButtonClicked);

            Debug.Log("Setting Up Submit Button... 3/3");

            submitTurnButton.gameObject.SetActive(false);

            Debug.Log("Submit Button Has Set Up");
        }
        else
        {
            Debug.LogError("No GameObject found with the tag 'SubmitTurn'");
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Check if it's the player's turn and if the left mouse button was clicked
        if (isTurnActive && Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is over a UI element (like the Submit button)
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on UI element. Ignoring input for movement planning.");
                return; // Exit the Update method early to avoid planning movement
            }

            Debug.Log("Player clicked to plan movement");

            // Raycast from the mouse position to the game world in 2D
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                // Convert hit.point from Vector2 to Vector3
                Vector3 targetPosition = new Vector3(hit.point.x, hit.point.y, player.transform.position.z);

                // Calculate the direction and distance for the planned move
                Vector3 direction = (targetPosition - player.transform.position).normalized;
                float distanceToTarget = Vector3.Distance(player.transform.position, targetPosition);
                float moveDistanceClamped = Mathf.Min(distanceToTarget, playerScript.moveDistance);

                // Determine the planned position
                plannedPosition = player.transform.position + direction * moveDistanceClamped;

                // Draw the line from the player to the planned position
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, player.transform.position);
                lineRenderer.SetPosition(1, plannedPosition);

                Debug.Log("Planned position set. Ready to submit turn.");
            }
        }
    }

    // Called when the Begin Turn button is clicked
    void OnBeginTurnButtonClicked()
    {
        Debug.Log("Begin Turn button clicked!");
        isTurnActive = true;
        submitTurnButton.gameObject.SetActive(true); // Show the Submit Turn button
        beginTurnButton.gameObject.SetActive(false);
    }

    // Called when the Submit Turn button is clicked
    void OnSubmitTurnButtonClicked()
    {
        Debug.Log("Submit Turn button clicked!");
        isTurnActive = false;
        lineRenderer.positionCount = 0; // Hide the planned path line

        // Move the player to the planned position
        playerScript.MoveToPosition(plannedPosition);

        submitTurnButton.gameObject.SetActive(false); // Hide the Submit Turn button again
        beginTurnButton.gameObject.SetActive(true);
    }
}
