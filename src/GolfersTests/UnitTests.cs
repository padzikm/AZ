using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Golfers;
using System.Collections.Generic;


namespace GolfersTests
{
    [TestClass]
    public class UnitTests
    {
        private TestDataProvider dataProvider = new TestDataProvider();
        private Algorithm solver = new Algorithm();
        private Validator validator = new Validator();

        [TestMethod]
        public void Given_CollinearPointsOnDiagonal_When_ArePointsCollinearCalled_Then_ResultIsTrue()
        {
            //given
            var p1 = new Point(0, 0, PointType.Golfer);
            var p2 = new Point(1, 1, PointType.Golfer);
            var p3 = new Point(10, 10, PointType.Hole);

            //when
            var result = PointExtensions.ArePointsCollinear(p1, p2, p3);

            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given_CollinearPointsOnAxis_When_ArePointsCollinearCalled_Then_ResultIsTrue()
        {
            //given
            var p1 = new Point(0, 0, PointType.Golfer);
            var p2 = new Point(1, 0, PointType.Golfer);
            var p3 = new Point(10, 0, PointType.Hole);

            //when
            var result = PointExtensions.ArePointsCollinear(p1, p2, p3);

            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given_NoncollinearPoints_When_ArePointsCollinearCalled_Then_ResultIsFalse()
        {
            //given
            var p1 = new Point(0, 0, PointType.Golfer);
            var p2 = new Point(1, 0, PointType.Golfer);
            var p3 = new Point(10, 7.5, PointType.Hole);

            //when
            var result = PointExtensions.ArePointsCollinear(p1, p2, p3);

            //then
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Given_ValidTestData_When_ValidityChecked_Then_ResultIsTrue()
        {
            //given
            var points = dataProvider.GetSmallValidData();

            //when
            var result = validator.IsDataValid(points);

            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given_InvalidTestDataWithMoreHoles_When_ValidityChecked_Then_ResultIsFalse()
        {
            //given
            var points = dataProvider.GetSmallInvalidDataWithMoreHoles();

            //when
            var result = validator.IsDataValid(points);

            //then
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Given_ValidRandomSquareData_When_ValidityChecked_Then_ResultIsTrue()
        {
            //given
            var points = dataProvider.GetRandomValidSquareData(100);

            //when
            var result = validator.IsDataValid(points);

            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given_ValidSolution_When_ValidityChecked_Then_ResultIsTrue()
        {
            //given
            var points = dataProvider.GetValidSolution();

            //when
            var result = validator.IsSolutionValid(points);

            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given_InvalidSolution_When_ValidityChecked_Then_ResultIsFalse()
        {
            //given
            var points = dataProvider.GetInvalidSolution();

            //when
            var result = validator.IsSolutionValid(points);

            //then
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Given_ValidSmallProblem_When_AlgorithmRunned_Then_ValidSolutionObtained()
        {
            //given
            var points = dataProvider.GetSmallValidData();

            //when
            var solution = solver.Solve(points);

            //then
            Assert.IsTrue(validator.IsSolutionValid(solution));
        }

        [TestMethod]
        public void Given_ValidRandomSquareProblem_When_AlgorithmRunned_Then_ValidSolutionObtained()
        {
            //given
            var points = dataProvider.GetRandomValidSquareData(1000);

            //when
            var solution = solver.Solve(points);

            //then
            Assert.IsTrue(validator.IsSolutionValid(solution));
        }
    }
}
