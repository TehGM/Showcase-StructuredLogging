# 6 - DataDog
Logging to files and databases is useful, and already really powerful. However it is possible to make it even better.

There are many logging aggregation services available on the internet. They usually provide rich tools for logs filtering, searching and alerting. I personally use `DataDog`, which in addition to logs intake, also support monitoring of RAM, Disk Space, CPU use etc via installable agents.

Fun fact: that's how I discovered that Raspbian default UI leaks memory, and managed to fix it by restarting UI service every night - without DataDog, I'd probably still have my Raspberry Pis die on me 2 times a month!

## Config
DataDog sink allows configuration just like other sinks, but I personally choose to create my own options class, and register the sink with Serilog in code. This allows me to resolve parameters like host name programmatically at runtime.