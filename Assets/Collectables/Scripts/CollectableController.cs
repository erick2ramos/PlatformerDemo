using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour {
    public int scoreValue;

    private void Start()
    {
        // Should init scoreValue from settings file
    }

    public void Collect()
    {
        // other functionality instead of just destroying
        // maybe animation, sound, and hide
        Destroy(gameObject);
    }
}
