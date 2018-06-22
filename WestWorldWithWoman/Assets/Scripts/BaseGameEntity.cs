using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEntity
{
    /// <summary>
    /// 每个实体具有一个唯一的识别数字
    /// </summary>
    private int m_ID;

    public string Name { get; protected set; }

    /// <summary>
    /// 这是下一个有效的ID，每次 BaseGameEntity 被实例化这个值就被更新
    /// </summary>
    public static int m_iNextValidID { get; private set; }

    protected BaseGameEntity(int id)
    {
        m_ID = id;
    }

    public int ID
    {
        get { return m_ID; }
        set
        {
            m_ID = value;
            m_iNextValidID = m_ID + 1;
        }
    }
    public abstract void EntityUpdate();
}