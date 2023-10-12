using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum MineralsType 
{ 
    stone, iron, gold, diamond, emerald
}
public class Minerals : MonoBehaviour
{
    public MineralsType mineralsType;
    public UnityAction<Minerals> act_Minerals;

    public SpriteRenderer mineralSprite;
    public Sprite[] stoneSprite, ironSprite, goldSprite;

    public float hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MineralsAttack(float damage)
    {
        hp -= damage;
        StartCoroutine(MineralDamage());
        MineralHpSkin();

        if (hp <= 0)
        {
            act_Minerals.Invoke(this);

            Destroy(gameObject);
        }
    }

    IEnumerator MineralDamage()
    {
        transform.position += new Vector3(-0.05f, 0, 0);
        yield return new WaitForSeconds(0.05f);
        transform.position += new Vector3(0.1f, 0, 0);
        yield return new WaitForSeconds(0.05f);
        transform.position += new Vector3(-0.05f, 0, 0);
    }

    void MineralHpSkin()
    {
        switch (mineralsType)
        {
            case MineralsType.stone:
                if(hp < 6 && hp > 2)
                    mineralSprite.sprite = stoneSprite[0];
                else if(hp < 3)
                    mineralSprite.sprite = stoneSprite[1];
                break;
            case MineralsType.iron:
                if (hp < 10 && hp > 5)
                    mineralSprite.sprite = ironSprite[0];
                else if (hp < 6)
                    mineralSprite.sprite = ironSprite[1];
                break;
            case MineralsType.gold:
                break;
            case MineralsType.diamond:
                break;
            case MineralsType.emerald:
                break;
            default:
                break;
        }
    }
}
