public abstract class State<T>
{
    /// <summary>
    /// 当状态被进入时执行这个函数
    /// </summary>
    public abstract void Enter(T entity);

    /// <summary>
    /// 旷工更新状态函数
    /// </summary>
    public abstract void Execute(T entity);

    /// <summary>
    /// 当状态退出时执行这个函数
    /// </summary>
    public abstract void Exit(T entity);

    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="entity">接受者</param>
    /// <param name="message">要处理的消息</param>
    /// <returns>消息是否成功被处理</returns>
    public abstract bool OnMessage(T entity, Telegram message);
}