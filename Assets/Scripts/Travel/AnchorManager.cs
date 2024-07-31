using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject upstairs;
    [SerializeField] private GameObject downstairs;


    // Start is called before the first frame update
    void Start()
    {
        upstairs.SetActive(true);
        downstairs.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            upstairs.SetActive(true);
            downstairs.SetActive(false);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            upstairs.SetActive(false);
            downstairs.SetActive(true);
        }
        
    }
}
