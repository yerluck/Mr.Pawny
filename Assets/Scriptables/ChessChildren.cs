﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chess Children", menuName = "ChessChildren", order = 0)]
public class ChessChildren : ScriptableObject {
    public Sprite[] figures = {};
    public Vector2[] positions = {};
    public Quaternion[] rotations = {};
    public Vector2[] scale = {};
    public bool[] xFlips = {};
    public int[] layerOrder = {};
    public AnimationClip animationClip;
}
