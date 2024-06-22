using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public enum pattern//not use?
    {
        triangle,
        square,
        pentago,
        hexagon,
        circle
    }
    public enum identity
    {
        reserve,
        warrior
    }
    public Game game;
    public SpriteRenderer spriteRenderer;
    public TextMesh textMesh;
    public pattern patternpattern;//not use?
    public identity identityidentity;
    Vector2 vector2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUpAsButton()
    {
        Debug.Log("OnMouseUpAsButton");
    }

    void OnMouseDown()
    {
        vector2 = transform.position;
    }

    void OnMouseDrag()
    {
        //Debug.Log("OnMouseDrag");
        if (identityidentity == Dice.identity.reserve)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
            transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
        }
    }

    void OnMouseUp()
    {
        // If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Drag ended!");
        if (identityidentity == Dice.identity.reserve)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position, Vector2.one, 0);
            Battlefield nearbattlefield = null;
            foreach (var item in collider2Ds)
            {
                Battlefield newbattlefield = item.GetComponent<Battlefield>();
                if(newbattlefield != null)
                {
                    if(nearbattlefield == null)
                    {
                        nearbattlefield = newbattlefield;
                    }
                    else if (Vector2.Distance(transform.position, newbattlefield.transform.position) < Vector2.Distance(transform.position, nearbattlefield.transform.position))
                    {
                        nearbattlefield = newbattlefield;
                    }
                }
            }
            if (nearbattlefield != null)
            {
                bool plase = game.PlaceDice(this, nearbattlefield);
                if (!plase)
                {
                    transform.position = vector2;
                }
            }
            else
            {
                transform.position = vector2;
            }
        }
    }
}
