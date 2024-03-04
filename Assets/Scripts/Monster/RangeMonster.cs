using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public class RangeMonster : MonoBehaviour, IDamagable
{
    [Header("Stats")]
    public int health;
    public float walkSpeed;
    public float runSpeed;

    [Header("AI")]
    private AIState aiState;
    public float detectDistance;
    public float safeDistance;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;


    [Header("Combat")]
    public int damage;
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;

    private float playerDistance;

    public float fieldOfView = 120f;

    public GameObject mucus;
    public Transform mucusSpawnPoint;
    public GameObject coin;

    private PlayerController playerController;
    private NavMeshAgent agent;
    private Animator animator;
    //private SkinnedMeshRenderer[] meshRenderers;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        //meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        SetState(AIState.Wandering);
        playerController = PlayerController.instance;
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, playerController.transform.position);

        animator.SetBool("Moving", aiState != AIState.Idle && aiState != AIState.Die);
        
        switch (aiState)
        {
            case AIState.Idle: PassiveUpdate(); break;
            case AIState.Wandering: PassiveUpdate(); break;
            case AIState.Attacking: AttackingUpdate(); break;
            case AIState.Fleeing: FleeingUpdate(); break;
            case AIState.Die: break;
        }

    }

    private void FleeingUpdate()
    {
        if (agent.remainingDistance < 0.1f)
        {
            agent.SetDestination(GetFleeLocation());
        }
        else
        {
            SetState(AIState.Idle);
        }
    }

    private void AttackingUpdate()
    {
        if (playerDistance > attackDistance || !IsPlaterInFireldOfView())
        {
            agent.isStopped = false;
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(playerController.transform.position, path))
            {
                agent.SetDestination(playerController.transform.position);
            }
            else
            {
                SetState(AIState.Fleeing);
            }
        }
        else
        {
            animator.SetBool("Moving", false);
            //Vector3 directionToPlayer = PlayerController.instance.transform.position - transform.position;
            Vector3 directionToPlayer = new Vector3
                //캐싱하기
            (playerController.transform.position.x - transform.position.x,
            playerController.transform.position.y + 1 - transform.position.y,
            playerController.transform.position.z - transform.position.z);

            // 플레이어를 바라보도록 회전 처리.
            UpdateRotation(directionToPlayer, 3f);
            agent.isStopped = true;
            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;
                Instantiate(mucus, mucusSpawnPoint.position,mucusSpawnPoint.rotation);
                //PlayerController.instance.GetComponent<IDamagable>().TakePhysicalDamage(damage);
                animator.speed = 1;
                animator.SetTrigger("Attack");
            }
        }
    }

    private void PassiveUpdate()
    {
        if (aiState == AIState.Idle)
        {
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }

        if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }

        if (playerDistance < detectDistance)
        {
            SetState(AIState.Attacking);
        }
    }

    bool IsPlaterInFireldOfView()
    {
        Vector3 directionToPlayer = playerController.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }

    private void SetState(AIState newState)
    {
        aiState = newState;
        switch (aiState)
        {
            case AIState.Idle:
                {
                    agent.speed = walkSpeed;
                    agent.isStopped = true;
                }
                break;
            case AIState.Wandering:
                {
                    agent.speed = walkSpeed;
                    agent.isStopped = false;
                }
                break;

            case AIState.Attacking:
                {
                    agent.speed = runSpeed;
                    agent.isStopped = false;
                }
                break;
            case AIState.Fleeing:
                {
                    agent.speed = runSpeed;
                    agent.isStopped = false;
                }
                break;
            case AIState.Die:
                {
                    agent.speed = 0;
                    agent.isStopped = true;
                }
                break;
        }

        animator.speed = agent.speed / walkSpeed;
    }

    void WanderToNewLocation()
    {
        if (aiState != AIState.Idle)
        {
            return;
        }
        SetState(AIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }


    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }

    Vector3 GetFleeLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (GetDestinationAngle(hit.position) > 90 || playerDistance < safeDistance)
        {

            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }

    float GetDestinationAngle(Vector3 targetPos)
    {
        return Vector3.Angle(transform.position - playerController.transform.position, transform.position + targetPos);
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health -= damageAmount;
        detectDistance = 100f;
        Debug.Log("hit");
        animator.SetTrigger("Hit");
        if (health <= 0)
        {
            Debug.Log("Die");
            //Die();
            SetState(AIState.Die);
            StartCoroutine("Die");
        }
        //StartCoroutine(DamageFlash());
    }
    IEnumerator Die()
    {
        animator.SetTrigger("Die");
        QuestManager.Instance.UpdateQuestKillCount();
        Instantiate(coin, transform.position,Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    public void UpdateRotation(Vector3 target, float damping)
    {
        if (target != Vector3.zero)
        {
            // 적 AI 캐릭터가 target 방향을 바라보도록 회전 설정.
            Quaternion rotation = Quaternion.LookRotation(target);

            // Slerp 함수를 사용해 부드럽게 회전 처리.
            transform.rotation = Quaternion.Slerp(
                transform.rotation, rotation, damping * Time.deltaTime
            );
            //MucusSpawnPoint이 target을 조준 할수있게 회전 설정
            mucusSpawnPoint.rotation = Quaternion.Slerp(
                mucusSpawnPoint.rotation, rotation, damping * Time.deltaTime
            );

            //Vector3 mucusRot = mucusSpawnPoint.eulerAngles;
            //mucusRot.x = -Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            //mucusSpawnPoint.eulerAngles = mucusRot;

            //MucusSpawnPoint.eulerAngles = new Vector3(target.y, 0, 0);

        }
    }

    //void Die()
    //{
    //    animator.SetTrigger("Die");
    //    Destroy(gameObject);
    //}

    //IEnumerator DamageFlash()
    //{
    //    for (int x = 0; x < meshRenderers.Length; x++)
    //        meshRenderers[x].material.color = new Color(1.0f, 0.6f, 0.6f);

    //    yield return new WaitForSeconds(0.1f);
    //    for (int x = 0; x < meshRenderers.Length; x++)
    //        meshRenderers[x].material.color = Color.white;
    //}
}