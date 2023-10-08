using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public CircleCollider2D circleCollider2D;

    public float toolDamage;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider2D.enabled = false;
        ToolPosReSetVector3();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToolUse()
    {
        StartCoroutine(ToolMoveAnimation());
    }

    IEnumerator ToolMoveAnimation()
    {
        ToolPosVector3(-0.075f, 0, 0);
        yield return new WaitForSeconds(1f);
        ToolPosVector3(0.025f, 0.15f, 0);
        yield return new WaitForSeconds(1f);
        ToolPosVector3(0, 0.45f, 0);
        yield return new WaitForSeconds(1f);
        ToolPosVector3(-0.25f, 0.5f, 0);
        yield return new WaitForSeconds(1f);
        ToolPosVector3(0.2f, 0.35f, 0);
        yield return new WaitForSeconds(1f);
        ToolPosVector3(0.18f, -0.095f, 0);
        transform.rotation = Quaternion.Euler(0, 0, -45);
        circleCollider2D.offset = new Vector2(0.25f, 0.25f);
        circleCollider2D.enabled = true;
        yield return new WaitForSeconds(1f);
        circleCollider2D.enabled = false;
        ToolPosReSetVector3();
    }

    void ToolPosVector3(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }

    void ToolPosReSetVector3()
    {
        ToolPosVector3(-0.2f, 0.025f, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        circleCollider2D.offset = new Vector2(0, 0);
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
