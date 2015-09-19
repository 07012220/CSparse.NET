﻿
namespace CSparse.Tests.Complex.Factorization
{
    using CSparse.Complex;
    using CSparse.Complex.Factorization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Numerics;

    [TestClass]
    public class SparseCholeskyTest
    {
        private const double EPS = 1.0e-6;

        [TestMethod]
        public void TestSolve()
        {
            // Load matrix from a file.
            var A = ResourceLoader.Get<Complex>("hermitian-40-spd.mat");

            // Create test data.
            var x = Helper.CreateTestVector(A.ColumnCount);
            var b = Helper.Multiply(A, x);
            var r = Vector.Clone(b);

            var chol = new SparseCholesky(A, ColumnOrdering.MinimumDegreeAtPlusA);

            // Solve Ax = b.
            chol.Solve(b, x);

            // Compute residual r = b - Ax.
            A.Multiply(-1.0, x, 1.0, r);

            Assert.IsTrue(Vector.Norm(r) < EPS);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestConstructorThrowsOnNonSpd()
        {
            // Load matrix from a file.
            var A = ResourceLoader.Get<Complex>("hermitian-40.mat");

            var chol = new SparseCholesky(A, ColumnOrdering.MinimumDegreeAtPlusA);
        }
    }
}
