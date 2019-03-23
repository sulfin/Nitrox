using System;

namespace NitroxModel.DataStructures.GameLogic
{
    [Serializable]
    public class InteractiveChildObjectIdentifier
    {
        public Type Type { get; }
        public string Guid { get; }
        public string GameObjectNamePath { get; }

        public InteractiveChildObjectIdentifier(Type type, string guid, string gameObjectNamePath)
        {
            Type = type;
            Guid = guid;
            GameObjectNamePath = gameObjectNamePath;
        }

        public override string ToString()
        {
            return "[InteractiveChildObjectIdentifier -  Type: " + Type + " Guid: " + Guid + " GameObjectNamePath: " + GameObjectNamePath + "]";
        }
    }
}
