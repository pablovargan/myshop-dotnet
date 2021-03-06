﻿namespace MVVMtpl.Services
{
    using System;
    using System.Collections.Generic;
    using MVVMtpl.ViewModels;
    using MVVMtpl.Views;

    /// <summary>
    /// Configuration two connect ViewModels with Views
    /// </summary>
    public class NavigationServiceConfiguration
    {
        /// <summary>
        /// Dictionary for this configuration
        /// </summary>
        public IDictionary<Type, Type> routingDictionary = new Dictionary<Type, Type>() 
        { 

            { typeof(MainViewModel), typeof(MainPage) },
            { typeof(ShopViewModel), typeof(ShopPage) },
            { typeof(OrdersViewModel), typeof(OrdersPage) },
            { typeof(OrdersSqlViewModel), typeof(OrdersSqlPage) },
            { typeof(ProductsMobileViewModel), typeof(ProductsMobilePage)}
        };
    }
}
