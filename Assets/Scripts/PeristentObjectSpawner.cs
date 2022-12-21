using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PeristentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;

        static bool hasSpawned = false;
        void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistanceObjects();
            hasSpawned = true;
        }

        void SpawnPersistanceObjects()
        {
            GameObject instantiatedObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(instantiatedObject);
        }
    }

}

