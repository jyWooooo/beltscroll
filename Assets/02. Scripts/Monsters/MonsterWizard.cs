public class MonsterWizard : MonsterBase
{
    public override MonsterStatus LoadData()
    {
        return GameManager.MonsterManager.MonsterData.GetMonsterData("Wizard");
    }
}