public class MonsterWerebear : MonsterBase
{
    public override MonsterStatus LoadData()
    {
        return GameManager.MonsterManager.MonsterData.GetMonsterData("Werebear");
    }
}