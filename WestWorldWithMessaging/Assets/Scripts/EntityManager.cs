using System.Collections.Generic;

/// <summary>
/// 管理所有实体的数据库
/// </summary>
public class EntityManager
{
    /// <summary>
    /// 存实体的字典，key 为实体的 id
    /// </summary>
    private Dictionary<int, BaseGameEntity> entityMap;

    private EntityManager()
    {
        entityMap = new Dictionary<int, BaseGameEntity>();
    }

    /// <summary>
    /// 单例
    /// </summary>
    public static EntityManager Instance { get; private set; }

    static EntityManager()
    {
        Instance = new EntityManager();
    }

    public void RegisterEntity(BaseGameEntity entity)
    {
        entityMap.Add(entity.ID, entity);
    }
    /// <summary>
    /// 根据 id 得到实体
    /// </summary>
    public BaseGameEntity GetEntityFromId(int id)
    {
        BaseGameEntity entity;
        bool result = entityMap.TryGetValue(id, out entity);
        return result ? entity : null;
    }

    public void RemoveEntity(int id)
    {
        entityMap.Remove(id);
    }
}