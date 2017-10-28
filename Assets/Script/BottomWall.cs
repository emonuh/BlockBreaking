using UnityEngine;

public class BottomWall : MonoBehaviour {
    private GameController gc;

    void Start() {
        gc = FindObjectOfType<GameController>();
    }

    void OnCollisionEnter(Collision collision) {
        Destroy(collision.gameObject);
        gc.Fail();
    }
}
