using UnityEngine;

public class Rotator : MonoBehaviour {
    	
	// Update is called once per frame
	void Update () {
        // Apply rotation
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}
}
