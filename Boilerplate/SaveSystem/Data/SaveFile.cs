using System;
using System.Collections.Generic;
using Facts.Data;
using UnityEngine;

namespace SaveSystem.Runtime
{
    public struct SaveFile
    {
        public readonly LinkedList<KeyValuePair<BooleanFactEnum, bool>> booleans;
        public readonly LinkedList<KeyValuePair<StringFactEnum, string>> strings;
        public readonly LinkedList<KeyValuePair<IntFactEnum, int>> ints;
        public readonly LinkedList<KeyValuePair<IntFactEnum, Enum>> enums;
        public readonly LinkedList<KeyValuePair<FloatFactEnum, float>> floats;
        public readonly LinkedList<KeyValuePair<TransformFactEnum, Transform>> transforms;
        public readonly LinkedList<KeyValuePair<GameObjectFactEnum, GameObject>> gameobjects;
        public readonly LinkedList<KeyValuePair<ComponentFactEnum, Component>> components;
        public readonly LinkedList<KeyValuePair<ComponentFactEnum, ISerializationCallbackReceiver>> serializable;
    }
}
