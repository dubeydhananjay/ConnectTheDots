using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenerateTiles : MonoBehaviour
{
    [SerializeField] private int row;
    [SerializeField] private int col;
    [SerializeField] private Tile tileSample;
    public List<Tile> tiles;
    private static GenerateTiles instance;
    public static GenerateTiles Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            TilesGeneration();
        }
    }

    //Generate 5x5 tiles
    private void TilesGeneration()
    {
        tiles = new List<Tile>();
        int count = 0;
        bool colorChange = false;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Tile tile = Instantiate(tileSample, transform);
                tile.transform.localPosition = new Vector2(j, i);
                tile.name = string.Format("T{0}", count);
                tiles.Add(tile);
                colorChange = !colorChange;
                count++;
            }
        }
        tileSample.gameObject.SetActive(false);
    }

    public Tile TileParent(string tileName,ColorNode node)
    {
        Tile tp = tiles.Where(x => x.name == tileName).First().GetComponent<Tile>();
        tp.SetNode(node);
        return tp;
    }

    public Tile TileFromPos(Vector2 pos)
    {
        Tile tile = tiles.Where(x => (Vector2)x.transform.position == pos).FirstOrDefault();
        return tile;
    }
}
