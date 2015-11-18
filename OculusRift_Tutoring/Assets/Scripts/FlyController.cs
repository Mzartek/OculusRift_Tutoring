using UnityEngine;
using System.Collections;

public class FlyController : MonoBehaviour {

    public float maxSpeed = 5.0f;
    public float accelerator = 1.0f;
    public float decelerator = 0.25f;

    private float speed = 0;
    private Vector3 simpleVector = new Vector3(0, 0.25f, 1);

	// Use this for initialization
	void Start () {
	}

    private void manageCommand() {
        if (Input.GetKey("space")) speed += (speed >= maxSpeed) ? 0 : accelerator;
        speed -= (speed <= 0) ? 0 : decelerator;

        if (Input.GetKey("down")) transform.Rotate(-1, 0, 0);
        if (Input.GetKey("up")) transform.Rotate(1, 0, 0);
        if (Input.GetKey("right")) transform.Rotate(0, 0, -1);
        if (Input.GetKey("left")) transform.Rotate(0, 0, 1);
        if (Input.GetKey("x")) transform.Rotate(0, -1, 0);
        if (Input.GetKey("c")) transform.Rotate(0, 1, 0);
    }

        // Update is called once per frame
    void FixedUpdate () {
        this.manageCommand();

        transform.Translate(simpleVector * speed * Time.deltaTime);
    }
}
