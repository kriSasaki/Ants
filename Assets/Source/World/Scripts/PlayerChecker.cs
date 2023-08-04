using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    [SerializeField] private Chunk[] _chunks;
    [SerializeField] private Mushroom[] mushrooms;

    private bool _isSpawned = false;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player) && _isSpawned==false)
        {
            

            foreach(Chunk chunk in _chunks) 
            {
                chunk.GetUp();
            }

            _isSpawned = true;
            //Destroy(gameObject);
        }
    }
}
