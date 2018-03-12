using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {
    //This method is triggered when the ball touches the LoseCollider game object
    //It loads the "Lose" scene (GameOver)
    void OnTriggerEnter2D(Collider2D collider)
    {
        //We need to instantiate a LevelManager object to call the LoadLevel method.
        LevelManager levelManager = new LevelManager();
        levelManager.LoadLevel("Lose");
    }
}
