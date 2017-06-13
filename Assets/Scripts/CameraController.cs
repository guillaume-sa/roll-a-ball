using UnityEngine;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour {

    private Vector3 offset;                         // Offset between camera and player

	// Use this for initialization
	void Start () {
        // If not local player, exit
        if (!isLocalPlayer)
        {
            return;
        }
        // Store offset
        offset = Camera.main.transform.position - transform.position;
	}
	
	// Update is called once per frame - after all updates
	void LateUpdate () {
        // If not local player, exit
        if (!isLocalPlayer)
        {
            return;
        }
        // Update camera position
        Camera.main.transform.position = transform.position + offset;
	}
}
