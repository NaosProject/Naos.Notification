﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.169.0)
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
    using global::OBeautifulCode.Type;
    using global::OBeautifulCode.Type.Recipes;

    using static global::System.FormattableString;

    [Serializable]
    public partial class PrepareToSendNotificationResult : IModel<PrepareToSendNotificationResult>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="PrepareToSendNotificationResult"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(PrepareToSendNotificationResult left, PrepareToSendNotificationResult right)
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
        /// Determines whether two objects of type <see cref="PrepareToSendNotificationResult"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(PrepareToSendNotificationResult left, PrepareToSendNotificationResult right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(PrepareToSendNotificationResult other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.ChannelToPrepareToSendOnChannelResultMap.IsEqualTo(other.ChannelToPrepareToSendOnChannelResultMap)
                      && this.CannotPrepareToSendOnChannelAction.IsEqualTo(other.CannotPrepareToSendOnChannelAction)
                      && this.ChannelsToSendOn.IsEqualTo(other.ChannelsToSendOn);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as PrepareToSendNotificationResult);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.ChannelToPrepareToSendOnChannelResultMap)
            .Hash(this.CannotPrepareToSendOnChannelAction)
            .Hash(this.ChannelsToSendOn)
            .Value;

        /// <inheritdoc />
        public object Clone() => this.DeepClone();

        /// <inheritdoc />
        public PrepareToSendNotificationResult DeepClone()
        {
            var result = new PrepareToSendNotificationResult(
                                 this.ChannelToPrepareToSendOnChannelResultMap?.DeepClone(),
                                 this.CannotPrepareToSendOnChannelAction.DeepClone(),
                                 this.ChannelsToSendOn?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="ChannelToPrepareToSendOnChannelResultMap" />.
        /// </summary>
        /// <param name="channelToPrepareToSendOnChannelResultMap">The new <see cref="ChannelToPrepareToSendOnChannelResultMap" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="PrepareToSendNotificationResult" /> using the specified <paramref name="channelToPrepareToSendOnChannelResultMap" /> for <see cref="ChannelToPrepareToSendOnChannelResultMap" /> and a deep clone of every other property.</returns>
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
        public PrepareToSendNotificationResult DeepCloneWithChannelToPrepareToSendOnChannelResultMap(IReadOnlyDictionary<IDeliveryChannel, PrepareToSendOnChannelResult> channelToPrepareToSendOnChannelResultMap)
        {
            var result = new PrepareToSendNotificationResult(
                                 channelToPrepareToSendOnChannelResultMap,
                                 this.CannotPrepareToSendOnChannelAction.DeepClone(),
                                 this.ChannelsToSendOn?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="CannotPrepareToSendOnChannelAction" />.
        /// </summary>
        /// <param name="cannotPrepareToSendOnChannelAction">The new <see cref="CannotPrepareToSendOnChannelAction" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="PrepareToSendNotificationResult" /> using the specified <paramref name="cannotPrepareToSendOnChannelAction" /> for <see cref="CannotPrepareToSendOnChannelAction" /> and a deep clone of every other property.</returns>
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
        public PrepareToSendNotificationResult DeepCloneWithCannotPrepareToSendOnChannelAction(CannotPrepareToSendOnChannelAction cannotPrepareToSendOnChannelAction)
        {
            var result = new PrepareToSendNotificationResult(
                                 this.ChannelToPrepareToSendOnChannelResultMap?.DeepClone(),
                                 cannotPrepareToSendOnChannelAction,
                                 this.ChannelsToSendOn?.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="ChannelsToSendOn" />.
        /// </summary>
        /// <param name="channelsToSendOn">The new <see cref="ChannelsToSendOn" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="PrepareToSendNotificationResult" /> using the specified <paramref name="channelsToSendOn" /> for <see cref="ChannelsToSendOn" /> and a deep clone of every other property.</returns>
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
        public PrepareToSendNotificationResult DeepCloneWithChannelsToSendOn(IReadOnlyCollection<IDeliveryChannel> channelsToSendOn)
        {
            var result = new PrepareToSendNotificationResult(
                                 this.ChannelToPrepareToSendOnChannelResultMap?.DeepClone(),
                                 this.CannotPrepareToSendOnChannelAction.DeepClone(),
                                 channelsToSendOn);

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override string ToString()
        {
            var result = Invariant($"Naos.Notification.Domain.PrepareToSendNotificationResult: ChannelToPrepareToSendOnChannelResultMap = {this.ChannelToPrepareToSendOnChannelResultMap?.ToString() ?? "<null>"}, CannotPrepareToSendOnChannelAction = {this.CannotPrepareToSendOnChannelAction.ToString() ?? "<null>"}, ChannelsToSendOn = {this.ChannelsToSendOn?.ToString() ?? "<null>"}.");

            return result;
        }
    }
}