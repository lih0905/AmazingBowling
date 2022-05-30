using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public LayerMask whatIsProp;
    public ParticleSystem explosionParticle;
    public AudioSource explosionAudio;
    
    public float maxDamage = 100f;
    public float explosionForce = 1000f;
    public float lifeTime = 10f;
    public float explosionRadius = 20f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, whatIsProp);

        for(int i=0; i<colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            Prop targetProp = colliders[i].GetComponent<Prop>();

            float damage = CalculateDamage(colliders[i].transform.position);
            targetProp.TakeDamage(damage);
        }

        explosionParticle.transform.parent = null; // 폭발하는 순간 부모를 해제하고 Ball을 파괴

        explosionParticle.Play();
        explosionAudio.Play();

        GameManager.instance.OnBallDestroy();
        
        Destroy(explosionParticle.gameObject, explosionParticle.main.duration); // 특수효과 끝나면 효과 파괴
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;
        float distance = explosionToTarget.magnitude;
        float edgeToCenterDistance = explosionRadius - distance;
        float percentage = edgeToCenterDistance / explosionRadius;
        float damage = Mathf.Max(maxDamage * percentage, 0);
        return damage;
    }
}
