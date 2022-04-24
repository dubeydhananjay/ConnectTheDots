using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class ColorNode : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private ConnectingLine connectingLine;
    public ConnectingLine Line => connectingLine;
    [SerializeField]
    private ColorNode otherColorNode;

    private void Awake()
    {
        otherColorNode = FindObjectsOfType<ColorNode>().Where(x => x != this && x.gameObject.tag == gameObject.tag).FirstOrDefault(); // find the other node with same tag
        connectingLine = FindObjectsOfType<ConnectingLine>().Where(x => x.gameObject.tag == gameObject.tag).FirstOrDefault();
    }

    //Set node to specific tile
    public void SetTileParent(string tileName)
    {
       transform.parent = GenerateTiles.Instance.TileParent(tileName,this).transform;
       Vector3 pos = Vector3.zero;
       pos.z = -1;
       transform.localPosition = pos;
    }

    private void ResetConnectingLine()
    {
        connectingLine.ResetSizeAndPos();
        connectingLine.SetStartPos(transform.position);
        SetLineParent();
    }

    private void SetLineParent()
    {
        connectingLine.SetLineParent(transform);
    }

    //Set this node and the connecting line as current node and current line
    public void OnPointerDown(PointerEventData eventData)
    {
        TilesDetection.Instance.RemoveTiles();
        TilesDetection.Instance.SetNodeAndLine(this, connectingLine);
        ResetConnectingLine();
    }

    public bool OtherNode(ColorNode node, Tile prevTile)
    {  
        if (node == otherColorNode)
        {
            connectingLine.SetForwardPos(node.transform.position);
            return true;
        }
        if(node == this)
        {
            connectingLine.SetBackwardPos(node.transform.position);
            ResetConnectingLine();
            return false;
        }
        return false;
    }
}
