using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomValue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int RandomIntValue(int[] array)
    {
        if (array.Length < 2) //�迭�� �Ѱ��� ���ִٸ� �� 0���� �����ֱ�
            return 0;

        int total = 0;

        for(int i = 0; i < array.Length; i++)
        {
            total += array[i]; //�迭�� �� ���Ͽ� �� ���ϱ�
        }

        int random = Random.Range(0, total); //�� ���� �迭���� �������� �� �������ϱ�
        int end = 0;

        for(int i = 0; i < array.Length; i++)
        {
            end += array[i]; //�迭�� ���Ͽ� end �� ���� ���� ������ ���ϱ�
            if (random < end) //end �� ���� �������� �������� �� i�� �������� ( i �� 1�̾����� �迭 1�� �ִ� �� �������� )
                return i;
        }

        return 0;
    }
}
