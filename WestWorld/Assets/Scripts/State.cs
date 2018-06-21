public abstract class State
{
    /// <summary>
    /// 当状态被进入时执行这个函数
    /// </summary>
    public abstract void Enter(Miner miner);

    /// <summary>
    /// 旷工更新状态函数
    /// </summary>
    public abstract void Execute(Miner miner);

    /// <summary>
    /// 当状态退出时执行这个函数
    /// </summary>
    public abstract void Exit(Miner miner);
}