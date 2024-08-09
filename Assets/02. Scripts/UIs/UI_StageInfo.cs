using TMPro;

public class UI_StageInfo : UI_Scene
{
    private enum Texts
    {
        textStageCnt,
    }

    private TextMeshProUGUI _stageCnt;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindText(typeof(Texts));
        _stageCnt = GetText((int)Texts.textStageCnt);
        GameManager.StageManager.OnNextStageMoveStarted += RefreshStageCnt;

        return true;
    }

    public void RefreshStageCnt(int stageCnt)
    {
        _stageCnt.text = stageCnt.ToString();
    }
}