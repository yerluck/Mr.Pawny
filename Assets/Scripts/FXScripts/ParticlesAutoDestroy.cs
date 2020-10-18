using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticlesAutoDestroy : MonoBehaviour
{
    protected void OnParticleSystemStopped()
    {
        Destroy(this.gameObject);
    }
}