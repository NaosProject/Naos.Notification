// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotification.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Diagnostics.CodeAnalysis;

    using Naos.CodeAnalysis.Recipes;

    /// <summary>
    /// A notice with information to be delivered.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = NaosSuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface INotification
    {
    }
}