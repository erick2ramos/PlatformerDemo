using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public GameObject[] pathSlices;
    public GameObject startPath;
    public GameObject finalPathPrefab;
    public int maxSlices;

    List<GameObject> selectedPaths;
    GameObject holder;

    private void Start()
    {
        holder = new GameObject("Level");
        GenerateLevel();
    }

    // Generate a random level
    public void GenerateLevel()
    {
        Transform snapPoint = startPath.GetComponent<PlatformController>().joinPoint;
        for (int i = 0; i < maxSlices; i++)
        {
            GameObject go = Instantiate(pathSlices[Random.Range(0, pathSlices.Length)], holder.transform, false);
            go.transform.position = snapPoint.position;
            snapPoint = go.GetComponent<PlatformController>().joinPoint;
        }

        GameObject final = Instantiate(finalPathPrefab, holder.transform, false);
        final.transform.position = snapPoint.position;
        snapPoint = null;
    }

    // Restart the level
    public void Reset()
    {
        
    }
}
