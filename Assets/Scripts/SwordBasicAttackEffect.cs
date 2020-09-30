using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBasicAttackEffect : MonoBehaviour, IAttacker
{
    private ParticleSystem particles;
    private bool facingRight;
    private enum effectEnum
    {
        Forward,
        Up,
        Down
    }
    private Vector3 localScale;
    private Quaternion rotation;
    private float startRotationY;
    private float flipRotation;


    public float dmgAmount {get; set;}

    public void InitAttack(object[] props)
    {
        particles = GetComponent<ParticleSystem>();

        if (props.Length <= 0)
        {
            return;
        }

        effectEnum attackNum = (effectEnum)props[0];
        var main = particles.main;
        facingRight = transform.root.localScale.x >= 0 ? true : false;

        // attack effect depending on state from controller
        // TODO: add other cases
        switch (attackNum)
        {
            case effectEnum.Forward:
            {
                transform.localScale = new Vector3(facingRight ? 1.7f : -1.7f, 1, 0.5f);
                transform.rotation = Quaternion.Euler(facingRight ? 90 : -90, 0, 0);
                main.startRotationY = facingRight ? 10 * Mathf.Deg2Rad : -50 * Mathf.Deg2Rad;
                main.flipRotation = facingRight ? 1 : 0;
                break;
            };
            case effectEnum.Down:
            {
                // transform.position = new Vector3(-0.62f, -0.07f, 1);
                transform.localScale = new Vector3(1.7f, 1, 1.2f);
                transform.rotation = Quaternion.Euler(180, 90, 90);
                main.startRotationY = -1 * Mathf.Deg2Rad;
                main.flipRotation = 1;
                break;
            };
        }
    }

    public void PerformAttack()
    {
        particles.Play();
    }
}
