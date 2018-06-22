public class Miner : BaseGameEntity
{
    /// <summary>
    /// 旷工当前所处的位置
    /// </summary>
    private LocationType m_Location;

    /// <summary>
    /// 旷工的包中装了多少金块
    /// </summary>
    private int m_iGoldCarried;

    /// <summary>
    /// 旷工在银行存了多少金块
    /// </summary>
    private int m_iMoneyInBank;

    /// <summary>
    /// 口渴程度，值越高，旷工越口渴
    /// </summary>
    private int m_iThirst;

    /// <summary>
    /// 疲倦程度,值越高，旷工越疲倦
    /// </summary>
    private int m_iFatigue;

    /// <summary>
    /// 银行中有多少金块 矿工才会感到安心
    /// </summary>
    public int ComfortLevel = 5;

    /// <summary>
    /// 旷工的包最多能装多少金块
    /// </summary>
    public int MaxNuggets = 3;

    /// <summary>
    /// 超过这数字会感到口渴
    /// </summary>
    public int ThirstLevel = 5;

    /// <summary>
    /// 超过这数字会感到疲倦
    /// </summary>
    public int TirednessThreshold = 5;

    private StateMachine<Miner> m_stateMachine;

    public Miner(int id, string name) : base(id)
    {
        Name = "矿工 " + name;
        m_Location = LocationType.Shack;
        m_iGoldCarried = 0;
        m_iMoneyInBank = 0;
        m_iThirst = 0;
        m_iFatigue = 0;
        m_stateMachine = new StateMachine<Miner>(this);
        m_stateMachine.SetCurrentState(GoHomeAndSleepTilRested.Instance);
    }

    public StateMachine<Miner> GetFSM()
    {
        return m_stateMachine;
    }

    /// <summary>
    /// 等于 Update 函数，在 GameManager 内被调用
    /// </summary>
    public override void EntityUpdate()
    {
        m_iThirst += 1;
        m_stateMachine.StateMachineUpdate();
    }

    public LocationType Location()
    {
        return m_Location;
    }

    /// <summary>
    /// 身上有多少金块
    /// </summary>
    public int GoldCarried()
    {
        return m_iGoldCarried;
    }

    /// <summary>
    /// 银行中存了多少金块
    /// </summary>
    public int Wealth()
    {
        return m_iMoneyInBank;
    }

    /// <summary>
    /// 改变矿工所在地点
    /// </summary>
    public void ChangeLocation(LocationType loc)
    {
        m_Location = loc;
    }

    /// <summary>
    /// 增加矿工拥有的金块
    /// </summary>
    public void AddToGoldCarried(int val)
    {
        m_iGoldCarried += val;
        if (m_iGoldCarried < 0) m_iGoldCarried = 0;
    }

    /// <summary>
    /// 设定矿工拥有的金块数
    /// </summary>
    public void SetGoldCarried(int val)
    {
        m_iGoldCarried = val;
    }

    public bool PocketsFull()
    {
        return m_iGoldCarried >= MaxNuggets;
    }

    /// <summary>
    /// 增加矿工在银行存的金块
    /// </summary>
    public void AddToWealth(int val)
    {
        m_iMoneyInBank += val;
        if (m_iMoneyInBank < 0) m_iMoneyInBank = 0;
    }

    /// <summary>
    /// 设定矿工在银行存的金块数
    /// </summary>
    void SetWealth(int val)
    {
        m_iMoneyInBank = val;
    }

    /// <summary>
    /// 是否口渴
    /// </summary>
    public bool Thirsty()
    {
        return m_iThirst >= ThirstLevel;
    }

    /// <summary>
    /// 用银行的金块买威士忌 并且喝了它
    /// </summary>
    public void BuyAndDrinkAWhiskey()
    {
        m_iThirst = 0;
        m_iMoneyInBank -= 2;
    }

    /// <summary>
    /// 是否已经筋疲力尽
    /// </summary>
    public bool Fatigued()
    {
        return m_iFatigue > TirednessThreshold;
    }

    /// <summary>
    /// 减少疲倦程度，矿工更有精力
    /// </summary>
    public void DecreaseFatigue()
    {
        m_iFatigue -= 1;
    }

    /// <summary>
    /// 增加疲倦程度，矿工更累
    /// </summary>
    public void IncreaseFatigue()
    {
        m_iFatigue += 1;
    }
}