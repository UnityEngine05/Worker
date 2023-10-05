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
        if (array.Length < 2) //배열이 한개만 들어가있다면 값 0으로 돌려주기
            return 0;

        int total = 0;

        for(int i = 0; i < array.Length; i++)
        {
            total += array[i]; //배열을 다 더하여 값 구하기
        }

        int random = Random.Range(0, total); //다 더한 배열까지 랜덤으로 값 나오게하기
        int end = 0;

        for(int i = 0; i < array.Length; i++)
        {
            end += array[i]; //배열을 더하여 end 와 값이 같을 때까지 더하기
            if (random < end) //end 와 값이 같아지면 같아졌을 때 i를 내보내기 ( i 가 1이었으면 배열 1에 있던 값 내보내기 )
                return i;
        }

        return 0;
    }
}
