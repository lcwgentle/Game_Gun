using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class trollManagers : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    private NavMeshAgent nam;
    bool move = false;
    public AudioClip playerHit;
    void Start()
    {
        anim = GetComponent<Animator>();
        nam = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        Move();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="playerMove")
        {
            move = true;
        } 
        if(other.tag=="decreaseFlood")
        {
            PlayerHealth.playerHealth -= 5;
            AudioSource.PlayClipAtPoint(playerHit,player.transform.position);

           // print(PlayerHealth.playerHealth);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //if(tr.IsDead)
        //{
        //    anim.SetFloat("death", 1.0F);
        //}
        if (other.tag == "PlayerAttract")
        {
            move = false;
            anim.SetFloat("clawAttack", 1.0F);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerAttract")
        {
            move = true;
            anim.SetFloat("clawAttack", -1F);
        }
    }
    void Move()
    {
        if(move==true)
        {
            //StartCoroutine(enumerator());

            transform.LookAt(player.transform.position);
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
            //transform.position += transform.forward * Time.deltaTime * speed;
            nam.SetDestination(player.transform.position);
            anim.SetFloat("run", 0.0F);
            anim.SetFloat("idle", 0F);
            anim.SetFloat("walk", 1.0F);
            // print(transform.position);
        }
        
    }
    public void animationToWalk()
    {
        anim.SetFloat("run", 0.0F);
        anim.SetFloat("idle", 0F);
        anim.SetFloat("walk", 1.0F);
    }
    public void animationToIdle()
    {
        anim.SetFloat("idle", 1F);
        anim.SetFloat("walk", 0.0F);
        anim.SetFloat("run", 0F);
    }
    //IEnumerator enumerator()
    //{
    //    transform.LookAt(player.transform.position);
    //    transform.Translate(Vector3.forward * speed * Time.deltaTime);
    //    yield return 0;
    //}
}
