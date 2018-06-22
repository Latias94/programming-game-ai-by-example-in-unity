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
}