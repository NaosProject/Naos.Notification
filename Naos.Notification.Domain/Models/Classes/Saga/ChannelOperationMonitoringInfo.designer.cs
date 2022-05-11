﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.178.0)
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using global::System;
    using global::System.CodeDom.Compiler;
    using global::System.Collections.Concurrent;
    using global::System.Collections.Generic;
    using global::System.Collections.ObjectModel;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System.Globalization;
    using global::System.Linq;

    using global::OBeautifulCode.Cloning.Recipes;
    using global::OBeautifulCode.Equality.Recipes;
    using global::OBeautifulCode.Representation.System;
    using global::OBeautifulCode.Type;
    using global::OBeautifulCode.Type.Recipes;

    using static global::System.FormattableString;

    [Serializable]
    public partial class ChannelOperationMonitoringInfo : IModel<ChannelOperationMonitoringInfo>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="ChannelOperationMonitoringInfo"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(ChannelOperationMonitoringInfo left, ChannelOperationMonitoringInfo right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = left.Equals(right);

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="ChannelOperationMonitoringInfo"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(ChannelOperationMonitoringInfo left, ChannelOperationMonitoringInfo right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(ChannelOperationMonitoringInfo other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.ChannelOperationTrackingCodeId.IsEqualTo(other.ChannelOperationTrackingCodeId)
                      && this.SucceededEventType.IsEqualTo(other.SucceededEventType)
                      && this.FailedEventType.IsEqualTo(other.FailedEventType);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as ChannelOperationMonitoringInfo);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.ChannelOperationTrackingCodeId)
            .Hash(this.SucceededEventType)
            .Hash(this.FailedEventType)
            .Value;

        /// <inheritdoc />
        public object Clone() => this.DeepClone();

        /// <inheritdoc />
        public ChannelOperationMonitoringInfo DeepClone()
        {
            var result = new ChannelOperationMonitoringInfo(
                                 this.ChannelOperationTrackingCodeId.DeepClone(),
                                 this.SucceededEventType?.DeepClone(),
                                 this.FailedEventType?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="ChannelOperationTrackingCodeId" />.
        /// </summary>
        /// <param name="channelOperationTrackingCodeId">The new <see cref="ChannelOperationTrackingCodeId" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="ChannelOperationMonitoringInfo" /> using the specified <paramref name="channelOperationTrackingCodeId" /> for <see cref="ChannelOperationTrackingCodeId" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public ChannelOperationMonitoringInfo DeepCloneWithChannelOperationTrackingCodeId(long channelOperationTrackingCodeId)
        {
            var result = new ChannelOperationMonitoringInfo(
                                 channelOperationTrackingCodeId,
                                 this.SucceededEventType?.DeepClone(),
                                 this.FailedEventType?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="SucceededEventType" />.
        /// </summary>
        /// <param name="succeededEventType">The new <see cref="SucceededEventType" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="ChannelOperationMonitoringInfo" /> using the specified <paramref name="succeededEventType" /> for <see cref="SucceededEventType" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public ChannelOperationMonitoringInfo DeepCloneWithSucceededEventType(TypeRepresentation succeededEventType)
        {
            var result = new ChannelOperationMonitoringInfo(
                                 this.ChannelOperationTrackingCodeId.DeepClone(),
                                 succeededEventType,
                                 this.FailedEventType?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="FailedEventType" />.
        /// </summary>
        /// <param name="failedEventType">The new <see cref="FailedEventType" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="ChannelOperationMonitoringInfo" /> using the specified <paramref name="failedEventType" /> for <see cref="FailedEventType" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public ChannelOperationMonitoringInfo DeepCloneWithFailedEventType(TypeRepresentation failedEventType)
        {
            var result = new ChannelOperationMonitoringInfo(
                                 this.ChannelOperationTrackingCodeId.DeepClone(),
                                 this.SucceededEventType?.DeepClone(),
                                 failedEventType);

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override string ToString()
        {
            var result = Invariant($"Naos.Notification.Domain.ChannelOperationMonitoringInfo: ChannelOperationTrackingCodeId = {this.ChannelOperationTrackingCodeId.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, SucceededEventType = {this.SucceededEventType?.ToString() ?? "<null>"}, FailedEventType = {this.FailedEventType?.ToString() ?? "<null>"}.");

            return result;
        }
    }
}