using UnityEngine;

public class FinishLineController : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // Wins the game, call the game manager 
            // to stop other entities and show final score
            GetComponent<BoxCollider>().enabled = false;
            MainManager.Get.gameManager.Win();
            // Maybe play somekind of victory animation
        }
    }
}
