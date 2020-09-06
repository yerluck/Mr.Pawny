using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessChildrenHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private ChessChildren children;
    [SerializeField] private string sortingLayer;
    public float HP { get; set; }
    private Animation animation;
    private SpriteRenderer[] figuresRenderers;
    private BoxCollider2D collider;


    //initialization of children
    private void Awake()
    {
        figuresRenderers = new SpriteRenderer[children.figures.Length];

        for (int i = 0; i < children.figures.Length; i++)
        {
            GameObject child = new GameObject(children.names[i]);
            child.transform.SetParent(gameObject.transform);
            child.transform.localPosition = children.positions[i];
            child.transform.localRotation = children.rotations[i];
            child.transform.localScale = children.scales[i];

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
