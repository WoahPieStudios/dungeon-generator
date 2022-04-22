using UnityEngine;

namespace DungeonGenerator.Extensions
{
    /// <summary>
    /// Fields with the ReadOnly attribute will not be modified in the inspector.
    /// </summary>
    /// <remarks>
    /// Referenced from: http://answers.unity.com/answers/801283/view.html
    /// </remarks>
    public class ReadOnlyAttribute : PropertyAttribute
    {
        
    }
}
