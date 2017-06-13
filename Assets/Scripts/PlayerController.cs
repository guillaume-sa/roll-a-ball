using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public float speed;                             // Player's speed
    private Text countText;                         // Score text
    private Text winText;                           // Win text

    private Rigidbody rb;                           // Player's Rigidbody
    private int count;                              // Player's score

    public int Count                                // Count property
    {
        get
        {
            return count;
        }
    }

	// Initialization
	void Start () {
        // Get Player's Rigidbody
        rb = GetComponent<Rigidbody>();
        // Initilialize the score
        count = 0;
        
        // Get player's count text
        countText = GameObject.Find("Canvas/Count Text").GetComponent<Text>();
        // Get player's win text
        winText = GameObject.Find("Canvas/Win Text").GetComponent<Text>();

        // Set score text
        SetCountText();

        // Empty win text
        winText.text = "";
    }


    // Physics Update
    void FixedUpdate()
    {
        // If not local player, exit
        if (!isLocalPlayer)
        {
            return;
        }

        // Get horizontal move from player's input
        float moveHorizontal = Input.GetAxis("Horizontal");
        // Get vertical move from player's input
        float moveVertical = Input.GetAxis("Vertical");

        // Compute movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Apply movement to player's rigidbody
        rb.AddForce(movement * speed);
    }

    // Run for each client locally
    public override void OnStartLocalPlayer()
    {
        // color local player blue
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    // Trigger
    void OnTriggerEnter(Collider other)
    {
        // If player collided with a pick up object
        if (other.gameObject.CompareTag("Pick Up"))
        {
            // Disable pick up object
            Destroy(other.gameObject);
            // Increment player's score
            count += 1;
            // Update player's score text
            SetCountText();
        }
    }

    // Set score text
    void SetCountText()
    {
        // If not local player, exit
        if (!isLocalPlayer)
        {
            return;
        }
        // Set score text
        countText.text = "Count: " + count.ToString();
        // If player gets all pick up objects
        if (count >= SettingsManager.MAX_SCORE)
        {
            // Set win text
            winText.text = "You Win!";
        }
    }
}
