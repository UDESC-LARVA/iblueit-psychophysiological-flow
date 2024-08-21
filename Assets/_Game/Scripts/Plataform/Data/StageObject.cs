using System;

namespace Ibit.Plataform.Data
{
    [Serializable]
    public class ObjectModel
    {
        public int Id;
        public StageObjectType Type; // Object to spawn (Target | Obstacle | Relax)
        public float DifficultyFactor; // Modifies Height/Size (min: 0.0, max: 1.0)
        public float PositionYFactor; // Relative Position as INSpiratory or EXPiratory (AIR = 1, WATER = -1)
        public float PositionXSpacing; // Distance from the last spawned object in unity's metric unit
    }
}

/*
Target;0.3;1;2.5;
Target;0.3;-1;2.5;

Target;0.3;1;2.5;
Target;0.3;-1;2.5;

Target;0.3;1;2.5;
Target;0.3;-1;2.5;

Target;0.3;1;2.5;
Target;0.3;-1;2.5;

Relax command does not use DifficultyFactor and PositionYFactor
Relax;0;0;1;
*/
