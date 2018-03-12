using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderStun : MonoBehaviour 
{
    Chara chara;
    public float spiderStunLength = 3f;

    private void Start()
    {
        chara = GameObject.FindGameObjectWithTag("Player").GetComponent<Chara>();
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
            //chara.callStun(spiderStunLength);
        }
    }
}
