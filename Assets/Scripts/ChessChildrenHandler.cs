using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessChildrenHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private ChessChildren[] childrenArr = {};
    [SerializeField] private string sortingLayer;
    [SerializeField] private ParticleSystem particle;
    private ChessChildren children;
    private Transform m_attacker;
    public float HP { get; set; }
    public Transform attacker { get { return m_attacker; } set { m_attacker = value; } }
    private Animation animation;
    private SpriteRenderer[] figuresRenderers;
    private BoxCollider2D collider;


    //initialization of children
    private void Awake()
    {
        children = childrenArr[Random.Range(0, childrenArr.Length)];
        figuresRenderers = new SpriteRenderer[children.figures.Length];

        for (int i = 0; i < children.figures.Length; i++)
        {
            // Transform initialization
            GameObject child = new GameObject(children.names[i]);
            child.transform.SetParent(gameObject.transform);
            child.transform.localPosition = children.positions[i];
            child.transform.localRotation = children.rotations[i];
            child.transform.localScale = children.scales[i];

            // Undamage sprite initialization
            SpriteRenderer figure = child.AddComponent<SpriteRenderer>();
            figuresRenderers[i] = figure;
            figure.sprite = children.figures[i];
            figure.flipX = children.xFlips[i];
            figure.sortingLayerName = sortingLayer;
            figure.sortingOrder = children.layerOrders[i];


            animation = GetComponent<Animation>();
            animation.AddClip(children.animationClip, "interruption");
        }

        collider = gameObject.AddComponent<BoxCollider2D>();
        collider.enabled = true;
        collider.size = children.colliderSize;
        collider.offset = children.colliderOffset;
        collider.isTrigger = true;
    }

    public void TakeDamage(float dmg = 0)
    {
        collider.enabled = false;
        Vector3 theScale = particle.transform.localScale;
		theScale.z *= Vector3.Normalize(gameObject.transform.position - attacker.position).x;
		particle.transform.localScale = theScale;
        particle.Play();

        for (int i = 0; i < figuresRenderers.Length; i++)
        {
            figuresRenderers[i].sprite = children.destroyedFigures[i];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        animation.Play("interruption");
    }
}
