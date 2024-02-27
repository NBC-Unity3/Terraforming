using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

// 총을 구현한다
public class PlayerShooter : MonoBehaviour {

    public Transform fireTransform;
    public Transform aimTransform;

    private LineRenderer bulletLineRenderer;

    private AudioSource gunAudioPlayer; 
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 25;

    public float timeBetFire = 0.12f;
    public float reloadTime = 1.8f; // 재장전 소요 시간


    private void Awake() {
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    public void Shot() {
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        Debug.DrawRay(fireTransform.position, aimTransform.forward, Color.white, 1.0f);
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit))
        {
            //레이가 어떤 물체와 충돌한 경우
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if (target != null)
            {
                target.OnDamage(damage, hit.point, hit.normal);
            }
            hitPosition = hit.point;
        }
        else
        {   
            // 최대 사정거리를 충돌위치로 사용
            hitPosition = fireTransform.position + fireTransform.forward;   // * attackRange
        }
        StartCoroutine(ShotEffect(hitPosition));
    }

    public void Reload()
    {
        gunAudioPlayer.PlayOneShot(reloadClip);
    }

    private IEnumerator ShotEffect(Vector3 hitPosition) {

        gunAudioPlayer.PlayOneShot(shotClip);

        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);

        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLineRenderer.enabled = false;
    }

}