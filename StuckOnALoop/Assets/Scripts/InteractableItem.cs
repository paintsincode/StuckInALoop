using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public GameObject PlayerChar;
    public GameObject ContainsItem;
    public GameObject TriggerObject;

    public string AvoidedTrigger;


    public bool isInteractable;
    // Start is called before the first frame update
    void Start()
    {
        PlayerChar = FindObjectOfType<PlayerControl>().gameObject;
        isInteractable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, PlayerChar.transform.position) < 1)
        {
            isInteractable = true;
            GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0, 0);
        }
        else
        {
            isInteractable = false;
            GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 1);
        }
    }

    public virtual void Interact()
    {
        if (isInteractable)
        {
            if (ContainsItem != null) //the interactable is a container that has an item (a drawer, a box, something like that)
            {
                PlayerChar.GetComponent<PlayerControl>().AddItem(ContainsItem);
              
            }

            if (TriggerObject != null)//the interactable is a trigger for another item in the world( a switch )
            {
                TriggerObject.SetActive(true);
            }

            if (AvoidedTrigger != null)
            {
                PlayerChar.GetComponent<PlayerControl>().SetAvoidedTrigger(AvoidedTrigger, true);
            }

            gameObject.SetActive(false);
        }
    }


}
