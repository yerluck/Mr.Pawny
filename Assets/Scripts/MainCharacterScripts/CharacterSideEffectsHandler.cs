﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CharacterSideEffectsHandler : CharacterController2D, IDamageable
{
    private float m_HitPoints;
    private Transform m_attacker;
    private Animator anim;
    private RaycastHit2D hit;
    [SerializeField] private ParticleSystem stepParticles;
    private ParticleSystem.ShapeModule shape;
    public float HP { get { return m_HitPoints; } set { m_HitPoints = value; }}
    public Transform attacker { get { return m_attacker; } set { m_attacker = value; } }
    private bool isLanded;
    private Vector3 SHAPE_ROTATION = new Vector3(0, 270, 0);

    protected override void Awake() {
        base.Awake();
        m_HitPoints = PlayerManager.Instance.hitPoints;
		anim = GetComponent<Animator>();
        isLanded = !m_Grounded;

        GameEvents.Instance.onPlayerFlip += CharacterFlipAction;
        if (stepParticles != null) 
        {
            shape = stepParticles.shape;
            if (transform.localScale.x >= 0) shape.rotation = SHAPE_ROTATION;
            else shape.rotation = -1 * SHAPE_ROTATION;
        }
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
        if (m_Grounded) 
        {
            anim.SetBool("isGrounded", true);
        } else
        {
            anim.SetBool("isGrounded", false);
        }

        //Handle landing animations, TODO: still need tests
        if (m_Rigidbody2D.velocity.y < -0.01) {
			hit = Physics2D.Raycast(m_GroundCheck.position, Vector2.down, m_LandingDistance, m_WhatIsGround);
			if (hit.collider != null && !isLanded) {
                anim.SetBool("isLanding", true);
                isLanded = true;
            }
		} else {
            anim.SetBool("isLanding", false);
            isLanded = false;
        }

        anim.SetFloat("xVelocity", Mathf.Abs(m_Rigidbody2D.velocity.x));
		anim.SetFloat("yVelocity", m_Rigidbody2D.velocity.y);
    }

    public void EmitStepDust() {
        stepParticles.Play();
    }

    private void OnParticleSystemStopped() {
    }

    public void TakeDamage(float dmg){
        HP -= dmg;

        if(HP <= 0) {
            Debug.Log("DEAD!"); //TODO: add actual death action
        }
	}

    private void CharacterFlipAction()
    {
        if (stepParticles != null)
        {
            shape.rotation *= -1;
        }
    }

    private void OnDestroy() {
        if(GameEvents.Instance != null)
            GameEvents.Instance.onPlayerFlip -= CharacterFlipAction;        
    }
}