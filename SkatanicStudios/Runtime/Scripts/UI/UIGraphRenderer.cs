using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIGraphRenderer : Graphic
{
    public Vector2Int gridSize = new Vector2Int(1, 1);
    public float thickness = 1;

    private float cellWidth;
    private float cellHeight;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if(gridSize.y == 0)
        {
            gridSize.y = 1;
        }
        if (gridSize.x == 0)
        {
            gridSize.x = 1;
        }

        cellHeight = rectTransform.rect.height / gridSize.y;
        cellWidth = rectTransform.rect.width / gridSize.x;

        UIVertex v = UIVertex.simpleVert;
        v.color = color;

        int count = 0;
        for(int x=0; x<gridSize.x; x++)
        {
            for(int y=0; y<gridSize.y; y++)
            {
                float xPos = cellWidth * x;
                float yPos = cellHeight * y;

                //Create four vertices
                v.position = new Vector3(xPos, yPos);
                vh.AddVert(v);

                v.position = new Vector3(xPos, yPos + cellHeight);
                vh.AddVert(v);


                v.position = new Vector3(xPos + cellWidth, yPos + cellHeight);
                vh.AddVert(v);


                v.position = new Vector3(xPos + cellWidth, yPos);
                vh.AddVert(v);

                float widthSqr = thickness * thickness;
                float distanceSqr = widthSqr / 2f;
                float distance = Mathf.Sqrt(distanceSqr);

                //Create inner vertices 
                v.position = new Vector3(xPos + distance, yPos + distance);
                vh.AddVert(v);

                v.position = new Vector3(xPos + distance, yPos + (cellHeight - distance));
                vh.AddVert(v);


                v.position = new Vector3(xPos + (cellWidth - distance), yPos + (cellHeight-distance));
                vh.AddVert(v);


                v.position = new Vector3(xPos + (cellWidth-distance), yPos + distance);
                vh.AddVert(v);

                //Connect them up into triangles
                int offset = count * 8;


                //Left Edge
                vh.AddTriangle(offset + 0, offset + 1, offset + 5);
                vh.AddTriangle(offset + 5, offset + 4, offset + 0);

                //Top Edge
                vh.AddTriangle(offset + 1, offset + 2, offset + 6);
                vh.AddTriangle(offset + 6, offset + 5, offset + 1);

                //Right Edge
                vh.AddTriangle(offset + 2, offset + 3, offset + 7);
                vh.AddTriangle(offset + 7, offset + 6, offset + 2);

                //Bottom Edge
                vh.AddTriangle(offset + 3, offset + 0, offset + 4);
                vh.AddTriangle(offset + 4, offset + 7, offset + 3);

                count++;

            }
        }

    }



}
