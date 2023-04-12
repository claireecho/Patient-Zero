using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSurgery : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip completeSound;
    public AudioClip trashSound;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (PlayerMovement.canTrashAffliction) {
                PlayerMovement.canTrashAffliction = false;
                Inventory.inventory[Inventory.selection].SetActive(false);
                GameObject[] newTools = new GameObject[Inventory.inventory.Length - 1];
                for (int i = 0; i < newTools.Length; i++) {
                    newTools[i] = Inventory.inventory[i];
                }
                Inventory.inventory = new GameObject[newTools.Length];
                Inventory.inventory = newTools;
                Inventory.selection = 0;
                Inventory.inventory[Inventory.selection].SetActive(true);
                surgery.step++;
                playSound(completeSound);

            }
        }
    }



    public void playSound(AudioClip sound) {
        audioSource.Stop();
        audioSource.clip = sound;
        audioSource.Play();
        audioSource.loop = false;
    }

}
