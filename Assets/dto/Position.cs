using UnityEngine;

namespace dto
{
    public class Position
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }
        public float XRotation { get; }

        public Position(float x, float y, float z, float xRotation)
        {
            X = x;
            Y = y;
            Z = z;
            XRotation = xRotation;
        }

        public Position(Vector3 position, Quaternion rotation)
        {
            X = position.x;
            Y = position.y;
            Z = position.z;
            XRotation = rotation.x;
        }

        public Vector3 GetPostion()
        {
            return new Vector3(X,Y,Z);
        }
    }
}