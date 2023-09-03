using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject effect;
    bool moveDoora = false;
    public float speed = 2;
    public float distance;
    public AudioClip doorClip;
    private int count = 0;
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            count++;
            moveDoora = true;
            if(count==1)
            {
                AudioSource.PlayClipAtPoint(doorClip, door.transform.position, 1.5f);
            }
            
            effect.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if(moveDoora)
        {
            float move = Mathf.Lerp(0, distance, speed * Time.deltaTime);
            door.transform.position -= new Vector3(0, 0, move);
            distance -= move;
            if(Mathf.Abs(distance)<0.05f)
            {
                moveDoora = false;
            }
        }
        
    }
}
