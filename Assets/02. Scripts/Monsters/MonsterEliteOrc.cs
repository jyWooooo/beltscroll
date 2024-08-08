public class MonsterEliteOrc : MonsterBase
{
    public override MonsterStatus LoadData()
    {
        return GameManager.MonsterManager.MonsterData.GetMonsterData("Elite Orc");
    }
}