using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GravMath
{
    //Create gravitational constant as single precision float

    public const float gravitationalConstant = 0.0001f;

    //Calculate mass from density and volume
    public static float Mass(float density, float volume)
    {
        float mass = density * volume;
        return mass;
    }

    //Calculate mass from surface gravity and radius
    public static float Mass(float surfaceGravity, float radius, bool surfaceGravityIsKnown)
    {
        float mass = surfaceGravity * radius * radius / gravitationalConstant;
        return mass;
    }
    
    //Calculate volume of a sphere
    public static float Volume(float radius)
    {
        float volume = (4f / 3f) * Mathf.PI * Mathf.Pow(radius, 3);
        return volume;
    }

    //Calculate density from mass and volume
    public static float Density(float mass, float volume)
    {
        float density = mass / volume;
        return density;
    }

    //Calculate the force of gravity between two objects
    public static float GravForce(float mass1, float mass2, float distance)
    {
        float force = gravitationalConstant * ((mass1 * mass2) / Mathf.Pow(distance, 2));
        return force;
    }
    
    //Calculate surface gravity from mass and radius
    public static float SurfaceGravity(float mass, float radius)
    {
        float surfaceGravity = gravitationalConstant * (mass / Mathf.Pow(radius, 2));
        return surfaceGravity;
    }

    //Calculate the acceleration of an object due to gravity
    public static float GravAccel(float mass, float distance)
    {
        float accel = gravitationalConstant * (mass / Mathf.Pow(distance, 2));
        return accel;
    }

    //Calculate signed distance between two vectors
    public static float Distance(Vector3 pos1, Vector3 pos2)
    {
        float distance = Mathf.Sqrt(Mathf.Pow((pos1.x - pos2.x), 2) + Mathf.Pow((pos1.y - pos2.y), 2) + Mathf.Pow((pos1.z - pos2.z), 2));
        return distance;
    }

    //Calculate the gravitational force between two objects and return a vector
    public static Vector3 GravForceVector(Vector3 pos1, Vector3 pos2, float mass1, float mass2)
    {
        float distance = Distance(pos1, pos2);
        float force = GravForce(mass1, mass2, distance);
        Vector3 forceVector = (pos1 - pos2).normalized * force;
        return forceVector;
    }
    

    //Calculate the gravitational acceleration between two objects and return a vector
    public static Vector3 GravAccelVector(Vector3 pos1, Vector3 pos2, float mass1)
    {
        float distance = Distance(pos1, pos2);
        float accel = GravAccel(mass1, distance);
        Vector3 accelVector = (pos1 - pos2).normalized * accel;
        return accelVector;
    }
    
    //Calculate escape velocity from mass and radius
    public static float EscapeVelocity(float mass, float radius)
    {
        float escapeVelocity = Mathf.Sqrt(2 * gravitationalConstant * mass / radius);
        return escapeVelocity;
    }

    //Calculate orbital period from mass and radius
    public static float OrbitalPeriod(float mass, float radius)
    {
        float orbitalPeriod = 2 * Mathf.PI * Mathf.Sqrt(Mathf.Pow(radius, 3) / (gravitationalConstant * mass));
        return orbitalPeriod;
    }

    //Calculate rotational period from mass and radius
    public static float RotationalPeriod(float mass, float radius)
    {
        float rotationalPeriod = 2 * Mathf.PI * Mathf.Sqrt(Mathf.Pow(radius, 2) / (gravitationalConstant * mass));
        return rotationalPeriod;
    }
    
    public static Vector3 CubeToSphere(Vector3 pointsOnUnitCube)
    {
        float x = pointsOnUnitCube.x;
        float y = pointsOnUnitCube.y;
        float z = pointsOnUnitCube.z;

        float x2 = x * x;
        float y2 = y * y;
        float z2 = z * z;

        float dx = x * Mathf.Sqrt(1 - (y2 / 2) - (z2 / 2) + (y2 * z2 / 3));
        float dy = y * Mathf.Sqrt(1 - (z2 / 2) - (x2 / 2) + (z2 * x2 / 3));
        float dz = z * Mathf.Sqrt(1 - (x2 / 2) - (y2 / 2) + (x2 * y2 / 3));

        Vector3 normalizedPoints = new Vector3(dx, dy, dz);
        return normalizedPoints;
    }
}
