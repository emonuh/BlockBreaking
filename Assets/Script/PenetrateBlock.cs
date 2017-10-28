using UnityEngine;

public class PenetrateBlock : Block {

    private Collider col;

    void Start() {
        gc = FindObjectOfType<GameController>();
        col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
        gc.UpdateScore(score);
        gc.DecrementBlockCount();
        if (gc.IsCleared()) {
            Destroy(other.gameObject);
            gc.Clear();
        }
    }
}
