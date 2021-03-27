/// <summary>
/// Interface to initialize common properties in Base classes
/// </summary>
public interface IEnemyCommonStats
{
    float   DetectionRate   { get; }
    int     FieldOfView     { get; }
    float   ViewDistance    { get; }
    float   ListenDistance  { get; }
    float   MaxHitPoints    { get; set; }
    bool    IsListening     { get; set; }
}