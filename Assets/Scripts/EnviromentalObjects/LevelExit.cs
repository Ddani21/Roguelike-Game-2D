using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelExit : MonoBehaviour {
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player") {
            ScoreManager.instance.IncrementScore(1000);
            StartCoroutine(LevelManager.instance.LevelEnd());
            
           
        }

    }

}
