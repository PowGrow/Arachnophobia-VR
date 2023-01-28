using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;

    public List<Transform> Points
    {
        get { return spawnPoints; }
    }
}
