using UnityEngine;

public class CollectableController : MonoBehaviour {
    public int scoreValue;

    private void Start()
    {
        // Should init scoreValue from settings file
        scoreValue = MainManager.Get.settings.data.collectableScoreValue;
    }

    private void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(20 * Time.deltaTime, Vector3.up); 
    }

    public void Collect()
    {
        // other functionality instead of just destroying
        // maybe animation, sound, and hide
        Destroy(gameObject);
    }
}
