using UnityEngine;
using UnityEngine.Networking;

public class PickUpManager : NetworkBehaviour {

    public float spawnTime;                    // How long between each spawn.
    public GameObject pickUpPrefab;            // Pick up prefab

    public GameObject ground;                  // Ground GameObject    
    private float groundX;                     // Ground x bound
    private float groundZ;                     // Ground z bound

    private bool gameOver;                     // GameOver boolean
    GameObject[] players = null;               // GameObject players

    // Use this for initialization
    void Start () {
        // Get ground renderer
        Renderer groundRenderer = ground.GetComponent<Renderer>();
        // Get render's size
        Vector3 groundSize = groundRenderer.bounds.size;
        // Store ground bounds
        groundX = (groundSize.x /2.0f) - 1f;
        groundZ = (groundSize.z /2.0f) - 1f;

        // Initialize gameOver to false
        gameOver = false;

        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Update()
    {
        // Game is not over and player reached maximum score
        if (!gameOver)
        {
            // Get the connected players 
            if (NetworkServer.connections.Count > 1 && players == null)
            {
                // Strore player's GameObject
                players = GameObject.FindGameObjectsWithTag("Player");
            }
            // Players are set
            if (players != null)
            {
                // Iterate players
                foreach (GameObject player in players)
                {
                    PlayerController playerController = player.GetComponent<PlayerController>();
                    if (playerController.Count >= SettingsManager.MAX_SCORE)
                    {
                        // Game is over
                        gameOver = true;
                        // Clean the board
                        Clean();
                        break;
                    }
                }
            }
        }
    }

    void Spawn()
    {
        if (NetworkServer.active && NetworkServer.connections.Count > 1 && !gameOver)
        {
            // Get random x and z coordinates
            float spawnX = Random.Range(-groundX, groundX);
            float spawnZ = Random.Range(-groundZ, groundZ);

            // Get a random position
            Vector3 randomSpawnPosition = new Vector3(spawnX, 0.5f, spawnZ);

            // Create an instance of the pick up prefab at the random position
            GameObject pickUp = (GameObject)Instantiate(pickUpPrefab, randomSpawnPosition, Quaternion.identity);

            // Spawn pickup object
            NetworkServer.Spawn(pickUp);
        }
    }

    void Clean()
    {
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
