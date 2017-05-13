using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;                       // Player GameObject to follow
    private Vector3 offset;                         // Offset between camera and player

	// Use this for initialization
	void Start () {
        // Store offset
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame - after all updates
	void LateUpdate () {
        // Update camera position
        transform.position = player.transform.position + offset;
	}
}
