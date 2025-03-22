using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapManager : MonoBehaviour
{
    public string color;
    public string shape;

    public GameObject prefab;
    public SpriteDict sprite_dict;
    public TrailRenderer trail; 

    public List<Color> colors;

    private float tap_time_threshold = 0.3f;
    private float last_tap_time = 0f;

    private bool is_waiting = false;

    private RaycastHit2D hit;

    public void Start()
    {
        color = "Azul";
        shape = "Armadura";
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);
                hit = Physics2D.Raycast(position, Vector2.zero);

                if(hit.collider == null)
                {
                    SpriteRenderer go = Instantiate(prefab, position, Quaternion.identity).GetComponent<SpriteRenderer>();

                    go.sprite = sprite_dict.GetSprite(shape);
                    go.color = GetColor(color);

                    trail.transform.position = position;
                    trail.emitting = false;
                    
                }
                else
                {
                    if (is_waiting && Time.time - last_tap_time <= tap_time_threshold)
                    {
                        Destroy(hit.collider.gameObject);
                        Debug.Log("Doble tap");
                    }
                    else
                    {
                        last_tap_time = Time.time;
                        is_waiting = true;
                    }
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (hit.collider != null)
                {
                    Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);
                    hit.collider.transform.position = position;
                }
                else
                {
                    Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);
                    RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
                    trail.transform.position = position;
                    trail.emitting = true;

                    if( hit.collider != null) Destroy(hit.collider.gameObject);

                }
                
            }
        }
        if(is_waiting && Time.time - last_tap_time > tap_time_threshold)
        {
            last_tap_time = 0;
            is_waiting = false;
            
        }
    }

    private Color GetColor(string color)
    {
        switch (color)
        {
            case "Azul": return colors[0];
            case "Amarillo": return colors[1];
            case "Rojo": return colors[2];
            case "Verde": return colors[3];
            default: return Color.white; 
        }
    }

    public void onClickColor(string color)
    {
        this.color = color;

        trail.startColor = GetColor(color);
        trail.endColor = GetColor(color);
    }

    public void onClickShape(string shape)
    {
        this.shape = shape;
    }
}
