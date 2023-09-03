using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addKeep : MonoBehaviour
{
    public AudioClip cliAddKeep;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "decreaseFlood")
        {
            AudioSource.PlayClipAtPoint(cliAddKeep, other.transform.position);
            if(PlayerHealth.runTime>=4.5f)
            {
                PlayerHealth.blueCount += 1;
            }
            else
            {
                PlayerHealth.runTime += 4;
            }
            StartCoroutine(disappearIE());
        }

    }
    IEnumerator disappearIE()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
