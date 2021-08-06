using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// TODO: not only the player should be paranted... parenting by surface (guess).
public class RidingMovingPlatform : MonoBehaviour
{
    [SerializeField] LayerMask whatRides;
    private BoxCollider2D _collider;
    private const float checkDistance = .1f;
    private List<Transform> children = new List<Transform>();

    private void Start() {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.up, checkDistance, whatRides);
        if (hits.Length == 0 && children.Count == 0) {
            return;
        }
        List<Transform> hitObjs = new List<Transform>();

        for (var i = 0; i < hits.Length; i++) {
            if (!children.Contains(hits[i].transform)) {
                hits[i].transform.SetParent(gameObject.transform);
                children.Add(hits[i].transform);
            }
            hitObjs.Add(hits[i].transform);
        }
        IEnumerable<Transform> toDelete = children.Except(hitObjs);
        foreach (var item in toDelete.ToList())
        {
            item.SetParent(null);
            children.Remove(item);
        }
    }
}
