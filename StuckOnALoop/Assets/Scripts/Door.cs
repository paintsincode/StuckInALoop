using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableItem
{
    public bool isLocked;
    public GameObject requiredItem;

    
    public override void Interact()
    {
        if (isInteractable)
        {
            if (isLocked)
            {
                if (PlayerChar.GetComponent<PlayerControl>().HasItem(requiredItem.name))
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
