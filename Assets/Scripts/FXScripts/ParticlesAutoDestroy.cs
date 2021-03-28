using UnityEngine;

// TODO: maybe this is useless (particle system has own autodestroy func)
[RequireComponent(typeof(ParticleSystem))]
public class ParticlesAutoDestroy : MonoBehaviour
{
    protected virtual void OnParticleSystemStopped()
    {
        Destroy(this.gameObject);
    }
}