using UnityEngine;

public class FinishLineController : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // Win the game tell the game manager 
            // to stop other entities and show final score
            print("Win");
        }
    }
}
