![CI](https://github.com/droyad/NotNullableReferenceTypeEnforcer/workflows/CI/badge.svg) 
![Feedz](https://img.shields.io/badge/endpoint.svg?url=https%3A%2F%2Ff.feedz.io%2Fdroyad%2Fpublic%2Fshield%2FNotNullableReferenceTypeEnforcer%2Flatest)
![NuGet](https://img.shields.io/nuget/dt/NotNullableReferenceTypeEnforcer)

# NotNullableReferenceTypeEnforcer

This library inspects an object and it's children and throws an exception if a property is null that shouldn't be based 
on the C# 8 `nullable reference type` feature.

To do this it traverses the object recursively via the public properties. It will throw an exception if all of the following are true for a property:
 - The property has a getter
 - The containing type was compiled with `nullable reference type` enabled
 - The property's type does not have the `?` postfix annotation
 - The property's value is null

 
## Why this library exists

The C# 8 `nullable reference type` feature does not guarantee that a property is **not null**, just that it could be **null**. Any checks are only
carried out at compile time. Null values can be set via reflection, `dynamic`, non-`nullable reference type` and other means. 

Therefore care must be taken that a object originating from outside the `nullable reference type` scope meets the nullability expectations.
Only once that is done can assumptions be made about the non-nullability of a property. This library provides some surety the contract is adhered to.

## Usage

Install the [NotNullableReferenceTypeEnforcer package](https://www.nuget.org/packages/NotNullableReferenceTypeEnforcer) from NuGet

```csharp
using NotNullableReferenceTypeEnforcer;

NotNullEnforcer.Validate(myThing);
```

or for dependency injection

```csharp
using NotNullableReferenceTypeEnforcer;

builder.RegisterInstance(NotNullEnforcer.Instance).As<INotNullEnforcer>().SingleInstance();
```

An exception will be thrown if a property is null and the containing type was built with `nullable reference type` option `enabled` 

## The gory detail

The `nullable reference type` feature can be turned on at the project, file or code section level. The compiler will add attributes to any types and members
that exist within a scope that has the feature enabled.

The compiler adds a `NullableAttribute` to the type. It sometimes adds a `NullableContextAttribute` as well (likely when there is more than one property).
The first constructor argument to `NullableContextAttribute` is a `byte` that indicates whether the default is to treat fields as nullable 
(`2` indicates `true`). The choice of default seems to depend on option is more common. If the attribute is missing the default behaviour is `false`.

The compiler also adds `NullableAttribute` to any members that should not receive the default treatment. Again `2` indicates it should be treated as nullable.

These attributes are embedded as internal types within each assembly.

[More Reading](https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/upgrade-to-nullable-references)