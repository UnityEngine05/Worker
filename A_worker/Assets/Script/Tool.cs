using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public CircleCollider2D circleCollider2D;

    public float toolDamage;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToolUse()
    {
        circleCollider2D.enabled = true;
        StartCoroutine(ToolColliderEnabledFalse());
    }

    IEnumerator ToolColliderEnabledFalse()
    {
        yield return new WaitForSeconds(0.1f);

        circleCollider2D.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Minerals")
        {
            Minerals minerals = collision.gameObject.GetComponent<Minerals>();

            minerals.MineralsAttack(toolDamage);
        }
    }
}
