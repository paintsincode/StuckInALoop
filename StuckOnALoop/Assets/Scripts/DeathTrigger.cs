using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    public GameObject PlayerChar;

    public string avoidableTrigger;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerChar = FindObjectOfType<PlayerControl>().gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if(collision.gameObject == PlayerChar)
        {
            if (PlayerChar.GetComponent<PlayerControl>().HasAvoidedTrigger(avoidableTrigger) == false)
            {
                SceneManager.LoadScene("TestLevel");
            }
        }
    }
}
