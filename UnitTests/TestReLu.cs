﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNSharp.SequentialBased.SequentialLayers;
using NNSharp.DataTypes;
using NNSharp.IO;
using NNSharp.Models;

namespace UnitTests
{
    [TestClass]
    public class TestReLu
    {

        [TestMethod]
        public void Test_ReLu_Execute()
        {
            relu = new ReLuLayer();

            Data2D data = new Data2D(2, 3, 1, 1);
            data[0, 0, 0, 0] = 4;
            data[0, 1, 0, 0] = 2;
            data[0, 2, 0, 0] = -2;

            data[1, 0, 0, 0] = 3;
            data[1, 1, 0, 0] = -1;
            data[1, 2, 0, 0] = -3;

            relu.SetInput(data);

            relu.Execute();

            Data2D output = relu.GetOutput() as Data2D;

            Assert.AreEqual(output[0, 0, 0, 0], 4.0, 0.00000001);
            Assert.AreEqual(output[0, 1, 0, 0], 2.0, 0.00000001);
            Assert.AreEqual(output[0, 2, 0, 0], 0.0, 0.00000001);

            Assert.AreEqual(output[1, 0, 0, 0], 3.0, 0.00000001);
            Assert.AreEqual(output[1, 1, 0, 0], 0.0, 0.00000001);
            Assert.AreEqual(output[1, 2, 0, 0], 0.0, 0.00000001);
        }

        [TestMethod]
        public void Test_ReLu_KerasModel()
        {
            string path = @"tests\test_relu_model.json";
            var reader = new ReaderKerasModel(path);

            SequentialModel model = reader.GetSequentialExecutor();

            Data2D inp = new Data2D(1, 8, 1, 1);

            inp[0, 0, 0, 0] = 1;
            inp[0, 1, 0, 0] = 2;
            inp[0, 2, 0, 0] = -1;
            inp[0, 3, 0, 0] = 0;

            inp[0, 4, 0, 0] = 3;
            inp[0, 5, 0, 0] = 1;
            inp[0, 6, 0, 0] = 1;
            inp[0, 7, 0, 0] = 2;

            Data2D ou = model.ExecuteNetwork(inp) as Data2D;

            Assert.AreEqual(ou.GetDimension().c, 4);
            Assert.AreEqual(ou.GetDimension().w, 1);

            Assert.AreEqual(ou[0, 0, 0, 0], 0.0, 0.00001);
            Assert.AreEqual(ou[0, 0, 1, 0], 5.5, 0.00001);
            Assert.AreEqual(ou[0, 0, 2, 0], 18.5, 0.00001);
            Assert.AreEqual(ou[0, 0, 3, 0], 9.0, 0.00001);
        }

        private ReLuLayer relu;
    }
}
