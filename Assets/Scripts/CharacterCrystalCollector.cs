using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCrystalCollector : MonoBehaviour
{
    public event Action CrystalCollected;

    private const string _crystalShardTag = "CrystalShard";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_crystalShardTag))
        {
            CrystalCollected?.Invoke();
            other.gameObject.SetActive(false);
        }
    }
}
