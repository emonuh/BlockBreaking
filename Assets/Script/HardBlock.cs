using UnityEngine;

public class HardBlock : Block {

    private int HP = 2;

    void OnCollisionEnter(Collision collision) {
        HP--;
        if (HP == 0) {
            Destroy(gameObject);
            gc.UpdateScore(score);
            gc.DecrementBlockCount();
            if (gc.IsCleared()) {
                Destroy(collision.gameObject);
                gc.Clear();
            }
        }
    }
}
