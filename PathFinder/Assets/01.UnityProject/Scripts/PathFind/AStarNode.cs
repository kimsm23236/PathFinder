using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarNode
{
    public TerrainController Terrain { get; private set; }
    public GameObject DestinationObj { get; private set; }

    // A star algorithm
    public float AstarF { get; private set; } = float.MaxValue;
    public float AstarG { get; private set; } = float.MaxValue;
    public float AstarH { get; private set; } = float.MaxValue;
    public AstarNode AstarPrevNode { get; private set; } = default;
    public AstarNode(TerrainController terrain_, GameObject destiationObj_)
    {
        Terrain = terrain_;
        DestinationObj = destiationObj_;
    }

    //! Astar �˰��� ����� ����� �����Ѵ�.
    public void UpdateCost(float gCost, float heuristic, AstarNode prevNode)
    {
        float astarF = gCost + heuristic;

        if(astarF < AstarF)
        {
            AstarG = gCost;
            AstarH = heuristic;
            AstarF = astarF;

            AstarPrevNode = prevNode;
        }   // if : ����� �� ���� ��쿡�� ������Ʈ �Ѵ�.
        else { /* Do Nothing */ }

    }   // UpdateCost()

    //! ������ ����� ����Ѵ�.
    public void ShowCost_Astar()
    {
        GFunc.Log($"TileIdx1D : {Terrain.TileIdx1D}, " +
                    $"F : {AstarF}, G : {AstarG}, H : {AstarH}");
    }   // ShowCost_Astar()
}
