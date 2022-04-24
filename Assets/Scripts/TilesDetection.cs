using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesDetection : MonoBehaviour
{
    private Vector2 touchPoint = Vector2.zero;
    new private string name = "";
    private ConnectingLine connectingLine;
    private ColorNode colorNode;
    private List<Tile> tiles;
    private static TilesDetection instance;
    public static TilesDetection Instance => instance;
    private bool detect = true;
    private Tile prevTile;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            tiles = new List<Tile>();
        }
    }

    //Check if the touch input is detected. Check if tiles or color nodes are detected
    private void Update()
    {
        if(Input.GetMouseButton(0) && detect)
        {
            touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hitInformation = Physics2D.Raycast(touchPoint, Camera.main.transform.forward);
            if (!hitInformation) return;
            if (name == hitInformation.collider.name) return;
            name = hitInformation.collider.name;

            ColorNode currentNode = hitInformation.collider.GetComponent<ColorNode>();
            if (colorNode)
            {
                if (currentNode && colorNode.OtherNode(currentNode, prevTile))
                {
                    detect = false;
                    connectingLine = null;
                    prevTile = null;
                }
            }
            if (!connectingLine) return;
            ProcessTiles(hitInformation);
        }

        if (Input.GetMouseButtonUp(0)) detect = true;
    }

    private void AddTile(Tile tile)
    {
        if (tiles.Contains(tile)) return;
        tiles.Add(tile);
    }

    public void RemoveTiles()
    {
        //for (int i = 0; i < tiles.Count; i++)
        //    tiles[i].ClearConnectingLine();
        tiles.Clear();
    }

    private void ProcessTiles(RaycastHit2D hitInformation)
    {
        Tile tile = hitInformation.collider.GetComponent<Tile>();
        if (!tile) return;
        
        if (tile.Node) return;
        if (!IsNeighborTile(tile, prevTile)) return;
        tile.SetConnectingLine(connectingLine);
        AddTile(tile);
        prevTile = tile;
    }

    private bool IsNeighborTile(Tile currentTile, Tile prevTile)
    {
        if (!prevTile) return true;
        Vector2 curPos = currentTile.transform.position;
        Vector2 prevPos = prevTile.transform.position;
        
        if (curPos.x == prevPos.x && curPos.y == prevPos.y + 1)
            return true;
        if (curPos.x == prevPos.x && curPos.y == prevPos.y - 1)
            return true;
        if (curPos.x == prevPos.x + 1 && curPos.y == prevPos.y)
            return true;
        if (curPos.x == prevPos.x - 1 && curPos.y == prevPos.y)
            return true;
        return false;
    }


    public void SetNodeAndLine(ColorNode node, ConnectingLine line)
    {
        colorNode = node;
        connectingLine = line;
        prevTile = null;
    }

}
