public class UI_PopUp : UI_Base
{
    public override bool Initialize()
    {
        if (!base.Initialize())
            return false;

        GameManager.UIManager.SetCanvas(gameObject, null);
        return true;
    }

    public virtual void ClosePopUpUI()
    {
        GameManager.UIManager.ClosePopUp(this);
    }
}