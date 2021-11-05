# 5 - Log scopes
It is possible to create a log scope with set of key and value pairs. Doing so has 2 significant benefits:
- These key-value pairs will be present in the log object (for example in DB), even if they're not used in log message.
- Logger will automatically resolve placeholders from log scope if they're not provided manually in the method.

## Scopes and Exceptions
C# has a quirk that exception handling code will run in a completely different scope than the calling method - this means that log scope will not be preserved.

However there's a way to work it around, and that's by abusing `catch`'s `when` expression. The code for this expression runs in the parent scope, so all properties end up being added to the log object!  
To make it easy, I tend to create a set of extension methods for `Exception` type that simply log the message and return true, making them usable with `when` clause.

There's unfortunately one more quirk - placeholders for log messages in `catch` and `finally` blocks aren't automatically populated from log scope (in fact, not providing them manually causes log to not be logged at all!). Perhaps there is a solution to this, but I didn't find it yet.