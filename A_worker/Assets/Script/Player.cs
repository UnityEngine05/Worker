using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Tool tool;
    public Animator animator;
    public GameObject inventoryGUI;
    public Rigidbody2D rigidbody2D;

    public float speed, toolUseCoolMaxTime, toolUseCoolTime;
    public bool moveStopPlayer;
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
    }

    public void PlayerAction(bool playerAction)
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

        if (Input.GetButton("Fire3"))
        {

        }
    }

    void InventoryGUI()
    {
        if (Input.GetButtonDown("Fire2") && !moveStopPlayer)
        {
            if (!inventoryGUI.active)
            {
                inventoryGUI.SetActive(true);
                PlayerAction(true);
            }
            else
            {
                inventoryGUI.SetActive(false);
                moveStopPlayer = false;
            }
        }
    }

    IEnumerator MoveStopPlayerCoroutine()
    {
        PlayerAction(true);
        yield return new WaitForSeconds(0.5f);
        tool.ToolUse();
        yield return new WaitForSeconds(0.25f);

        if(!inventoryGUI.active)
            moveStopPlayer = false;
    }    
}
