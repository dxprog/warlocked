namespace Warlocked.Entities.Units
{
    public struct ResourceBundle
    {
        public enum Resource { Fuel, Gold }

        public Resource ResourceType;
        public int Amount;
    }
}
