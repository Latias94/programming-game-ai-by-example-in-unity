public class MinersWife : BaseGameEntity
{
    private StateMachine<MinersWife> m_pStateMachine;
    private LocationType m_Location;


    public MinersWife(int id, string name) : base(id)
    {
        Name = "矿工妻子 " + name;
        m_pStateMachine = new StateMachine<MinersWife>(this);
        m_pStateMachine.SetCurrentState(DoHouseWork.Instance); // 其实可以忽略这行，因为有全局状态的存在
        m_pStateMachine.SetGlobalState(WifesGlobalState.Instance);
    }

    public override void EntityUpdate()
    {
        m_pStateMachine.StateMachineUpdate();
    }

    public StateMachine<MinersWife> GetFSM()
    {
        return m_pStateMachine;
    }

    // 这边和 Miner 类差不多，不过懒得再给妻子设定多一个地点枚举了
    public void ChangeLocation(LocationType loc)
    {
        m_Location = loc;
    }
}