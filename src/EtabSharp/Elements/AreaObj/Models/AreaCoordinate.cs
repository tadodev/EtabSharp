namespace EtabSharp.Elements.AreaObj.Models;

/// <summary>
/// Represents a coordinate point for area object definition.
/// </summary>
public class AreaCoordinate
{
    /// <summary>
    /// Gets or sets the X coordinate.
    /// </summary>
    public double X { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the Y coordinate.
    /// </summary>
    public double Y { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the Z coordinate (elevation).
    /// </summary>
    public double Z { get; set; } = 0.0;

    /// <summary>
    /// Initializes a new instance of the AreaCoordinate class.
    /// </summary>
    public AreaCoordinate()
    {
    }

    /// <summary>
    /// Initializes a new instance of the AreaCoordinate class with specified coordinates.
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="z">Z coordinate</param>
    public AreaCoordinate(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Converts the coordinate to an array format.
    /// </summary>
    /// <returns>Array containing [X, Y, Z]</returns>
    public double[] ToArray()
    {
        return new double[] { X, Y, Z };
    }

    /// <summary>
    /// Creates an AreaCoordinate from an array.
    /// </summary>
    /// <param name="coordinates">Array containing [X, Y, Z]</param>
    /// <returns>AreaCoordinate instance</returns>
    public static AreaCoordinate FromArray(double[] coordinates)
    {
        if (coordinates == null || coordinates.Length < 3)
            throw new ArgumentException("Coordinates array must contain at least 3 values", nameof(coordinates));

        return new AreaCoordinate(coordinates[0], coordinates[1], coordinates[2]);
    }

    /// <summary>
    /// Calculates the distance to another coordinate.
    /// </summary>
    /// <param name="other">Other coordinate</param>
    /// <returns>Distance between coordinates</returns>
    public double DistanceTo(AreaCoordinate other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        double dx = X - other.X;
        double dy = Y - other.Y;
        double dz = Z - other.Z;

        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }

    /// <summary>
    /// Calculates the 2D distance (ignoring Z) to another coordinate.
    /// </summary>
    /// <param name="other">Other coordinate</param>
    /// <returns>2D distance between coordinates</returns>
    public double Distance2DTo(AreaCoordinate other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        double dx = X - other.X;
        double dy = Y - other.Y;

        return Math.Sqrt(dx * dx + dy * dy);
    }

    /// <summary>
    /// Returns a string representation of the coordinate.
    /// </summary>
    /// <returns>String in format (X, Y, Z)</returns>
    public override string ToString()
    {
        return $"({X:F3}, {Y:F3}, {Z:F3})";
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current coordinate.
    /// </summary>
    /// <param name="obj">Object to compare</param>
    /// <returns>True if equal, false otherwise</returns>
    public override bool Equals(object? obj)
    {
        if (obj is AreaCoordinate other)
        {
            const double tolerance = 1e-6;
            return Math.Abs(X - other.X) < tolerance &&
                   Math.Abs(Y - other.Y) < tolerance &&
                   Math.Abs(Z - other.Z) < tolerance;
        }
        return false;
    }

    /// <summary>
    /// Returns a hash code for the coordinate.
    /// </summary>
    /// <returns>Hash code</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    /// <summary>
    /// Adds two coordinates together.
    /// </summary>
    /// <param name="a">First coordinate</param>
    /// <param name="b">Second coordinate</param>
    /// <returns>Sum of coordinates</returns>
    public static AreaCoordinate operator +(AreaCoordinate a, AreaCoordinate b)
    {
        if (a == null || b == null)
            throw new ArgumentNullException();

        return new AreaCoordinate(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    /// <summary>
    /// Subtracts one coordinate from another.
    /// </summary>
    /// <param name="a">First coordinate</param>
    /// <param name="b">Second coordinate</param>
    /// <returns>Difference of coordinates</returns>
    public static AreaCoordinate operator -(AreaCoordinate a, AreaCoordinate b)
    {
        if (a == null || b == null)
            throw new ArgumentNullException();

        return new AreaCoordinate(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }

    /// <summary>
    /// Multiplies a coordinate by a scalar.
    /// </summary>
    /// <param name="coord">Coordinate</param>
    /// <param name="scalar">Scalar value</param>
    /// <returns>Scaled coordinate</returns>
    public static AreaCoordinate operator *(AreaCoordinate coord, double scalar)
    {
        if (coord == null)
            throw new ArgumentNullException(nameof(coord));

        return new AreaCoordinate(coord.X * scalar, coord.Y * scalar, coord.Z * scalar);
    }

    /// <summary>
    /// Multiplies a coordinate by a scalar.
    /// </summary>
    /// <param name="scalar">Scalar value</param>
    /// <param name="coord">Coordinate</param>
    /// <returns>Scaled coordinate</returns>
    public static AreaCoordinate operator *(double scalar, AreaCoordinate coord)
    {
        return coord * scalar;
    }
}