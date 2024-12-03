using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    private void OnMouseDown()
    {
        if(towerPrefab == null)
        {
            isPlaceable = false;
            return;
        }

        if (isPlaceable)
        {
            bool isPlaced =  towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
