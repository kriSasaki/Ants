using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransmitter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Inventory _inventory;

    public Player Player => _player;
    public Inventory Inventory => _inventory;
}
