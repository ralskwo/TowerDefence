using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
#if true
    List<Node> path = new List<Node>();
#else
    [SerializeField] List<Tile> path = new List<Tile>();
#endif
    [SerializeField] [Range(0f, 5f)] float speed = 1.0f;

    Enemy enemy;

#if true
    GridManager gridManager;
    PathFinder pathFinder;
#endif

    // Start is called before the first frame update
    void OnEnable()
    {
        RecalculatePath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void RecalculatePath()
    {
        path.Clear();

#if true
        path = pathFinder.GetNewPath();
#else
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child  in parent.transform)
        {
            Tile waypoint = child .GetComponent<Tile>();

            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
#endif
    }

    void ReturnToStart()
    {
#if true
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
#else
        transform.position = path[0].transform.position;
#endif
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
#if true
        for (int i = 0;  i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);

            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                yield return new WaitForEndOfFrame();
            }
        }
#else
        foreach (Tile point in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = point.transform.position;

            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                yield return new WaitForEndOfFrame();
            }
        }
#endif


        FinishPath();
    }
}