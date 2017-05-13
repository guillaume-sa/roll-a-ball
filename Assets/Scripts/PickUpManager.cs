using UnityEngine;

public class PickUpManager : MonoBehaviour {

    public float spawnTime;                    // How long between each spawn.
    public GameObject pickUp;                  // Pick up prefab

    public GameObject ground;                  // Ground GameObject    
    private float groundX;                     // Ground x bound
    private float groundZ;                     // Ground z bound

    private PlayerController player;           // Player
    private bool gameOver;                     // GameOver boolean

    // Use this for initialization
    void Start () {
        // Get ground renderer
        Renderer groundRenderer = ground.GetComponent<Renderer>();
        // Get render's size
        Vector3 groundSize = groundRenderer.bounds.size;
        // Store ground bounds
        groundX = (groundSize.x /2.0f) - 1f;
        groundZ = (groundSize.z /2.0f) - 1f;

        // Get Player's gameobject
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // Initialize gameOver to false
        gameOver = false;

        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Update()
    {
        // Game is not over and player reached maximum score
        if (!gameOver && player.Count >= SettingsManager.MAX_SCORE)
        {
            // Set gameOver to true
            gameOver = true;

            // Find all remaining pick up objects
            GameObject[] pickUpObjects = GameObject.FindGameObjectsWithTag("Pick Up");
            // Iterate list of remaining objects
            foreach (GameObject pickUp in pickUpObjects)
            {
                // Destroy pick up object
                Destroy(pickUp);
            }
        }
    }

    void Spawn()
    {       
        if (!gameOver)
        {
            // Get random x and z coordinates
            float spawnX = Random.Range(-groundX, groundX);
            float spawnZ = Random.Range(-groundZ, groundZ);

            // Get a random position
            Vector3 randomSpawnPosition = new Vector3(spawnX, 0.5f, spawnZ);

            // Create an instance of the pick up prefab at the random position
            Instantiate(pickUp, randomSpawnPosition, Quaternion.identity);
        }
    }
}
