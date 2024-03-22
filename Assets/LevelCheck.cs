using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script dealing with all states influenced by the LevelCollider object attached to the player character

public class LevelCheck : MonoBehaviour
{

    // Initialises necessary states for enemy

    public static bool gotKeyOne = false;
    public static bool gotKeyTwo = false;
    public static bool gotKeyThree = false;
    public static bool doorOneOpen = false;
    public static bool doorTwoOpen = false;
    public static bool doorThreeOpen = false;
    public static bool atDoorOne = false;
    public static bool atDoorTwo = false;
    public static bool atDoorThree = false;
    public static bool atDoorOver = false;

    // Creates sprite objects to store door sprites
    public SpriteRenderer theSR;
    public SpriteRenderer theSR2;
    public SpriteRenderer theSR3;
    public Sprite unlockedDoor;
    public Sprite openDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buttonCheck();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Detects if a key is picked up
        if (collision.gameObject.tag == "KeyOne")
        {
            gotKeyOne = true;
        }
        if (collision.gameObject.tag == "KeyTwo")
        {
            gotKeyTwo = true;
        }
        if (collision.gameObject.tag == "KeyThree")
        {
            gotKeyThree = true;
        }

        // Unlocks doors and changes door sprites, checks whether player is at the door or not, and plays unlock audio if door is unlocked
        if (collision.gameObject.tag == "LevelTagOne" && gotKeyOne == true)
        {
            if (theSR.sprite != unlockedDoor)
            {
                SoundManagerScript.PlaySound("unlockSound");
            }
            doorOneOpen = true;
            theSR.sprite = unlockedDoor;
            atDoorOne = true;
        }
        if (collision.gameObject.tag == "LevelTagTwo" && gotKeyTwo == true)
        {
            if (theSR2.sprite != unlockedDoor)
            {
                SoundManagerScript.PlaySound("unlockSound");
            }
            doorTwoOpen = true;
            theSR2.sprite = unlockedDoor;
            atDoorTwo = true;
        }
        if (collision.gameObject.tag == "LevelTagThree" && gotKeyThree == true)
        {
            if (theSR3.sprite != unlockedDoor)
            {
                SoundManagerScript.PlaySound("unlockSound");
            }
            doorThreeOpen = true;
            theSR3.sprite = unlockedDoor;
            atDoorThree = true;
        }
        if (collision.gameObject.tag == "LevelTagOver")
        {
            atDoorOver = true;
        }

        // Checks if slime enemy is 'eating' the player
        if (collision.gameObject.tag == "Slime")
        {
            InvokeRepeating("SlimeDamage", 0f, 0.5f);
        }

        // Checks if player has picked up heart power-up, increases max health if true
        if (collision.gameObject.tag == "Heart")
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().IncreaseMaxHealth(150);
        }
    }

    // Lets player open and enter door using 'E', as long as the door is unlocked and they are at said door
    void buttonCheck()
    {
        if (doorOneOpen == true && atDoorOne == true && Input.GetKeyDown(KeyCode.E))
        {
            theSR.sprite = openDoor;
            atDoorOne = false;
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadLevel(2);
        }
        if (doorTwoOpen == true && atDoorTwo == true && Input.GetKeyDown(KeyCode.E))
        {
            theSR2.sprite = openDoor;
            atDoorTwo = false;
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadLevel(3);
        }
        if (doorThreeOpen == true && atDoorThree == true && Input.GetKeyDown(KeyCode.E))
        {
            theSR3.sprite = openDoor;
            atDoorThree = false;
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadLevel(4);
        }
        if (atDoorOver == true && Input.GetKeyDown(KeyCode.E))
        {
            theSR.sprite = openDoor;
            atDoorOver = false;
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadLevel(1);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LevelTagOne")
        {
            atDoorOne = false;
        }
        if (collision.gameObject.tag == "LevelTagTwo")
        {
            atDoorTwo = false;
        }
        if (collision.gameObject.tag == "LevelTagThree")
        {
            atDoorThree = false;
        }
        if (collision.gameObject.tag == "LevelTagOver")
        {
            atDoorOver = false;
        }
        if (collision.gameObject.tag == "Slime")
        {
            CancelInvoke();
        }
    }

    // Damage dealt by slime enemy
    void SlimeDamage()
    {
        GameObject.Find("Player").GetComponent<PlayerHealth>().PlayerDamage(5);
    }
}
