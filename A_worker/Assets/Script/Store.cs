using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Store : MonoBehaviour
{
    public GameObject storeUI;

    public UnityAction act_StoreOpen;
    // Start is called before the first frame update
    void Start()
    {
        storeUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            act_StoreOpen.Invoke();
            storeUI.SetActive(true);
        }
    }

    public void StoreExitButton()
    {
        act_StoreOpen.Invoke();
        storeUI.SetActive(false);
    }
}
