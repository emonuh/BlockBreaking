using UnityEngine;

public class Player : MonoBehaviour {
    public float Accel;

    private GameController gc;
    private Rigidbody rb;

    void Start() {
        gc = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        rb.AddForce(transform.right * Input.GetAxisRaw("Horizontal") * Accel, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag != "Ball") {
            return;
        }
        gc.ResetCombo();
        ReflectBall(collision);
    }

    private void ReflectBall(Collision collision) {
        float hitPoint = collision.contacts[0].point.x - transform.position.x;
        float halfLength = transform.lossyScale.x / 2;
        float rate = hitPoint / halfLength;
        Ball ball = collision.gameObject.GetComponent<Ball>();
        ball.RotateVelocity(0, rate * 30, 0);
    }
}
