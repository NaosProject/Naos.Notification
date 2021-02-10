// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChannelOperationInstruction.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.Collections.Generic;

    using Naos.Protocol.Domain;

    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// Contains information needed to put a channel-specific operation (e.g. SendEmailOp) into a stream
    /// monitor it to determine whether it has been executed and if so, determine the outcome (i.e. success or failure).
    /// </summary>
    /// <remarks>
    /// The operation could be the only operation or one of many on the channel for the same notification.
    /// Likewise, the notification might be sent on one or many channels.
    /// This class is scoped to a single operation on a single channel.
    /// </remarks>
    public partial class ChannelOperationInstruction : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelOperationInstruction"/> class.
        /// </summary>
        /// <param name="operation">The channel operation.</param>
        /// <param name="monitoringInfo">The information needed to monitor the execution of the operation.</param>
        /// <param name="tags">OPTIONAL tags to use when putting the operation into the channel-specific Operation Stream.  DEFAULT is to specify no tags.</param>
        public ChannelOperationInstruction(
            IOperation operation,
            ChannelOperationMonitoringInfo monitoringInfo,
            IReadOnlyDictionary<string, string> tags = null)
        {
            new { operation }.AsArg().Must().NotBeNull();
            new { monitoringInfo }.AsArg().Must().NotBeNull();

            this.Operation = operation;
            this.MonitoringInfo = monitoringInfo;
            this.Tags = tags;
        }

        /// <summary>
        /// Gets the channel operation.
        /// </summary>
        public IOperation Operation { get; private set; }

        /// <summary>
        /// Gets the information needed to monitor the execution of the operation.
        /// </summary>
        public ChannelOperationMonitoringInfo MonitoringInfo { get; private set; }

        /// <summary>
        /// Gets the tags to use when putting the operation into the channel-specific Operation Stream.
        /// </summary>
        public IReadOnlyDictionary<string, string> Tags { get; private set; }
    }
}
