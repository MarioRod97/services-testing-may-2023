# Tools

## NUnit
"Idiomatic" rewrite of Kent Beck's JUnit.

```java
public class CustomerTests {
    public void HasCorrectName() {

    }
}
```
NUnit Example

```csharp
[TestClass]
public class CustomerTest{
    [TestMethod]
    public void HasCorrectNameTest() {

    }
}
```

## MsTest
Microsoft's testing tool.
The first couple versions SUCKED. Bad. They had no idea what they were doing.
The documentation was even worse.

But.... they sort of locked us in.

## xUnit
- This is what we will use.
- This is what Microsoft uses now for building and testing .NET
- The vast majority of documenation on testing .NET is xUnit.
- Also, good integration with API stuff, as we'll see (the test fixture stuff)