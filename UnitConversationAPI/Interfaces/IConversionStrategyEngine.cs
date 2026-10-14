namespace UnitConversationAPI.Interfaces
{
    public interface IConversionStrategyEngine
    {
        IConversionStrategy GetStrategy(string category);

    }
}
