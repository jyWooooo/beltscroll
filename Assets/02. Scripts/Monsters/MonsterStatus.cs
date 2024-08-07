public class MonsterStatus
{
    public enum GradeType
    {
        Common,
        Rare,
        Magic,
        Legendary,
        Hero,
    }

    public string Name { get; private set; }
    public GradeType Grade { get; private set; }
    public float Speed { get; private set; }
    public float Health { get; private set; }
}