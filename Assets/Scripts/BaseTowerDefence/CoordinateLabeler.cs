using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.red;

#if true
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = Color.blue;
#endif


    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
#if true
    GridManager gridManager;
#else
    Tile waypoint;
#endif


    private void Awake()
    {        
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
#if true
        gridManager = FindObjectOfType<GridManager>();
#else
        waypoint = GetComponentInParent<Tile>();
#endif
        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLables();
    }

    private void SetLabelColor()
    {
#if true
        if (gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if (node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
#else
        if (waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor; 
        }
#endif
    }

        void ToggleLables()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            label.enabled = !label.IsActive();
        }
    }

    private void DisplayCoordinates()
    {
#if true
        if (gridManager == null) { return; }

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
#elif UNITY_EDITOR
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = $"{coordinates.x}, {coordinates.y}";
#endif
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
