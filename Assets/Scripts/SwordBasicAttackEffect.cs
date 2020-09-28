using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBasicAttackEffect : WeaponAttackEffect
{
    private void Awake() {
        // initialization of baze class
        Init();

        var main = particles.main;
        var theScale = transform.localScale;
        theScale.x = facingRight ? 1.7f : -1.7f;
        var transformRotX = facingRight ? 90 : -90;


        //initialization of particles
        transform.localScale = theScale;
        transform.rotation = Quaternion.Euler(transformRotX, 0, 0);
        main.startRotationY = facingRight ? 10 * Mathf.Deg2Rad : -50 * Mathf.Deg2Rad;
        main.flipRotation = facingRight ? 1 : 0;

        particles.Play();
    }
}
