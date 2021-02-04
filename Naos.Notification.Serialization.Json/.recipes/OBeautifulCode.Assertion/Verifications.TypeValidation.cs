﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Verifications.TypeValidation.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Assertion.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Assertion.Recipes
{
    using global::System;
    using global::System.Collections;
    using global::System.Collections.Generic;
    using global::System.Globalization;
    using global::System.Linq;

    using OBeautifulCode.Type.Recipes;

    using static global::System.FormattableString;

#if !OBeautifulCodeAssertionSolution
    internal
#else
    public
#endif
    static partial class Verifications
    {
#pragma warning disable SA1201
        private static readonly Type EnumerableType = typeof(IEnumerable);

        private static readonly Type BoolType = typeof(bool);

        private static readonly Type NullableBoolType = typeof(bool?);

        private static readonly Type StringType = typeof(string);

        private static readonly Type GuidType = typeof(Guid);

        private static readonly Type NullableGuidType = typeof(Guid?);

        private static readonly Type DateTimeType = typeof(DateTime);

        private static readonly Type NullableDateTimeType = typeof(DateTime?);

        private static readonly Type DictionaryType = typeof(IDictionary);

        private static readonly Type UnboundGenericDictionaryType = typeof(IDictionary<,>);

        private static readonly Type UnboundGenericReadOnlyDictionaryType = typeof(IReadOnlyDictionary<,>);

        private static readonly IReadOnlyCollection<TypeValidation> MustBeAssignableToNullTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfTypeCannotBeAssignedToNull,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeBooleanOrNullableBooleanTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfNotAssignableToType,
                ReferenceTypes = new[] { BoolType, NullableBoolType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeStringTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfNotAssignableToType,
                ReferenceTypes = new[] { StringType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeGuidOrNullableGuidTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfNotAssignableToType,
                ReferenceTypes = new[] { GuidType, NullableGuidType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeDateTimeOrNullableDateTimeTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfNotAssignableToType,
                ReferenceTypes = new[] { DateTimeType, NullableDateTimeType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeNullableDateTimeTypeValidations = new[]
        {
            new TypeValidation
            {
                // DateTime is assignable to DateTime?, so we call ThrowIfNotEqualToType instead of ThrowIfNotAssignableToType
                Handler = ThrowIfNotEqualToType,
                ReferenceTypes = new[] { NullableDateTimeType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeEnumerableTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfNotAssignableToType,
                ReferenceTypes = new[] { EnumerableType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeEnumerableWhoseElementTypeCanBeAssignedToNullValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfNotAssignableToType,
                ReferenceTypes = new[] { EnumerableType },
            },
            new TypeValidation
            {
                Handler = ThrowIfEnumerableElementTypeCannotBeAssignedToNull,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeDictionaryTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfNotAssignableToType,
                ReferenceTypes = new[] { DictionaryType, UnboundGenericDictionaryType, UnboundGenericReadOnlyDictionaryType },
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> MustBeDictionaryWhoseValueTypeCanBeAssignedToNullValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfNotAssignableToType,
                ReferenceTypes = new[] { DictionaryType, UnboundGenericDictionaryType, UnboundGenericReadOnlyDictionaryType },
            },
            new TypeValidation
            {
                Handler = ThrowIfDictionaryValueTypeCannotBeAssignedToNull,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> InequalityTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfTypeDoesNotHaveWorkingDefaultComparer,
            },
            new TypeValidation
            {
                Handler = ThrowIfTypeIsNotEqualToAllVerificationParameterType,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> BeSameReferenceAsTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfValueType,
            },
            new TypeValidation
            {
                Handler = ThrowIfTypeIsNotEqualToAllVerificationParameterType,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> EqualsTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfTypeIsNotEqualToAllVerificationParameterType,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> AlwaysThrowTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = Throw,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> ContainmentTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfNotAssignableToType,
                ReferenceTypes = new[] { EnumerableType },
            },
            new TypeValidation
            {
                Handler = ThrowIfEnumerableElementTypeDoesNotEqualAllVerificationParameterTypes,
            },
        };

        private static readonly IReadOnlyCollection<TypeValidation> DictionaryKeyContainmentTypeValidations = new[]
        {
            new TypeValidation
            {
                Handler = ThrowIfNotAssignableToType,
                ReferenceTypes = new[] { DictionaryType, UnboundGenericDictionaryType, UnboundGenericReadOnlyDictionaryType },
            },
            new TypeValidation
            {
                Handler = ThrowIfDictionaryKeyTypeDoesNotEqualAllVerificationParameterTypes,
            },
        };

        private static void Throw(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemTypeReadableString = verifiableItem.ItemType.ToStringReadable();

            WorkflowExtensions.ThrowImproperUseOfFramework(Invariant($"verificationName: {verification.Name}, isElementInEnumerable: {verifiableItem.ItemIsElementInEnumerable}, verifiableItemTypeName: {verifiableItemTypeReadableString}"));
        }

        private static void ThrowIfValueType(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            if (verifiableItem.ItemType.IsValueType)
            {
                ThrowSubjectUnexpectedType(verification, verifiableItem, AnyReferenceTypeName);
            }
        }

        private static void ThrowIfTypeCannotBeAssignedToNull(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemType = verifiableItem.ItemType;

            if (!verifiableItemType.IsClosedTypeAssignableToNull())
            {
                ThrowSubjectUnexpectedType(verification, verifiableItem, AnyReferenceTypeName, NullableGenericTypeName);
            }
        }

        private static void ThrowIfEnumerableElementTypeCannotBeAssignedToNull(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemType = verifiableItem.ItemType;

            var elementType = verifiableItemType.GetClosedEnumerableElementType();

            if (!elementType.IsClosedTypeAssignableToNull())
            {
                ThrowSubjectUnexpectedType(verification, verifiableItem, EnumerableOfAnyReferenceTypeName, EnumerableOfNullableGenericTypeName, EnumerableWhenNotEnumerableOfAnyValueTypeName);
            }
        }

        private static void ThrowIfDictionaryValueTypeCannotBeAssignedToNull(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemType = verifiableItem.ItemType;

            var dictionaryValueType = verifiableItemType.GetClosedDictionaryValueType();

            if (!dictionaryValueType.IsClosedTypeAssignableToNull())
            {
                ThrowSubjectUnexpectedType(verification, verifiableItem, DictionaryTypeName, DictionaryWithValueOfAnyReferenceTypeName, DictionaryWithValueOfNullableGenericTypeName, ReadOnlyDictionaryWithValueOfAnyReferenceTypeName, ReadOnlyDictionaryWithValueOfNullableGenericTypeName);
            }
        }

        private static void ThrowIfNotAssignableToType(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemType = verifiableItem.ItemType;

            var validTypes = typeValidation.ReferenceTypes;

            if (!validTypes.Any(_ => verifiableItemType.IsAssignableTo(_, treatGenericTypeDefinitionAsAssignableTo: true)))
            {
                ThrowSubjectUnexpectedType(verification, verifiableItem, validTypes);
            }
        }

        private static void ThrowIfNotEqualToType(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemType = verifiableItem.ItemType;

            var validTypes = typeValidation.ReferenceTypes;

            if (validTypes.All(_ => verifiableItemType != _))
            {
                ThrowSubjectUnexpectedType(verification, verifiableItem, validTypes);
            }
        }

        private static void ThrowIfTypeDoesNotHaveWorkingDefaultComparer(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            if (!verifiableItem.ItemType.HasWorkingDefaultComparer())
            {
                ThrowSubjectUnexpectedType(verification, verifiableItem, AnyTypeWithWorkingDefaultComparerName);
            }
        }

        private static void ThrowIfTypeIsNotEqualToAllVerificationParameterType(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var verifiableItemType = verifiableItem.ItemType;

            foreach (var verificationParameter in verification.VerificationParameters)
            {
                if (verificationParameter.ParameterType != verifiableItemType)
                {
                    ThrowVerificationParameterUnexpectedType(verification.Name, verificationParameter.ParameterType, verificationParameter.Name, verifiableItemType);
                }
            }
        }

        private static void ThrowIfEnumerableElementTypeDoesNotEqualAllVerificationParameterTypes(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var elementType = verifiableItem.ItemType.GetClosedEnumerableElementType();

            foreach (var verificationParameter in verification.VerificationParameters)
            {
                if (verificationParameter.ParameterType != elementType)
                {
                    ThrowVerificationParameterUnexpectedType(verification.Name, verificationParameter.ParameterType, verificationParameter.Name, elementType);
                }
            }
        }

        private static void ThrowIfDictionaryKeyTypeDoesNotEqualAllVerificationParameterTypes(
            Verification verification,
            VerifiableItem verifiableItem,
            TypeValidation typeValidation)
        {
            var keyType = verifiableItem.ItemType.GetClosedDictionaryKeyType();

            foreach (var verificationParameter in verification.VerificationParameters)
            {
                if (verificationParameter.ParameterType != keyType)
                {
                    ThrowVerificationParameterUnexpectedType(verification.Name, verificationParameter.ParameterType, verificationParameter.Name, keyType);
                }
            }
        }

        private static void ThrowSubjectUnexpectedType(
            Verification verification,
            VerifiableItem verifiableItem,
            IReadOnlyList<Type> expectedTypes)
        {
            var expectedTypeStrings = expectedTypes.Select(_ => _.ToStringReadable()).ToArray();

            ThrowSubjectUnexpectedType(verification, verifiableItem, expectedTypeStrings);
        }

        private static void ThrowSubjectUnexpectedType(
            Verification verification,
            VerifiableItem verifiableItem,
            params string[] expectedTypes)
        {
            var verifiableItemType = verifiableItem.ItemType;

            var verificationName = verification.Name;

            var isElementInEnumerable = verifiableItem.ItemIsElementInEnumerable;

            var expectedTypesMessage = string.Join(", ", expectedTypes.Select(_ => isElementInEnumerable ? Invariant($"IEnumerable<{_}>") : _));

            var valueTypeMessage = isElementInEnumerable ? Invariant($"IEnumerable<{verifiableItemType.ToStringReadable()}>") : verifiableItemType.ToStringReadable();

            var exceptionMessage = string.Format(CultureInfo.InvariantCulture, SubjectUnexpectedTypeErrorMessage, verificationName, valueTypeMessage, expectedTypesMessage);

            WorkflowExtensions.ThrowImproperUseOfFramework(exceptionMessage);
        }

        private static void ThrowVerificationParameterUnexpectedType(
            string verificationName,
            Type verificationParameterType,
            string verificationParameterName,
            params Type[] expectedTypes)
        {
            var expectedTypesStrings = expectedTypes.Select(_ => _.ToStringReadable()).ToArray();

            var expectedTypesMessage = string.Join(", ", expectedTypesStrings);

            var exceptionMessage = string.Format(CultureInfo.InvariantCulture, VerificationParameterUnexpectedTypeErrorMessage, verificationName, verificationParameterName, verificationParameterName, verificationParameterType.ToStringReadable(), expectedTypesMessage);

            WorkflowExtensions.ThrowImproperUseOfFramework(exceptionMessage);
        }
#pragma warning restore SA1201
    }
}
