public class StateMachine<T>
{
    private T m_pOwner;
    private State<T> m_pCurrentState;
    private State<T> m_pPreviousState;
    private State<T> m_pGlobalState;

    public StateMachine(T owner)
    {
        m_pOwner = owner;
    }

    public void SetCurrentState(State<T> state)
    {
        m_pCurrentState = state;
    }

    public void SetPreviousState(State<T> state)
    {
        m_pPreviousState = state;
    }

    public void SetGlobalState(State<T> state)
    {
        m_pGlobalState = state;
    }

    public void StateMachineUpdate()
    {
        // 如果有一个全局状态存在，调用它的执行方法
        if (m_pGlobalState != null)
        {
            m_pGlobalState.Execute(m_pOwner);
        }

        if (m_pCurrentState != null)
        {
            m_pCurrentState.Execute(m_pOwner);
        }
    }

    public void ChangeState(State<T> newState)
    {
        m_pPreviousState = m_pCurrentState;
        m_pCurrentState.Exit(m_pOwner);
        m_pCurrentState = newState;
        m_pCurrentState.Enter(m_pOwner);
    }

    /// <summary>
    /// 返回之前的状态
    /// </summary>
    public void RevertToPreviousState()
    {
        ChangeState(m_pPreviousState);
    }

    public State<T> CurrentState()
    {
        return m_pCurrentState;
    }

    public State<T> PreviousState()
    {
        return m_pPreviousState;
    }

    public State<T> GlobalState()
    {
        return m_pGlobalState;
    }

    public bool IsInState(State<T> state)
    {
        return m_pCurrentState == state;
    }
}