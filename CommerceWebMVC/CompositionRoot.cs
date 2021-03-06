﻿using System;
using System.Configuration;
using System.Web.Mvc;
using Ploeh.Samples.Commerce.Domain;

namespace CommerceWebMVC {
    public class CompositionRoot {
        private readonly IControllerFactory controllerFactory;

        public CompositionRoot() {
            this.controllerFactory = CompositionRoot.CreateControllerFactory();
        }

        public IControllerFactory ControllerFactory {
            get { return this.controllerFactory; }
        }

        // Gets passed into SetControllerFactory in Global.asax
        private static IControllerFactory CreateControllerFactory() {
            string connectionString = ConfigurationManager.ConnectionStrings["CommerceObjectContext"].ConnectionString;
            string productRepositoryTypeName = ConfigurationManager.AppSettings["ProductRepositoryType"];
            var productRepositoryType = Type.GetType(productRepositoryTypeName, true);
            var repository = (ProductRepository)Activator.CreateInstance(productRepositoryType, connectionString);
            
            // If we did this, then the UI would have a dependency on the Data layer
            //var repository = new SqlProductRepository(connectionString);
            var controllerFactory = new CommerceControllerFactory(repository);
            return controllerFactory;
        }
    }
}