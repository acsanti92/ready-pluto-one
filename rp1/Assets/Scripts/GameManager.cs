using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public Text scoreText;
    // Current score
    private int score;

    // Start is called before the first frame update
    private void Start()
    {
        NewGame();
    }

    // NewGame will reset the game to its starting conditions
    public void NewGame()
    {
        // Reset the score to zero
        score = 0;
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        // Update the score text
        scoreText.text = score.ToString();
    }
}
