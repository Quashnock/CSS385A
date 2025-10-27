using UnityEngine;
using System.Collections.Generic;

[System.Serializable]

public class GameData
{
    public Vector3 playerPosition;
    public Vector3 cameraPosition;
    public SerializableDictionary<string, bool> enemiesDefeated;

    public GameData()
    {
        playerPosition = Vector3.zero;
        cameraPosition = Vector3.zero;
        enemiesDefeated = new SerializableDictionary<string, bool>();
    }
}
