using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemy = new List<GameObject>();
    public bool isEmpty
    {
        get
        {
            return enemy.Count == 0;
        }
    }
}
