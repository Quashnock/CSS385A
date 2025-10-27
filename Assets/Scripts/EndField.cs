using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndField : MonoBehaviour
{
    public GameObject gameEndUI;
    public Text scoreText;
    public Text endText;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameEndUI.SetActive(true);
            int score = ScoreManager.Instance.GetScore();
            scoreText.text = "Enemies Defeated: " + score.ToString();
            endText.text = "Zone End!";
            Time.timeScale = 0f;
        }
    } 
    public void RetryLevel()
    {
        // Clear save data to start fresh
        if (DataPersistanceManager.instance != null)
        {
            DataPersistanceManager.instance.ClearSaveData();
        }
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        // Clear save data before quitting
        if (DataPersistanceManager.instance != null)
        {
            DataPersistanceManager.instance.ClearSaveData();
        }
        
        Time.timeScale = 1f;
        Application.Quit();
    }
}
