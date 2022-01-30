using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class UILineRenderer : MaskableGraphic
{
    public List<float> points;

    public UIGraphRenderer graphRenderer;

    public float thickness = 1;

    private Vector2Int gridSize = new Vector2Int();

    public float stepHeight;
    public float pointOffset = 0f;

    private float width;
    private float height;


    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if (graphRenderer != null)
        {
            gridSize = graphRenderer.gridSize;
        }

        width = rectTransform.rect.width / gridSize.x;
        height = rectTransform.rect.height / gridSize.y;


        for(int i=0; i<points.Count; i++)
        {
            DrawPoint(vh, i);
        }

        for (int j = 0; j < points.Count-1; j++)
        {
            Connect(vh, j);
        }

    }
    
    public float GetAngle(Vector2 me, Vector2 target)
    {
        return float.Parse((Mathf.Atan2(target.y - me.y, target.x - me.x) * (180 / Mathf.PI)).ToString());
    }

    protected void DrawPoint(VertexHelper vh, int index)
    {
        float thisPoint = Mathf.Abs((points[index]-pointOffset)/stepHeight);
        float nextPoint = (index >= (points.Count-1)) ? thisPoint : Mathf.Abs((points[index + 1] - pointOffset)/ stepHeight);

        float posX = index * width;

        float startY = thisPoint * height;
        float endY = nextPoint * height;

        Vector2 pointA = new Vector2(posX, startY);
        Vector2 pointB = new Vector2(posX + width, endY);


        Vector2 bottomLeft = new Vector2(-thickness / 2f, 0);
        Vector2 bottomRight = new Vector2(thickness / 2f, 0);
        Vector2 topLeft = new Vector2(-thickness / 2f, 0);
        Vector2 topRight = new Vector2(thickness / 2f, 0);

        float angle = (GetAngle(pointA, pointB)) + 90f;

        bottomLeft = Quaternion.Euler(0, 0, angle) * bottomLeft;
        topLeft = Quaternion.Euler(0, 0, angle) * topLeft;
        topRight = Quaternion.Euler(0, 0, angle) * topRight;
        bottomRight = Quaternion.Euler(0, 0, angle) * bottomRight;

        bottomLeft += pointA;
        bottomRight += pointA;
        topLeft += pointB;
        topRight += pointB;

        UIVertex vert = UIVertex.simpleVert;
        vert.color = color;

        //Create four points at the point
        vert.position = bottomLeft;
        vh.AddVert(vert);

        vert.position = topLeft;
        vh.AddVert(vert);

        vert.position = topRight;
        vh.AddVert(vert);

        vert.position = bottomRight;
        vh.AddVert(vert);

        int offset = index * 4;
        vh.AddTriangle(0 + offset, 1 + offset, 2 + offset);
        vh.AddTriangle(2 + offset, 3 + offset, 0 + offset);

    }
    
    protected void Connect(VertexHelper vh, int index)
    {
        int offset = index * 4;
        vh.AddTriangle(1 + offset, 4 + offset, 7 + offset);
        vh.AddTriangle(7 + offset, 2 + offset, 1 + offset);

    }

    private void Update()
    {
#if UNITY_EDITOR
        if (graphRenderer != null && gridSize != graphRenderer.gridSize)
        {
            this.OnValidate();
            
        }
#endif
    }



}
