using Assets.ECS;

public class SystemUpdateSunlightRotation : SingletonSystem<OrbitalLocation, SunLight>
{
    protected override void Handle(OrbitalLocation orbitalLocation, SunLight sunLight)
    {
        sunLight.transform.localRotation = orbitalLocation.SunlightDirection;
    }
}