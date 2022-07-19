using System;
using UnityEngine;

public abstract class Movable : MonoBehaviour
{
    public static Action onMoveEnd;
    public abstract void Move(Transform destination);

}
