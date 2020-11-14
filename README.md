[![Build Status](https://dev.azure.com/ruairidh/ScopeGuard/_apis/build/status/rkm.ScopeGuard?repoName=rkm%2FScopeGuard&branchName=main)](https://dev.azure.com/ruairidh/ScopeGuard/_build/latest?definitionId=3&repoName=rkm%2FScopeGuard&branchName=main)

# Scope Guard

A simple C# implementation of a Scope Guard, with an "Armed" variant.

Scope guards are useful in methods which have complex logic paths, where you need to ensure a block of code is executed on every return path.

These are implemented as `IDisposable`s in C#, so the callback provided will be called as soon as the guard goes out of scope. They can therefore also be used in anonymous blocks.

## Usage

The below examples use the C# 8.0 variant of the `using` statement, which doesn't require braces and uses the end of the method as the scope boundary.

### ScopeGuard

ScopeGuard runs the provided callback on any return path.

```c#
public void Foo(MyObj guarded)
{
    using var sg = new ScopeGuard(() => { guarded.HasDescoped = true; });

    // ...

    return;
} // HasDeScoped is now true
```

### ArmedScopeGuard

ArmedScopeGuard runs the provided callback on any return path, unlesss its `Disarm` method is called. This can be useful in cases where you have one or more "successful" return paths.

```
public void Foo(MyObj guarded)
{
    using var asg = new ArmedScopeGuard(() => { guarded.HasDescoped = true; });

    // Any return statements will callback

    // Success case - disarm and don't callback
    if(someCondition)
        asg.Disarm();
}
```

## Example - Usage in a text parser

```c#
public void Parse(Parser parser)
{
    using var asg = new ArmedScopeGuard(() => { parser.State = ParserState.Invalid; });

    while (parser.ReadChar())
    {
        if (parser.CurrentChar == '{')
        {
            parser.State = ParserState.StartOfBlock;
            // ...
            continue;
        }

        // ...
    }

    if (parser.State == ParserState.Complete)
        asg.Disarm();
}
```

## Kudos

This is a port of the `AK/ScopeGuard.h` implementation from the excellent [SerenityOS](https://github.com/SerenityOS) project. The BSD 2-Clause License is therefore retained.
