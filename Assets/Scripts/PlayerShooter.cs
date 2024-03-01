using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// 총을 구현한다
public class PlayerShooter : MonoBehaviour {

    public enum State {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }

    public State state { get; private set; }

    public Transform fireTransform;

    private LineRenderer bulletLineRenderer;

    private AudioSource gunAudioPlayer; 
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public Gun gun;

    public int ammo;    // 보유중인 탄환수

    private float lastFireTime;

    public Action<int> onFire;

    private void Awake() {
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    private void Start() {
        // 총 상태 초기화
        state = State.Ready;
        lastFireTime = 0;
        ammo = 100;
    }

    public void Fire() {
        if (state == State.Ready && Time.time >= lastFireTime + 60f/gun.rpm)
        {
            lastFireTime = Time.time;
            Shot();
        }
    }

    private void Shot() {
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit))
        {
            //레이가 어떤 물체와 충돌한 경우
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if (target != null)
            {
                target.OnDamage(gun.damage, hit.point, hit.normal);
            }
            hitPosition = hit.point;
        }
        else
        {   
            // 최대 사정거리를 충돌위치로 사용
            hitPosition = fireTransform.position + fireTransform.forward * gun.range;
        }
        StartCoroutine(ShotEffect(hitPosition));

        gun.magazine--;
        onFire?.Invoke(gun.magazine);
        if (gun.magazine <= 0)
        {
            state = State.Empty;
        }
    }

    public void Reload()
    {
        if (state == State.Reloading || ammo <= 0 || gun.magazine >= gun.capacity)
        {
            return;
        }

        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ShotEffect(Vector3 hitPosition) {

        gunAudioPlayer.PlayOneShot(shotClip);

        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);

        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLineRenderer.enabled = false;
    }

    private IEnumerator ReloadRoutine() {
        state = State.Reloading;
        gunAudioPlayer.PlayOneShot(reloadClip);
        
        // 재장전 소요 시간 만큼 처리를 쉬기
        yield return new WaitForSeconds(gun.reload);    // 대기시간동안 state값이 Reloading으로 고정

        // 재장전할 탄알 계산
        int ammoToFill = gun.capacity - gun.magazine;
        if (ammo < ammoToFill)
        {
            ammoToFill = ammo;
        }

        gun.magazine += ammoToFill;
        ammo -= ammoToFill;

        onFire?.Invoke(gun.magazine);

        state = State.Ready;
    }

}