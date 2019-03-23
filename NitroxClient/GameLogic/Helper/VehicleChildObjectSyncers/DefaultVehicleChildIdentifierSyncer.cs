using NitroxModel.DataStructures.GameLogic;
using UnityEngine;

namespace NitroxClient.GameLogic.Helper.VehicleChildObjectSyncers
{
    public class DefaultVehicleChildIdentifierSyncer : IVehicleChildIdentifierSyncer
    {
        public InteractiveChildObjectIdentifier ExtractId(Component component, string componentPath)
        {
            string guid = GuidHelper.GetGuid(component.gameObject);

            return new InteractiveChildObjectIdentifier(component.GetType(), guid, componentPath);
        }

        public void SetId(GameObject gameObject, InteractiveChildObjectIdentifier childIdentifier)
        {
            GuidHelper.SetNewGuid(gameObject, childIdentifier.Guid);
        }
    }
}
