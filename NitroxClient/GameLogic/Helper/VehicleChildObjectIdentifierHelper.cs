using System;
using System.Collections.Generic;
using NitroxClient.GameLogic.Helper.VehicleChildObjectSyncers;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.Logger;
using UnityEngine;

namespace NitroxClient.GameLogic.Helper
{
    public class VehicleChildObjectIdentifierHelper
    {
        private static IVehicleChildIdentifierSyncer defaultChildIdentifierSyncer = new DefaultVehicleChildIdentifierSyncer();

        private static readonly Dictionary<Type, IVehicleChildIdentifierSyncer> childIdentifierSyncersByType = new Dictionary<Type, IVehicleChildIdentifierSyncer>()
        {
            { typeof(Openable), defaultChildIdentifierSyncer },
            { typeof(CyclopsLocker), defaultChildIdentifierSyncer  },
            { typeof(Fabricator), defaultChildIdentifierSyncer },
            { typeof(FireExtinguisherHolder), defaultChildIdentifierSyncer },
            { typeof(StorageContainer), defaultChildIdentifierSyncer },
            { typeof(SeamothStorageContainer), defaultChildIdentifierSyncer },
            { typeof(VehicleDockingBay), defaultChildIdentifierSyncer },
            { typeof(DockedVehicleHandTarget), defaultChildIdentifierSyncer },
            { typeof(UpgradeConsole), defaultChildIdentifierSyncer },
            { typeof(DockingBayDoor), defaultChildIdentifierSyncer },
            { typeof(CyclopsDecoyLoadingTube), defaultChildIdentifierSyncer },
            { typeof(EnergyMixin), new EnergyMixinVehicleChildIdentifierSyncer() }
        };

        public static  List<InteractiveChildObjectIdentifier> ExtractGuidsOfInteractiveChildren(GameObject constructedObject)
        {
            List<InteractiveChildObjectIdentifier> ids = new List<InteractiveChildObjectIdentifier>();

            string constructedObjectsName = constructedObject.GetFullName() + "/";

            foreach (KeyValuePair<Type, IVehicleChildIdentifierSyncer> syncerWithType in childIdentifierSyncersByType)
            {
                Type type = syncerWithType.Key;
                IVehicleChildIdentifierSyncer syncer = syncerWithType.Value;

                Component[] components = constructedObject.GetComponentsInChildren(type, true);

                foreach (Component component in components)
                {
                    string componentName = component.gameObject.GetFullName();
                    string componentPath = componentName.Replace(constructedObjectsName, "");

                    ids.Add(syncer.ExtractId(component, componentPath));
                }
            }

            return ids;
        }

        public static void SetInteractiveChildrenGuids(GameObject constructedObject, List<InteractiveChildObjectIdentifier> interactiveChildIdentifiers)
        {
            foreach (InteractiveChildObjectIdentifier childIdentifier in interactiveChildIdentifiers)
            {
                IVehicleChildIdentifierSyncer syncer = childIdentifierSyncersByType[childIdentifier.Type];
                Transform transform = constructedObject.transform.Find(childIdentifier.GameObjectNamePath);

                if (transform != null)
                {
                    syncer.SetId(transform.gameObject, childIdentifier);
                }
                else
                {
                    Log.Error("Error GUID tagging interactive child due to not finding it: " + childIdentifier.GameObjectNamePath);
                }
            }
        }
    }
}
