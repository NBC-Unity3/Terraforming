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
        // ���� ���ᰡ �ƴ� ��쿡�� ����.
        while (isGameOver == false)
        {
            // Enemy �±׸� ���� ���� ������Ʈ�� ��� �˻��� ���� �ʿ� ��ġ�� 
            // �� ĳ���� �� Ȯ��.
            int rangeSlimeCount = (int)GameObject.FindGameObjectsWithTag("RangeSlime").Length;
            int slimeCount = (int)GameObject.FindGameObjectsWithTag("Slime").Length;
            int turtleShellCount = (int)GameObject.FindGameObjectsWithTag("TurtleShell").Length;

            // ���� ������ �� ĳ���Ͱ� maxEnemy�� ������ ���ں���
            // ���� ��쿡�� �߰��� �� ����.
            if (rangeSlimeCount < maxEnemy)
            {
                yield return new WaitForSeconds(1f);
                // ���� ���� �������� ����.
                int index = Random.Range(0, spawnPoint.Length);

                // �� ĳ���� ����.
                Instantiate(rangeSlime, spawnPoint[index].position, spawnPoint[index].rotation);
            }
            if (slimeCount < maxEnemy)
            {
                yield return new WaitForSeconds(1f);
                // ���� ���� �������� ����.
                int index = Random.Range(0, spawnPoint.Length);

                // �� ĳ���� ����.
                Instantiate(slime, spawnPoint[index].position, spawnPoint[index].rotation);
            }
            if (turtleShellCount < maxEnemy)
            {
                yield return new WaitForSeconds(1f);
                // ���� ���� �������� ����.
                int index = Random.Range(0, spawnPoint.Length);

                // �� ĳ���� ����.
                Instantiate(turtleShell, spawnPoint[index].position, spawnPoint[index].rotation);
            }


            // �� ĳ���Ͱ� maxEnemy ��ŭ ������ ������, �߰��� �������� �ʰ� ���.
            else
            {
                yield return new WaitForSeconds(3f);
            }
        }
    }
}
