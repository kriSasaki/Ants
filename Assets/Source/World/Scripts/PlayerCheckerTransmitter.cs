using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckerTransmitter : MonoBehaviour
{
    [SerializeField] private PlayerChecker _playerChecker;

    public PlayerChecker PlayerChecker => _playerChecker;
}
