using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Transform Ball;
    public Transform NormalBlock;
    public Transform HardBlock;
    public Transform PenetrateBlock;
    public Text ScoreLabel;
    public Text LifeLabel;
    public Text ResultScoreLabel;
    public GameObject ResultPanel;
    public int life;
    public int blockRow;
    public int blockColmun;

    private int blockCount;
    private int totalScore;
    private int comboCount;

    void Start() {
        ResultPanel.SetActive(false);
        DecrementLifeLabel();
        DeployBlocks();
        blockCount = blockRow * blockColmun;
    }

    private void DeployBlocks() {
        float fieldX = 39;
        float spaceLength = 0.5f;
        float spacesLength = spaceLength * (blockColmun + 1);
        float blockScaleX = (fieldX - spacesLength) / blockColmun;
        float z = 14f;
        for (int i = 0; i < blockRow; i++) {
            float x = (fieldX / 2) - (blockScaleX / 2) - spaceLength;
            for (int j = 0; j < blockColmun; j++) {
                Transform block = GetBlock();
                block.localScale = new Vector3(blockScaleX, 1, 1);
                block.position = new Vector3(x, 0, z);
                x -= blockScaleX + spaceLength;
            }
            z -= 2f;
        }
    }

    private Transform GetBlock() {
        switch ((int)(Random.Range(0.1f, 0.4f) * 10)) {
            case 1:
                return Instantiate(HardBlock);
            case 2:
                return Instantiate(PenetrateBlock);
            default:
                return Instantiate(NormalBlock);
        }
    }

    public void ResetCombo() {
        comboCount = 0;
    }

    public void UpdateScore(int score) {
        int combodScore = (int)(score * (1 + comboCount * 0.1));
        totalScore += combodScore;
        ScoreLabel.text = totalScore.ToString();
        comboCount++;
    }

    public void DecrementLifeLabel() {
        life--;
        PrintLife();
    }

    public void DecrementBlockCount() {
        blockCount--;
    }

    public bool IsCleared() {
        return blockCount == 0;
    }

    public void Clear() {
        ShowResult();
    }

    public void Fail() {
        if (life > 0) {
            Instantiate(Ball);
            DecrementLifeLabel();
            return;
        }
        ShowResult();
    }

    public void ShowResult() {
        ResultScoreLabel.text = totalScore.ToString();
        ResultPanel.SetActive(true);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PrintLife() {
        LifeLabel.text = "あと " + life.ToString() + " かい";
    }
}
