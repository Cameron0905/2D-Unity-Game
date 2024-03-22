using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    // Initialises all required audio clips

    public static AudioClip walkSound;
    public static AudioClip jumpSound;
    public static AudioClip attackSound;
    public static AudioClip deathSound;
    public static AudioClip hitSound;
    public static AudioClip enemySound;
    public static AudioClip doorSound;
    public static AudioClip unlockSound;
    public static AudioClip itemSound;
    public static AudioClip ghostSound;
    public static AudioClip slimeAttackSound;
    public static AudioClip slimeDeathSound;
    public static AudioClip endSound;

    static AudioSource audioSrc;

    // Start is called before the first frame update, sets clip variables to desired .wav files in resources folder
    void Start()
    {
        
        walkSound = Resources.Load<AudioClip>("Footstep_Gravel_4");
        jumpSound = Resources.Load<AudioClip>("Jump3");
        attackSound = Resources.Load<AudioClip>("HitsAccept2");
        deathSound = Resources.Load<AudioClip>("Explosion_04");
        hitSound = Resources.Load<AudioClip>("Julie_Jump_3");
        enemySound = Resources.Load<AudioClip>("Ed_Attack_1");
        doorSound = Resources.Load<AudioClip>("DistClickBlocked1");
        unlockSound = Resources.Load<AudioClip>("PickUp_1");
        itemSound = Resources.Load<AudioClip>("Coin_Pick_Up_03");
        ghostSound = Resources.Load<AudioClip>("Zombie_01");
        slimeAttackSound = Resources.Load<AudioClip>("Monster_01");
        slimeDeathSound = Resources.Load<AudioClip>("Monster_00");
        endSound = Resources.Load<AudioClip>("levelComplete8Bit");

        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Involves a switch statement which plays the appropiate audio clip
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            // SoundManagerScript.PlaySound(walkSound)
            case "walkSound":
                audioSrc.volume = 0.05f;
                audioSrc.PlayOneShot(walkSound);
                break;
            case "jumpSound":
                audioSrc.volume = 0.05f;
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "attackSound":
                audioSrc.volume = 0.05f;
                audioSrc.PlayOneShot(attackSound);
                break;
            case "deathSound":
                audioSrc.volume = 0.2f;
                audioSrc.PlayOneShot(deathSound);
                break;
            case "hitSound":
                audioSrc.volume = 0.05f;
                audioSrc.PlayOneShot(hitSound);
                break;
            case "enemySound":
                audioSrc.volume = 0.05f;
                audioSrc.PlayOneShot(enemySound);
                break;
            case "doorSound":
                audioSrc.volume = 0.1f;
                audioSrc.PlayOneShot(doorSound);
                break;
            case "unlockSound":
                audioSrc.volume = 0.05f;
                audioSrc.PlayOneShot(unlockSound);
                break;
            case "itemSound":
                audioSrc.volume = 0.1f;
                audioSrc.PlayOneShot(itemSound);
                break;
            case "ghostSound":
                audioSrc.volume = 0.01f;
                audioSrc.PlayOneShot(ghostSound);
                break;
            case "slimeAttackSound":
                audioSrc.volume = 0.01f;
                audioSrc.PlayOneShot(slimeAttackSound);
                break;
            case "slimeDeathSound":
                audioSrc.volume = 0.01f;
                audioSrc.PlayOneShot(slimeDeathSound);
                break;
            case "endSound":
                audioSrc.volume = 0.01f;
                audioSrc.PlayOneShot(endSound);
                break;
        }
    }
}
