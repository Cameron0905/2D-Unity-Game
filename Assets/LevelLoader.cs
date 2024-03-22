using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Creates load screen and switch scenes

public class LevelLoader : MonoBehaviour
{

    private Animator animator;
    private string currentAnim;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ChangeAnimationState(string newAnim)
    {
        if (currentAnim == newAnim) return;
        animator.Play(newAnim);
        currentAnim = newAnim;
    }

    // Creates coroutine to ensure loading screen transition
    public void LoadLevel(int index)
    {
        SoundManagerScript.PlaySound("doorSound");
        ChangeAnimationState("TransitionStart");
        StartCoroutine(LoadDelay(index));
    }

    // Loads desired scene, if scene is main menu, resets appropiate player states and overworld spawner
    IEnumerator LoadDelay(int index)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(index);
        if (index == 0)
        {
            OverworldSpawner.hasDied = false;
            LevelCheck.gotKeyOne = false;
            LevelCheck.gotKeyTwo = false;
            LevelCheck.gotKeyThree = false;
            LevelCheck.doorOneOpen = false;
            LevelCheck.doorTwoOpen = false;
            LevelCheck.doorThreeOpen = false;
            PlayerHealth.maxHealth = 100;
        }
    }
}
