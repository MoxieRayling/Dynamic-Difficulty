using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public string target;
    public float vel;
    public Vector3 dir;

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
            col.gameObject.GetComponent<Enemy>().Hurt();
        }
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
