using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    public Transform SpawnPoint=> _spawnPoint;
}
