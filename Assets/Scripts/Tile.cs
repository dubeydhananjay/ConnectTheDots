using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Tile : MonoBehaviour, IPointerDownHandler
{
   [SerializeField] private ConnectingLine connectingLine;
    private ColorNode node;
    public ColorNode Node => node;
    public void SetConnectingLine(ConnectingLine line)
    {
        if (!connectingLine)
        {
            connectingLine = line;
            connectingLine.SetForwardPos(transform.position);
            return;
        }
        if(connectingLine == line)
        {
            connectingLine.SetBackwardPos(transform.position);
            connectingLine = null;
            return;
        }

        if(connectingLine != line)
        {
            connectingLine.SetBackwardPos(transform.position,true);
            line.SetForwardPos(transform.position);
            connectingLine = line;
        }
    }

    public void SetNode(ColorNode node)
    {
        this.node = node;
    }

    public void ClearConnectingLine(ConnectingLine line)
    {
        if(line == connectingLine)
            connectingLine = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!connectingLine) return;
        ColorNode node = connectingLine.transform.parent.GetComponent<ColorNode>();
        TilesDetection.Instance.SetNodeAndLine(node, connectingLine);
    }
}
