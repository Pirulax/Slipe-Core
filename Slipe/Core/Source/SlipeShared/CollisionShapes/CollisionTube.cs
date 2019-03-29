﻿using Slipe.MTADefinitions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Slipe.Shared.CollisionShapes
{
    /// <summary>
    /// This is a shape that has a position and a 2D (X/Y) radius and a height.
    /// </summary>
    public class CollisionTube: CollisionShape
    {
        /// <summary>
        /// Creates a collision tube from a position, radius and height
        /// </summary>
        public CollisionTube(Vector3 position, float radius, float height)
        {
            element = MTAShared.CreateColTube(position.X, position.Y, position.Z, radius, height);
            ElementManager.Instance.RegisterElement(this);
        }
    }
}