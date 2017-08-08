using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public GameObject[] pathSlices;
    public GameObject startPath;
    public GameObject finalPathPrefab;
    public int maxSlices;

    Queue<GameObject> selectedPaths;
    GameObject holder;

    public void Init()
    {
        selectedPaths = new Queue<GameObject>();
        holder = new GameObject("Level");
    }

    // Generate a random level
    public void GenerateLevel()
    {
        if (selectedPaths == null)
        {
            selectedPaths = new Queue<GameObject>();
        }

        Transform snapPoint = startPath.GetComponent<PlatformController>().joinPoint;
        for (int i = 0; i < maxSlices; i++)
        {
            GameObject go = Instantiate(pathSlices[Random.Range(0, pathSlices.Length)], holder.transform, false);
            go.transform.position = snapPoint.position;
            go.GetComponent<PlatformController>().SpawnObjectsOnPois();
            selectedPaths.Enqueue(go);
            snapPoint = go.GetComponent<PlatformController>().joinPoint;
        }

        GameObject final = Instantiate(finalPathPrefab, holder.transform, false);
        final.transform.position = snapPoint.position;
        selectedPaths.Enqueue(final);
        snapPoint = null;
    }

    // Destroys all created path slices
    public void UnloadLevel()
    {
        while(selectedPaths.Count > 0)
        {
            Destroy(selectedPaths.Dequeue());
        }
    }
}
