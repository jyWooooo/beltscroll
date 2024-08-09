public class UI_MainScene : UI_Scene
{
    private enum Texts
    {
        textUserID,
    }

    private enum Objects
    {
        UI_MonsterInfo,
        UI_StageInfo,
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindText(typeof(Texts));
        BindObject(typeof(Objects));
        GetText((int)Texts.textUserID).text = $"UserID: {GameManager.DataManager.UserID}";

        return true;
    }
}