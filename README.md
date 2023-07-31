# LiteDBUtility
Provides a quick way of integrating LiteDB and using LiteDB.Studio into C# applications

### Features
+ Includes an embedded database IDE LiteDB.Studio
+ References a LiteDB nuget package

### Tutorial

This utility in nuget package form automatically references a recent [LiteDB](https://www.litedb.org/) nuget package. However, keep in mind that [LiteDB Studio](https://github.com/mbdavid/LiteDB.Studio) is using an older version of LiteDB. The database editor can be extracted to the current working directory and opened using this code:
```cs
using LiteDBUtility;

LiteDBStudio.Start();
```

LiteDB can be used without starting LiteDB Studio.
