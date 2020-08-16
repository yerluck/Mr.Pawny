﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSideEffectsHandler : CharacterController2D, IDamageable
{
    private float m_HitPoints;
    private Animator anim;
    private RaycastHit2D hit;
    [SerializeField] private ParticleSystem stepParticles;

    public float HP { get { return m_HitPoints; } set { m_HitPoints = value; }}

    protected override void Awake() {
        base.Awake();
        m_HitPoints = PlayerManager.Instance.hitPoints;
		anim = GetComponent<Animator>();
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

        if (m_Rigidbody2D.velocity.y < 0) {
			hit = Physics2D.Raycast(m_GroundCheck.position, Vector2.down, m_LandingDistance, m_WhatIsGround);
			if (hit.collider != null) anim.SetTrigger("isLanding");
		}

        anim.SetFloat("xVelocity", Mathf.Abs(m_Rigidbody2D.velocity.x));
		anim.SetFloat("yVelocity", m_Rigidbody2D.velocity.y);
    }

    public void EmitStepDust() {
        stepParticles.Play();
    }

    public void takeDamage(float dmg){
        HP -= dmg;

        if(HP <= 0) {
            Debug.Log("DEAD!"); //TODO: add actual death action
        }
	}
}
