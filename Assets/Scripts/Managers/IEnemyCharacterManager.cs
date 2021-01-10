/// <summary>
/// Interface to initialize common properties in Base classes
/// </summary>
public interface IEnemyCharacterManager
{
    float DetectionRate { get; }

    int FieldOfView { get; }

    float ViewDistance { get; }
}