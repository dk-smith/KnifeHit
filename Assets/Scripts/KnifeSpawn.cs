using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawn : MonoBehaviour
{

    [SerializeField] private GameObject knife;
    private GameObject current;
    private int count = 0;

    private void OnEnable()
    {
        if (current == null) Spawn();   
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && current != null)
        {
            Throw();
        }
    }

    private void Throw()
    {
        current.GetComponent<KnifeController>().Throw();
        current = null;
    }

    public void Spawn()
    {
        current = Instantiate(knife, transform.position, Quaternion.identity, transform);
        current.name = "Knife_" + count++.ToString();
    }

}
