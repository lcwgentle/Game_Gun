using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class mushroom : MonoBehaviour
{
    public GameObject mushUI;
    private float timer = 5;
    public GameObject timerText;
    public GameObject successText;
    public GameObject blueTh;
    public GameObject redTh;
    public bool IsBlue;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            mushUI.SetActive(true);    
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            mushUI.SetActive(false);
            timer = 5;
        }
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {
            timer -= Time.deltaTime;
            timerText.GetComponent<Text>().text = ((int)timer+1).ToString();
            if(timer<=0)
            {
                gameObject.SetActive(false);
                mushUI.SetActive(false);
               if(IsBlue)
                {
                    blueTh.SetActive(true);
                  //  transform.GetComponent<addKeep>().gameObject.SetActive(true);
                }
               else
                {
                    redTh.SetActive(true);
                  //  transform.GetComponent<AddFlood>().gameObject.SetActive(true);
                }
            }
        }
    }
    private void Update()
    {
        //if(isStay)
        //{
        //   // print(mu.IsCatch());
        //    Cursor.lockState = CursorLockMode.None;//鼠标解锁并显示
        //    if (mu.IsCatch())
        //    {
        //        mushUI.SetActive(false);
        //        timer -= Time.deltaTime;
        //        timeText.SetActive(true);
        //        timeText.GetComponentInChildren<Text>().text = ((int)timer + 1).ToString();
        //        if (timer <= 0)
        //        {
        //            timeText.SetActive(false);
        //            gameObject.SetActive(false);
        //        }
        //    }
        //}
    }
   
}
