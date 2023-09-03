using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class propUI : MonoBehaviour
{
    public GameObject textMa;
    public GameObject textMb;
    public GameObject textMc;
    public GameObject gun;
    public GameObject player;
    public bool haveGun = false;
    public AudioClip clipButton;
    public AudioClip clipSuccess;

    private void Update()
    {
        if(player.GetComponentInChildren<InteractiveWeapon>(true))
        {
            haveGun = true;
        }
    }
    public void backGameClick()
    {
        AudioSource.PlayClipAtPoint(clipButton, player.transform.position);
        gameObject.SetActive(false);
    }
    public void buyRedClick()
    {
        AudioSource.PlayClipAtPoint(clipButton, player.transform.position);
        if (PlayerHealth.coin>=15)
        {
            AudioSource.PlayClipAtPoint(clipSuccess, player.transform.position);
            PlayerHealth.redCount += 1;
            PlayerHealth.coin -= 15;
            StartCoroutine(successMessage());
        } 
        else
        {
            StartCoroutine(failMessage());
        }
    }
    public void buyBlueClick()
    {
        AudioSource.PlayClipAtPoint(clipButton, player.transform.position);
        if (PlayerHealth.coin >= 15)
        {
            AudioSource.PlayClipAtPoint(clipSuccess, player.transform.position);
            PlayerHealth.blueCount += 1;
            PlayerHealth.coin -= 15;
            StartCoroutine(successMessage());
        }
        else
        {
            StartCoroutine(failMessage());
        }
    }
    public void buyGunClick()
    {
        AudioSource.PlayClipAtPoint(clipButton, player.transform.position);
        if (PlayerHealth.coin >= 30)
        {
            AudioSource.PlayClipAtPoint(clipSuccess, player.transform.position);
            PlayerHealth.coin -= 30;
            gun.SetActive(true);
            StartCoroutine(successMessage());
        }
        else
        {
            StartCoroutine(failMessage());
        }
    }
    public void buyBulletClick()
    {
        AudioSource.PlayClipAtPoint(clipButton, player.transform.position);
        if (PlayerHealth.coin >= 10&&haveGun)
        {
            AudioSource.PlayClipAtPoint(clipSuccess, player.transform.position);
            player.GetComponentInChildren<InteractiveWeapon>().totalBullets += 60;
            PlayerHealth.coin -= 10;
            StartCoroutine(successMessage());
        }
        else
        {
            StartCoroutine(cantBuyMessage());
        }
    }

    IEnumerator failMessage()
    {
        textMa.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        textMa.SetActive(false);
    }
    IEnumerator successMessage()
    {
        textMb.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        textMb.SetActive(false);
    }
    IEnumerator cantBuyMessage()
    {
        textMc.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        textMc.SetActive(false);
    }
}
