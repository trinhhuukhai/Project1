using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBrick : Singleton<UnBrick>
{
    public GameObject brick;

    private bool isCollect = false;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollect)
        {
            isCollect = true;
            brick.SetActive(true);
            other.GetComponent<Player>().RemoveBrick();
        }
    }
}
