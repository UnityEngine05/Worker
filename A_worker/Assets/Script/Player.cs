using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Player : MonoBehaviour
{
    public Tool tool;
    public Animator animator;
    public GameObject inventoryGUI, fireObject;
    public SpriteRenderer spriteRenderer;

    public float speed, toolUseCoolMaxTime, toolUseCoolTime;
    public bool moveStopPlayer, fireObjectBool;
    public int fireNum;
    // Start is called before the first frame update
    void Start()
    {
        inventoryGUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        toolUseCoolTime -= Time.deltaTime;

        Move();
        Digging();
        InventoryGUI();
        FireObject();
    }

    public void PlayerActionStop(bool playerAction)
    {
        animator.SetBool("Walk", false);
        moveStopPlayer = playerAction;
    }

    void Move()
    {
        if (moveStopPlayer) return;

        float _x = Input.GetAxis("Horizontal");
        float _y = Input.GetAxis("Vertical");

        transform.position += new Vector3(_x, _y, 0) * Time.deltaTime * speed;
        if(_x != 0 || _y != 0)
            animator.SetBool("Walk", true);
        else
            animator.SetBool("Walk", false);
        if (_x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (_x > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    void Digging()
    {
        if (moveStopPlayer) return;

        if (Input.GetButtonDown("Fire1"))
        {
            if(toolUseCoolTime <= 0)
            {
                StartCoroutine(MoveStopPlayerCoroutine());
                animator.SetTrigger("Digging");
                toolUseCoolTime = toolUseCoolMaxTime;
            }
        }
    }

    void InventoryGUI()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (!inventoryGUI.active && !moveStopPlayer)
            {
                inventoryGUI.SetActive(true);
                PlayerActionStop(true);
            }
            else
            {
                inventoryGUI.SetActive(false);
                moveStopPlayer = false;
            }
        }
    }

    void FireObject()
    {
        if (moveStopPlayer) return;

        if (Input.GetButtonDown("Fire3") && fireNum > 0 && !fireObjectBool)
        {
            fireNum--;
            GameObject fireObj = Instantiate(fireObject);
            fireObj.transform.position = transform.position;
        }
    }

    IEnumerator MoveStopPlayerCoroutine()
    {
        PlayerActionStop(true);
        yield return new WaitForSeconds(0.5f);
        tool.ToolUse();
        yield return new WaitForSeconds(0.25f);

        if(!inventoryGUI.active)
            moveStopPlayer = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Hole"))
        {
            Vector3 holePos = collision.transform.position;
            StartCoroutine(EnterHole(holePos));
        }

        if (collision.gameObject.CompareTag("Fire"))
        {
            fireObjectBool = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            fireObjectBool = false;
        }
    }

    IEnumerator EnterHole(Vector3 holePos)
    {
        if (moveStopPlayer)
            yield break;

        animator.SetTrigger("EnterHole");
        PlayerActionStop(true);
        float times = 0;
        float OnTime = 0.5f;
        Vector3 OrgPos = transform.position;
        while (times < OnTime)
        {
            times += Time.deltaTime;
            
            transform.position = Vector3.Lerp(OrgPos, OrgPos + new Vector3(0, 0.75f, 0), times/OnTime);
            yield return new WaitForEndOfFrame();
        }
        times = 0;
        while (times < OnTime)
        {
            times += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, holePos, times / OnTime);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, times / OnTime);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.6f);
    }
}
