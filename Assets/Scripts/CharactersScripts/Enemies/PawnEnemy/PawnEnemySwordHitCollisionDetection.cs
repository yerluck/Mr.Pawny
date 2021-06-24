using UnityEngine;

public class PawnEnemySwordHitCollisionDetection : DetectHitCollisionBase
{
    [SerializeField] string slashTransName;
    Transform _slash;

    void Awake()
    {
        _slash = transform.parent.FirstOrDefault(x => x.name.Equals(slashTransName));
    }

    private void OnEnable()
    {
        if(_slash != null)
        {
            _slash.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if(_slash != null)
        {
            _slash.gameObject.SetActive(false);
        }
    }
}
