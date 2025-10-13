using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndField : MonoBehaviour
{
    public GameObject gameEndUI;
    public Text scoreText;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameEndUI.SetActive(true);
            int score = ScoreManager.Instance.GetScore();
            scoreText.text = "Enemies Defeated: " + score.ToString();
            Time.timeScale = 0f;
        }
    } 
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
