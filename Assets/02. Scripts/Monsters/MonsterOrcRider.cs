public class MonsterOrcRider : MonsterBase
{
    public override MonsterStatus LoadData()
    {
        return GameManager.MonsterManager.MonsterData.GetMonsterData("Orc rider");
    }
}