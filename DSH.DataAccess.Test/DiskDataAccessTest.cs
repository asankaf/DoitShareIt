﻿using DSH.DataAccess.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DSH.Access.DataModels;

namespace DSH.Main.Web.Tests
{
    
    
    /// <summary>
    ///This is a test class for DiskDataAccessTest and is intended
    ///to contain all DiskDataAccessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DiskDataAccessTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ProfilePicturePath
        ///</summary>
        [TestMethod()]
        public void ProfilePicturePathTest()
        {
            Users userInfo = new Users
                                 {
                                     CreationDate = null,
                                     DisplayName = "Roshan Dhananajaya",
                                     Downvotes = 0,
                                     Id = 0,
                                     LastAccessDate = null,
                                     PicLocation = string.Empty, // TODO: fill this up 

                                 }; // TODO: Initialize to an appropriate value
            string picturesFolder = "image_store";
            string fileName = string.Empty; // generated by function
            string fileNameExpected = "794440113172189291352261931191312262321647247122116246.jpg";
            string profilePictureFilePath = string.Empty; // generated by function
            string profilePictureFilePathExpected =
                @"D:\WorkDir\DoitShareit\DoitShareIt.git\trunk\DSH.Main.Web\image_store\794440113172189291352261931191312262321647247122116246.jpg";
            
            DiskDataAccess.ProfilePicturePath(userInfo, picturesFolder, out fileName, out profilePictureFilePath);
            Assert.AreEqual(fileNameExpected, fileName);
            Assert.AreEqual(profilePictureFilePathExpected, profilePictureFilePath);
//            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
