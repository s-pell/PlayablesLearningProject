using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "LevelPartsData", menuName = "Configs/LevelPartsData")]
public class LevelTilesConfig : ScriptableObject
{
    public int LevelIndex;
    public float CheckDeltaTime;
    public float SpawnDepth = -2f;
    public float Duration = 1f;
    public float TileSideSize;
    [Range(0,100)]
    public int ShowNextOffset;
}

[System.Serializable]
public struct LevelPart
{
    public LevelPart(int level)
    {
        name = level.ToString();
        index = Vector2Int.zero;
    }
    public string name;
    public Vector2Int index;
}
