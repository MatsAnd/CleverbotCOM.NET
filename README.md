# CleverbotCOM.NET [![Build Status](https://travis-ci.org/MatsAnd/CleverbotCOM.NET.svg?branch=master)](https://travis-ci.org/MatsAnd/CleverbotCOM.NET) [![NuGet version](https://badge.fury.io/nu/MatsAnd.Cleverbot.CleverbotCOMNET.svg)](https://badge.fury.io/nu/MatsAnd.Cleverbot.CleverbotCOMNET) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
A .NET wrapper for the Cleverbot API.

### C# Example:
```Csharp
  using CleverbotCOM.NET;


  Cleverbot cleverbot = new Cleverbot("API-KEY");
  
  while (true)
  {
      string question = Console.ReadLine();
      Console.WriteLine("Cleverbot: " + cleverbot.Ask(question));
  }
```
