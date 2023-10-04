using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entered : MonoBehaviour
{
    public bool playerEntered = false;

    void Start()
    {
        playerEntered = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            playerEntered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            playerEntered = false;
        }
    }
}
