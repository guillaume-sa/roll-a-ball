using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;                             // Player's speed
    public Text countText;                          // Score text
    public Text winText;                            // Win text

    private Rigidbody rb;                           // Player's Rigidbody
    private int count;                              // Player's score

    public int Count
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
        // Set score text
        SetCountText();
        // Empty win text
        winText.text = "";
    }


    // Physics Update
    void FixedUpdate()
    {
        // Get horizontal move from player's input
        float moveHorizontal = Input.GetAxis("Horizontal");
        // Get vertical move from player's input
        float moveVertical = Input.GetAxis("Vertical");

        // Compute movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Apply movement to player's rigidbody
        rb.AddForce(movement * speed);
    }

    // Trigger
    void OnTriggerEnter(Collider other)
    {
        // If player collided with a pick up object
        if (other.gameObject.CompareTag("Pick Up"))
        {
            // Disable pick up object
            // other.gameObject.SetActive(false);
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
