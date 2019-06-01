using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int health = 3;

    public void Hurt()
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
