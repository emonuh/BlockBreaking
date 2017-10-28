using UnityEngine;

public class Ball : MonoBehaviour {
    public Transform Player;
    public float InitSpeed;
    public float Acceleration;
    public float MaxSpeed;

    private Rigidbody rb;
    private float adjust = 5;
    private int collisionCount;
    private float speedLimit;

    void Start() {
        rb = GetComponent<Rigidbody>();
        speedLimit = InitSpeed;
        if (Player == null) {
            Player = FindObjectOfType<Player>().transform;
        }
        TracePlayer();
    }

    void Update() {
        if (rb.velocity == Vector3.zero) {
            TracePlayer();
            if (Input.GetButtonUp("Jump")) {
                rb.AddForce((transform.forward + transform.right) * InitSpeed, ForceMode.VelocityChange);
            }
        }
        if (rb.velocity.magnitude > speedLimit) {
            rb.velocity = rb.velocity.normalized * speedLimit;
        }
    }

    void OnCollisionEnter(Collision collision) {
        collisionCount++;
        if (speedLimit < MaxSpeed && collisionCount % 10 == 0) {
            speedLimit = (speedLimit + Acceleration) > MaxSpeed ? MaxSpeed : speedLimit + Acceleration;
            rb.velocity = rb.velocity.normalized * speedLimit;
        }

        if (Mathf.Abs(rb.velocity.z) < adjust) {
            float to = (Mathf.Abs(rb.velocity.z) < Mathf.Epsilon) ? 1 : rb.velocity.z;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, to * adjust);
        }

        if (Mathf.Abs(rb.velocity.x) < adjust) {
            float to = (Mathf.Abs(rb.velocity.x) < Mathf.Epsilon) ? 1 : rb.velocity.x;
            rb.velocity = new Vector3(to * adjust, rb.velocity.y, rb.velocity.z);
        }
    }

    public void RotateVelocity(float x, float y, float z) {
        rb.velocity = Quaternion.Euler(x, y, z) * rb.velocity;
    }

    private void TracePlayer() {
        Vector3 position = new Vector3(Player.position.x, Player.position.y, Player.position.z + 1.5f);
        transform.SetPositionAndRotation(position, Quaternion.identity);
    }
}
