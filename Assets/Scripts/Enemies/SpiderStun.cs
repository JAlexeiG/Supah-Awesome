using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderStun : MonoBehaviour {
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator Stun()
    {
        player.GetComponent<Chara>().enabled = false;
        //player.GetComponent<Animator>().enabled = false; //dont need this just wanted a visual
        Debug.Log("start stun");
        yield return new WaitForSeconds(3);
        player.GetComponent<Chara>().enabled = true;
        //player.GetComponent<Animator>().enabled = true;
        Debug.Log("end stun");
    }
    //slow isnt needed anymore but i kept it in here in case we need it for something else
    /*IEnumerator Slow()
    {
        float playerSpeed = player.GetComponent<SimpleCharacterControl>().m_moveSpeed;
        float startSpeed = player.GetComponent<SimpleCharacterControl>().m_moveSpeed;
        playerSpeed *= 0.5f;
        yield return new WaitForSeconds(3);
        playerSpeed = startSpeed;
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("Stun");
            Debug.Log("Stun");
        }
    }
}
