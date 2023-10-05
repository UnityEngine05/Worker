using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 - sprite layer
    0 ~ 10 Background
    11 ~ 20 Item
    21 ~ 30 Person
    31 ~ 40 More
    41 ~ 50 GUI
*/

public class GameManager : MonoBehaviour
{
    public Player player;
    public MineralsManager mineralsManager;
    public Text[] mineralsTextBox;
    public Store store;

    public int cameraSpeed;
    public long stoneNum, ironNum, goldNum, diamondNum, emeraldNum;
    public bool storeUI;
    // Start is called before the first frame update
    void Start()
    {
        mineralsManager.act_Minerals = MineralsNumAdd;
        store.act_StoreOpen = StoreOpen;
    }

    // Update is called once per frame
    void Update()
    {
        mineralsTextBox[0].text = string.Format("{0:N0}", stoneNum);
        mineralsTextBox[1].text = string.Format("{0:N0}", ironNum);
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
            new Vector3(player.transform.position.x, player.transform.position.y, -10), Time.deltaTime * cameraSpeed);
    }

    void MineralsNumAdd(Minerals _mineral)
    {
        switch (_mineral.mineralsType)
        {
            case MineralsType.stone:
                stoneNum++;
                break;
            case MineralsType.iron:
                ironNum++;
                break;
            case MineralsType.gold:
                goldNum++;
                break;
            case MineralsType.diamond:
                diamondNum++;
                break;
            case MineralsType.emerald:
                emeraldNum++;
                break;
            default:
                break;
        }
    }

    void StoreOpen()
    {
        if(!storeUI)
        {
            player.PlayerActionStop(true);
            storeUI = true;
        }
        else
        {
            player.PlayerActionStop(false);
            storeUI = false;
        }
    }
}
