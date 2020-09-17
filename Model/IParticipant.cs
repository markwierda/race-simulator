namespace Model
{
    public enum TeamColors
    {
        Red,
        Green,
        Yellow,
        Grey,
        Blue
    }

    public interface IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment IEquipment { get; set; }
        public TeamColors TeamColor { get; set; }
    }
}
