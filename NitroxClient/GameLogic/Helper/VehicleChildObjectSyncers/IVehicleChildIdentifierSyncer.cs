using NitroxModel.DataStructures.GameLogic;
using UnityEngine;

namespace NitroxClient.GameLogic.Helper.VehicleChildObjectSyncers
{
    interface IVehicleChildIdentifierSyncer
    {
        InteractiveChildObjectIdentifier ExtractId(Component component, string componentPath);
        void SetId(GameObject gameObject, InteractiveChildObjectIdentifier childIdentifier);
    }
}
