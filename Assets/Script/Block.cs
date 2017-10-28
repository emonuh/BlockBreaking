using UnityEngine;

public class Block : MonoBehaviour {
    public int score;

    protected GameController gc;

    void Start() {
        gc = FindObjectOfType<GameController>();
    }

    void OnCollisionEnter(Collision collision) {
        Destroy(gameObject);
        gc.UpdateScore(score);
        gc.DecrementBlockCount();
        if (gc.IsCleared()) {
            Destroy(collision.gameObject);
            gc.Clear();
        }
    }
}
