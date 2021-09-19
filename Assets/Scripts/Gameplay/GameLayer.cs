using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayer : MonoBehaviour
{
    [SerializeField] LayerMask solidObjectsLayer;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] LayerMask grassLayer;

    public static GameLayer Instance { get; set; } //appelé i dans la vidéo

    private void Awake()
    {
        Instance = this;
    }
    public LayerMask SolidLayer { 
        get => solidObjectsLayer;
    }

    public LayerMask InteractableLayer
    {
        get => interactableLayer;
    }

    public LayerMask GrassLayer
    {
        get => grassLayer;
    }
}
