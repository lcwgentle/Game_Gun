using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static float runTime =5;
    public static float playerHealth=100;
    public Slider flood;
    public Slider Keep;
    public static float coin = 55;
    public static int redCount = 0;
    public static int blueCount = 0;
    public GameObject gameoverUI;
    public GameObject daojuUI;
    public AudioClip buttonClip;
    public GameObject ga;
    private void Update()
    {
        flood.value = playerHealth;
        flood.GetComponentInChildren<Text>().text = playerHealth.ToString();
        Keep.value = runTime;
        Keep.GetComponentInChildren<Text>().text = ((int)(runTime*20)).ToString();
        if(playerHealth<=0)
        {
            playerHealth = 0;
            gameoverUI.SetActive(true);
            //StartCoroutine(cilpCour());
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            runTime -= Time.deltaTime;
            if(runTime<=0)
            {
                runTime = 0;
                GetComponent<MoveBehaviour>().sprintSpeed = 0;
            }
            else
            {
                GetComponent<MoveBehaviour>().sprintSpeed = 2;
            }
        }
        if(runTime>=5)
        {
            runTime = 5;
        }
        runTime += Time.deltaTime*0.2f;

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioSource.PlayClipAtPoint(buttonClip,transform.position);
            if (daojuUI.activeInHierarchy)
            {
                daojuUI.SetActive(false);
            }
            else
            {
                daojuUI.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioSource.PlayClipAtPoint(buttonClip,transform.position);
            if (ga.activeInHierarchy)
            {
                ga.SetActive(false);
            }
            else
            {
                ga.SetActive(true);
            }
        }
    }
}
