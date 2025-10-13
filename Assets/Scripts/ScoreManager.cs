using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [HideInInspector] public int enemiesDefeated = 0;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoint()
    {
        enemiesDefeated++;
    }

    public int GetScore()
    {
        int score = enemiesDefeated;
        enemiesDefeated = 0;

        return score;
    }
}