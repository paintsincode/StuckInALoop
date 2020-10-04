
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{

    public float moveSpeed;

    public GameObject CameraRig;
    Rigidbody2D rb;

    List<GameObject> inventory;

    Dictionary<string, bool> avoidedTriggers;

   

    
    public GameObject NotesPanel; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = new List<GameObject>();
        avoidedTriggers = new Dictionary<string, bool>();

        avoidedTriggers.Add("DormLights", false);

        NotesPanel = GameObject.Find("NotesPanel");

        NotesPanel.SetActive(false);

        GetComponent<Notes>().LoadNotes();
    }

    // Update is called once per frame
    void Update()
    {

        
        if (GetComponent<Notes>().isNotesFocused() == false)
        {
            Move();

            if (Input.GetKeyDown(KeyCode.E))
             {
            InteractableItem closest = GetClosestInteractable();
            if(closest != null)
            {
                closest.Interact();
            }
             }

       
            if (Input.GetKeyDown(KeyCode.N))
            {
                NotesPanel.SetActive(!NotesPanel.activeSelf);
            }
        }


       
        

    }

    private void Move()
    {
        // convert mouse position into world coordinates 
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // get direction you want to point at 
        Vector3 lookPos = (mouseScreenPosition - transform.position);

        // set vector of transform directly 
        transform.up = new Vector3(lookPos.x, lookPos.y, 0);

        float xx = Input.GetAxis("Horizontal") * moveSpeed;// * Time.deltaTime;
        float yy = Input.GetAxis("Vertical") * moveSpeed;// * Time.deltaTime;


        //transform.position = new Vector3(transform.position.x + xx, transform.position.y + yy, transform.position.z);

        rb.velocity = new Vector2(xx, yy);


        CameraRig.transform.position = transform.position + ((lookPos - transform.position) * 0.1f);
        CameraRig.transform.position = new Vector3(CameraRig.transform.position.x, CameraRig.transform.position.y, -10);
    }

    InteractableItem GetClosestInteractable()
    {
        InteractableItem[] items = FindObjectsOfType<InteractableItem>();

        float minDist = 100000;
        InteractableItem closestItem = null;
        foreach (InteractableItem ii in items)
        {

            float distToPlayer = Vector3.Distance(transform.position, ii.transform.position);
            if(distToPlayer < minDist)
            {
                minDist = distToPlayer;
                closestItem = ii;
            }
        }
        
        return closestItem;
    }

    public void AddItem(GameObject item)
    {
        inventory.Add(item);
    }

    public bool HasItem(string itemName)
    {
        bool itemInInventory = false;
        foreach (GameObject go in inventory){
            if(go.name == itemName)
            {
                itemInInventory = true;
                break;
            }
            else
            {
                itemInInventory = false;
            }

        }
        return itemInInventory;
    }

    public void SetAvoidedTrigger(string trigger, bool val)
    {
        if (avoidedTriggers.ContainsKey(trigger))
        {
            avoidedTriggers[trigger] = val;
        }
    }

    public bool HasAvoidedTrigger(string trigger)
    {
        
        return avoidedTriggers[trigger];
        
    }


}
