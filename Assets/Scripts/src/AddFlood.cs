using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFlood : MonoBehaviour
{
    public AudioClip cliAddFlood;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="decreaseFlood")
        {
            AudioSource.PlayClipAtPoint(cliAddFlood, other.transform.position);
            if(PlayerHealth.playerHealth==100)
            {
                PlayerHealth.redCount += 1;
            }
            else
            {
                PlayerHealth.playerHealth += 20;
                if (PlayerHealth.playerHealth > 100)
                {
                    PlayerHealth.playerHealth = 100;
                }
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
