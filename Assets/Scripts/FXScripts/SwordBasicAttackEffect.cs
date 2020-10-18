using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBasicAttackEffect : ParticlesAutoDestroy, IAttacker
{
    private ParticleSystem particles;
    private bool facingRight;
    private enum effectEnum
    {
        Forward,
        Up,
        Down
    }
    private GameObject colliderHolder;
    [SerializeField] private GameObject[] colliderHolders = {};
    private effectEnum attackNum;
    private ParticleSystem.MainModule main;


    public float dmgAmount {get; set;}

    public void InitAttack(object[] props)
    {
        particles = GetComponent<ParticleSystem>();

        if (props.Length <= 0)
        {
            return;
        }

        attackNum = (effectEnum)props[0];
        main = particles.main;
        facingRight = (bool)props[1];
        dmgAmount = 10f;
        colliderHolder = colliderHolders[(int)attackNum];
        AttackEffectCollision collisionScript = colliderHolder.GetComponent<AttackEffectCollision>();
        collisionScript.dmg = dmgAmount;

        // attack effect depending on state from controller
        // TODO: add other cases
        switch (attackNum)
        {
            case effectEnum.Forward:
            {
                colliderHolder.transform.rotation = Quaternion.Euler(facingRight ? 90 : -90, 0, 0);
                colliderHolder.transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);
                transform.localScale = new Vector3(facingRight ? 2f : -2f, 1, 0.7f);
                transform.rotation = Quaternion.Euler(facingRight ? 65 : -65, 0, 0);
                main.startRotationY = facingRight ? -20 * Mathf.Deg2Rad : -70 * Mathf.Deg2Rad;
                main.flipRotation = facingRight ? 1 : 0;
                break;
            };

            case effectEnum.Up:
            {
                colliderHolder.transform.rotation = Quaternion.Euler(facingRight? 90 : -90, 0, 0);
                colliderHolder.transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);
                transform.localScale = new Vector3(1.7f, 1, 1.1f);
                transform.rotation = Quaternion.Euler(0, 90, 90);
                main.startRotationY = facingRight ? -100 * Mathf.Deg2Rad : -50 * Mathf.Deg2Rad;
                main.flipRotation = facingRight? 0 : 1;
                break;
            }

            case effectEnum.Down:
            {
                colliderHolder.transform.rotation = Quaternion.Euler(facingRight? 90 : -90, 0, 0);
                colliderHolder.transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);
                transform.localScale = new Vector3(1.5f, 1, 1.3f);
                transform.rotation = Quaternion.Euler(180, 90, 90);
                main.startRotationY = facingRight ? -25 * Mathf.Deg2Rad : -80 * Mathf.Deg2Rad;
                main.flipRotation = facingRight ? 1 : 0;
                break;
            };
        }

        GameEvents.Instance.onPlayerFlip += FlipAttack;
    }

    public void PerformAttack()
    {
        PlayerManager.Instance.paralized = true;
        colliderHolder.SetActive(true);
        particles.Play();
    }

    private void OnParticleSystemStopped()
    {
        colliderHolder.SetActive(false);
        PlayerManager.Instance.paralized = false;
        GameEvents.Instance.onPlayerFlip -= FlipAttack;
        base.OnParticleSystemStopped(); 
    }


    // Method called when playerFlip event triggered
    private void FlipAttack()
    {
        switch (attackNum)
        {
            case effectEnum.Forward:
            {
                colliderHolder.transform.rotation = Quaternion.Euler(facingRight ? 90 : -90, 0, 0);
                colliderHolder.transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);
                transform.localScale = new Vector3(facingRight ? 1.7f : -1.7f, 1, 0.5f);
                transform.rotation = Quaternion.Euler(facingRight ? 90 : -90, 0, 0);
                // main.startRotationY = facingRight ? 10 * Mathf.Deg2Rad : -50 * Mathf.Deg2Rad;
                // main.flipRotation = facingRight ? 1 : 0;
                break;
            };

            case effectEnum.Up:
            {
                colliderHolder.transform.rotation = Quaternion.Euler(facingRight? 90 : -90, 0, 0);
                colliderHolder.transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);
                transform.localScale = new Vector3(1.7f, 1, 1.1f);
                transform.rotation = Quaternion.Euler(0, 90, 90);
                // main.startRotationY = facingRight ? -60 * Mathf.Deg2Rad : -10 * Mathf.Deg2Rad;
                // main.flipRotation = facingRight? 0 : 1;
                break;
            }

            case effectEnum.Down:
            {
                colliderHolder.transform.rotation = Quaternion.Euler(facingRight? 90 : -90, 0, 0);
                colliderHolder.transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);
                transform.localScale = new Vector3(1.5f, 1, 1.2f);
                transform.rotation = Quaternion.Euler(180, 90, 90);
                // main.startRotationY = facingRight ? -1 * Mathf.Deg2Rad : -45 * Mathf.Deg2Rad;
                // main.flipRotation = facingRight ? 1 : 0;
                break;
            };
        }
    }
}
