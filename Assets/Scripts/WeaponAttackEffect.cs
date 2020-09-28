using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackEffect : MonoBehaviour
{
    protected ParticleSystem particles;
    protected bool facingRight;

    protected void Init() {
        particles = GetComponent<ParticleSystem>();

        Debug.Log(particles.main.startRotationY);

        facingRight = transform.root.localScale.x >= 0 ? true : false;
    }
}
