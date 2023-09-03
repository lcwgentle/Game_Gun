using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopUIshow : MonoBehaviour
{
    public GameObject player;
    public AudioClip buttonClip;
    public GameObject shopUI;
    public bool isCuesor = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            AudioSource.PlayClipAtPoint(buttonClip, player.transform.position);
            isCuesor = true;
            shopUI.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {     
          
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isCuesor = false;
            shopUI.SetActive(false);
        }
    }
    private void Update()
    {
        if (shopUI.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;//鼠标解锁并显示
        }
    }
}
