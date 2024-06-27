using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasAllEquipment : Conditional {
    private Humanoid humanoid;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
    }

    public override TaskStatus OnUpdate() {
        bool hasAllEquipment = true;

        if(humanoid.GetMainHandItem() != null) {
            if(humanoid.GetMainHandItem().amount == 0) {
                hasAllEquipment = false;
            }
        }

        if (humanoid.GetSecondaryHandItem() != null) {
            if (humanoid.GetSecondaryHandItem().amount == 0) {
                hasAllEquipment = false;
            }
        }

        if (humanoid.GetHeadItem() != null) {
            if (humanoid.GetHeadItem().amount == 0) {
                hasAllEquipment = false;
            }
        }

        if (humanoid.GetBootsItem() != null) {
            if (humanoid.GetBootsItem().amount == 0) {
                hasAllEquipment = false;
            }
        }

        if (humanoid.GetNecklaceItem() != null) {
            if (humanoid.GetNecklaceItem().amount == 0) {
                hasAllEquipment = false;
            }
        }

        if (humanoid.GetRingItem() != null) {
            if (humanoid.GetRingItem().amount == 0) {
                hasAllEquipment = false;
            }
        }
        if(hasAllEquipment) {
            return TaskStatus.Success;
        } else {
            return TaskStatus.Failure;
        }

    }
}
