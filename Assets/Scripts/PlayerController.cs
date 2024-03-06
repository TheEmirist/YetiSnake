using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 direction = Vector2.up;
    public List<Transform> segments = new List<Transform>();
    [SerializeField] Transform segmentPrefab;
    [SerializeField] int initialSize = 2;
    [SerializeField] float snakeSpeed = 0.1f;
    Vector2 startTouchPosition;
    Vector2 endTouchPosition;

    void Start()
    {
        GlobalEventManager.OnFoodEaten.AddListener(Grow);
        Time.timeScale = snakeSpeed;
        ResetState();
    }

    void Update()
    {
        KeyboardControls();
        TouchControls();
        MouseControls(); // uncomment when working in Unity editor
    }

    void MouseControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
        
            Vector2 inputVector = endTouchPosition - startTouchPosition;

            if (inputVector.magnitude < 50)
            {
                return;
            }
                
            if(Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
            {
                if(inputVector.x > 0 && direction != Vector2.left)
                {
                    direction = Vector2.right;
                }
                else if (direction != Vector2.right)
                {
                    direction = Vector2.left;
                }
            }
            else
            {
                if (inputVector.y > 0 && direction != Vector2.down)
                {
                    direction = Vector2.up;
                }
                else if (direction != Vector2.up)
                {
                    direction = Vector2.down;
                }
            }
        }
    }

    void TouchControls()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
        
            Vector2 inputVector = endTouchPosition - startTouchPosition;
                
            if(Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
            {
                if(inputVector.x > 0 && direction != Vector2.left)
                {
                    direction = Vector2.right;
                }
                else if (direction != Vector2.right)
                {
                    direction = Vector2.left;
                }
            }
            else
            {
                if (inputVector.y > 0 && direction != Vector2.down)
                {
                    direction = Vector2.up;
                }
                else if (direction != Vector2.up)
                {
                    direction = Vector2.down;
                }
            }
        }
    }
    

    void KeyboardControls()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    void FixedUpdate()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        transform.position = new Vector3(
            Mathf.Round(transform.position.x + direction.x),
            Mathf.Round(transform.position.y + direction.y),
            0.0f
        );
    }
    
    void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
    }

    public void ResetState()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(transform);

        for (int i = 1; i < initialSize; i++)
        {
            segments.Add(Instantiate(segmentPrefab));
        }

        transform.position = Vector3.zero;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Food"))
        {
            GlobalEventManager.SendFoodEaten();
        }

        if (other.CompareTag("Obstacle"))
        {
            GlobalEventManager.SendSnakeDeath();
        }
    }
}
