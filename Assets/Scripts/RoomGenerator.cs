using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour {

    public int Height;
    public int Width;
    public int Border;

    public Sprite Background;
    public Sprite Floor;

    public int BorderLayer;

    public void Generate()
    {

        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var startPoint = new Vector2(-(Width + (Border * 2)) / 2, -(Height + Border * 2)/2);

        // Create Simple Rect Room with Border
        for (var x = 0; x < Width + (Border * 2); x++)
        {
            for (var y = 0; y < Height + (Border * 2); y++)
            {
                var tile = new GameObject();
                var renderer = tile.AddComponent<SpriteRenderer>();

                renderer.sortingLayerName = "Background";

                tile.transform.position = new Vector3(startPoint.x + x, startPoint.y + y);
                tile.name = string.Format("{0}_{1}_", x, y);

                if (x < Border || x > (Border + Width) - 1 || y < Border || y > (Border + Height) - 1)
                {
                    renderer.sprite = Background;
                    tile.name += "BG";
                    var collider = tile.AddComponent<BoxCollider2D>();
                    tile.layer = BorderLayer;
                }
                else
                {
                    renderer.sprite = Floor;
                    tile.name += "FLOOR";
                }

                tile.transform.SetParent(transform);
            }
        }
    }
}
