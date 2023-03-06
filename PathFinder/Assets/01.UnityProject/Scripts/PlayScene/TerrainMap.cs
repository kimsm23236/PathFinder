using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMap : TileMapController
{
    private const string TERRAIN_TILEMAP_OBJ_NAME = "TerrainTilemap";

    private Vector2Int mapCellSize = default;
    private Vector2 mapCellGap = default;
    private List<TerrainController> allTerrains = default;

    public override void InitAwake(MapBoard mapController_)
    {
        this.tileMapObjName = TERRAIN_TILEMAP_OBJ_NAME;
        base.InitAwake(mapController_);
        allTerrains = new List<TerrainController>();

        //{ Ÿ���� x�� ������ ��ü Ÿ���� ���� ���� ����, ���� ����� �����Ѵ�.
        mapCellSize = Vector2Int.zero;
        float tempTileY = allTileObjs[0].transform.localPosition.y;
        for (int i = 0; i < allTileObjs.Count; i++)
        {
            if (tempTileY.IsEqual(allTileObjs[i].transform.localPosition.y) == false)
            {
                mapCellSize.x = i;
                break;
            }   // if : ù��° Ÿ���� y ��ǥ�� �޶����� ���� �������� ���� ���� �� ũ���̴�.
        }
        // ��ü Ÿ���� ���� ���� ���� �� ũ��� ���� ���� ���� ���� �� ũ���̴�.
        mapCellSize.y = Mathf.FloorToInt(allTileObjs.Count / mapCellSize.x);
        // } Ÿ���� x�� ������ ��ü Ÿ���� ���� ���� ����, ���� ����� �����Ѵ�.

        // { x �� ���� �� Ÿ�ϰ�, y �� ���� �� Ÿ�� ������ ���� ���������� Ÿ�� ���� �����Ѵ�.
        mapCellGap = Vector2.zero;
        mapCellGap.x = allTileObjs[1].transform.localPosition.x - allTileObjs[0].transform.localPosition.x;
        mapCellGap.y = allTileObjs[mapCellSize.x].transform.localPosition.y - allTileObjs[0].transform.localPosition.y;
        // } x �� ���� �� Ÿ�ϰ�, y �� ���� �� Ÿ�� ������ ���� ���������� Ÿ�� ���� �����Ѵ�.
    }

    private void Start()
    {
        // { Ÿ�ϸ��� �Ϻθ� ���� Ȯ���� �ٸ� Ÿ�Ϸ� ��ü�ϴ� ����
        GameObject changeTilePrefab = ResManager.Instance.terrainPrefabs[RDefine.TERRAIN_PREF_OCEAN];
        const float CHANGE_PERCENTAGE = 15.0f;
        float correctChangePercentage = allTileObjs.Count * (CHANGE_PERCENTAGE / 100.0f);
        // �ٴٷ� ��ü�� Ÿ���� ������ ����Ʈ ���·� �����ؼ� ���´�.
        List<int> changedTileResult = GFunc.CreateList(allTileObjs.Count, 1);
        changedTileResult.Shuffle();

        GameObject tempChangeTile = default;
        for(int i = 0; i < allTileObjs.Count; i++)
        {
            if (correctChangePercentage <= changedTileResult[i]) { continue; }

            // �������� �ν��Ͻ�ȭ�ؼ� ��ü�� Ÿ���� Ʈ�������� ī���Ѵ�.
            tempChangeTile = Instantiate(changeTilePrefab, tileMap.transform);
            tempChangeTile.name = changeTilePrefab.name;
            tempChangeTile.SetLocalScale(allTileObjs[i].transform.localScale);
            tempChangeTile.SetLocalPos(allTileObjs[i].transform.localPosition);

            allTileObjs.Swap(ref tempChangeTile, i);
            tempChangeTile.DestroyObj();
        }   // loop : ������ ������ ������ ���� Ÿ�ϸʿ� �ٴٸ� �����ϴ� ����
        // } Ÿ�ϸ��� �Ϻθ� ���� Ȯ���� �ٸ� Ÿ�Ϸ� ��ü�ϴ� ����

        // { ������ �����ϴ� Ÿ���� ������ �����ϰ�, ��Ʈ�ѷ��� ĳ���ϴ� ����

        // } ������ �����ϴ� Ÿ���� ������ �����ϰ�, ��Ʈ�ѷ��� ĳ���ϴ� ����
    }
    //! �ʱ�ȭ�� Ÿ���� ������ ������ ���� ����, ���� ũ�⸦ �����Ѵ�.
    public Vector2Int GetCellSize()
    {
        return mapCellSize;
    }
    //! �ʱ�ȭ�� Ÿ���� ������ ������ Ÿ�� ������ ���� �����Ѵ�.
    public Vector2 GetCellGap()
    {
        return mapCellGap;
    }
    //! �ε����� �ش��ϴ� Ÿ���� �����Ѵ�.
    public TerrainController GetTile(int tileIdx1D)
    {
        if(allTerrains.IsValid(tileIdx1D))
        {
            return allTerrains[tileIdx1D];
        }
        return default;
    }   // GetTile()
}