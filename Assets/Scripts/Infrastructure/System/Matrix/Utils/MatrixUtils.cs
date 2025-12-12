using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.System.Matrix.Utils
{
    // TODO: Test
    public static class MatrixUtils
    {
        public const int MaxRotationSteps = 4;

        #region Clockwise rotation

        public static void GetClockwiseRotatedIndices(
            int columns,
            int row,
            int column,
            out int rotatedRow,
            out int rotatedColumn)
        {
            rotatedRow = columns - column - 1;
            rotatedColumn = row;
        }

        public static void GetClockwiseRotatedIndices(
            int rows,
            int columns,
            int row,
            int column,
            out int rotatedRow,
            out int rotatedColumn,
            int steps)
        {
            rotatedRow = row;
            rotatedColumn = column;

            steps %= MaxRotationSteps;

            for (int step = 0; step < steps; ++step)
            {
                int columnsTarget = step % 2 == 0 ? columns : rows;

                GetClockwiseRotatedIndices(columnsTarget, row, column, out rotatedRow, out rotatedColumn);

                row = rotatedRow;
                column = rotatedColumn;
            }
        }

        [NotNull]
        public static T[,] RotateClockwise<T>([NotNull] this T[,] matrix, int steps)
        {
            ArgumentNullException.ThrowIfNull(matrix);

            steps %= MaxRotationSteps;

            for (int step = 0; step < steps; ++step)
            {
                matrix = matrix.RotateClockwise();
            }

            return matrix;
        }

        [NotNull]
        public static T[,] RotateClockwise<T>([NotNull] this T[,] matrix)
        {
            ArgumentNullException.ThrowIfNull(matrix);

            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            T[,] newMatrix = new T[columns, rows];

            for (int row = 0; row < rows; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    GetClockwiseRotatedIndices(columns, row, column, out int rotatedRow, out int rotatedColumn);

                    newMatrix[rotatedRow, rotatedColumn] = matrix[row, column];
                }
            }

            return newMatrix;
        }

        #endregion

        #region Counterclockwise rotation

        public static void GetCounterClockwiseRotatedIndices(
            int rows,
            int row,
            int column,
            out int rotatedRow,
            out int rotatedColumn)
        {
            rotatedRow = column;
            rotatedColumn = rows - row - 1;
        }

        public static void GetCounterClockwiseRotatedIndices(
            int rows,
            int columns,
            int row,
            int column,
            out int rotatedRow,
            out int rotatedColumn,
            int steps)
        {
            rotatedRow = row;
            rotatedColumn = column;

            steps %= MaxRotationSteps;

            for (int step = 0; step < steps; ++step)
            {
                int rowsTarget = step % 2 == 0 ? rows : columns;

                GetCounterClockwiseRotatedIndices(rowsTarget, row, column, out rotatedRow, out rotatedColumn);

                row = rotatedRow;
                column = rotatedColumn;
            }
        }

        [NotNull]
        public static T[,] RotateCounterClockwise<T>([NotNull] this T[,] matrix, int steps)
        {
            ArgumentNullException.ThrowIfNull(matrix);

            steps %= MaxRotationSteps;

            for (int step = 0; step < steps; ++step)
            {
                matrix = matrix.RotateCounterClockwise();
            }

            return matrix;
        }

        [NotNull]
        public static T[,] RotateCounterClockwise<T>([NotNull] this T[,] matrix)
        {
            ArgumentNullException.ThrowIfNull(matrix);

            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            T[,] newMatrix = new T[columns, rows];

            for (int row = 0; row < rows; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    GetCounterClockwiseRotatedIndices(rows, row, column, out int rotatedRow, out int rotatedColumn);

                    newMatrix[rotatedRow, rotatedColumn] = matrix[row, column];
                }
            }

            return newMatrix;
        }

        #endregion

        public static void Fill<T>([NotNull] this T[,] matrix, T value)
        {
            ArgumentNullException.ThrowIfNull(matrix);

            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            for (int row = 0; row < rows; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    matrix[row, column] = value;
                }
            }
        }
    }
}