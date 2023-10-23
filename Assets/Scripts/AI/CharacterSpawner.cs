using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering.Universal;

public class CharacterSpawner : MonoBehaviour
{
    void Awake()
    {
        bool spawnWoman = true;
        string path = "Prefabs/Characters/";
        List<string> men = GetFilenames(path + "Men/");
        List<string> women = GetFilenames(path + "Women/");

        foreach (Transform agent in transform)
        {
            List<string> listToUse = spawnWoman ? women : men;
            int index = Random.Range(0, listToUse.Count);
            var split = listToUse[index].Split('/');
            string nameToUse = split[split.Length - 1].Split(".")[0];
            listToUse.RemoveAt(index);

            string fullPath = path + (spawnWoman ? "Women/" : "Men/") + nameToUse;
            var obj = Resources.Load(fullPath);
            Instantiate(obj, agent);

            spawnWoman = !spawnWoman;
        }
    }

    List<string> GetFilenames(string path)
    {
        string fullPath = Application.dataPath + "/Resources/" + path;
        return new List<string>(Directory.GetFiles(fullPath, "*.prefab"));
    }
}
