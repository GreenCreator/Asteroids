using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject scoreText;
    private int score = 0;

    public void increaseScore(int increment)
    {
        score += increment;
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score;
    }
}
