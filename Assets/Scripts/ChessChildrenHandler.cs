using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessChildrenHandler : MonoBehaviour
{
    [SerializeField] private ChessChildren children;
    [SerializeField] private string sortingLayer;
    private Animation animation;


    //initialization of children
    private void Awake() {
        for (int i = 0; i < children.figures.Length; i++)
        {
            GameObject child = new GameObject(children.names[i]);
            child.transform.SetParent(gameObject.transform);
            child.transform.localPosition = children.positions[i];
            child.transform.localRotation = children.rotations[i];
            child.transform.localScale = children.scales[i];

            SpriteRenderer figure = child.AddComponent<SpriteRenderer>();
            figure.sprite = children.figures[i];
            figure.flipX = children.xFlips[i];
            figure.sortingLayerName = sortingLayer;
            figure.sortingOrder = children.layerOrders[i];

            animation = GetComponent<Animation>();
            animation.AddClip(children.animationClip, "interruption");
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        animation.Play("interruption");
    }
}
