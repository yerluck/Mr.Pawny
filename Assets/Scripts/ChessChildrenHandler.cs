using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessChildrenHandler : MonoBehaviour
{
    [SerializeField] private ChessChildren children;
    [SerializeField] private string sortingLayer;
    // private SimpleAnimation m_CustomAnimationPlayable;
    private AnimationClip clip;


    // private Transform[] children;
    
    //initialization of children
    private void Awake() {
        for (int i = 0; i < children.figures.Length; i++)
        {
            GameObject child = new GameObject();
            child.transform.SetParent(gameObject.transform);
            child.transform.localPosition = children.positions[i];
            child.transform.localRotation = children.rotations[i];
            child.transform.localScale = children.scales[i];

            SpriteRenderer figure = child.AddComponent<SpriteRenderer>();
            figure.sprite = children.figures[i];
            figure.flipX = children.xFlips[i];
            figure.sortingLayerName = sortingLayer;
            figure.sortingOrder = children.layerOrders[i];
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
