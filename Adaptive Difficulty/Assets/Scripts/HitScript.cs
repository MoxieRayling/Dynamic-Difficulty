using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    public string target;

    void Start()
    {
        StartCoroutine(Timeout());
    }
    
    void Update()
    {

    }

    void OnTriggerEnter2D (Collider2D col) 
	{
        if (col.tag == target && target == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().Hurt();
            Destroy(gameObject);
        }
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
