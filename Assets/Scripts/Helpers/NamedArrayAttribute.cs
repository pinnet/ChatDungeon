using UnityEngine;
namespace Helpers.CustomEditor
{
    public class NamedArrayAttribute : PropertyAttribute
    {
        public string[] names;
        public NamedArrayAttribute(string[] names) { this.names = names; }
    }
}