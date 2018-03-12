using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class will help us control the behavior of several types of bricks
public class Brick : MonoBehaviour {
    //Public properties
    public AudioClip crack;
    public Sprite[] hitSprites;
    public static int breakableCount = 0;
    public GameObject smoke;

    //Private properties
    private int timesHit;
    private LevelManager levelManager;
    private bool isBreakable;

	// Use this for initialization
	void Start () {
        //We identify if the brick is breakable or not (using tags) and set the
        //isBreakable flag accordingly
        isBreakable = (this.tag == "breakable");

        //We keep track of how many breakable bricks we have created, increasing
        //the statc property breakableCount.

        if (isBreakable)
        {
            breakableCount++;
        }

        //We set the number of times the brick has been hit to 0
        timesHit = 0;
        //We link the LevelManager object to this script
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    //we use the OnCollisionEnter2D message to play the cracking sound when the ball
    //hits the brick, and to decide if we are going to update the brick's sprite
    void OnCollisionEnter2D (Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(crack, transform.position, 0.8f);
        //If the brick is breakable one, we call the HabdleHits() method
        if (isBreakable)
        {
            HandleHits();
        }
    }

    void HandleHits()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if(timesHit >= maxHits)
        {
            breakableCount--;
            levelManager.BrickDestroyed();
            PuffSmoke();
            Destroy(gameObject);
        } else
        {
            LoadSprites();
        }
    }

    void PuffSmoke()
    {
        GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity);
        //smokePuff.GetComponent<ParticleSystem>().main.startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    void LoadSprites()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex]!= null)
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        } else
        {
            Debug.LogError("Brick sprite missing");
        }
    }
}
