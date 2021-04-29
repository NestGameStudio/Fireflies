using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "Player") {

            other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            other.gameObject.GetComponent<CollisionCheck>().InstaDeath();
            //LevelManagerRL.Instance.ChooseNewMap();
            //LevelManager.Instance.nextLevel();
            //nextLevel.PlayOneShot(nextLevel.clip,nextLevel.volume);
            //nextLevel.Play();
        }
    }

}
