using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    [SerializeField] BoxCollider2D grid;
    [SerializeField] PlayerController player;
    [SerializeField] List<Transform> obstacles = new List<Transform>();

    void Start()
    {
        RandomizePosition();
        GlobalEventManager.OnFoodEaten.AddListener(RandomizePosition);
    }

    void RandomizePosition()
    {
        Bounds bounds = grid.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);

        for (int i = 0; i < player.segments.Count; i++)
        {
            if (transform.position == player.segments[i].transform.position)
            {
                RandomizePosition();

                return;
            }
        }

        for (int i = 0; i < obstacles.Count; i++)
        {
            if (transform.position == obstacles[i].transform.position)
            {
                RandomizePosition();

                return;
            }
        }
    }
}
