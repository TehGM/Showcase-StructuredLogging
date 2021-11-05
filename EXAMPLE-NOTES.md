# 4 - Database
This example adds a MongoDB sink. Now this makes structured logging actually shine.

Of course other Database engines are also supported - MongoDB is just my personal preference.

## Config
A new sink has been added to appsettings.json. Additionally, appsecrets.json now overrides that specific array element, adding a connection string arg.

This config assumes that MongoDB cluster, database and collection already exists.

## Fixes
There is a small change in logs - instead of using Timespan, it uses timespan's total seconds. This is because MongoDB sink currently does not support timespan for some reason.