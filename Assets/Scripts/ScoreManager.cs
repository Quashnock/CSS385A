using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour, IDataPersistence
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
        return enemiesDefeated;
    }

    public void ResetScore()
    {
        enemiesDefeated = 0;
    }


    public void LoadData(GameData data)
    {
        // Reset the score before loading to avoid double counting
        enemiesDefeated = 0;
        
        // Count defeated enemies from save data
        foreach (KeyValuePair<string, bool> pair in data.enemiesDefeated)
        {
            if (pair.Value)
            {
                enemiesDefeated++;
            }
        }
    }

    public void SaveData(ref GameData data)
    {

    }
}