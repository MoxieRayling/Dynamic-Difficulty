using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public string target;
    public float vel;
    public Vector3 dir;
    private static int playerHits = 0;

    void Start()
    {
        StartCoroutine(Timeout());
    }

    void Update()
    {
        transform.position = transform.position + dir.normalized * vel * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == target && target == "Enemy")
        {
            col.gameObject.GetComponent<Shooter>().Hurt();
            Destroy(gameObject);
        }
        if (col.tag == target && target == "Player")
        {
            Debug.Log(++playerHits);
            Destroy(gameObject);
        }
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
