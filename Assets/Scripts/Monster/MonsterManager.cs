using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject rangeSlime;
    public GameObject slime;
    public GameObject turtleShell;
    public Transform[] spawnPoint;
    public int maxEnemy = 20;
    public bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CreateEnemy");
    }

    IEnumerator CreateEnemy()
    {
        // 게임 종료가 아닌 경우에만 생성.
        while (isGameOver == false)
        {
            // Enemy 태그를 가진 게임 오브젝트를 모두 검사해 현재 맵에 배치된 
            // 적 캐릭터 수 확인.
            int rangeSlimeCount = (int)GameObject.FindGameObjectsWithTag("RangeSlime").Length;
            int slimeCount = (int)GameObject.FindGameObjectsWithTag("Slime").Length;
            int turtleShellCount = (int)GameObject.FindGameObjectsWithTag("TurtleShell").Length;

            // 현재 생성된 적 캐릭터가 maxEnemy에 설정된 숫자보다
            // 작은 경우에만 추가로 적 생성.
            if (rangeSlimeCount < maxEnemy)
            {
                yield return new WaitForSeconds(1f);
                // 생성 지점 랜덤으로 선택.
                int index = Random.Range(0, spawnPoint.Length);

                // 적 캐릭터 생성.
                Instantiate(rangeSlime, spawnPoint[index].position, spawnPoint[index].rotation);
            }
            if (slimeCount < maxEnemy)
            {
                yield return new WaitForSeconds(1f);
                // 생성 지점 랜덤으로 선택.
                int index = Random.Range(0, spawnPoint.Length);

                // 적 캐릭터 생성.
                Instantiate(slime, spawnPoint[index].position, spawnPoint[index].rotation);
            }
            if (turtleShellCount < maxEnemy)
            {
                yield return new WaitForSeconds(1f);
                // 생성 지점 랜덤으로 선택.
                int index = Random.Range(0, spawnPoint.Length);

                // 적 캐릭터 생성.
                Instantiate(turtleShell, spawnPoint[index].position, spawnPoint[index].rotation);
            }


            // 적 캐릭터가 maxEnemy 만큼 생성돼 있으면, 추가로 생성하지 않고 대기.
            else
            {
                yield return new WaitForSeconds(3f);
            }
        }
    }
}
