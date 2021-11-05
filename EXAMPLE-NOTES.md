# 2 - Structured logging
This example replaces in-line string concating with structured logging pattern.

## Benefits
This has numerous benefits. One of them is that no string building happens unless the message is actually being logged - this might increase performance when there are many Trace or Debug log entries.  
An another benefit is that logging code becomes more readable - the actual values are shifted to the end of log method, and the string itself becomes much easier to read.
 
There of course are other benefits, which will be covered in further examples.