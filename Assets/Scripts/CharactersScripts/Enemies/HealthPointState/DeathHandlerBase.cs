using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandlerBase : MonoBehaviour
{
    /// <summary>
    /// Structer need to initialize die action
    /// </summary>
    public struct DeathProperties
    {
        public readonly Transform characterTransform;
        public readonly Transform attackerTransform;

        public DeathProperties(Transform character, Transform attacker)
        {
            characterTransform = character;
            attackerTransform = attacker;
        }
    }

    Animation _animation;
    Rigidbody2D _rigidbody;
    ParticleSystem _deathParticles;
    [SerializeField] string forwardFallAnimationTitle;
    [SerializeField] string backwardFallAnimationTitle;
    [SerializeField] float forceAmount = 1;

    private void Awake()
    {
        _animation = GetComponent<Animation>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _deathParticles = GetComponent<ParticleSystem>();
    }

    // TODO: should to subscribe on event (after SO based event system implemented)
    public void StartDeathAction(DeathProperties properties)
    {
        transform.position = properties.characterTransform.position;
        transform.rotation = properties.characterTransform.rotation;
        transform.localScale = properties.characterTransform.localScale;

        // Stop character
        _rigidbody.velocity = Vector2.zero;

        bool isTargetBehind = properties.characterTransform.IsTargetBehind(properties.attackerTransform);
        var clip = isTargetBehind ? forwardFallAnimationTitle : backwardFallAnimationTitle;
        var force = new Vector2 ((isTargetBehind ? 1 : -1) * transform.localScale.x * forceAmount, forceAmount);

        _rigidbody.AddForce(force, ForceMode2D.Impulse);
        _animation.Play(clip);
    }

    public void EmitDeathParticles()
    {
        _deathParticles.Play(false);
    }

    private void OnParticleSystemStopped()
    {
        Destroy(gameObject);    
    }
}
