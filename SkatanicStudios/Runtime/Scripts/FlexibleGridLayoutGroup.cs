using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkatanicStudios.UI { 

  public class FlexibleGridLayoutGroup : LayoutGroup
    {
        public enum FitType
        {
            Square,
            Width,
            Height,
            FixedRows,
            FixedColumns
        }

        public FitType fitType;
        public Vector2 spacing;
        public Vector2 offset = new Vector2(0, 0);
        public Vector2 cellSize;

        public int rows;
        public int columns;

        private int itemsPerRow;

        public bool centerLastRow;

        public bool fitX;
        public bool fitY;
        public bool squareCells;

        public override void CalculateLayoutInputHorizontal()
        {
            m_minHeight = 0;

            base.CalculateLayoutInputHorizontal();

            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            int numChildren = rectChildren.Count;

            if (numChildren != 0)
            {
                float sqrt = Mathf.Sqrt(numChildren);


                if (fitType == FitType.Height || fitType == FitType.Width || fitType == FitType.Square)
                {
                    fitX = true;
                    fitY = true;

                    rows = Mathf.CeilToInt(sqrt);
                    columns = Mathf.CeilToInt(sqrt);

                    if (fitType == FitType.Height)
                    {
                        columns = Mathf.CeilToInt(numChildren / (float)rows);
                    }
                    else if (fitType == FitType.Width)
                    {
                        rows = Mathf.CeilToInt(numChildren / (float)columns);
                    }

                }
                else
                {
                    if (fitType == FitType.FixedRows)
                    {
                        columns = Mathf.CeilToInt(numChildren / (float)rows); ;
                    }
                    else if (fitType == FitType.FixedColumns)
                    {
                        rows = Mathf.CeilToInt(numChildren / (float)columns);
                    }
                }
            }
            




            int rowCount = 0;
            int columnCount = 0;


            float cellWidth = parentWidth / (float)columns;
            float cellHeight = parentHeight / (float)rows;

            cellWidth = cellWidth - (padding.left / (float)columns) - (padding.right / (float)columns);
            cellHeight = cellHeight - (padding.top / (float)rows) - (padding.bottom / (float)rows);

            if (squareCells)
            {
                if (cellWidth < cellHeight)
                {
                    cellHeight = cellWidth;
                }
                else
                {
                    cellWidth = cellHeight;
                }
            }

            if (squareCells)
            {
                cellSize.y = cellSize.x;
            }
            else
            {
                cellSize.x = (fitX ? cellWidth : cellSize.x);
                cellSize.y = (fitY ? cellHeight : cellSize.y);
            }


            float currentPositionX = 0;
            float currentPositionY = 0;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                rowCount = i / columns;
                columnCount = i % (int)columns;

                if (columnCount == 0)
                {
                    currentPositionX = 0;
                }

                var item = rectChildren[i];

                //float spacingX = (i == 0) ? 0 : (spacing.x / (float)((columns > 1) ? (columns - 1) : columns));
                float spacingX = (i == 0) ? 0 : spacing.x;
                float spacingY = (i == 0) ? 0 : spacing.y;
                //float spacingY = (i == 0) ? 0 : (spacing.y / (float)((rows > 1) ? (rows - 1) : rows));

                float xPos = (cellSize.x * columnCount) + (spacingX * columnCount) + padding.left + (offset.x * rowCount) + currentPositionX;

                float yPos = (cellSize.y * rowCount) + (spacingY * rowCount) + padding.top + (offset.y * rowCount) + currentPositionY;
                
                if (m_ChildAlignment == TextAnchor.MiddleCenter)
                {
                    xPos += ((parentWidth / 2f)) - ((cellSize.x /2f) * columns); 
                }
                else if (centerLastRow && (rowCount + 1) == rows)
                {
                    xPos += ((parentWidth / 2f)) - ((cellSize.x / 2f) * columns);
                }

                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);

            }

            m_minHeight = (cellSize.y + padding.bottom) * rows + (spacing.y);


        }

        private float m_minHeight;
        public override float minHeight { get { return m_minHeight; } }


        public override void CalculateLayoutInputVertical()
        {

        }

        public override void SetLayoutHorizontal()
        {
        }

        public override void SetLayoutVertical()
        {

        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
        }
    }
}