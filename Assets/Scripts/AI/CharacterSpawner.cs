using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private List<GameObject> menPrefabs = new List<GameObject>();

    [SerializeField]
    private List<GameObject> womenPrefabs = new List<GameObject>();

    private void Awake()
    {
        if (menPrefabs.Count == 0 || womenPrefabs.Count == 0)
        {
            Debug.LogError("Please assign Men and Women prefabs in the inspector.");
            return;
        }

        bool spawnWoman = true;

        foreach (Transform agent in transform)
        {
            List<GameObject> listToUse = spawnWoman ? womenPrefabs : menPrefabs;
            int index = Random.Range(0, listToUse.Count);
            GameObject prefabToUse = listToUse[index];

            if (prefabToUse != null)
            {
                Instantiate(prefabToUse, agent);
                listToUse.RemoveAt(index);
            }
            else
            {
                Debug.LogError("Prefab not found for " + (spawnWoman ? "Women" : "Men"));
            }

            spawnWoman = !spawnWoman;
        }
    }
}