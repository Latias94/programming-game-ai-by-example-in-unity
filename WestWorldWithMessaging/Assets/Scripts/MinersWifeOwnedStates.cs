using UnityEngine;

public class WifesGlobalState : State<MinersWife>
{
    public static WifesGlobalState Instance { get; private set; }

    static WifesGlobalState()
    {
        Instance = new WifesGlobalState();
    }

    public override void Enter(MinersWife minersWife)
    {
    }

    public override void Execute(MinersWife minersWife)
    {
        if (Random.Range(0, 10) < 1)
        {
            minersWife.GetFSM().ChangeState(VisitBathroom.Instance);
        }
    }

    public override void Exit(MinersWife minersWife)
    {
    }

    public override bool OnMessage(MinersWife minersWife, Telegram message)
    {
        switch (message.Message)
        {
            case MessageType.HiHoneyImHome:
                Debug.Log(string.Format("消息已被 {0} 处理，时间为：{1}", minersWife.Name, Time.time));
                Debug.Log(string.Format("{0}：亲爱的~我来给你做点好吃的！", minersWife.Name));
                minersWife.GetFSM().ChangeState(CookStew.Instance);
                return true;
        }

        return false;
    }
}

// 暂时没用 MinersWife现在只会做家务和上厕所
public class DoHouseWork : State<MinersWife>
{
    public static DoHouseWork Instance { get; private set; }

    static DoHouseWork()
    {
        Instance = new DoHouseWork();
    }

    public override void Enter(MinersWife minersWife)
    {
        Debug.Log(minersWife.Name + "：是时候做点家务活了！");
    }

    public override void Execute(MinersWife minersWife)
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                Debug.Log(minersWife.Name + "：拖拖地~");
                break;
            case 1:
                Debug.Log(minersWife.Name + "：洗洗碗~");
                break;
            case 2:
                Debug.Log(minersWife.Name + "：整理床~");
                break;
        }
    }

    public override void Exit(MinersWife minersWife)
    {
    }

    public override bool OnMessage(MinersWife minersWife, Telegram message)
    {
        return false;
    }
}

public class VisitBathroom : State<MinersWife>
{
    public static VisitBathroom Instance { get; private set; }

    static VisitBathroom()
    {
        Instance = new VisitBathroom();
    }

    public override void Enter(MinersWife minersWife)
    {
        Debug.Log(minersWife.Name + "：走去厕所");
    }

    public override void Execute(MinersWife minersWife)
    {
        Debug.Log(minersWife.Name + "：啊~好舒服！");
        minersWife.GetFSM().RevertToPreviousState();
    }

    public override void Exit(MinersWife minersWife)
    {
        Debug.Log(minersWife.Name + "：离开厕所");
    }

    public override bool OnMessage(MinersWife minersWife, Telegram message)
    {
        return false;
    }
}

public class CookStew : State<MinersWife>
{
    public static CookStew Instance { get; private set; }

    static CookStew()
    {
        Instance = new CookStew();
    }

    public override void Enter(MinersWife minersWife)
    {
        if (!minersWife.IsCooking)
        {
            Debug.Log(string.Format("{0} 把肉放在烤炉里", minersWife.Name));
            // 发送延时消息给自己：这样自己就知道要在一分半后拿出烤肉
            MessageDispatcher.Instance.DispatchMessage(1.5f, minersWife.ID, minersWife.ID, MessageType.StewReady, null);
            minersWife.setCooking(true);
        }
    }

    public override void Execute(MinersWife minersWife)
    {
        Debug.Log(minersWife.Name + "：做晚饭做晚饭~");
    }

    public override void Exit(MinersWife minersWife)
    {
        Debug.Log(minersWife.Name + "：把菜放到桌子上");
    }

    public override bool OnMessage(MinersWife minersWife, Telegram message)
    {
        switch (message.Message)
        {
            case MessageType.StewReady:
                Debug.Log(string.Format("消息已被 {0} 处理，时间为：{1}", minersWife.Name, Time.time));
                Debug.Log(string.Format("{0}：做好饭啦！让我们一起吃饭吧！", minersWife.Name));
                // 做好饭后，立刻发消息通知矿工来吃饭
                MessageDispatcher.Instance.DispatchMessage(0, minersWife.ID, 1, MessageType.StewReady, null);
                // 这时候已经做好饭了
                minersWife.setCooking(false);
                // 做好饭后做家务
                minersWife.GetFSM().ChangeState(DoHouseWork.Instance);
                return true;
        }

        return false;
    }
}