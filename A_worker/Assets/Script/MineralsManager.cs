using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

public class MineralsManager : MonoBehaviour
{
    public GameObject mineralsBlocks;
    public Transform[] mineralPos;
    public Sprite[] mineralSprites;
    public RandomValue randomValue;

    public UnityAction<Minerals> act_Minerals;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        for (int i = 0; i < mineralPos.Length; i++)
            MakeMineral(i);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeMineral(int mineralBlockPos)
    {
        for(int h = 0; h > -20; h--)
        {
            for (int i = 0; i < 22; i++)
            {
                GameObject mineralBlock = Instantiate(mineralsBlocks);
                Minerals minerals = mineralBlock.GetComponent<Minerals>();
                int[] randomInt = { 65, 16, 11, 6, 2 };
                int makeMineralNum = randomValue.RandomIntValue(randomInt);

                switch (makeMineralNum)
                {
                    case 0:
                        minerals.mineralsType = MineralsType.stone;
                        break;
                    case 1:
                        minerals.mineralsType = MineralsType.iron;
                        break;
                    case 2:
                        minerals.mineralsType = MineralsType.gold;
                        break;
                    case 3:
                        minerals.mineralsType = MineralsType.diamond;
                        break;
                    case 4:
                        minerals.mineralsType = MineralsType.emerald;
                        break;
                    default:
                        break;
                }

                switch (minerals.mineralsType)
                {
                    case MineralsType.stone:
                        minerals.mineralSprite.sprite = mineralSprites[0];
                        minerals.hp = 8;
                        break;
                    case MineralsType.iron:
                        minerals.mineralSprite.sprite = mineralSprites[1];
                        minerals.hp = 16;
                        break;
                    case MineralsType.gold:
                        minerals.mineralSprite.sprite = mineralSprites[2];
                        minerals.hp = 32;
                        break;
                    case MineralsType.diamond:
                        minerals.mineralSprite.sprite = mineralSprites[3];
                        minerals.hp = 64;
                        break;
                    case MineralsType.emerald:
                        minerals.mineralSprite.sprite = mineralSprites[4];
                        minerals.hp = 128;
                        break;
                    default:
                        break;
                }
                mineralBlock.transform.position = mineralPos[mineralBlockPos].transform.position + new Vector3(i, h, 0);
                minerals.act_Minerals = MineralSendGameManager;
            }
        }
    }

    void MineralSendGameManager(Minerals _mineral)
    {
        act_Minerals.Invoke(_mineral);
    }
}
