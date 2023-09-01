﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.181.0)
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
    public partial class PrepareToSendOnChannelResult : IModel<PrepareToSendOnChannelResult>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="PrepareToSendOnChannelResult"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(PrepareToSendOnChannelResult left, PrepareToSendOnChannelResult right)
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
        /// Determines whether two objects of type <see cref="PrepareToSendOnChannelResult"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(PrepareToSendOnChannelResult left, PrepareToSendOnChannelResult right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(PrepareToSendOnChannelResult other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.ChannelOperationInstructions.IsEqualTo(other.ChannelOperationInstructions)
                      && this.Failures.IsEqualTo(other.Failures)
                      && this.FailureAction.IsEqualTo(other.FailureAction);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as PrepareToSendOnChannelResult);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.ChannelOperationInstructions)
            .Hash(this.Failures)
            .Hash(this.FailureAction)
            .Value;

        /// <inheritdoc />
        public object Clone() => this.DeepClone();

        /// <inheritdoc />
        public PrepareToSendOnChannelResult DeepClone()
        {
            var result = new PrepareToSendOnChannelResult(
                                 this.ChannelOperationInstructions?.DeepClone(),
                                 this.Failures?.DeepClone(),
                                 this.FailureAction.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="ChannelOperationInstructions" />.
        /// </summary>
        /// <param name="channelOperationInstructions">The new <see cref="ChannelOperationInstructions" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="PrepareToSendOnChannelResult" /> using the specified <paramref name="channelOperationInstructions" /> for <see cref="ChannelOperationInstructions" /> and a deep clone of every other property.</returns>
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
        public PrepareToSendOnChannelResult DeepCloneWithChannelOperationInstructions(IReadOnlyList<ChannelOperationInstruction> channelOperationInstructions)
        {
            var result = new PrepareToSendOnChannelResult(
                                 channelOperationInstructions,
                                 this.Failures?.DeepClone(),
                                 this.FailureAction.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="Failures" />.
        /// </summary>
        /// <param name="failures">The new <see cref="Failures" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="PrepareToSendOnChannelResult" /> using the specified <paramref name="failures" /> for <see cref="Failures" /> and a deep clone of every other property.</returns>
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
        public PrepareToSendOnChannelResult DeepCloneWithFailures(IReadOnlyCollection<IFailure> failures)
        {
            var result = new PrepareToSendOnChannelResult(
                                 this.ChannelOperationInstructions?.DeepClone(),
                                 failures,
                                 this.FailureAction.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="FailureAction" />.
        /// </summary>
        /// <param name="failureAction">The new <see cref="FailureAction" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="PrepareToSendOnChannelResult" /> using the specified <paramref name="failureAction" /> for <see cref="FailureAction" /> and a deep clone of every other property.</returns>
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
        public PrepareToSendOnChannelResult DeepCloneWithFailureAction(PrepareToSendOnChannelFailureAction failureAction)
        {
            var result = new PrepareToSendOnChannelResult(
                                 this.ChannelOperationInstructions?.DeepClone(),
                                 this.Failures?.DeepClone(),
                                 failureAction);

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override string ToString()
        {
            var result = Invariant($"Naos.Notification.Domain.PrepareToSendOnChannelResult: ChannelOperationInstructions = {this.ChannelOperationInstructions?.ToString() ?? "<null>"}, Failures = {this.Failures?.ToString() ?? "<null>"}, FailureAction = {this.FailureAction.ToString() ?? "<null>"}.");

            return result;
        }
    }
}