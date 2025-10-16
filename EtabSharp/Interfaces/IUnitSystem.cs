using EtabSharp.UnitSystem.Models;

namespace EtabSharp.Interfaces;

public interface IUnitSystem
{
    /// <summary>
    /// Sets the unit system using predefined unit system types (US or Metric).
    /// </summary>
    /// <param name="systemType">The unit system type to set</param>
    int SetPresentUnits(Units systemType);

    /// <summary>
    /// Gets detailed information about the current unit system.
    /// </summary>
    /// <returns>Unit system information including force, length, and temperature units</returns>
    Units GetPresentUnits();

}