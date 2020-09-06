using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chess Children", menuName = "ChessChildren", order = 0)]
public class ChessChildren : ScriptableObject {
    public Sprite[] figures = {};
    public Sprite[] destroyedFigures = {};
    public Vector2[] positions = {};
    public Quaternion[] rotations = {};
    public Vector2[] scales = {};
    public Vector2 colliderSize = new Vector2(0,0);
    public Vector2 colliderOffset = new Vector2(0,0);
    public bool[] xFlips = {};
    public int[] layerOrders = {};
    public string[] names = {};
    public AnimationClip animationClip = null;
}
