using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftUiButtons : MonoBehaviour
{
    //! A* find path ��ư�� ���� ���
    public void OnClickAstarFindBtn()
    {
        PathFinder.Instance.FindPath_Astar();
        GFunc.Log("astar button click");
    }
}
