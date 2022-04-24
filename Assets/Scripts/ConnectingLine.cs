using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConnectingLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private int index;
    private Dictionary<int, Vector2> posDict = new Dictionary<int, Vector2>();
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ResetSizeAndPos();
    }

    //Set next line position
    public void SetForwardPos(Vector2 blockPos)
    {
        Vector3 pos = blockPos;
        pos.z = -1;
        index += 1;
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(index, pos);
        posDict.Add(index, blockPos);
    }

    //Set previous line position
    public void SetBackwardPos(Vector2 blockPos, bool otherLine = false)
    {
        index = posDict.Where(x => x.Value == blockPos).FirstOrDefault().Key;
        blockPos = posDict[otherLine ? index - 1 : index];
        
        Vector3 pos = blockPos;
        pos.z = -1;
        lineRenderer.positionCount = index + 1;
        lineRenderer.SetPosition(index, pos);
        RemoveUnneededPos();
    }

    public void ResetSizeAndPos()
    {
        index = 0;
        lineRenderer.positionCount = 1;
    }

    public void SetLineParent(Transform parent)
    {
        transform.parent = parent;
        transform.position = Vector3.zero;
    }

    public void SetStartPos(Vector2 pos)
    {
        lineRenderer.SetPosition(0, pos);
        RemoveUnneededPos();
        posDict.Clear();
        posDict.Add(index, pos);
    }

    public void RemoveUnneededPos()
    {
        try
        {
            for (int i = posDict.Count - 1; i > index; i--)
            {
                Tile tile = GenerateTiles.Instance.TileFromPos(posDict[i]);
                tile.ClearConnectingLine(this);
                posDict.Remove(i);
            }
        }
        catch(System.Exception)
        {
            Debug.Log("No tiles to be removed");
        }
    }    
}
